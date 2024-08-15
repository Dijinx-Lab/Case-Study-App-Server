using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CaseStudyAppServer.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionAndTiming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d5b1e18-7a73-4032-b4a8-fb69a2b80c81");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ffbed221-e280-4259-9023-346418138b54");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4c646bd4-2619-4586-bc58-4d9a54354b13", null, "Admin", "ADMIN" },
                    { "64dee89b-bfa1-4968-ac4d-cc5c3110593c", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4c646bd4-2619-4586-bc58-4d9a54354b13");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "64dee89b-bfa1-4968-ac4d-cc5c3110593c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d5b1e18-7a73-4032-b4a8-fb69a2b80c81", null, "Admin", "ADMIN" },
                    { "ffbed221-e280-4259-9023-346418138b54", null, "User", "USER" }
                });
        }
    }
}
