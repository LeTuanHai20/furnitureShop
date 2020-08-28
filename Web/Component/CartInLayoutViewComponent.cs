using Domain.Shop.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Component
{
    public class CartInLayoutViewComponent:ViewComponent
    {
        private readonly ShoppingCart cart;
        private readonly IServiceProvider services;

        public CartInLayoutViewComponent(ShoppingCart shoppingCart, IServiceProvider services)
        {
            this.cart = shoppingCart;
            this.services = services;
        }
        public IViewComponentResult Invoke()
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
        public string GetCart(IServiceProvider service)
        {

            try
            {
                HttpRequest cookie = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Request;
                var context = service.GetService<ShopDBContext>();
                if(cookie.Cookies["cardId"] == null)
                {
                    return null;
                }
                else
                {
                    return cookie.Cookies["cardId"];
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
