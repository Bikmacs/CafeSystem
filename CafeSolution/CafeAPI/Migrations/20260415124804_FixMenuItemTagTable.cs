using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixMenuItemTagTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "MenuItemTag",
                columns: table => new
                {
                    MenuItemsMenuItemId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItemTag", x => new { x.MenuItemsMenuItemId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_MenuItemTag_MenuItems_MenuItemsMenuItemId",
                        column: x => x.MenuItemsMenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MenuItemTag_Tag_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tag",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 1,
                column: "Description",
                value: "Классический борщ с мясом, свеклой и сметаной");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 25, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 23, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 1, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 19, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 24, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 5, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 1, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 24, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 14, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 12, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 25, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 7, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 15, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 4, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 4, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 4, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 1, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 7, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 25, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 18, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 19, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 23, 18, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 1, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 31,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 19, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 32,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 13, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 33,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 5, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 34,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 1, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 35,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 12, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 36,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 13, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 37,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 38,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 5, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 39,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 4, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 40,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 13, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 41,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 42,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 12, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 43,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 15, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 44,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 14, 18, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 45,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 6, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 46,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 47,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 24, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 48,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 49,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 50,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 51,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 52,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 53,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 23, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 54,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 13, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 55,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 7, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 56,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 7, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 57,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 58,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 15, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 59,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 60,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 61,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 23, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 62,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 6, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 63,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 24, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 64,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 24, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 65,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 66,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 4, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 67,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 13, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 68,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 6, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 69,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 7, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 70,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 9, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 71,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 18, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 72,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 13, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 73,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 6, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 74,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 5, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 75,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 18, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 76,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 13, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 77,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 78,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 79,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 80,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 18, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 81,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 22, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 82,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 18, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 83,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 20, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 84,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 2, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 85,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 5, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 86,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 1, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 87,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 24, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 88,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 16, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 89,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 14, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 90,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 4, 15, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 91,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 10, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 92,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 17, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 93,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 14, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 94,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 95,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 96,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 23, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 97,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 3, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 98,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 8, 15, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 99,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 21, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 100,
                column: "CreatedAt",
                value: new DateTime(2026, 4, 11, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.InsertData(
                table: "Tag",
                columns: new[] { "TagId", "TagName" },
                values: new object[,]
                {
                    { 1, "Острое" },
                    { 2, "Вегетарианское" },
                    { 3, "Мясо" },
                    { 4, "Новинка" },
                    { 5, "Постное" }
                });

            migrationBuilder.InsertData(
                table: "MenuItemTag",
                columns: new[] { "MenuItemsMenuItemId", "TagsTagId" },
                values: new object[,]
                {
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 2 },
                    { 4, 3 },
                    { 5, 2 },
                    { 6, 2 },
                    { 6, 5 },
                    { 7, 2 },
                    { 8, 3 },
                    { 9, 2 },
                    { 10, 3 },
                    { 10, 4 },
                    { 11, 3 },
                    { 12, 3 },
                    { 13, 2 },
                    { 14, 1 },
                    { 14, 3 },
                    { 15, 2 },
                    { 16, 3 },
                    { 17, 2 },
                    { 18, 4 },
                    { 19, 2 },
                    { 20, 2 },
                    { 21, 2 },
                    { 25, 2 },
                    { 25, 5 },
                    { 26, 2 },
                    { 26, 5 },
                    { 27, 2 },
                    { 27, 5 },
                    { 28, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemTag_TagsTagId",
                table: "MenuItemTag",
                column: "TagsTagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuItemTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.UpdateData(
                table: "MenuItems",
                keyColumn: "MenuItemId",
                keyValue: 1,
                column: "Description",
                value: "Классический украинский борщ с мясом, свеклой и сметаной");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 23, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 1, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 19, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 15, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 24, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 1, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 24, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 12, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 16,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 17,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 22, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 18,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 15, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 19,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 20,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 21,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 22,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 23,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 1, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 24,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 25,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 25, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 26,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 18, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 27,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 28,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 19, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 29,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 23, 18, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 30,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 1, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 31,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 19, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 32,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 33,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 34,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 1, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 35,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 12, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 36,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 37,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 38,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 39,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 40,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 41,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 42,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 12, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 43,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 15, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 44,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 18, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 45,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 6, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 46,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 22, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 47,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 24, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 48,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 49,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 50,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 51,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 52,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 53,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 23, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 54,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 55,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 56,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 57,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 58,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 15, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 59,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 60,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 9, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 61,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 23, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 62,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 6, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 63,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 24, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 64,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 24, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 65,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 22, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 66,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 67,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 68,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 6, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 69,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 7, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 70,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 9, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 71,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 72,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 8, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 73,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 6, 13, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 74,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 17, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 75,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 18, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 76,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 13, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 77,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 78,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 79,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 80,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 81,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 22, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 82,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 18, 10, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 83,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 20, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 84,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 2, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 85,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 5, 16, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 86,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 1, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 87,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 24, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 88,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 16, 12, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 89,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 90,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 4, 15, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 91,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 10, 9, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 92,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 17, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 93,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 14, 19, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 94,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 11, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 95,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 96,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 23, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 97,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 3, 20, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 98,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 8, 15, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 99,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 21, 14, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 100,
                column: "CreatedAt",
                value: new DateTime(2026, 3, 11, 10, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
