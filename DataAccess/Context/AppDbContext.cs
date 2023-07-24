using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DataAccess.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        { }
        public DbSet<Models.FinancialData> CompanyFinancialData { get; set; } = default!;

        public DbSet<Models.Company> Companies { get; set; } = default!;
        /// <summary>
        /// Override method to be able to configure the different models in entity framework.
        /// Uses Fluent API methods to validate the data before inserting into the Database.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasMany(c => c.FinancialDatas)
                .WithOne()
                .HasForeignKey(f => f.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Company>()
                .Property(c => c.CIK)
                .HasMaxLength(10);

            modelBuilder.Entity<FinancialData>()
                .Property(f => f.EndDate)
                .HasMaxLength(10);

            // Get the properties of FinancialData class
            var decimalProperties = typeof(FinancialData).GetProperties()
                .Where(p => p.PropertyType == typeof(decimal));

            foreach (var property in decimalProperties)
            {
                modelBuilder.Entity<FinancialData>()
                    .Property(property.Name)
                    .HasPrecision(18, 2);
            }
        }
    }
}
