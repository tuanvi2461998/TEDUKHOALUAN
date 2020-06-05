using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.Blog;

namespace TeduCoreApp.Models.BlogViewModels
{
    public class BlogDetailViewModel
    {
        public BlogViewModel Blog { get; set; }

        public List<BlogViewModel> GetReatedBlogs { get; set; }
    }
}
