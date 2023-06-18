using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesStoreWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserAndCollectionCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    CollectionedGameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollectionerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => new { x.CollectionedGameId, x.CollectionerId, x.TypeId });
                    table.ForeignKey(
                        name: "FK_Collection_AspNetUsers_CollectionerId",
                        column: x => x.CollectionerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collection_CollectionType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "CollectionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collection_Games_CollectionedGameId",
                        column: x => x.CollectionedGameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collection_CollectionerId",
                table: "Collection",
                column: "CollectionerId");

            migrationBuilder.CreateIndex(
                name: "IX_Collection_TypeId",
                table: "Collection",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collection");
        }
    }
}
