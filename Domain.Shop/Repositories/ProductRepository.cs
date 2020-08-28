using Domain.Shop.Dto.Products;
using Domain.Shop.Entities;
using Domain.Shop.IRepositories;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Shop.Application;
using Infrastructure.Common;
using Domain.Shop.Enums;

namespace Domain.Shop.Repositories
{
    public class ProductRepository : Repository<ShopDBContext, Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork<ShopDBContext> unitOfWork) : base(unitOfWork)
        {

        }

        public Product GetProductById(string id)
        {
            return this.All.Where(m => m.Id == id).FirstOrDefault();
        }

        public ProductViewModel GetProductViewModelById(string id)
        {
            var model =   this.All.Where(m => m.Id == id).Select(m => new ProductViewModel
            {
                Id = m.Id,
                ProductCode = m.ProductCode,
                ProductName = m.ProductName,
                Slug = m.Slug,
                Description = m.Description,
                ProductTypeId = m.ProductTypeId,
                ProductTypeName = m.ProductType.TypeName,
                MaterialId = m.MaterialId,
                MaterialName = m.Material.MaterialName,
                CategoryId = m.CategoryId,
                CategoryName = m.Category.CategoryName,
                PriceType = m.PriceType.ToString(),
                Price = m.Price,
                DisplayImages = m.ProductImages.Select(s => s.Url).ToList()
            }).FirstOrDefault();
            return model;
        }

        public IEnumerable<ProductViewModel> GetProductViewModels()
        {
            return this.All.Select(m => new ProductViewModel
            {
                Id = m.Id,
                ProductCode = m.ProductCode,
                ProductName = m.ProductName,
                Slug = m.Slug,
                Description = m.Description,
                ProductTypeId = m.ProductTypeId,
                ProductTypeName = m.ProductType.TypeName,
                MaterialId = m.MaterialId,
                MaterialName = m.Material.MaterialName,
                CategoryId = m.CategoryId,
                CategoryName = m.Category.CategoryName,
                PriceType = m.PriceType.ToString(),
                Price = m.Price,
                DisplayImages = m.ProductImages.Select(s => s.Url).ToList()
            }).ToList();
        }
    }
}
