using ETicaretMVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETicaretMVC.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<GeneralCategory> GeneralCategories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(x=> x.Price).HasPrecision(10,2);
            modelBuilder.Entity<Orders>().Property(x => x.Price).HasPrecision(10, 2);
            modelBuilder.Entity<Basket>().Property(x => x.Price).HasPrecision(12,2);
            modelBuilder.Entity<Basket>().Property(x => x.ProductPrice).HasPrecision(10, 2);
            modelBuilder.Entity<Favorite>().Property(x => x.ProductPrice).HasPrecision(10, 2);
            modelBuilder.Entity<Bill>().Property(x => x.TotalPrice).HasPrecision(12, 2);
            modelBuilder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.Address).HasMaxLength(255);
        builder.Property(x => x.Name).HasMaxLength(30);
        builder.Property(x => x.Surname).HasMaxLength(30);

    }
}
