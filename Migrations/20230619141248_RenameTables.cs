using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesStoreWebApi.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collection_AspNetUsers_CollectionerId",
                table: "Collection");

            migrationBuilder.DropForeignKey(
                name: "FK_Collection_CollectionType_TypeId",
                table: "Collection");

            migrationBuilder.DropForeignKey(
                name: "FK_Collection_Games_CollectionedGameId",
                table: "Collection");

            migrationBuilder.DropForeignKey(
                name: "FK_Discount_Games_DiscountedGameId",
                table: "Discount");

            migrationBuilder.DropForeignKey(
                name: "FK_Key_Games_GameId",
                table: "Key");

            migrationBuilder.DropForeignKey(
                name: "FK_Key_Purchase_PurchaseId",
                table: "Key");

            migrationBuilder.DropForeignKey(
                name: "FK_Price_Games_PricedGameId",
                table: "Price");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchase_AspNetUsers_PurchaserId",
                table: "Purchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchase",
                table: "Purchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Price",
                table: "Price");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Key",
                table: "Key");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discount",
                table: "Discount");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CollectionType_Type",
                table: "CollectionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollectionType",
                table: "CollectionType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collection",
                table: "Collection");

            migrationBuilder.RenameTable(
                name: "Purchase",
                newName: "Purchases");

            migrationBuilder.RenameTable(
                name: "Price",
                newName: "Prices");

            migrationBuilder.RenameTable(
                name: "Key",
                newName: "Keys");

            migrationBuilder.RenameTable(
                name: "Discount",
                newName: "Discounts");

            migrationBuilder.RenameTable(
                name: "CollectionType",
                newName: "CollectionTypes");

            migrationBuilder.RenameTable(
                name: "Collection",
                newName: "Collections");

            migrationBuilder.RenameIndex(
                name: "IX_Purchase_PurchaserId",
                table: "Purchases",
                newName: "IX_Purchases_PurchaserId");

            migrationBuilder.RenameIndex(
                name: "IX_Key_PurchaseId",
                table: "Keys",
                newName: "IX_Keys_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Key_GameId",
                table: "Keys",
                newName: "IX_Keys_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Collection_TypeId",
                table: "Collections",
                newName: "IX_Collections_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Collection_CollectionerId",
                table: "Collections",
                newName: "IX_Collections_CollectionerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Prices",
                table: "Prices",
                columns: new[] { "PricedGameId", "StartDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Keys",
                table: "Keys",
                column: "KeyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts",
                columns: new[] { "DiscountedGameId", "StartDate" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CollectionTypes_Type",
                table: "CollectionTypes",
                column: "Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollectionTypes",
                table: "CollectionTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                columns: new[] { "CollectionedGameId", "CollectionerId", "TypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AspNetUsers_CollectionerId",
                table: "Collections",
                column: "CollectionerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_CollectionTypes_TypeId",
                table: "Collections",
                column: "TypeId",
                principalTable: "CollectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Games_CollectionedGameId",
                table: "Collections",
                column: "CollectionedGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Games_DiscountedGameId",
                table: "Discounts",
                column: "DiscountedGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Keys_Games_GameId",
                table: "Keys",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Keys_Purchases_PurchaseId",
                table: "Keys",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Prices_Games_PricedGameId",
                table: "Prices",
                column: "PricedGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_AspNetUsers_PurchaserId",
                table: "Purchases",
                column: "PurchaserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AspNetUsers_CollectionerId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Collections_CollectionTypes_TypeId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Games_CollectionedGameId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Games_DiscountedGameId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Keys_Games_GameId",
                table: "Keys");

            migrationBuilder.DropForeignKey(
                name: "FK_Keys_Purchases_PurchaseId",
                table: "Keys");

            migrationBuilder.DropForeignKey(
                name: "FK_Prices_Games_PricedGameId",
                table: "Prices");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_AspNetUsers_PurchaserId",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Prices",
                table: "Prices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Keys",
                table: "Keys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Discounts",
                table: "Discounts");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CollectionTypes_Type",
                table: "CollectionTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollectionTypes",
                table: "CollectionTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "Purchase");

            migrationBuilder.RenameTable(
                name: "Prices",
                newName: "Price");

            migrationBuilder.RenameTable(
                name: "Keys",
                newName: "Key");

            migrationBuilder.RenameTable(
                name: "Discounts",
                newName: "Discount");

            migrationBuilder.RenameTable(
                name: "CollectionTypes",
                newName: "CollectionType");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "Collection");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_PurchaserId",
                table: "Purchase",
                newName: "IX_Purchase_PurchaserId");

            migrationBuilder.RenameIndex(
                name: "IX_Keys_PurchaseId",
                table: "Key",
                newName: "IX_Key_PurchaseId");

            migrationBuilder.RenameIndex(
                name: "IX_Keys_GameId",
                table: "Key",
                newName: "IX_Key_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_TypeId",
                table: "Collection",
                newName: "IX_Collection_TypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_CollectionerId",
                table: "Collection",
                newName: "IX_Collection_CollectionerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchase",
                table: "Purchase",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Price",
                table: "Price",
                columns: new[] { "PricedGameId", "StartDate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Key",
                table: "Key",
                column: "KeyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Discount",
                table: "Discount",
                columns: new[] { "DiscountedGameId", "StartDate" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CollectionType_Type",
                table: "CollectionType",
                column: "Type");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollectionType",
                table: "CollectionType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collection",
                table: "Collection",
                columns: new[] { "CollectionedGameId", "CollectionerId", "TypeId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_AspNetUsers_CollectionerId",
                table: "Collection",
                column: "CollectionerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_CollectionType_TypeId",
                table: "Collection",
                column: "TypeId",
                principalTable: "CollectionType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_Games_CollectionedGameId",
                table: "Collection",
                column: "CollectionedGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Discount_Games_DiscountedGameId",
                table: "Discount",
                column: "DiscountedGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Key_Games_GameId",
                table: "Key",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Key_Purchase_PurchaseId",
                table: "Key",
                column: "PurchaseId",
                principalTable: "Purchase",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Price_Games_PricedGameId",
                table: "Price",
                column: "PricedGameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchase_AspNetUsers_PurchaserId",
                table: "Purchase",
                column: "PurchaserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
