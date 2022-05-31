using CategoryManager.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CategoryManager.Repository.Context
{
    public class CategoryManagerContext : DbContext
    {
        public CategoryManagerContext(DbContextOptions<CategoryManagerContext> option) : base(option)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Category>()
                .Property(k => k.Name)
                .HasMaxLength(100)
                .IsRequired();

            //modelBuilder.Entity<Category>()
            //.HasMany(c => c.Children)
            //.WithOne()
            //.OnDelete(DeleteBehavior.ClientCascade);


            //modelBuilder.Entity<Category>()
            //   .HasMany(k => k.Children);

            //modelBuilder.Entity<CategoryChild>()
            //.HasKey(bc => new { bc.CategoryId, bc.ChildId });
            //modelBuilder.Entity<CategoryChild>()
            //    .HasOne(bc => bc.Category)
            //    .WithMany(b => b.Children)
            //    .HasForeignKey(bc => bc.CategoryId);
            //modelBuilder.Entity<CategoryChild>()
            //    .HasOne(bc => bc.Child)
            //    .WithMany(c => c.Children)
            //    .HasForeignKey(bc => bc.ChildId);

            //modelBuilder.Entity<Category>()
            //    .HasMany(c => c.Children)
            //    .WithOne(e => e.Category);
        }

        public DbSet<Category> Categories { get; set; }
        //public DbSet<CategoryChild> Children { get; set; }

    }
}
