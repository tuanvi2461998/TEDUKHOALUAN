using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TeduCoreApp.Data.Enums
{
    public enum BillStatus
    {
        [Description("Đơn mới")]
        New,
        [Description("Đang xử lý")]
        InProgress,
        [Description("Trả hàng")]
        Returned,
        [Description("Hoàn thành")]
        Completed
    }
}
