using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DddEf.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DddEfMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tm_Customer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedDateOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedDateOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    VersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tm_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tm_Item",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedDateOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedDateOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    VersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tm_Item", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tx_SalesOrder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShipAddress_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ShipAddress_Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BillAddress_City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BillAddress_Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    CreatedDateOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ModifiedDateOffset = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    VersionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tx_SalesOrder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tx_SalesOrder_Item",
                columns: table => new
                {
                    DetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<double>(type: "float", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: true),
                    LineStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tx_SalesOrder_Item", x => x.DetId);
                    table.ForeignKey(
                        name: "FK_Tx_SalesOrder_Item_Tx_SalesOrder_Id",
                        column: x => x.Id,
                        principalTable: "Tx_SalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tx_SalesOrder_ItemSecond",
                columns: table => new
                {
                    DetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Qty = table.Column<double>(type: "float", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Total = table.Column<double>(type: "float", nullable: true),
                    LineStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tx_SalesOrder_ItemSecond", x => x.DetId);
                    table.ForeignKey(
                        name: "FK_Tx_SalesOrder_ItemSecond_Tx_SalesOrder_Id",
                        column: x => x.Id,
                        principalTable: "Tx_SalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tx_SalesOrder_Item_Bin",
                columns: table => new
                {
                    DetDetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    BinName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tx_SalesOrder_Item_Bin", x => x.DetDetId);
                    table.ForeignKey(
                        name: "FK_Tx_SalesOrder_Item_Bin_Tx_SalesOrder_Item_DetId",
                        column: x => x.DetId,
                        principalTable: "Tx_SalesOrder_Item",
                        principalColumn: "DetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tx_SalesOrder_ItemSecond_Bin",
                columns: table => new
                {
                    DetDetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RowNumber = table.Column<int>(type: "int", nullable: false),
                    BinName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tx_SalesOrder_ItemSecond_Bin", x => x.DetDetId);
                    table.ForeignKey(
                        name: "FK_Tx_SalesOrder_ItemSecond_Bin_Tx_SalesOrder_ItemSecond_DetId",
                        column: x => x.DetId,
                        principalTable: "Tx_SalesOrder_ItemSecond",
                        principalColumn: "DetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tx_SalesOrder_Item_Id_RowNumber",
                table: "Tx_SalesOrder_Item",
                columns: new[] { "Id", "RowNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tx_SalesOrder_Item_Bin_DetId_RowNumber",
                table: "Tx_SalesOrder_Item_Bin",
                columns: new[] { "DetId", "RowNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tx_SalesOrder_ItemSecond_Id_RowNumber",
                table: "Tx_SalesOrder_ItemSecond",
                columns: new[] { "Id", "RowNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tx_SalesOrder_ItemSecond_Bin_DetId_RowNumber",
                table: "Tx_SalesOrder_ItemSecond_Bin",
                columns: new[] { "DetId", "RowNumber" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tm_Customer");

            migrationBuilder.DropTable(
                name: "Tm_Item");

            migrationBuilder.DropTable(
                name: "Tx_SalesOrder_Item_Bin");

            migrationBuilder.DropTable(
                name: "Tx_SalesOrder_ItemSecond_Bin");

            migrationBuilder.DropTable(
                name: "Tx_SalesOrder_Item");

            migrationBuilder.DropTable(
                name: "Tx_SalesOrder_ItemSecond");

            migrationBuilder.DropTable(
                name: "Tx_SalesOrder");
        }
    }
}
