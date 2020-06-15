using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Enums;
using TeduCoreApp.Utilities.Constants;

namespace TeduCoreApp.Data.EF
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;
        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;
        public DbInitializer(AppDbContext context, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Description = "Top manager"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Staff",
                    NormalizedName = "Staff",
                    Description = "Staff"
                });
                await _roleManager.CreateAsync(new AppRole()
                {
                    Name = "Customer",
                    NormalizedName = "Customer",
                    Description = "Customer"
                });
            }
            if (!_userManager.Users.Any())
            {
                await _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin",
                    FullName = "Administrator",
                    Email = "admin@gmail.com",
                    Balance = 0,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    Status = Status.Active
                }, "123654$");
                var user = await _userManager.FindByNameAsync("admin");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            if (!_context.Contacts.Any())
            {
                _context.Contacts.Add(new Contact()
                {
                    Id = CommonConstants.DefaultContactId,
                    Address = "320A, QL61, Vĩnh Hoà Hiệp, Châu Thành, tỉnh Kiên Giang",
                    Email = "tuanvi.2461998@gmail.com",
                    Name = "Shoes Shop",
                    Phone = "0328328190",
                    Status = Status.Active,
                    Website = "http://shoesshop.com",
                    Lat = 9.9192069,
                    Lng = 105.1444079
                });
            }
            if (_context.Functions.Count() == 0)
            {
                _context.Functions.AddRange(new List<Function>()
                {
                    new Function() {Id = "SYSTEM", Name = "Hệ thống",ParentId = null,SortOrder = 1,Status = Status.Active,URL = "/",IconCss = "fa-desktop"  },
                    new Function() {Id = "ROLE", Name = "Quyền",ParentId = "SYSTEM",SortOrder = 1,Status = Status.Active,URL = "/admin/role/index",IconCss = "fa-home"  },
                    new Function() {Id = "FUNCTION", Name = "Chức năng",ParentId = "SYSTEM",SortOrder = 2,Status = Status.Active,URL = "/admin/function/index",IconCss = "fa-home"  },
                    new Function() {Id = "USER", Name = "Người dùng",ParentId = "SYSTEM",SortOrder =3,Status = Status.Active,URL = "/admin/user/index",IconCss = "fa-home"  },

                    new Function() {Id = "PRODUCT",Name = "Quản lý sản phẩm",ParentId = null,SortOrder = 2,Status = Status.Active,URL = "/",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_CATEGORY",Name = "Loại sản phẩm",ParentId = "PRODUCT",SortOrder =1,Status = Status.Active,URL = "/admin/productcategory/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "PRODUCT_LIST",Name = "Sản phẩm",ParentId = "PRODUCT",SortOrder = 2,Status = Status.Active,URL = "/admin/product/index",IconCss = "fa-chevron-down"  },
                    new Function() {Id = "BILL",Name = "Đơn hàng",ParentId = "PRODUCT",SortOrder = 3,Status = Status.Active,URL = "/admin/bill/index",IconCss = "fa-chevron-down"  },

                    new Function() {Id = "CONTENT",Name = "Nội dung",ParentId = null,SortOrder = 3,Status = Status.Active,URL = "/",IconCss = "fa-table"  },
                    new Function() {Id = "BLOG",Name = "Bài viết",ParentId = "CONTENT",SortOrder = 1,Status = Status.Active,URL = "/admin/blog/index",IconCss = "fa-table"  },
                    new Function() {Id = "PAGE",Name = "Trang",ParentId = "CONTENT",SortOrder = 2,Status = Status.Active,URL = "/admin/page/index",IconCss = "fa-table"  },

                    new Function() {Id = "UTILITY",Name = "Tiện ích",ParentId = null,SortOrder = 4,Status = Status.Active,URL = "/",IconCss = "fa-clone"  },
                    new Function() {Id = "FEEDBACK",Name = "Phản hồi",ParentId = "UTILITY",SortOrder = 2,Status = Status.Active,URL = "/admin/feedback/index",IconCss = "fa-clone"  },
                    new Function() {Id = "CONTACT",Name = "Contact",ParentId = "UTILITY",SortOrder = 4,Status = Status.Active,URL = "/admin/contact/index",IconCss = "fa-clone"  },
                });
            }

            if (_context.Footers.Count(x => x.Id == CommonConstants.DefaultFooterId) == 0)
            {
                string content = "Footer";
                _context.Footers.Add(new Footer()
                {
                    Id = CommonConstants.DefaultFooterId,
                    Content = content
                });
            }

            if (_context.Colors.Count() == 0)
            {
                List<Color> listColor = new List<Color>()
                {
                    new Color() {Name="Đen", Code="#000000" },
                    new Color() {Name="Trắng", Code="#FFFFFF"},
                    new Color() {Name="Đỏ", Code="#ff0000" },
                    new Color() {Name="Xanh", Code="#1000ff" },
                };
                _context.Colors.AddRange(listColor);
            }
            if (_context.AdvertistmentPages.Count() == 0)
            {
                List<AdvertistmentPage> pages = new List<AdvertistmentPage>()
                {
                    new AdvertistmentPage() {Id="home", Name="Home",AdvertistmentPositions = new List<AdvertistmentPosition>(){
                        new AdvertistmentPosition(){Id="home-left",Name="Bên trái"}
                    } },
                    new AdvertistmentPage() {Id="product-cate", Name="Product category" ,
                        AdvertistmentPositions = new List<AdvertistmentPosition>(){
                        new AdvertistmentPosition(){Id="product-cate-left",Name="Bên trái"}
                    }},
                    new AdvertistmentPage() {Id="product-detail", Name="Product detail",
                        AdvertistmentPositions = new List<AdvertistmentPosition>(){
                        new AdvertistmentPosition(){Id="product-detail-left",Name="Bên trái"}
                    } },

                };
                _context.AdvertistmentPages.AddRange(pages);
            }


            if (_context.Slides.Count() == 0)
            {
                List<Slide> slides = new List<Slide>()
                {
                    new Slide() {Name="Slide 1",Image="/client-side/images/slider/slide-1.jpg",Url="#",DisplayOrder = 0,GroupAlias = "top",Status = true },
                    new Slide() {Name="Slide 2",Image="/client-side/images/slider/slide-2.jpg",Url="#",DisplayOrder = 1,GroupAlias = "top",Status = true },
                    new Slide() {Name="Slide 3",Image="/client-side/images/slider/slide-3.jpg",Url="#",DisplayOrder = 2,GroupAlias = "top",Status = true },

                    new Slide() {Name="Slide 1",Image="/client-side/images/brand1.png",Url="#",DisplayOrder = 1,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 2",Image="/client-side/images/brand2.png",Url="#",DisplayOrder = 2,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 3",Image="/client-side/images/brand3.png",Url="#",DisplayOrder = 3,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 4",Image="/client-side/images/brand4.png",Url="#",DisplayOrder = 4,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 5",Image="/client-side/images/brand5.png",Url="#",DisplayOrder = 5,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 6",Image="/client-side/images/brand6.png",Url="#",DisplayOrder = 6,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 7",Image="/client-side/images/brand7.png",Url="#",DisplayOrder = 7,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 8",Image="/client-side/images/brand8.png",Url="#",DisplayOrder = 8,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 9",Image="/client-side/images/brand9.png",Url="#",DisplayOrder = 9,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 10",Image="/client-side/images/brand10.png",Url="#",DisplayOrder = 10,GroupAlias = "brand",Status = true },
                    new Slide() {Name="Slide 11",Image="/client-side/images/brand11.png",Url="#",DisplayOrder = 11,GroupAlias = "brand",Status = true },
                };
                _context.Slides.AddRange(slides);
            }


            if (_context.Sizes.Count() == 0)
            {
                List<Size> listSize = new List<Size>()
                {
                    new Size() { Name="35" },
                    new Size() { Name="36"},
                    new Size() { Name="37" },
                    new Size() { Name="38" },
                    new Size() { Name="39" },
                    new Size() { Name="40" },
                    new Size() { Name="41" },
                    new Size() { Name="42" },
                    new Size() { Name="43" },
                    new Size() { Name="44" }
                };
                _context.Sizes.AddRange(listSize);
            }

            if (_context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>()
                {
                    new ProductCategory() { Name="Adidas",SeoAlias="adidas",ParentId = null,Status=Status.Active,SortOrder=1,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "GIÀY NIKE VOMERO",DateCreated=DateTime.Now,Image="/uploaded/images/20200608/giay-nike-air-max-720-nam-xam-cam-01-800x800_0.jpg",SeoAlias = "GIAY-NIKE-VOMERO",Price =  1000000,Status = Status.Active,OriginalPrice = 1000000},
                            new Product(){Name = "Giày Sneaker Alexander Mcqueen",DateCreated=DateTime.Now,Image="/uploaded/images/20200608/giay-sneaker-alexander-mcqueen-size-39-5e1eb64b563a9-15012020135051.jpg",SeoAlias = "Giay-Sneaker-Alexander-Mcqueen",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "GIÀY SUPERSTAR",DateCreated=DateTime.Now,Image="/uploaded/images/20200608/Giay_Superstar_Mau_trang_EG4958_01_standard.jpg",SeoAlias = "san-pham-3",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "GIÀY ULTRABOOST 19",DateCreated=DateTime.Now,Image="/uploaded/images/20200608/Giay_UltraBoost_19_Mau_xanh_la_G27132.jpg",SeoAlias = "san-pham-4",Price = 1000,Status = Status.Active,OriginalPrice = 1000},
                            new Product(){Name = "GIÀY ULTRABOOST 20",DateCreated=DateTime.Now,Image="/uploaded/images/20200608/Giay_UltraBoost_20_Mau_xam_EG0705.jpg",SeoAlias = "san-pham-5",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                        }
                    },
                    new ProductCategory() { Name="Nike",SeoAlias="nike",ParentId = null,Status=Status.Active ,SortOrder=2,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Giày AphaBounce",DateCreated=DateTime.Now,Image="/uploaded/images/20200608/Alphabounce.jpg",SeoAlias = "san-pham-6",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "DIDAS ALPHABOUNCE INSTINCT M CORE",DateCreated=DateTime.Now,Image="/uploaded/images/20200608/adidas-alphabounce-instinct-m-core-black-1-1.jpg",SeoAlias = "san-pham-7",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "ADIDAS ALPHABOUNCE INSTINCT GREYISH",DateCreated=DateTime.Now,Image="/uploaded/images/20200608/adidas-alphabounce-instinct-greyish-nam-nu.jpg",SeoAlias = "san-pham-8",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "Giày Convers",DateCreated=DateTime.Now,Image="/uploaded/images/20200520/Conver.jpg",SeoAlias = "san-pham-9",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "Product 10",DateCreated=DateTime.Now,Image="/uploaded/images/20200520/Conver.jpg",SeoAlias = "san-pham-10",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                        }},
                    new ProductCategory() { Name="Balenciaga",SeoAlias="balenciaga",ParentId = null,Status=Status.Active ,SortOrder=3,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Air Max 720 Pink Sea (PRE – ORDER)",DateCreated=DateTime.Now,Image="/uploaded/images/20200608/air-force-1-shadow-pale-ivory-nu.jpg",SeoAlias = "san-pham-11",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "Product 12",DateCreated=DateTime.Now,Image="/uploaded/images/20200520/Conver.jpg",SeoAlias = "san-pham-12",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "Product 13",DateCreated=DateTime.Now,Image="/uploaded/images/20200520/Conver.jpg",SeoAlias = "san-pham-13",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "Product 14",DateCreated=DateTime.Now,Image="/uploaded/images/20200520/Conver.jpg",SeoAlias = "san-pham-14",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "Product 15",DateCreated=DateTime.Now,Image="/uploaded/images/20200520/Conver.jpg",SeoAlias = "san-pham-15",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                        }},
                    new ProductCategory() { Name="Converse",SeoAlias="converse",ParentId = null,Status=Status.Active,SortOrder=4,
                        Products = new List<Product>()
                        {
                            new Product(){Name = "Nike Air Force One",DateCreated=DateTime.Now, Image="/uploaded/images/20200608/nike-air-force-1-07-lux-white-platinum-tint-nam-nu.jpg",SeoAlias = "san-pham-16",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "Giày Nike Air F1",DateCreated=DateTime.Now,Image="/uploaded/images/20200612/nike-air-force-1-da-lon-co-cao-xam-1-1.jpg",SeoAlias = "san-pham-17",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "Product 18",DateCreated=DateTime.Now,Image="/uploaded/images/20200520/Conver.jpg",SeoAlias = "san-pham-18",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "Product 19",DateCreated=DateTime.Now,Image="/uploaded/images/20200520/Conver.jpg",SeoAlias = "san-pham-19",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                            new Product(){Name = "Product 20",DateCreated=DateTime.Now,Image="/uploaded/images/20200520/Conver.jpg",SeoAlias = "san-pham-20",Price =  1000000,Status = Status.Active,OriginalPrice =  1000000},
                        }}
                };
                _context.ProductCategories.AddRange(listProductCategory);
            }

            if (!_context.SystemConfigs.Any(x => x.Id == "HomeTitle"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeTitle",
                    Name = "Home's title",
                    Value1 = "Tuan Vi Shop home",
                    Status = Status.Active
                });
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaKeyword"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaKeyword",
                    Name = "Home Keyword",
                    Value1 = "shopping, sales",
                    Status = Status.Active
                });
            }
            if (!_context.SystemConfigs.Any(x => x.Id == "HomeMetaDescription"))
            {
                _context.SystemConfigs.Add(new SystemConfig()
                {
                    Id = "HomeMetaDescription",
                    Name = "Home Description",
                    Value1 = "Home shoes",
                    Status = Status.Active
                });
            }
            await _context.SaveChangesAsync();

        }
    }
}
