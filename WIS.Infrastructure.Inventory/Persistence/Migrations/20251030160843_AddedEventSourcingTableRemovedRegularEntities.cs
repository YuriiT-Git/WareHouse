using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WIS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedEventSourcingTableRemovedRegularEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryStocks");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropIndex(
                name: "IX_InventoryAuditLog_Code",
                table: "InventoryAuditLog");

            migrationBuilder.CreateTable(
                name: "EventsStorage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EventType = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Data = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsStorage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAuditLog_Code",
                table: "InventoryAuditLog",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsStorage");

            migrationBuilder.DropIndex(
                name: "IX_InventoryAuditLog_Code",
                table: "InventoryAuditLog");

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Brand = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Model = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ProductType = table.Column<int>(type: "integer", nullable: false),
                    Size = table.Column<int>(type: "integer", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InventoryStocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InventoryItemId = table.Column<int>(type: "integer", nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryStocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryStocks_InventoryItems_InventoryItemId",
                        column: x => x.InventoryItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAuditLog_Code",
                table: "InventoryAuditLog",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_Code",
                table: "InventoryItems",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryStocks_InventoryItemId",
                table: "InventoryStocks",
                column: "InventoryItemId",
                unique: true);
        }
    }
}
