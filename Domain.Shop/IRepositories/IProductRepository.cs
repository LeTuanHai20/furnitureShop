using Domain.Shop.Dto.Products;
using Domain.Shop.Entities;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Shop.IRepositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<ProductViewModel> GetProductViewModels();
        ProductViewModel GetProductViewModelById(string id);
        Product GetProductById(string id);
    }
}
