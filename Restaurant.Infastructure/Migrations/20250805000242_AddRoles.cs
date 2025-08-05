using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Insert roles into AspNetRoles table
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[,]
                {
                    { Guid.NewGuid().ToString(), "Admin", "ADMIN", Guid.NewGuid().ToString() },
                    { Guid.NewGuid().ToString(), "Owner", "OWNER", Guid.NewGuid().ToString() },
                    { Guid.NewGuid().ToString(), "User", "USER", Guid.NewGuid().ToString() }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove roles by NormalizedName
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "NormalizedName",
                keyValues: new object[] { "ADMIN", "OWNER", "USER" });
        }
    }
}
