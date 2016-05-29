using System.Collections.Generic;

namespace refactor_me.Models
{
    public class ProductOptions
    {
        public List<ProductOption> Items { get; private set; }

        public ProductOptions()
        {
            Items = new List<ProductOption>();
        }
    }
}