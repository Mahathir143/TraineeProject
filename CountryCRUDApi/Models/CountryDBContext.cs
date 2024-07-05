using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CountryCRUDApi.Models
{
    public partial class CountryDBContext : DbContext
    {
        public CountryDBContext()
        {
        }

        public CountryDBContext(DbContextOptions<CountryDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblRoleMaster> TblRoleMasters { get; set; } = null!;
        public virtual DbSet<TblUserMaster> TblUserMasters { get; set; } = null!;
        public virtual DbSet<TheCountryMaster> TheCountryMasters { get; set; } = null!;
        public virtual DbSet<TheStateMaster> TheStateMasters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=ARASAN-EOD;Database=CountryData;User Id=sa;Password=easy@123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblRoleMaster>(entity =>
            {
                entity.ToTable("Tbl_Role_Master");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Isactive).HasColumnName("ISActive");

                entity.Property(e => e.RoleCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblUserMaster>(entity =>
            {
                entity.ToTable("Tbl_User_Master");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TheCountryMaster>(entity =>
            {
                entity.ToTable("The_Country_Master");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CountryCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TheStateMaster>(entity =>
            {
                entity.ToTable("The_State_Master");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.StateCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
