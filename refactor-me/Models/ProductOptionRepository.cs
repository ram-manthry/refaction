using System;
using System.Data.SqlClient;

namespace refactor_me.Models
{
    public class ProductOptionRepository
    {
        public ProductOption Get(Guid id)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"select * from productoption where id = '{id}'", conn);
            conn.Open();

            var rdr = cmd.ExecuteReader();
            if (!rdr.Read()) return null;

            var option = new ProductOption()
            {
                Id = Guid.Parse(rdr["Id"].ToString()),
                ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                Name = rdr["Name"].ToString(),
                Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString()
            };
            return option;
        }

        public void Create(ProductOption option)
        {
            var conn = Helpers.NewConnection();
            var cmd =
                new SqlCommand(
                    $"insert into productoption (id, productid, name, description) values ('{option.Id}', '{option.ProductId}', '{option.Name}', '{option.Description}')",
                    conn);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(ProductOption option)
        {
            var conn = Helpers.NewConnection();
            var cmd = new SqlCommand($"update productoption set name = '{option.Name}', description = '{option.Description}' where id = '{option.Id}'", conn);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(Guid id)
        {
            var conn = Helpers.NewConnection();
            conn.Open();
            var cmd = new SqlCommand($"delete from productoption where id = '{id}'", conn);
            cmd.ExecuteReader();
        }
    }
}