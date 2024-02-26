using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DoAn_LTWeb.Models
{
    public class CompanyDBContext : DbContext
    {
        public CompanyDBContext() : base("MyConnectionString") 
        {
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<User> Users { get; set; }
    }
}