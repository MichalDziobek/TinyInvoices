using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TinyInvoices.Data;

namespace TinyInvoices.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170525173943_migration2")]
    partial class migration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<int>("UserGroupId");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.Bill", b =>
                {
                    b.Property<int>("BillId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChargeId");

                    b.Property<int>("UserToGroupMappingId");

                    b.Property<decimal>("Value");

                    b.HasKey("BillId");

                    b.HasIndex("ChargeId");

                    b.HasIndex("UserToGroupMappingId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.Charge", b =>
                {
                    b.Property<int>("ChargeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("InvoiceId");

                    b.Property<int>("RuleId");

                    b.Property<decimal>("Value");

                    b.HasKey("ChargeId");

                    b.HasIndex("InvoiceId");

                    b.ToTable("Charges");
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.Cost", b =>
                {
                    b.Property<int>("CostId")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan?>("Interval");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsRepeatable");

                    b.Property<string>("Name");

                    b.Property<DateTime>("StartingDate");

                    b.Property<int>("UserGroupId");

                    b.Property<decimal>("Value");

                    b.HasKey("CostId");

                    b.HasIndex("UserGroupId");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CalculationDate");

                    b.Property<int>("UserGroupId");

                    b.HasKey("InvoiceId");

                    b.HasIndex("UserGroupId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.UserGroup", b =>
                {
                    b.Property<int>("UserGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("AutomaticInvoiceInterval");

                    b.Property<string>("Description");

                    b.Property<DateTime>("FirstAutomaticInvoiceGenerationDate");

                    b.Property<string>("Name");

                    b.HasKey("UserGroupId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.UserToGroupMapping", b =>
                {
                    b.Property<int>("UserToGroupMappingId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsAdmin");

                    b.Property<int>("UserGroupId");

                    b.Property<string>("UserId");

                    b.HasKey("UserToGroupMappingId");

                    b.HasIndex("UserGroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserToGroupMappings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TinyInvoices.Models.DatabaseModel.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TinyInvoices.Models.DatabaseModel.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TinyInvoices.Models.DatabaseModel.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.Bill", b =>
                {
                    b.HasOne("TinyInvoices.Models.DatabaseModel.Charge", "Charge")
                        .WithMany("Bills")
                        .HasForeignKey("ChargeId");

                    b.HasOne("TinyInvoices.Models.DatabaseModel.UserToGroupMapping", "UserToGroupMapping")
                        .WithMany("Bills")
                        .HasForeignKey("UserToGroupMappingId");
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.Charge", b =>
                {
                    b.HasOne("TinyInvoices.Models.DatabaseModel.Invoice", "Invoice")
                        .WithMany("Charges")
                        .HasForeignKey("InvoiceId");
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.Cost", b =>
                {
                    b.HasOne("TinyInvoices.Models.DatabaseModel.UserGroup", "UserGroup")
                        .WithMany("Costs")
                        .HasForeignKey("UserGroupId");
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.Invoice", b =>
                {
                    b.HasOne("TinyInvoices.Models.DatabaseModel.UserGroup", "UserGroup")
                        .WithMany("Invoices")
                        .HasForeignKey("UserGroupId");
                });

            modelBuilder.Entity("TinyInvoices.Models.DatabaseModel.UserToGroupMapping", b =>
                {
                    b.HasOne("TinyInvoices.Models.DatabaseModel.UserGroup", "UserGroup")
                        .WithMany("UserToGroupMappings")
                        .HasForeignKey("UserGroupId");

                    b.HasOne("TinyInvoices.Models.DatabaseModel.ApplicationUser", "User")
                        .WithMany("UserToGroupMappings")
                        .HasForeignKey("UserId");
                });
        }
    }
}
