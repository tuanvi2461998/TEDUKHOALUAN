using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Utilities.Dtos;

namespace TeduCoreApp.Models.BlogViewModels
{
    public class BlogViewModel
    {
        public PagedResult<BlogViewModel> Data { get; set; }

        public string SortType { set; get; }

        public int? PageSize { set; get; }
    }
}
