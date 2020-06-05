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
        public IActionResult Index(int? pageSize, string sortBy, int page = 1)
        {
            var bl = new AllBlogViewModel();
            ViewData["BodyClass"] = "blog_page";
            if (pageSize == null)
                pageSize = _configuration.GetValue<int>("PageSize");
            bl.PageSize = pageSize;
            bl.SortType = sortBy;
            bl.Data = _blogService.GetAllPaging(string.Empty,pageSize.Value , page);
            return View(bl);
        }
        [Route("{alias}-b.{id}.html", Name = "BlogDetail")]
        public IActionResult Details(int id)
        {
            var post = new BlogDetailViewModel();

            ViewData["BodyClass"] = " blog_post";
            post.Blog = _blogService.GetById(id);
            post.GetReatedBlogs = _blogService.GetReatedBlogs(id,3);
            return View(post);
        }
    }
}