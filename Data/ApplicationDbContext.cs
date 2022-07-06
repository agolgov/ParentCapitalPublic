using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ParentCapitalBlazor.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Indexation> Indexations { get; set; }
        
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Gender> Genders { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

    }
}
