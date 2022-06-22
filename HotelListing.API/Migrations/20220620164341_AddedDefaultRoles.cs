using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.API.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38ed1c32-c6e0-472b-b79c-f3cc9265b745", "1b4d843a-a7d5-4e57-ac3c-d96ab4d9e68d", "Administrator", "ADMINISTRATOR" },
                    { "f97f653f-a4b2-436e-a4f1-f44cdc167c48", "65ee6698-2c47-48f6-b889-8d6240433d9a", "User", "USER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38ed1c32-c6e0-472b-b79c-f3cc9265b745");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f97f653f-a4b2-436e-a4f1-f44cdc167c48");
        }
    }
}
