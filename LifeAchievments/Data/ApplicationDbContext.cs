using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using LifeAchievments.Models;

namespace LifeAchievments.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public DbSet<Achievment> AchievmentsCollection { get; set; }
        public DbSet<Category> CategoryCollection { get; set; }
        public DbSet<Icon> IconsCollection { get; set; }
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=ec2-34-199-68-114.compute-1.amazonaws.com;Port=5432;Database=dc4rvderfarvth;Username=vhsheenxvugshb;Password=b6258586aae5ac3ecf6746f9432ba6896ffe2258d01315fc34c8fb500c3b8d99");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
