using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Application.ViewModels.Product;
using TeduCoreApp.Application.ViewModels.System;

namespace TeduCoreApp.Models.ProductViewModels
{
    public class OrderViewModels 
    {
        public AppUserViewModel FullNameme { get; set; }

        public List<BillViewModel> BiillCustomer { get; set; }

        public List<BillDetailViewModel> BillDetailCus { get; set; }

       public int total { get; set; }

    }
}
