using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HappyBDay.Models;

#nullable disable

namespace HappyBDay.Data
{
    public partial class HappyBDayContext : DbContext
    {
        public HappyBDayContext()
        {
        }

        public HappyBDayContext(DbContextOptions<HappyBDayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Consultants> Consultants { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Consultants>(entity =>
            {
                entity.HasOne(d => d.IdDepartmentsNavigation)
                    .WithMany(p => p.Consultants)
                    .HasForeignKey(d => d.IdDepartments)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Consultants_Departments");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasOne(d => d.IdProfileNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdProfile)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Profile");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
