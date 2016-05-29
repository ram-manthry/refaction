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
                cmd.CommandText = "SELECT Id, Name, Description, Price, DeliveryPrice FROM Product WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

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
                cmd.CommandText = "INSERT INTO Product (Id, Name, Description, Price, DeliveryPrice) " +
                                  "VALUES (@Id, @Name, @Description, @Price, @DeliveryPrice)";
                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@DeliveryPrice", product.DeliveryPrice);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(Product product)
        {
            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE Product SET Name = @Name, Description = @Description, Price = @Price, DeliveryPrice = @DeliveryPrice WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", product.Id);
                cmd.Parameters.AddWithValue("@Name", product.Name);
                cmd.Parameters.AddWithValue("@Description", product.Description);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@DeliveryPrice", product.DeliveryPrice);

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
                cmd.CommandText = $"DELETE FROM Product WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", productId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Products GetAll()
        {
            var products = new Products();

            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Id, Name, Description, Price, DeliveryPrice FROM Product";

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            Id = reader.GetGuid("Id"),
                            Name = reader.GetString("Name"),
                            Description = reader.GetString("Description"),
                            Price = reader.GetDecimal("Price"),
                            DeliveryPrice = reader.GetDecimal("DeliveryPrice")
                        };
                        products.Items.Add(product);
                    }
                    return products;
                }
            }
        }

        public Products Get(string name)
        {
            var products = new Products();

            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Id, Name, Description, Price, DeliveryPrice FROM Product WHERE LOWER(Name) LIKE @Name";
                cmd.Parameters.AddWithValue("@Name", "%"+name+"%");

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            Id = reader.GetGuid("Id"),
                            Name = reader.GetString("Name"),
                            Description = reader.GetString("Description"),
                            Price = reader.GetDecimal("Price"),
                            DeliveryPrice = reader.GetDecimal("DeliveryPrice")
                        };
                        products.Items.Add(product);
                    }
                    return products;
                }
            }
        }
    }
}