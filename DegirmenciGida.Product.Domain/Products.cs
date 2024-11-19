using Persistence.Repositories;

namespace DegirmenciGida.Product.Domain
{
    public class Products:BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

    }
}
