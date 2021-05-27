using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiCarApp.Models
{
    public class CarsContext : DbContext
    {
        public CarsContext() { }
        public virtual DbSet<Automobile> Automobiles {get;set; }
        public virtual DbSet<BodyType> BodyTypes { get; set; }
        public virtual DbSet<FuelType> FuelTypes { get; set; }
        public virtual DbSet<CategoriesAutomobiles> CategoriesAutomobiles { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Firm> Firms { get; set; }
        public virtual DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-5R9TST7; Database=Cars; Trusted_Connection=True;");
            }
        }

        public CarsContext(DbContextOptions<CarsContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
