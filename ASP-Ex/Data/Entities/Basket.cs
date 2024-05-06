using System.Text.Json.Serialization;

namespace ASP_Ex.Data.Entities
{
	public class Basket
	{
		public Guid Id { get; set; }
		public Guid ProductsId { get; set; }
		public Guid UserId { get; set; }
		public DateTime? DeleteDt { get; set; }
		public DateTime? Date{ get; set; }

		[JsonIgnore] public User User { get; set; }
		[JsonIgnore] public Product Product { get; set; }

	}
}
