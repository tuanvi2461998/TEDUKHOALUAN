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

        [Route("order.html", Name = "Order")]
        public IActionResult Index(string Name)
        {
            var order = new OrderViewModels();
            ViewData["BodyClass"] = "orders_list_page";
            order.Biill =  _billService.GetByCustomer(Name);
            return View(order);
        }
    }
}