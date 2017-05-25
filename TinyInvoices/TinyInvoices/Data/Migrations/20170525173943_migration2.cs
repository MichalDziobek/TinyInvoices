using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyInvoices.Data.Migrations
{
    public partial class migration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "AutomaticInvoiceInterval",
                table: "UserGroups",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstAutomaticInvoiceGenerationDate",
                table: "UserGroups",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutomaticInvoiceInterval",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "FirstAutomaticInvoiceGenerationDate",
                table: "UserGroups");
        }
    }
}
