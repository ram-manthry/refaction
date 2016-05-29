using System;
using System.Data;
using refactor_me.Models;

namespace refactor_me.DataAccess
{
    public class ProductOptionRepository : IProductOptionRepository
    {
        public ProductOption Get(Guid id)
        {
            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Id, ProductId, Name, Description FROM ProductOption WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();

                using (var reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    if (!reader.Read()) return null;

                    var option = new ProductOption
                    {
                        Id = reader.GetGuid("Id"),
                        ProductId = reader.GetGuid("ProductId"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description")
                    };
                    return option;
                }
            }
        }

        public void Create(ProductOption option)
        {
            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO ProductOption (Id, ProductId, Name, Description) " +
                                  "VALUES (@Id, @ProductId, @Name, @Description)";
                cmd.Parameters.AddWithValue("@Id", option.Id);
                cmd.Parameters.AddWithValue("@ProductId", option.ProductId);
                cmd.Parameters.AddWithValue("@Name", option.Name);
                cmd.Parameters.AddWithValue("@Description", option.Description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Update(ProductOption option)
        {
            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE ProductOption SET Name = @Name, Description = @Description WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", option.Id);
                cmd.Parameters.AddWithValue("@Name", option.Name);
                cmd.Parameters.AddWithValue("@Description", option.Description);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(Guid id)
        {
            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM ProductOption WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public ProductOptions GetByProductId(Guid productId)
        {
            var options = new ProductOptions();

            using (var conn = Helpers.NewConnection())
            {
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT Id FROM ProductOption WHERE productId = @ProductId";
                cmd.Parameters.AddWithValue("@ProductId", productId);

                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var id = reader.GetGuid("Id");
                        var product = Get(id);
                        options.Items.Add(product);
                    }
                    return options;
                }
            }
        }
    }
}