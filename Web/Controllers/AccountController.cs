using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Common.Security;
using System.Threading.Tasks;
using Domain.Application.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Domain.Shop.IRepositories;
using Domain.Shop.Dto.Dictrict;
using System.Linq.Dynamic.Core;
using Domain.Shop.Dto.Customer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Infrastructure.Common;
using Infrastructure.Database.Models;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly IServiceProvider serviceProvider;
        private readonly IProvinceRepository provinceRepository;
        private readonly IDictrictRepository dictrictRepository;
        private readonly IUserRepository userRepository;

        public AccountController(IAccountRepository accountRepository, IServiceProvider serviceProvider,
            IProvinceRepository provinceRepository, IDictrictRepository dictrictRepository, IUserRepository userRepository)
        {
            this.accountRepository = accountRepository;
            this.serviceProvider = serviceProvider;
            this.provinceRepository = provinceRepository;
            this.dictrictRepository = dictrictRepository;
            this.userRepository = userRepository;
        }
        public ActionResult Index()
        {
            HttpRequest cookie = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Request;
            string userId = cookie.Cookies[SecurityManager._securityToken];
            ViewBag.ListProvince = provinceRepository.GetProvinceViewModels();
            
            if (userId != null)
            {
                string id = SecurityManager.getUserId(userId);
                var model = accountRepository.GetCustomerViewModel(id);
                if(model == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.ListDistrict = dictrictRepository.GetDictrictViewModels(model.Province);
                return View(model);
            }
            return View();
        }
        public JsonResult GetDictricts(string value)
        {
            try
            {
                var model = dictrictRepository.GetDictrictViewModels(value);
                 return Json(model);     
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult Update(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = accountRepository.Get(model.Id);
                    PropertyCopy.Copy(model, customer);
                    if (model.OldPassword != null && model.Password != null)
                    {
                        var user = userRepository.Get(model.Id);
                        if (Security.EncryptPassword(model.OldPassword) == user.Password)
                        {
                            user.Password = Security.EncryptPassword(model.Password);
                        }
                        else
                        {
                            ViewBag.sucsess = false;
                            return View();
                        }
                        userRepository.Update(user);
                        userRepository.Save();
                    }
                    accountRepository.Update(customer);
                    accountRepository.Save();
                    ViewBag.sucsess = true;
                    return View();
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            ViewBag.sucsess = false;
            return View();
        }
    }
}
