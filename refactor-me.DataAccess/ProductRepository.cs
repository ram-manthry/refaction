using System;
using System.Data.SqlClient;
using refactor_me.Models;

namespace refactor_me.DataAccess
{
    public class ProductRepository
    {
        private readonly ProductOptionRepository _productOptionRepository = new ProductOptionRepository();

        public Product Get(Guid id)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"select * from product where id = '{id}'", conn);
            conn.Open();

            var rdr = cmd.ExecuteReader();
            if (!rdr.Read()) return null;

            var product = new Product()
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
            Name = rdr["Name"].ToString(),
            Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
            Price = decimal.Parse(rdr["Price"].ToString()),
            DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
        };
            return product;
        }

        public void Create(Product product)
        {
            var conn = Helpers.NewConnection();
            var cmd =
                new SqlCommand(
                    $"insert into product (id, name, description, price, deliveryprice) values ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})",
                    conn);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(Product product)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"update product set name = '{product.Name}', description = '{product.Description}', price = {product.Price}, deliveryprice = {product.DeliveryPrice} where id = '{product.Id}'", conn);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(Guid productId)
        {
            var options = _productOptionRepository.GetByProductId(productId);
            foreach (var option in options.Items)
                _productOptionRepository.Delete(option.Id);

            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = new SqlCommand($"delete from product where id = '{productId}'", conn);
            cmd.ExecuteNonQuery();
        }

        public Products GetAll()
        {
            return LoadProducts(String.Empty);
        }

        public Products Get(string name)
        {
            return LoadProducts($"where lower(name) like '%{name.ToLower()}%'");
        }

        private Products LoadProducts(string where)
        {
            var products = new Products();

            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"select id from product {where}", conn);
            conn.Open();

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr["id"].ToString());
                var product = Get(id);
                products.Items.Add(product);
            }
            return products;
        }
    }
}