using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TinyInvoices.Models;
using TinyInvoices.Models.DatabaseModel;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TinyInvoices.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Charge> Charges { get; set; }
        public virtual DbSet<Cost> Costs { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<UserToGroupMapping> UserToGroupMappings { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Bill>(b =>
            {
                b.HasKey(x => x.BillId);
                b.HasOne(x => x.Charge)
                    .WithMany(x => x.Bills)
                    .HasForeignKey(x => x.ChargeId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.UserToGroupMapping)
                    .WithMany(x => x.Bills)
                    .HasForeignKey(x => x.UserToGroupMappingId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Charge>(b =>
            {
                b.HasKey(x => x.ChargeId);
                b.HasOne(x => x.Invoice)
                    .WithMany(x => x.Charges)
                    .HasForeignKey(x => x.InvoiceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Cost>(b =>
            {
                b.HasKey(x => x.CostId);
                b.HasOne(x => x.UserGroup)
                    .WithMany(x => x.Costs)
                    .HasForeignKey(x => x.UserGroupId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Invoice>(b =>
            {
                b.HasKey(x => x.InvoiceId);
                b.HasOne(x => x.UserGroup)
                    .WithMany(x => x.Invoices)
                    .HasForeignKey(x => x.UserGroupId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<UserGroup>(b =>
            {
                b.HasKey(x => x.UserGroupId);
            });
            builder.Entity<UserToGroupMapping>(b =>
            {
                b.HasKey(x => x.UserToGroupMappingId);
                b.HasOne(x => x.UserGroup)
                    .WithMany(x => x.UserToGroupMappings)
                    .HasForeignKey(x => x.UserGroupId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(x => x.User)
                    .WithMany(x => x.UserToGroupMappings)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

    }
}
