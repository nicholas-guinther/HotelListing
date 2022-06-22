using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.API.Migrations
{
    public partial class SeedingDefaultUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38ed1c32-c6e0-472b-b79c-f3cc9265b745");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f97f653f-a4b2-436e-a4f1-f44cdc167c48");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "85a37c08-41f8-40e0-888f-43882508891a", "Administrator", "ADMINISTRATOR" },
                    { "2", "1341ee91-ce5a-4cf8-83bc-b5adf9c56c8a", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, "dbcbf046-a7a2-4f8f-85cc-01496e9df6c4", "admin@email.com", false, "Admin", "User", false, null, "ADMIN@EMAIL.COM", "ADMIN@EMAIL.COM", "AQAAAAEAACcQAAAAEB7ws+8+MGPvuIZhbNw9hCAowyzvJsB3Seml2GVplkpqewrbLOu4rtOcUNnB4i/fqg==", null, false, "11771f83-ed6b-492b-aa89-0175d3396429", false, "admin@email.com" },
                    { "2", 0, "cc8c771b-aa74-4489-b4b4-b50e572686ed", "standard@email.com", false, "Standard", "User", false, null, "STANDARD@EMAIL.COM", "STANDARD@EMAIL.COM", "AQAAAAEAACcQAAAAEHScEdZXhZVXOOA3CfKIB4rO6fE5m+sJblnbI7gXLFV9Eu5rR/aNZho6CxhMJRDRvw==", null, false, "d94c03b5-9561-4ff0-b6f7-14aa9b33b68b", false, "standard@email.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38ed1c32-c6e0-472b-b79c-f3cc9265b745", "1b4d843a-a7d5-4e57-ac3c-d96ab4d9e68d", "Administrator", "ADMINISTRATOR" },
                    { "f97f653f-a4b2-436e-a4f1-f44cdc167c48", "65ee6698-2c47-48f6-b889-8d6240433d9a", "User", "USER" }
                });
        }
    }
}
