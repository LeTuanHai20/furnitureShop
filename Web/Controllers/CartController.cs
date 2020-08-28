using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Shop.Entities;
using Domain.Shop.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application;
using Web.Models;

namespace Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly ShoppingCart cart;
        IServiceProvider services;
     

        public CartController(IProductRepository productRepository, ShoppingCart cart, IServiceProvider services)
        {
            this.productRepository = productRepository;
            this.cart = cart;
            this.services = services;

        }
        public string GetCart(IServiceProvider service)
        {

            try
            {
                HttpRequest cookie = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Request;
                var context = service.GetService<ShopDBContext>();
                string cartId = cookie.Cookies["cardId"] ?? Guid.NewGuid().ToString();
                CookieOptions option = new CookieOptions();
                Response.Cookies.Append("cardId", cartId, option);
                return cartId;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public ViewResult ShoppingCart()
        {
            cart.Id = GetCart(services);
            var items = cart.GetCartProducts(cart.Id);
            cart.cartProducts = items;
            cart.Total = cart.GetShoppingCartTotal();
            var model = new ShoppingCartViewModel()
            {
                ShoppingCart = cart,
            };
            return View(model);
        }
        public ActionResult AddToShoppingCart(string id)
        {
            string cartId = GetCart(services);
            var product = productRepository.All.Where(d => d.Id == id).FirstOrDefault();
            if (product != null)
            {
                cart.AddToCart(product, cartId);
                return RedirectToAction("ShoppingCart");
            }
            return View();
        }
        [HttpPost]
        public bool Delete(string id)
        {
            try
            {
                cart.RemoveFromCart(productRepository.All.FirstOrDefault(d => d.Id == id), GetCart(services));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        public bool Update(string id , string quantity)
        {
            try
            {
                cart.UpdateQuantityInCart(id, quantity,GetCart(services));
                return true;
            }
            catch (Exception)
            {
                return false;
               
            }
            
        }
    }
}
