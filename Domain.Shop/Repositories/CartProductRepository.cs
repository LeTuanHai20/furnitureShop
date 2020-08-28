using Domain.Shop.Entities;
using Domain.Shop.IRepositories;
using Infrastructure.Database;
using Shop.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Shop.Repositories
{
    public class CartProductRepository : Repository<ShopDBContext, CartProduct>, ICartProductsRepository
    {
        public BlogCategoryRepository(IUnitOfWork<ShopDBContext> unitOfWork) : base(unitOfWork)
        {

        }
    }
}
