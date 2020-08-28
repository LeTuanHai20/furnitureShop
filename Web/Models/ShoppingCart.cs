using Domain.Shop.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ShoppingCart : Cart
    {
        private readonly ShopDBContext context;

        public List<CartProduct> cartProducts { get; set; }
        public ShoppingCart(ShopDBContext context)
        {
            this.context = context;
        }
      
        public void AddToCart(Product product,string cartId)
        {
            var CartProduct = context.CartProducts.SingleOrDefault(
                s => s.ProductId == product.Id && s.CartId == cartId);
            if (CartProduct == null)
            {
                Cart cart = context.Carts.ToList().Where(c => c.Id == cartId).FirstOrDefault();
                if (cart == null)
                {
                    cart = new Cart()
                    {
                        Id = cartId
                    };
                    context.Carts.Add(cart);
                }
                CartProduct = new CartProduct()
                {
                    Id = Guid.NewGuid().ToString(),
                    Product = product,
                    Quantity = 1,
                    CartId = cartId
                };
                
                context.CartProducts.Add(CartProduct);
            }
            else
            {
                CartProduct.Quantity++;
            }
            context.SaveChanges();
        }
        public int RemoveFromCart(Product product)
        {
            var cartProduct = context.CartProducts
                .SingleOrDefault(s => s.ProductId == product.Id && s.CartId == Id);
            int nowQuantity = 0;
            if (cartProduct != null)
            {
                if (cartProduct.Quantity > 1)
                {
                    cartProduct.Quantity--;
                    nowQuantity = cartProduct.Quantity;
                }
                else
                {
                    context.CartProducts.Remove(cartProduct);
                }
            }
            context.SaveChanges();
            return nowQuantity;
        }
        public List<CartProduct> GetCartProducts(string cardId)
        {
            var cart = context.CartProducts.Where(c => c.CartId == cardId)
                           .Include(s => s.Product).Include(s => s.Product.ProductType).Include(s => s.Product.ProductImages)
                           .ToList();
            return cartProducts ?? cart;
        }
        public void ClearCart()
        {
            var cartItems = context
                .CartProducts
                .Where(cart => cart.CartId == Id);

            context.CartProducts.RemoveRange(cartItems);

            context.SaveChanges();
        }

        public long GetShoppingCartTotal()
        {
            var total = context.CartProducts.Where(c => c.CartId == Id)
                .Select(c => c.Product.Price * c.Quantity).Sum();
            return total.Value;
        }
    }
}
