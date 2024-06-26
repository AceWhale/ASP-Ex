﻿namespace ASP_Ex.Data.Entities
{
	public class Category
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = null!;
		public string Description { get; set; } = null!;
		public DateTime? DeleteDt { get; set; }

		public String? PhotoUrl { get; set; }

        public String? Slug { get; set; }
    }
}
