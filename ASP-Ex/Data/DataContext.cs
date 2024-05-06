using Microsoft.EntityFrameworkCore;

namespace ASP_Ex.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Entities.User> Users { get; set; }
		public DbSet<Entities.Category> Categories { get; set; }
		public DbSet<Entities.Product> Products { get; set; }
		public DbSet<Entities.Basket> Baskets { get; set; }

		public DataContext(DbContextOptions options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //	// base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Category>()
                .HasIndex(c => c.Slug)
                .IsUnique();

			modelBuilder.Entity<Entities.Basket>()
				.HasOne(r => r.User)
				.WithMany(r => r.Baskets)
				.HasForeignKey(r => r.UserId);
			modelBuilder.Entity<Entities.Basket>()
				.HasOne(r => r.Product)
				.WithMany(r => r.Baskets)
				.HasForeignKey(r => r.ProductsId);
		}
    }
}
