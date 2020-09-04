using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Shop.Dto.CartProduct;
using Domain.Shop.Dto.Products;
using Domain.Shop.Dto.ShoppingCart;
using Domain.Shop.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Controllers
{
   
    public class CartController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IShoppingCartRepository shoppingCart;
        IServiceProvider services;
        private readonly ICartRepository cartRepository;

        public CartController(IProductRepository productRepository,IShoppingCartRepository shoppingCart, 
            IServiceProvider services, ICartRepository cartRepository)
        {
            this.productRepository = productRepository;
            this.shoppingCart = shoppingCart;
            this.services = services;
            this.cartRepository = cartRepository;
        }
        public string GetCart(IServiceProvider service)
        {

            try
            {
                HttpRequest cookie = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Request;
                string cartId = cookie.Cookies["cardId"] ?? Guid.NewGuid().ToString();
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Append("cardId", cartId, option);
                return cartId;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public ViewResult ShoppingCart() {

            string cartId = GetCart(services);
            List<CartProductViewModel> CartProductViewModels = new List<CartProductViewModel>();
            
            foreach (var item in shoppingCart.GetCartProducts(cartId))
            {
                var cartProductViewModel = new CartProductViewModel() { 
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
        [HttpPost]
        public bool AddToShoppingCart(string id)
        {
            try
            {
                string cartId = GetCart(services);
                var product = productRepository.All.Where(d => d.Id == id).FirstOrDefault();
                if (product != null)
                {      
                    shoppingCart.AddToCart(product, cartId);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
           
        }
        public ActionResult AddToShoppingCartInDetail(ProductViewModel model, string quantity)
        {
           
                string cartId = GetCart(services);
                var product = productRepository.All.Where(d => d.Id == model.Id).FirstOrDefault();
                if (product != null)
                {
                    if (quantity != null)
                    {
                        shoppingCart.AddToCartWithQuantity(product, cartId, Convert.ToInt32(quantity));
                    }
                    return RedirectToAction("ShoppingCart");
                }
                return View("Detail","Home");
        }
        [HttpPost]
        public bool Delete(string id)
        {
            try
            {
                shoppingCart.RemoveFromCart(productRepository.All.FirstOrDefault(d => d.Id == id), GetCart(services));
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
                shoppingCart.UpdateQuantityInCart(id, Convert.ToInt32(quantity),GetCart(services));
                return true;
            }
            catch (Exception)
            {
                return false;
               
            }
            
        }
       
    }
}
