using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Catalog.Persistence.Database.Migrations
{
    /// <inheritdoc />
    public partial class InicialCarritoProductosDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Catalog",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "Catalog",
                columns: table => new
                {
                    ProductInStockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ProductInStockId);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalSchema: "Catalog",
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Products",
                columns: new[] { "ProductId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, " Description for product 1", "Product1", 559m },
                    { 2, " Description for product 2", "Product2", 934m },
                    { 3, " Description for product 3", "Product3", 846m },
                    { 4, " Description for product 4", "Product4", 587m },
                    { 5, " Description for product 5", "Product5", 718m },
                    { 6, " Description for product 6", "Product6", 985m },
                    { 7, " Description for product 7", "Product7", 785m },
                    { 8, " Description for product 8", "Product8", 242m },
                    { 9, " Description for product 9", "Product9", 650m },
                    { 10, " Description for product 10", "Product10", 979m },
                    { 11, " Description for product 11", "Product11", 191m },
                    { 12, " Description for product 12", "Product12", 973m },
                    { 13, " Description for product 13", "Product13", 241m },
                    { 14, " Description for product 14", "Product14", 956m },
                    { 15, " Description for product 15", "Product15", 594m },
                    { 16, " Description for product 16", "Product16", 494m },
                    { 17, " Description for product 17", "Product17", 354m },
                    { 18, " Description for product 18", "Product18", 508m },
                    { 19, " Description for product 19", "Product19", 190m },
                    { 20, " Description for product 20", "Product20", 176m },
                    { 21, " Description for product 21", "Product21", 239m },
                    { 22, " Description for product 22", "Product22", 901m },
                    { 23, " Description for product 23", "Product23", 721m },
                    { 24, " Description for product 24", "Product24", 731m },
                    { 25, " Description for product 25", "Product25", 351m },
                    { 26, " Description for product 26", "Product26", 233m },
                    { 27, " Description for product 27", "Product27", 545m },
                    { 28, " Description for product 28", "Product28", 60m },
                    { 29, " Description for product 29", "Product29", 268m },
                    { 30, " Description for product 30", "Product30", 541m },
                    { 31, " Description for product 31", "Product31", 731m },
                    { 32, " Description for product 32", "Product32", 121m },
                    { 33, " Description for product 33", "Product33", 239m },
                    { 34, " Description for product 34", "Product34", 324m },
                    { 35, " Description for product 35", "Product35", 583m },
                    { 36, " Description for product 36", "Product36", 520m },
                    { 37, " Description for product 37", "Product37", 816m },
                    { 38, " Description for product 38", "Product38", 418m },
                    { 39, " Description for product 39", "Product39", 550m },
                    { 40, " Description for product 40", "Product40", 777m },
                    { 41, " Description for product 41", "Product41", 514m },
                    { 42, " Description for product 42", "Product42", 478m },
                    { 43, " Description for product 43", "Product43", 107m },
                    { 44, " Description for product 44", "Product44", 605m },
                    { 45, " Description for product 45", "Product45", 354m },
                    { 46, " Description for product 46", "Product46", 703m },
                    { 47, " Description for product 47", "Product47", 166m },
                    { 48, " Description for product 48", "Product48", 840m },
                    { 49, " Description for product 49", "Product49", 372m },
                    { 50, " Description for product 50", "Product50", 517m },
                    { 51, " Description for product 51", "Product51", 339m },
                    { 52, " Description for product 52", "Product52", 782m },
                    { 53, " Description for product 53", "Product53", 110m },
                    { 54, " Description for product 54", "Product54", 928m },
                    { 55, " Description for product 55", "Product55", 610m },
                    { 56, " Description for product 56", "Product56", 667m },
                    { 57, " Description for product 57", "Product57", 483m },
                    { 58, " Description for product 58", "Product58", 644m },
                    { 59, " Description for product 59", "Product59", 65m },
                    { 60, " Description for product 60", "Product60", 614m },
                    { 61, " Description for product 61", "Product61", 580m },
                    { 62, " Description for product 62", "Product62", 91m },
                    { 63, " Description for product 63", "Product63", 187m },
                    { 64, " Description for product 64", "Product64", 350m },
                    { 65, " Description for product 65", "Product65", 177m },
                    { 66, " Description for product 66", "Product66", 526m },
                    { 67, " Description for product 67", "Product67", 677m },
                    { 68, " Description for product 68", "Product68", 972m },
                    { 69, " Description for product 69", "Product69", 107m },
                    { 70, " Description for product 70", "Product70", 789m },
                    { 71, " Description for product 71", "Product71", 271m },
                    { 72, " Description for product 72", "Product72", 636m },
                    { 73, " Description for product 73", "Product73", 140m },
                    { 74, " Description for product 74", "Product74", 397m },
                    { 75, " Description for product 75", "Product75", 233m },
                    { 76, " Description for product 76", "Product76", 837m },
                    { 77, " Description for product 77", "Product77", 680m },
                    { 78, " Description for product 78", "Product78", 568m },
                    { 79, " Description for product 79", "Product79", 657m },
                    { 80, " Description for product 80", "Product80", 246m },
                    { 81, " Description for product 81", "Product81", 966m },
                    { 82, " Description for product 82", "Product82", 581m },
                    { 83, " Description for product 83", "Product83", 171m },
                    { 84, " Description for product 84", "Product84", 321m },
                    { 85, " Description for product 85", "Product85", 718m },
                    { 86, " Description for product 86", "Product86", 472m },
                    { 87, " Description for product 87", "Product87", 394m },
                    { 88, " Description for product 88", "Product88", 390m },
                    { 89, " Description for product 89", "Product89", 800m },
                    { 90, " Description for product 90", "Product90", 495m },
                    { 91, " Description for product 91", "Product91", 194m },
                    { 92, " Description for product 92", "Product92", 117m },
                    { 93, " Description for product 93", "Product93", 490m },
                    { 94, " Description for product 94", "Product94", 498m },
                    { 95, " Description for product 95", "Product95", 744m },
                    { 96, " Description for product 96", "Product96", 163m },
                    { 97, " Description for product 97", "Product97", 530m },
                    { 98, " Description for product 98", "Product98", 602m },
                    { 99, " Description for product 99", "Product99", 993m },
                    { 100, " Description for product 100", "Product100", 191m }
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Stocks",
                columns: new[] { "ProductInStockId", "ProductId", "Stock" },
                values: new object[,]
                {
                    { 1, 1, 40 },
                    { 2, 2, 13 },
                    { 3, 3, 21 },
                    { 4, 4, 15 },
                    { 5, 5, 27 },
                    { 6, 6, 46 },
                    { 7, 7, 12 },
                    { 8, 8, 29 },
                    { 9, 9, 46 },
                    { 10, 10, 7 },
                    { 11, 11, 32 },
                    { 12, 12, 2 },
                    { 13, 13, 9 },
                    { 14, 14, 49 },
                    { 15, 15, 7 },
                    { 16, 16, 9 },
                    { 17, 17, 17 },
                    { 18, 18, 44 },
                    { 19, 19, 16 },
                    { 20, 20, 35 },
                    { 21, 21, 48 },
                    { 22, 22, 21 },
                    { 23, 23, 45 },
                    { 24, 24, 14 },
                    { 25, 25, 26 },
                    { 26, 26, 43 },
                    { 27, 27, 19 },
                    { 28, 28, 24 },
                    { 29, 29, 19 },
                    { 30, 30, 39 },
                    { 31, 31, 45 },
                    { 32, 32, 0 },
                    { 33, 33, 44 },
                    { 34, 34, 7 },
                    { 35, 35, 37 },
                    { 36, 36, 17 },
                    { 37, 37, 48 },
                    { 38, 38, 23 },
                    { 39, 39, 38 },
                    { 40, 40, 12 },
                    { 41, 41, 45 },
                    { 42, 42, 10 },
                    { 43, 43, 38 },
                    { 44, 44, 10 },
                    { 45, 45, 10 },
                    { 46, 46, 24 },
                    { 47, 47, 3 },
                    { 48, 48, 9 },
                    { 49, 49, 29 },
                    { 50, 50, 0 },
                    { 51, 51, 45 },
                    { 52, 52, 35 },
                    { 53, 53, 24 },
                    { 54, 54, 46 },
                    { 55, 55, 27 },
                    { 56, 56, 45 },
                    { 57, 57, 2 },
                    { 58, 58, 39 },
                    { 59, 59, 24 },
                    { 60, 60, 14 },
                    { 61, 61, 8 },
                    { 62, 62, 19 },
                    { 63, 63, 10 },
                    { 64, 64, 40 },
                    { 65, 65, 7 },
                    { 66, 66, 42 },
                    { 67, 67, 44 },
                    { 68, 68, 1 },
                    { 69, 69, 25 },
                    { 70, 70, 10 },
                    { 71, 71, 21 },
                    { 72, 72, 44 },
                    { 73, 73, 31 },
                    { 74, 74, 2 },
                    { 75, 75, 12 },
                    { 76, 76, 17 },
                    { 77, 77, 38 },
                    { 78, 78, 31 },
                    { 79, 79, 17 },
                    { 80, 80, 45 },
                    { 81, 81, 32 },
                    { 82, 82, 14 },
                    { 83, 83, 22 },
                    { 84, 84, 46 },
                    { 85, 85, 25 },
                    { 86, 86, 32 },
                    { 87, 87, 17 },
                    { 88, 88, 38 },
                    { 89, 89, 30 },
                    { 90, 90, 33 },
                    { 91, 91, 29 },
                    { 92, 92, 11 },
                    { 93, 93, 28 },
                    { 94, 94, 18 },
                    { 95, 95, 29 },
                    { 96, 96, 9 },
                    { 97, 97, 33 },
                    { 98, 98, 22 },
                    { 99, 99, 14 },
                    { 100, 100, 41 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                schema: "Catalog",
                table: "Products",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                schema: "Catalog",
                table: "Stocks",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "Catalog");
        }
    }
}
