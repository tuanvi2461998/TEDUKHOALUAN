using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Application.Interfaces;
using TeduCoreApp.Application.ViewModels.Blog;
using TeduCoreApp.Application.ViewModels.Common;
using TeduCoreApp.Utilities.Helpers;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class ContactController : BaseController
    {
        public IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region AJAX API
        public IActionResult GetAll()
        {
            var model = _contactService.GetAll();
            return new OkObjectResult(model);
        }
        [HttpGet]
        public IActionResult GetById(string id)
        {
            var model = _contactService.GetById(id);

            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(string keyword, int page, int pageSize)
        {
            var model = _contactService.GetAllPaging(keyword, page, pageSize);
            return new OkObjectResult(model);
        }
        [HttpPost]
        public IActionResult SaveEntity(ContactViewModel contactVm)
        {
            if (!ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
                return new BadRequestObjectResult(allErrors);
            }
            else
            {  
                if (contactVm.Id == null)
                {
                    _contactService.Add(contactVm);
                }
                else
                {
                    _contactService.Update(contactVm);
                }
                _contactService.Save();
                return new OkObjectResult(contactVm);
            }
        }
        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _contactService.Delete(id);
                _contactService.Save();
                return new OkObjectResult(id);
            }
        }
        #endregion
    }
}