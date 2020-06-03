var contactController = function () {

    this.initialize = function () {
        loadData();
        registerEvents();
    }
    function registerEvents() {
        //Init validation
        $('#frmMaintainance').validate({
            errorClass: 'red',
            ignore: [],
            lang: 'vi',
            rules: {
                txtNameM: { required: true },
                txtLatM: {
                    required: true,
                    number: true
                },
                txtLngM: {
                    required: true,
                    number: true
                },
                txtPhoneM: {
                    number: true
                }
            }
            
        });
        //todo: binding events to controls
        $('#ddlShowPage').on('change', function () {
            tedu.configs.pageSize = $(this).val();
            tedu.configs.pageIndex = 1;
            loadData(true);
        });
        $('#btnSearch').on('click', function () {
            loadData();
        });
        $('#txtKeyword').on('keypress', function (e) {
            if (e.which === 13) {
                loadData();
            }
        });

        $("#btnCreate").on('click', function () {
            resetFormMaintainance();
            $('#modal-add-edit').modal('show');
        });
        $('body').on('click', '.btn-edit', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            loadDetails(that);
        });
        $('body').on('click', '.btn-delete', function (e) {
            e.preventDefault();
            var that = $(this).data('id');
            tedu.confirm('Are you sure to delete?', function () {
                $.ajax({
                    type: "POST",
                    url: "/Admin/Contact/Delete",
                    data: { id: that },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function () {
                        tedu.notify('Delete page successful', 'success');
                        tedu.stopLoading();
                        loadData();
                    },
                    error: function () {
                        tedu.notify('Have an error in progress', 'error');
                        tedu.stopLoading();
                        loadData();
                    }
                });
            });
        });

        $('#btnSave').on('click', function (e) {
            if ($('#frmMaintainance').valid()) {
                e.preventDefault();
                var id = $('#hidIdM').val();
                var name = $('#txtNameM').val();
                var email = $('#txtEmailM').val();
                var address = $('#txtAddressM').val();
                var lat = $('#txtLatM').val();
                var lng = $('#txtLngM').val();
                var phone = $('#txtPhoneM').val();
                var website = $('#txtWebsiteM').val();
                var status = $('#ckStatusM').prop('checked') == true ? 1 : 0;
                $.ajax({
                    type: "POST",
                    url: "/Admin/Contact/SaveEntity",
                    data: {
                        Id: id,
                        Name: name,
                        Email: email,
                        Address: address,
                        Lat: lat,
                        Lng: lng,
                        Phone: phone,
                        Website: website,
                        Status: status
                    },
                    dataType: "json",
                    beforeSend: function () {
                        tedu.startLoading();
                    },
                    success: function (response) {
                        tedu.notify('Update contact successful', 'success');
                        $('#modal-add-edit').modal('hide');
                        resetFormMaintainance();

                        tedu.stopLoading();
                        loadData(true);
                    },
                    error: function () {
                        tedu.notify('Has an error in save contact progress', 'error');
                        tedu.stopLoading();
                    }
                });
                return false;
            }
        });
    }
   
    function loadData(isPageChanged) {
        $.ajax({
            type: "GET",
            url: "/admin/contact/GetAllPaging",
            data: {
                keyword: $('#txt-search-keyword').val(),
                page: tedu.configs.pageIndex,
                pageSize: tedu.configs.pageSize
            },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var template = $('#table-template').html();
                var render = "";
                if (response.RowCount > 0) {
                    $.each(response.Results, function (i, item) {
                        render += Mustache.render(template, {
                            Id: item.Id,
                            Name: item.Name,
                            Email: item.Email,
                            Address: item.Address,
                            Lat: item.Lat,
                            Lng: item.Lng,
                            Phone: item.Phone,
                            Website: item.Website,
                            Status: tedu.getStatus(item.Status)
                        });
                    });
                    $("#lbl-total-records").text(response.RowCount);
                    if (render != undefined) {
                        $('#tbl-content').html(render);

                    }
                    wrapPaging(response.RowCount, function () {
                        loadData();
                    }, isPageChanged);


                }
                else {
                    $('#tbl-content').html('');
                }
                tedu.stopLoading();
            },
            error: function (status) {
                console.log(status);
            }
        });
    };
    function loadDetails(that) {
        $.ajax({
            type: "GET",
            url: "/Admin/Contact/GetById",
            data: { id: that },
            dataType: "json",
            beforeSend: function () {
                tedu.startLoading();
            },
            success: function (response) {
                var data = response;
                $('#hidIdM').val(data.Id);
                $('#txtNameM').val(data.Name);
                $('#txtEmailM').val(data.Email);
                $('#txtAddressM').val(data.Address);
                $('#txtLatM').val(data.Lat);
                $('#txtLngM').val(data.Lng);
                $('#txtPhoneM').val(data.Phone);
                $('#txtWebsiteM').val(data.Website);
                $('#ckStatusM').prop('checked', data.Status == 1);


                $('#modal-add-edit').modal('show');
                tedu.stopLoading();

            },
            error: function (status) {
                tedu.notify('Có lỗi xảy ra', 'error');
                tedu.stopLoading();
            }
        });
    }
    function resetFormMaintainance() {
        $('#hidIdM').val('');
        $('#txtNameM').val('');
        $('#txtEmailM').val('');
        $('#txtAddressM').val('');
        $('#txtLatM').val('');
        $('#txtLngM').val('');
        $('#txtPhoneM').val('');
        $('#txtWebsiteM').val('');
        $('#ckStatusM').prop('checked', true);

    }
    function wrapPaging(recordCount, callBack, changePageSize) {
        var totalsize = Math.ceil(recordCount / tedu.configs.pageSize);
        //Unbind pagination if it existed or click change pagesize
        if ($('#paginationUL a').length === 0 || changePageSize === true) {
            $('#paginationUL').empty();
            $('#paginationUL').removeData("twbs-pagination");
            $('#paginationUL').unbind("page");
        }
        //Bind Pagination Event
        $('#paginationUL').twbsPagination({
            totalPages: totalsize,
            visiblePages: 7,
            first: 'Đầu',
            prev: 'Trước',
            next: 'Tiếp',
            last: 'Cuối',
            onPageClick: function (event, p) {
                tedu.configs.pageIndex = p;
                setTimeout(callBack(), 200);
            }
        });
    }
}
