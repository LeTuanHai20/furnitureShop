using System;
using System.Collections.Generic;
using Domain.Common.Security;
using Domain.Shop.Dto.CartProduct;
using Domain.Shop.Dto.Order;
using Domain.Shop.Dto.ShoppingCart;
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
    public class OrderController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IShoppingCartRepository shoppingCart;
        IServiceProvider services;
        private readonly IAccountRepository accountRepository;
        private readonly ICartRepository cartRepository;

        public OrderController(IProductRepository productRepository, IShoppingCartRepository shoppingCart, IServiceProvider services, 
            IAccountRepository accountRepository, ICartRepository cartRepository)
        {
            this.productRepository = productRepository;
            this.shoppingCart = shoppingCart;
            this.services = services;
            this.accountRepository = accountRepository;
            this.cartRepository = cartRepository;
        }
       
        public IActionResult HomeOrder()
        {
            try
            {
                HttpRequest cookie = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Request;
                string cartId = cookie.Cookies["cardId"];
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
                //get customer
                string customerId = SecurityManager.getUserId(cookie.Cookies[SecurityManager._securityToken]);

                var customer = accountRepository.GetCustomerViewModel(customerId);
                if (customer == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                var model = new OrderViewModel()
                {
                    CustomerId = customer.Id,
                    FullName = customer.FirstName +" "+customer.LastName,
                    Address = customer.Address,
                    District = customer.District,
                    Province = customer.Province,
                    PhoneNo = customer.PhoneNo,
                    Email = customer.Email,
                    ShoppingCart = cart
                };
                return View(model);
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }
        [HttpPost]
        public ActionResult AddCart(OrderViewModel model)
        {
            try
            {
                HttpRequest cookie = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Request;
                string cartId = cookie.Cookies["cardId"];
                Cart cart = cartRepository.Get(cartId);
                cart.CustomerId = model.CustomerId;
                cart.CreateAt = DateTime.UtcNow;
                cart.Total += shoppingCart.GetShoppingCartTotal(cartId);
                cartRepository.Update(cart);
                cartRepository.Save();
                shoppingCart.ClearCart(cookie.Cookies["cardId"]);
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
