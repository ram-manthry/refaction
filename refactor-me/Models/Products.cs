using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace refactor_me.Models
{
    public class Products
    {
        public List<Product> Items { get; private set; }

        public Products()
        {
            Items = new List<Product>();
        }
    }
}