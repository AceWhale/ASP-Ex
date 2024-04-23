using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Ex.Migrations
{
    /// <inheritdoc />
    public partial class SlugCategory : Migration
    {
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Slug",
				table: "Categories",
				type: "nvarchar(450)",
				nullable: true);

			migrationBuilder.CreateIndex(
				name: "IX_Categories_Slug",
				table: "Categories",
				column: "Slug",
				unique: true,
				filter: "[Slug] IS NOT NULL");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
				name: "IX_Categories_Slug",
				table: "Categories");

			migrationBuilder.DropColumn(
				name: "Slug",
				table: "Categories");
		}
	}
}
