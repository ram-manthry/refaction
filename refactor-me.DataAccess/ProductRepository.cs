using System;
using System.Data;
using refactor_me.Models;

namespace refactor_me.DataAccess
{
    public class ProductRepository
    {
        private readonly ProductOptionRepository _productOptionRepository = new ProductOptionRepository();

        public Product Get(Guid id)
        {
            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT Id, Name, Description, Price, DeliveryPrice " +
                                  $"FROM Product WHERE Id = '{id}'";

                conn.Open();

                using (var reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    if (!reader.Read()) return null;

                    var product = new Product
                    {
                        Id = reader.GetGuid("Id"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                        Price = reader.GetDecimal("Price"),
                        DeliveryPrice = reader.GetDecimal("DeliveryPrice")
                    };
                    return product;
                }
            }
        }

        public void Create(Product product)
        {
            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"INSERT INTO Product (Id, Name, Description, Price, DeliveryPrice) " +
                                  $"VALUES ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Product product)
        {
            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText =
                    $"UPDATE Product SET Name = '{product.Name}', Description = '{product.Description}', Price = {product.Price}, DeliveryPrice = {product.DeliveryPrice} " +
                    $"WHERE Id = '{product.Id}'";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Guid productId)
        {
            var options = _productOptionRepository.GetByProductId(productId);
            foreach (var option in options.Items)
                _productOptionRepository.Delete(option.Id);

            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"DELETE FROM Product WHERE Id = '{productId}'";

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Products GetAll()
        {
            return LoadProducts(String.Empty);
        }

        public Products Get(string name)
        {
            return LoadProducts($"WHERE LOWER(Name) LIKE '%{name.ToLower()}%'");
        }

        private Products LoadProducts(string where)
        {
            var products = new Products();

            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT Id FROM Product {where}";

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetGuid("Id");
                        var product = Get(id);
                        products.Items.Add(product);
                    }
                    return products;
                }
            }
        }
    }
}