﻿using Microsoft.EntityFrameworkCore;

namespace CafeActivity.Models
{
    public class CafeActivityContext : DbContext
    {
        public CafeActivityContext() { }
        public CafeActivityContext(DbContextOptions<CafeActivityContext> options) : base(options) { }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Artist> Artists { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
            "Server=EMRULLAH\\SQLEXPRESS;Database=CafeActivityDb;" +
            "TrustServerCertificate=True;Trusted_Connection=True;Encrypt=False"
               );
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
}
