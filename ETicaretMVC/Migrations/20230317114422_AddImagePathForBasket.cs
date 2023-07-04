using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathForBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductImagePath",
                table: "Baskets",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImagePath",
                table: "Baskets");
        }
    }
}
