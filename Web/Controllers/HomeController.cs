using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Domain.Common.Security;
using Domain.Shop.Dto.Products;
using Domain.Shop.Entities;
using Domain.Shop.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IProductTagRepository productTagRepository;
        private readonly IServiceProvider serviceProvider;
        private readonly IProductReViewRepository productReViewRepository;

        public HomeController(IProductRepository productRepository, 
            IProductTagRepository productTagRepository, IServiceProvider serviceProvider,
            IProductReViewRepository productReViewRepository)
        {
            this.productRepository = productRepository;
            this.productTagRepository = productTagRepository;
            this.serviceProvider = serviceProvider;
            this.productReViewRepository = productReViewRepository;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
           return View(productRepository.GetProductViewModels());
        }
      
        [AllowAnonymous]
        public ActionResult GetProducts(string value)
        {
            try
            {
                List<ProductViewModel> ProductList = productRepository.GetProductViewModelsByOrder(Convert.ToInt32(value)).ToList();
                return Json(new { data = ProductList });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [AllowAnonymous]
        public IActionResult ProductDeltail(string id)
        {
            return View(productRepository.GetProductViewModelById(id));
        }
        [AllowAnonymous]
        public ActionResult Filter(string categoryName)
        {
            try
            {
                List<ProductViewModel> ProductList = productRepository.GetProductViewModelsByCategory(categoryName).ToList();
                return Json(new { data = ProductList });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [AllowAnonymous]
        public ActionResult SearchByPrice(string min, string max)
        {

            try
            {
                List<ProductViewModel> ProductList = productRepository.GetProductViewModelsByPrice(Convert.ToInt32(min), Convert.ToInt32(max)).ToList();
                return Json(new { data = ProductList });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [AllowAnonymous]
        public JsonResult SortProducts(string value)
        {
            try
            {
                return Json(new { data = productRepository.SortProductViewModels(value)});
            }
            catch (Exception)
            {

                throw;
            }
        }
        [AllowAnonymous]
        public ActionResult Detail(string id)
        {
            ViewBag.ProductTagDetail = productTagRepository.GetProductTagViewModelsByProductId(id).Select(s => s.TagName).ToList();
            ViewBag.RelateProduct = productRepository.GetProductViewModelsByOrder(5).ToList();
            var model = productRepository.GetProductViewModelById(id);
            return View(model);
        }
       
        public ActionResult Review(string id,string name, string review, string inlineRadioOptions)
        {
            try
            {
                HttpRequest cookie = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Request;
                string token  = cookie.Cookies[SecurityManager._securityToken];
                string customerId = SecurityManager.getUserId(token);
                ProductReview productReview = new ProductReview()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Review = review,
                    ProductId = id,
                    Star = Convert.ToInt32(inlineRadioOptions),
                    CustomerId = customerId,
                    CreateAt = DateTime.UtcNow
                };
                productReViewRepository.Add(productReview);
                productReViewRepository.Save();
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
