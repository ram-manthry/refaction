using System;
using System.Data.SqlClient;

namespace refactor_me.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        protected bool Equals(ProductOption other)
        {
            return Id.Equals(other.Id) && ProductId.Equals(other.ProductId) && string.Equals(Name, other.Name) && string.Equals(Description, other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ProductOption) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode*397) ^ ProductId.GetHashCode();
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }
        
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