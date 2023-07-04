﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddProductNameForBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Baskets");
        }
    }
}
