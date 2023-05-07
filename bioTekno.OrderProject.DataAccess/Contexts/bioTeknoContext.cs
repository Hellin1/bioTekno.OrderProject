using bioTekno.OrderProject.DataAccess.Configurations;
using bioTekno.OrderProject.Entities.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bioTekno.OrderProject.DataAccess.Contexts
{
    public class bioTeknoContext : DbContext
    {
        public bioTeknoContext(DbContextOptions<bioTeknoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            //modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }



    }
}
