using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddResturantpropertyInDishClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restaurantes_RestaurantId",
                table: "Dishes");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Restaurantes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Dishes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restaurantes_RestaurantId",
                table: "Dishes",
                column: "RestaurantId",
                principalTable: "Restaurantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Restaurantes_RestaurantId",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Restaurantes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Dishes");

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Restaurantes_RestaurantId",
                table: "Dishes",
                column: "RestaurantId",
                principalTable: "Restaurantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
