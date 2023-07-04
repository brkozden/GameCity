﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddProductPriceForBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "Baskets",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "Baskets");
        }
    }
}
