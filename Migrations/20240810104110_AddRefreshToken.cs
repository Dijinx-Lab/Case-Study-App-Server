using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaseStudyAppServer.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "585eaeae-8d42-4e74-a327-623b5f85e98d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "de3827cf-ffba-4d79-bfa6-e77ec9451cfa");

            migrationBuilder.AddColumn<string>(
                name: "RefreshValue",
                table: "Tokens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33c248dc-fbea-465d-a89b-13c71ca9a5b2", null, "Admin", "ADMIN" },
                    { "8eb62b2d-5d76-48dd-9872-6282d1697a6a", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33c248dc-fbea-465d-a89b-13c71ca9a5b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8eb62b2d-5d76-48dd-9872-6282d1697a6a");

            migrationBuilder.DropColumn(
                name: "RefreshValue",
                table: "Tokens");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "585eaeae-8d42-4e74-a327-623b5f85e98d", null, "User", "USER" },
                    { "de3827cf-ffba-4d79-bfa6-e77ec9451cfa", null, "Admin", "ADMIN" }
                });
        }
    }
}
