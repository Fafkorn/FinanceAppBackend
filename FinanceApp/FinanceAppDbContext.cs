using FinanceApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp
{
    public class FinanceAppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<Expense> Expenses { get; set; }

        private readonly string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=FinanaceAppDb2;Trusted_Connection=True";
        
        public FinanceAppDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = new Guid("fab2c13f-f950-4977-a935-8e14e8dbac20"),
                    Name = "Administrator"
                },
                new Role()
                {
                    Id = new Guid("0758e8d0-90cc-42f9-a49a-baf74ba3da15"),
                    Name = "User"
                }
            );

            modelBuilder.Entity<ExpenseCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
        }
    }
}
