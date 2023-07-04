using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddGeneralCategoryColumnForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeneralCategoryId",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneralCategoryId",
                table: "Products");
        }
    }
}
