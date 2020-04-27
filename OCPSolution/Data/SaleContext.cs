using Microsoft.EntityFrameworkCore;
using OCPSolution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OCPSolution.Data
{
    public class SaleContext : DbContext
    {
        public SaleContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<Row> Rows { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
