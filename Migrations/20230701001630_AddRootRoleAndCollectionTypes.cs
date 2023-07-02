using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GamesStoreWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddRootRoleAndCollectionTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("de86b3db-8137-4e9e-b784-b45c3d5080b8"), "180dd38f-8105-477e-bbb9-061f41d9c369", "Root", "ROOT" });

            migrationBuilder.InsertData(
                table: "CollectionTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("3650af9b-3872-47fe-ad5d-111aaa194f6c"), "Wish list" },
                    { new Guid("3c3b723c-f749-4162-a73f-03cbe163944d"), "Shopping cart" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("de86b3db-8137-4e9e-b784-b45c3d5080b8"));

            migrationBuilder.DeleteData(
                table: "CollectionTypes",
                keyColumn: "Id",
                keyValue: new Guid("3650af9b-3872-47fe-ad5d-111aaa194f6c"));

            migrationBuilder.DeleteData(
                table: "CollectionTypes",
                keyColumn: "Id",
                keyValue: new Guid("3c3b723c-f749-4162-a73f-03cbe163944d"));
        }
    }
}
