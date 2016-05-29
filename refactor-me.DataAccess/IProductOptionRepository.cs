using System;
using refactor_me.Models;

namespace refactor_me.DataAccess
{
    public interface IProductOptionRepository
    {
        ProductOption Get(Guid id);
        void Create(ProductOption option);
        void Update(ProductOption option);
        void Delete(Guid id);
        ProductOptions GetByProductId(Guid productId);
    }
}