using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaseStudyAppServer.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33c248dc-fbea-465d-a89b-13c71ca9a5b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8eb62b2d-5d76-48dd-9872-6282d1697a6a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d59d673-7017-4249-b4f2-0882f3b7d0cc", null, "Admin", "ADMIN" },
                    { "4321fc22-9962-4363-a030-a3a45413084b", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d59d673-7017-4249-b4f2-0882f3b7d0cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4321fc22-9962-4363-a030-a3a45413084b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33c248dc-fbea-465d-a89b-13c71ca9a5b2", null, "Admin", "ADMIN" },
                    { "8eb62b2d-5d76-48dd-9872-6282d1697a6a", null, "User", "USER" }
                });
        }
    }
}
