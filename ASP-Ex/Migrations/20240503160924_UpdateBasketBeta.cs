using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Ex.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBasketBeta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Baskets",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Baskets");
        }
    }
}
