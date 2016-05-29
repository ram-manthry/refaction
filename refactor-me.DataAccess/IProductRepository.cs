using System;
using refactor_me.Models;

namespace refactor_me.DataAccess
{
    public interface IProductRepository
    {
        Product Get(Guid id);
        void Create(Product product);
        void Update(Product product);
        void Delete(Guid productId);
        Products GetAll();
        Products Get(string name);
    }
}