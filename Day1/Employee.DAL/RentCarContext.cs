using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Employee.DAL
{
    public partial class RentCarContext : DbContext
    {
        public RentCarContext()
            : base("name=RentCarContext")
        {
        }

        public virtual DbSet<CAR> CAR { get; set; }
        public virtual DbSet<CUSTOMER> CUSTOMER { get; set; }
        public virtual DbSet<CUSTOMERINFO> CUSTOMERINFO { get; set; }
        public virtual DbSet<DRIVER> DRIVER { get; set; }
        public virtual DbSet<EMPLOYEE> EMPLOYEE { get; set; }
        public virtual DbSet<EMPLOYEEREVIEW> EMPLOYEEREVIEW { get; set; }
        public virtual DbSet<MODEL> MODEL { get; set; }
        public virtual DbSet<RENTAL> RENTAL { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CAR>()
                .Property(e => e.RentPrice)
                .HasPrecision(5, 2);

            modelBuilder.Entity<CAR>()
                .HasMany(e => e.DRIVER)
                .WithRequired(e => e.CAR)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CAR>()
                .HasMany(e => e.RENTAL)
                .WithRequired(e => e.CAR)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<CUSTOMER>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<CUSTOMER>()
                .HasMany(e => e.RENTAL)
                .WithRequired(e => e.CUSTOMER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CUSTOMER>()
                .HasOptional(e => e.CUSTOMERINFO)
                .WithRequired(e => e.CUSTOMER);

            modelBuilder.Entity<CUSTOMERINFO>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<CUSTOMERINFO>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<CUSTOMERINFO>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<EMPLOYEE>()
                .HasMany(e => e.DRIVER)
                .WithRequired(e => e.EMPLOYEE)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EMPLOYEEREVIEW>()
                .Property(e => e.Review)
                .IsUnicode(false);

            modelBuilder.Entity<MODEL>()
                .Property(e => e.MakeName)
                .IsUnicode(false);

            modelBuilder.Entity<MODEL>()
                .Property(e => e.ModelName)
                .IsUnicode(false);

            modelBuilder.Entity<MODEL>()
                .HasMany(e => e.CAR)
                .WithRequired(e => e.MODEL)
                .WillCascadeOnDelete(false);
        }
    }
}
