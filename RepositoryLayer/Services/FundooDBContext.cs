using CommonLayer.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FundooDBContext : DbContext
    {
        public FundooDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        protected override void
        OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
            .HasIndex(u => u.email)
            .IsUnique();
        }
    }
}
