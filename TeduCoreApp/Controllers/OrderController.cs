using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Models.ProductViewModels;

namespace TeduCoreApp.Controllers
{
    public class OrderController : Controller
    {
        IBillService _billService;
        public OrderController(IBillService billService)
        {
            _billService = billService;
        }

        [Route("order.html", Name = "Đơn Hàng")]
        public IActionResult Index(string Names)
        {
            var order = new OrderViewModels();
            ViewData["BodyClass"] = "orders_list_page";
            order.BiillCustomer =  _billService.GetByCustomer(Names);
            return View(order);
        }

        [Route("orderdetail-{id}.html", Name = "Chi Tiết")]
        public IActionResult OrderDetailCustomer(int id)
        {
            var order = new OrderViewModels();
            ViewData["BodyClass"] = "orders_list_page";
            order.BillDetailCus = _billService.GetBillDetails(id);
            return View(order);
        }
        #region AJAX Request
        #endregion
    }
}