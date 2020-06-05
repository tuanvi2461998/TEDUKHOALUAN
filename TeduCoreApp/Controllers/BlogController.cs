using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Models.BlogViewModels;

namespace TeduCoreApp.Controllers
{
    public class BlogController : Controller
    {
        IBlogService _blogService;
        IConfiguration _configuration;
        public BlogController(IBlogService blogService,
             IConfiguration configuration)
        {
            _blogService = blogService;
            _configuration = configuration;
        }
        [Route("blog.html")]
        public IActionResult Index(int pageSize, string sortBy, int page)
        {
            var blog = new BlogViewModel();
            ViewData["BodyClass"] = "blog_page";
         

            blog.SortType = sortBy;
            return View(_blogService.GetAll());
        }
        [Route("{alias}-b.{id}.html", Name = "BlogDetail")]
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}