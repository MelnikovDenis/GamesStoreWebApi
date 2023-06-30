using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GamesStoreWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7d85b102-0f69-450e-93e8-28192d10aafe"), "1748c6d1-e1f6-4566-a4f9-269334ac65f3", "Administrator", "ADMINISTRATOR" },
                    { new Guid("cfdedf85-d8fc-4286-a9e8-d77e1a7dae1c"), "235384a7-be0b-4094-b34c-0b0e9cb15fd3", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7d85b102-0f69-450e-93e8-28192d10aafe"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cfdedf85-d8fc-4286-a9e8-d77e1a7dae1c"));
        }
    }
}
