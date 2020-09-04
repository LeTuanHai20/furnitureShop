using Domain.Shop.Dto.CartProduct;
using Domain.Shop.Dto.ShoppingCart;
using Domain.Shop.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.Component
{
    public class CartInLayoutViewComponent:ViewComponent
    {
        private readonly IShoppingCartRepository shoppingCart;
        private readonly ICartRepository cartRepository;
        private readonly IServiceProvider services;
        private readonly IProductRepository productRepository;

        public CartInLayoutViewComponent(IShoppingCartRepository shoppingCart, ICartRepository cartRepository, IServiceProvider services, IProductRepository productRepository)
        {
            this.shoppingCart = shoppingCart;
            this.cartRepository = cartRepository;
            this.services = services;
            this.productRepository = productRepository;
        }
        public IViewComponentResult Invoke()
        {
            string cartId = GetCart(services);
            List<CartProductViewModel> CartProductViewModels = new List<CartProductViewModel>();

            foreach (var item in shoppingCart.GetCartProducts(cartId))
            {
                var cartProductViewModel = new CartProductViewModel()
                {
                    Id = item.Id,
                    CartId = item.CartId,
                    Cart = cartRepository.GetCartViewModel(item.CartId),
                    ProductId = item.ProductId,
                    Product = productRepository.GetProductViewModelById(item.ProductId),
                    Price = item.Price,
                    PriceType = item.PriceType,
                    Quantity = item.Quantity,
                    Total = item.Total
                };
                CartProductViewModels.Add(cartProductViewModel);

            }
            var cart = new ShoppingCart()
            {
                Id = cartId,
                cartProducts = CartProductViewModels,
                Total = shoppingCart.GetShoppingCartTotal(cartId)
            };

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
