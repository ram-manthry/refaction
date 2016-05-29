using System;

namespace refactor_me.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        protected bool Equals(Product other)
        {
            return Id.Equals(other.Id) && string.Equals(Name, other.Name) &&
                   string.Equals(Description, other.Description) && Price == other.Price &&
                   DeliveryPrice == other.DeliveryPrice;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Product) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id.GetHashCode();
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Price.GetHashCode();
                hashCode = (hashCode*397) ^ DeliveryPrice.GetHashCode();
                return hashCode;
            }
        }
    }
}