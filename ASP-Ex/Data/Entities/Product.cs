namespace ASP_Ex.Data.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? CompanyId { get; set; }
        public int? Stars { get; set; }
        public int? Count { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime? DeleteDt { get; set; } 
        public Double Price { get; set; }
        public String? PhotoUrl { get; set; }
        public String? Slug { get; set; }

        public List<Basket> Baskets { get; set; } = [];
    }
}
