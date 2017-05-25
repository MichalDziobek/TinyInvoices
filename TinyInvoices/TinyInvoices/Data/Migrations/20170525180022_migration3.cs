using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TinyInvoices.Data.Migrations
{
    public partial class migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RuleId",
                table: "Charges",
                newName: "CostId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_CostId",
                table: "Charges",
                column: "CostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Costs_CostId",
                table: "Charges",
                column: "CostId",
                principalTable: "Costs",
                principalColumn: "CostId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Costs_CostId",
                table: "Charges");

            migrationBuilder.DropIndex(
                name: "IX_Charges_CostId",
                table: "Charges");

            migrationBuilder.RenameColumn(
                name: "CostId",
                table: "Charges",
                newName: "RuleId");
        }
    }
}
