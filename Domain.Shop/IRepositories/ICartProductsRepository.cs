using Domain.Shop.Entities;
using Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Shop.IRepositories
{
    public interface ICartProductsRepository : IRepository<CartProduct>
    {
    }
}
