using Microsoft.EntityFrameworkCore;
using MobilePhoneBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilePhoneBD.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Manufacturers> Мanufacturers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    }
}
