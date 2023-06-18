using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesStoreWebApi.Migrations
{
    /// <inheritdoc />
    public partial class KeyCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Key",
                columns: table => new
                {
                    KeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Key", x => x.KeyId);
                    table.ForeignKey(
                        name: "FK_Key_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Key_Purchase_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchase",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Key_GameId",
                table: "Key",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Key_PurchaseId",
                table: "Key",
                column: "PurchaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Key");
        }
    }
}
