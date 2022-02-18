using CommonLayer.Note;
using CommonLayer.User;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entities;
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
        public DbSet<Note> Notes { get; set; }
        public DbSet<Label> Label { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>()
               .HasIndex(u => u.email)
               .IsUnique();
        }
    }
}
