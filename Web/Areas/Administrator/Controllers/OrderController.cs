using Domain.Shop.Dto.Cart;
using Domain.Shop.Dto.CartProduct;
using Domain.Shop.Entities;
using Domain.Shop.IRepositories;
using Infrastructure.Web;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [BaseAuthorization]
    public class OrderController : BaseController
    {
        private readonly ICartRepository cartRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IProductRepository productRepository;
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IDictrictRepository dictrictRepository;
        private readonly IProvinceRepository provinceRepository;

        public OrderController(ICartRepository cartRepository, IAccountRepository accountRepository, 
            IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository,IDictrictRepository 
            dictrictRepository, IProvinceRepository provinceRepository)
        {
            this.cartRepository = cartRepository;
            this.accountRepository = accountRepository;
            this.productRepository = productRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.dictrictRepository = dictrictRepository;
            this.provinceRepository = provinceRepository;
        }
        public ActionResult Index()
        {
            List<CartViewModel> model = cartRepository.GetCartViewModels().ToList();
            foreach (var item in model)
            {
                item.Customer = accountRepository.GetCustomerViewModel(item.CustomerId);
                item.Customer.District = dictrictRepository.GetDictrictViewModel(accountRepository.GetCustomerViewModel(item.CustomerId).District).Name;
                item.Customer.Province = provinceRepository.GetProvinceViewModel(accountRepository.GetCustomerViewModel(item.CustomerId).Province).Name;
            }
            return View(model);
        }
        public ActionResult Detail(string id)
        {
            var model = cartRepository.GetCartViewModel(id);
            model.Customer = accountRepository.GetCustomerViewModel(model.CustomerId);
            model.Customer.District = dictrictRepository.GetDictrictViewModel(accountRepository.GetCustomerViewModel(model.CustomerId).District).Name;
            model.Customer.Province = provinceRepository.GetProvinceViewModel(accountRepository.GetCustomerViewModel(model.CustomerId).Province).Name;
            List<CartProductViewModel> CartProductViewModels = new List<CartProductViewModel>();
            foreach (var cartProduct in shoppingCartRepository.GetCartProductsBought(id,model.CustomerId))
            {
                var cartProductViewModel = new CartProductViewModel()
                {
                    Id = cartProduct.Id,
                    CartId = cartProduct.CartId,
                    Cart = cartRepository.GetCartViewModel(cartProduct.CartId),
                    ProductId = cartProduct.ProductId,
                    Product = productRepository.GetProductViewModelById(cartProduct.ProductId),
                    Price = cartProduct.Price,
                    PriceType = cartProduct.PriceType,
                    Quantity = cartProduct.Quantity,
                    Total = cartProduct.Total
                };
                CartProductViewModels.Add(cartProductViewModel);
                model.Total += cartProductViewModel.Total;
            }
            model.Products = CartProductViewModels;
            return View(model);
        }
        [HttpPost]
        public bool Delete(string id)
        {
            try
            {
                var model = cartRepository.GetCartViewModel(id);
                var cartProduct = shoppingCartRepository.GetCartProductsBought(id,model.CustomerId);          
                Cart cart = cartRepository.Get(id);
                shoppingCartRepository.Delete(cartProduct);
                cartRepository.Save();
                cartRepository.Delete(cart);
                cartRepository.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
