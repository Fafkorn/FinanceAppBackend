using FinanceApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp
{
    public class FinanceAppDbContext : DbContext
    {
        private readonly string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=FinanaceAppDb;Trusted_Connection=True";
        public DbSet<User> Users { get; set; }

        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ExpenseCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
    }
}
