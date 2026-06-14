using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAfterModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Indigriend",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indigriend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    MenuItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.MenuItemId);
                    table.ForeignKey(
                        name: "FK_MenuItems_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Login = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DishItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishItems_Indigriend_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Indigriend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TableNumber = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MenuItemId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_MenuItems_MenuItemId",
                        column: x => x.MenuItemId,
                        principalTable: "MenuItems",
                        principalColumn: "MenuItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Супы" },
                    { 2, "Салаты" },
                    { 3, "Закуски" },
                    { 4, "Основные блюда" },
                    { 5, "Пицца" },
                    { 6, "Паста" },
                    { 7, "Десерты" },
                    { 8, "Напитки" },
                    { 9, "Гарниры" },
                    { 10, "Соусы" }
                });

            migrationBuilder.InsertData(
                table: "Indigriend",
                columns: new[] { "Id", "Name", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "Говядина", 0m },
                    { 2, "Свинина", 0m },
                    { 3, "Курица", 0m },
                    { 4, "Индейка", 0m },
                    { 5, "Фарш", 0m },
                    { 6, "Молоко", 0m },
                    { 7, "Сметана", 0m },
                    { 8, "Творог", 0m },
                    { 9, "Сыр", 0m },
                    { 10, "СливочноеМасло", 0m },
                    { 11, "Мука", 0m },
                    { 12, "Сахар", 0m },
                    { 13, "Соль", 0m },
                    { 14, "ПодсолнечноеМасло", 0m },
                    { 15, "Рис", 0m },
                    { 16, "Гречка", 0m },
                    { 17, "МакаронныеИзделия", 0m },
                    { 18, "Картофель", 0m },
                    { 19, "ЛукРепчатый", 0m },
                    { 20, "Морковь", 0m },
                    { 21, "Капуста", 0m },
                    { 22, "Свекла", 0m },
                    { 23, "ХлебПшеничный", 0m },
                    { 24, "ХлебРжаной", 0m },
                    { 25, "Батон", 0m }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "Waiter" },
                    { 3, "Cook" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "MenuItemId", "Available", "CategoryId", "Description", "Image", "Name", "Price" },
                values: new object[,]
                {
                    { 1, true, 1, "Классический украинский борщ с мясом, свеклой и сметаной", null, "Борщ", 450m },
                    { 2, true, 1, "Лёгкий куриный бульон с домашней лапшой", null, "Куриный суп с лапшой", 350m },
                    { 3, true, 1, "Нежный суп-пюре из лесных грибов с гренками", null, "Грибной крем-суп", 420m },
                    { 4, true, 2, "Салат с курицей, сыром пармезан и сухариками", null, "Цезарь с курицей", 480m },
                    { 5, true, 2, "Огурцы, помидоры, фета, оливки, красный лук", null, "Греческий салат", 450m },
                    { 6, true, 2, "Микс свежих овощей с оливковым маслом", null, "Витаминный салат", 400m },
                    { 7, true, 3, "Хрустящий хлеб с томатами, базиликом и оливковым маслом", null, "Брускетта с томатами", 350m },
                    { 8, true, 3, "Тонко нарезанная говядина с лимонным соусом", null, "Карпаччо из говядины", 700m },
                    { 9, true, 3, "Сыр моцарелла с томатами и базиликом", null, "Моцарелла с томатами", 400m },
                    { 10, true, 4, "Сочный говяжий стейк, прожарка по желанию", null, "Стейк рибай", 1500m },
                    { 11, true, 4, "Куриное филе на гриле с пряными травами", null, "Курица гриль", 950m },
                    { 12, true, 4, "Мясо свинины с соусом BBQ и овощами", null, "Свинина в соусе барбекю", 1050m },
                    { 13, true, 5, "Томатный соус, сыр моцарелла, базилик", null, "Маргарита", 650m },
                    { 14, true, 5, "Пицца с пепперони и сыром моцарелла", null, "Пепперони", 750m },
                    { 15, true, 5, "Моцарелла, горгонзола, пармезан, чеддер", null, "Четыре сыра", 850m },
                    { 16, true, 6, "Спагетти с мясным соусом и пармезаном", null, "Спагетти Болоньезе", 700m },
                    { 17, true, 6, "Паста с кремовым соусом и сыром", null, "Феттучини Альфредо", 650m },
                    { 18, true, 6, "Паста с креветками, кальмарами и чесночным соусом", null, "Паста с морепродуктами", 950m },
                    { 19, true, 7, "Классический итальянский десерт с маскарпоне и кофе", null, "Тирамису", 400m },
                    { 20, true, 7, "Тёплый шоколадный кекс с жидкой начинкой", null, "Шоколадный фондан", 420m },
                    { 21, true, 7, "Классический чизкейк с клубничным соусом", null, "Чизкейк Нью-Йорк", 450m },
                    { 22, true, 8, "Эспрессо с горячим молоком и пенкой", null, "Капучино", 250m },
                    { 23, true, 8, "Классический черный чай", null, "Чай черный", 150m },
                    { 24, true, 8, "Свежевжатый апельсиновый сок", null, "Сок апельсиновый", 200m },
                    { 25, true, 9, "Хрустящий картофель фри", null, "Картофель фри", 250m },
                    { 26, true, 9, "Отварной рис с овощами", null, "Рис с овощами", 220m },
                    { 27, true, 9, "Сезонные овощи на пару", null, "Овощи на пару", 230m },
                    { 28, true, 10, "Сладко-пряный соус для мяса", null, "Соус BBQ", 100m },
                    { 29, true, 10, "Соус из томатов для пиццы и пасты", null, "Томатный соус", 90m },
                    { 30, true, 10, "Нежный соус со сметаной и зеленью", null, "Сметанный соус", 80m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CreatedAt", "FullName", "Login", "PasswordHash", "RoleId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Admin", "admin", "$2a$12$.gijsKvNhylDhZfxAknuDesvmZnx13DhA2NVKk9LZH32YiRAQM8YW", 1 },
                    { 2, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Waiter", "waiter", "$2a$12$C2Ek4ejvfw.so/k2AezYpuflw5YAaQ4vmHqU0xq0Gmz85Z.I3bSyG", 2 },
                    { 3, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cook", "cook", "$2a$12$9iwlfcfL1S1uYa7BLe3xZO03pZ7mOTQzZDDGWOP7h/Xq4GaCSNBCm", 3 }
                });

            migrationBuilder.InsertData(
                table: "DishItems",
                columns: new[] { "Id", "Amount", "IngredientId", "MenuItemId", "UnitType" },
                values: new object[,]
                {
                    { 1, 150m, 1, 1, 4 },
                    { 2, 80m, 22, 1, 4 },
                    { 3, 50m, 21, 1, 4 },
                    { 4, 100m, 18, 1, 4 },
                    { 5, 30m, 7, 1, 4 },
                    { 6, 5m, 13, 1, 4 },
                    { 7, 150m, 3, 2, 4 },
                    { 8, 50m, 17, 2, 4 },
                    { 9, 40m, 20, 2, 4 },
                    { 10, 5m, 13, 2, 4 },
                    { 11, 150m, 18, 3, 4 },
                    { 12, 0.15m, 6, 3, 5 },
                    { 13, 20m, 10, 3, 4 },
                    { 14, 5m, 13, 3, 4 },
                    { 15, 120m, 3, 4, 4 },
                    { 16, 40m, 9, 4, 4 },
                    { 17, 100m, 21, 4, 4 },
                    { 18, 0.5m, 25, 4, 0 },
                    { 19, 80m, 9, 5, 4 },
                    { 20, 30m, 19, 5, 4 },
                    { 21, 0.03m, 14, 5, 5 },
                    { 22, 3m, 13, 5, 4 },
                    { 23, 150m, 21, 6, 4 },
                    { 24, 80m, 20, 6, 4 },
                    { 25, 0.02m, 14, 6, 5 },
                    { 26, 0.2m, 25, 7, 0 },
                    { 27, 30m, 9, 7, 4 },
                    { 28, 0.01m, 14, 7, 5 },
                    { 29, 150m, 1, 8, 4 },
                    { 30, 30m, 9, 8, 4 },
                    { 31, 0.02m, 14, 8, 5 },
                    { 32, 4m, 13, 8, 4 },
                    { 33, 120m, 9, 9, 4 },
                    { 34, 0.02m, 14, 9, 5 },
                    { 35, 300m, 1, 10, 4 },
                    { 36, 0.02m, 14, 10, 5 },
                    { 37, 6m, 13, 10, 4 },
                    { 38, 250m, 3, 11, 4 },
                    { 39, 0.02m, 14, 11, 5 },
                    { 40, 5m, 13, 11, 4 },
                    { 41, 250m, 2, 12, 4 },
                    { 42, 10m, 12, 12, 4 },
                    { 43, 5m, 13, 12, 4 },
                    { 44, 200m, 11, 13, 4 },
                    { 45, 150m, 9, 13, 4 },
                    { 46, 4m, 13, 13, 4 },
                    { 47, 0.02m, 14, 13, 5 },
                    { 48, 200m, 11, 14, 4 },
                    { 49, 100m, 9, 14, 4 },
                    { 50, 80m, 2, 14, 4 },
                    { 51, 5m, 13, 14, 4 },
                    { 52, 200m, 11, 15, 4 },
                    { 53, 250m, 9, 15, 4 },
                    { 54, 0.02m, 14, 15, 5 },
                    { 55, 150m, 17, 16, 4 },
                    { 56, 120m, 5, 16, 4 },
                    { 57, 30m, 9, 16, 4 },
                    { 58, 4m, 13, 16, 4 },
                    { 59, 150m, 17, 17, 4 },
                    { 60, 40m, 10, 17, 4 },
                    { 61, 60m, 9, 17, 4 },
                    { 62, 3m, 13, 17, 4 },
                    { 63, 150m, 17, 18, 4 },
                    { 64, 30m, 10, 18, 4 },
                    { 65, 30m, 9, 18, 4 },
                    { 66, 4m, 13, 18, 4 },
                    { 67, 120m, 8, 19, 4 },
                    { 68, 40m, 12, 19, 4 },
                    { 69, 0.05m, 6, 19, 5 },
                    { 70, 60m, 11, 20, 4 },
                    { 71, 50m, 10, 20, 4 },
                    { 72, 50m, 12, 20, 4 },
                    { 73, 200m, 8, 21, 4 },
                    { 74, 50m, 11, 21, 4 },
                    { 75, 60m, 12, 21, 4 },
                    { 76, 40m, 10, 21, 4 },
                    { 77, 0.2m, 6, 22, 5 },
                    { 78, 10m, 12, 22, 4 },
                    { 79, 15m, 12, 23, 4 },
                    { 80, 5m, 12, 24, 4 },
                    { 81, 250m, 18, 25, 4 },
                    { 82, 0.1m, 14, 25, 5 },
                    { 83, 5m, 13, 25, 4 },
                    { 84, 150m, 15, 26, 4 },
                    { 85, 50m, 20, 26, 4 },
                    { 86, 30m, 19, 26, 4 },
                    { 87, 4m, 13, 26, 4 },
                    { 88, 100m, 21, 27, 4 },
                    { 89, 60m, 20, 27, 4 },
                    { 90, 60m, 22, 27, 4 },
                    { 91, 3m, 13, 27, 4 },
                    { 92, 15m, 12, 28, 4 },
                    { 93, 3m, 13, 28, 4 },
                    { 94, 10m, 12, 29, 4 },
                    { 95, 4m, 13, 29, 4 },
                    { 96, 80m, 7, 30, 4 },
                    { 97, 3m, 13, 30, 4 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CreatedAt", "Status", "TableNumber", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 25, 20, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 30, 2 },
                    { 2, new DateTime(2026, 3, 23, 14, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 8, 2 },
                    { 3, new DateTime(2026, 3, 1, 21, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 7, 2 },
                    { 4, new DateTime(2026, 3, 19, 16, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 28, 2 },
                    { 5, new DateTime(2026, 3, 15, 9, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 37, 2 },
                    { 6, new DateTime(2026, 3, 24, 20, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 20, 2 },
                    { 7, new DateTime(2026, 3, 5, 13, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 7, 2 },
                    { 8, new DateTime(2026, 3, 21, 21, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 6, 2 },
                    { 9, new DateTime(2026, 3, 1, 8, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 30, 2 },
                    { 10, new DateTime(2026, 3, 24, 13, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 34, 2 },
                    { 11, new DateTime(2026, 3, 14, 16, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 10, 2 },
                    { 12, new DateTime(2026, 3, 12, 14, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 34, 2 },
                    { 13, new DateTime(2026, 3, 25, 10, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 6, 2 },
                    { 14, new DateTime(2026, 3, 7, 14, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 7, 2 },
                    { 15, new DateTime(2026, 3, 8, 19, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 22, 2 },
                    { 16, new DateTime(2026, 3, 17, 8, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 16, 2 },
                    { 17, new DateTime(2026, 3, 22, 11, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 1, 2 },
                    { 18, new DateTime(2026, 3, 11, 15, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 11, 2 },
                    { 19, new DateTime(2026, 3, 4, 8, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 27, 2 },
                    { 20, new DateTime(2026, 3, 11, 17, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 33, 2 },
                    { 21, new DateTime(2026, 3, 4, 19, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 6, 2 },
                    { 22, new DateTime(2026, 3, 4, 11, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 12, 2 },
                    { 23, new DateTime(2026, 3, 1, 16, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 3, 2 },
                    { 24, new DateTime(2026, 3, 7, 20, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 3, 2 },
                    { 25, new DateTime(2026, 3, 25, 13, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 1, 2 },
                    { 26, new DateTime(2026, 3, 8, 18, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 24, 2 },
                    { 27, new DateTime(2026, 3, 3, 12, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 1, 2 },
                    { 28, new DateTime(2026, 3, 19, 17, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 36, 2 },
                    { 29, new DateTime(2026, 3, 23, 18, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 4, 2 },
                    { 30, new DateTime(2026, 3, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 12, 2 },
                    { 31, new DateTime(2026, 3, 19, 21, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 31, 2 },
                    { 32, new DateTime(2026, 3, 13, 13, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 31, 2 },
                    { 33, new DateTime(2026, 3, 5, 10, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 31, 2 },
                    { 34, new DateTime(2026, 3, 1, 9, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 12, 2 },
                    { 35, new DateTime(2026, 3, 12, 9, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 20, 2 },
                    { 36, new DateTime(2026, 3, 13, 16, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 17, 2 },
                    { 37, new DateTime(2026, 3, 20, 8, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 15, 2 },
                    { 38, new DateTime(2026, 3, 5, 21, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 39, 2 },
                    { 39, new DateTime(2026, 3, 4, 10, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 23, 2 },
                    { 40, new DateTime(2026, 3, 13, 17, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 23, 2 },
                    { 41, new DateTime(2026, 3, 11, 13, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 29, 2 },
                    { 42, new DateTime(2026, 3, 12, 20, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 38, 2 },
                    { 43, new DateTime(2026, 3, 16, 15, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 28, 2 },
                    { 44, new DateTime(2026, 3, 14, 18, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 7, 2 },
                    { 45, new DateTime(2026, 3, 6, 10, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 20, 2 },
                    { 46, new DateTime(2026, 3, 22, 16, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 31, 2 },
                    { 47, new DateTime(2026, 3, 24, 20, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 28, 2 },
                    { 48, new DateTime(2026, 3, 10, 8, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 6, 2 },
                    { 49, new DateTime(2026, 3, 11, 12, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 8, 2 },
                    { 50, new DateTime(2026, 3, 17, 21, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 24, 2 },
                    { 51, new DateTime(2026, 3, 20, 19, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 28, 2 },
                    { 52, new DateTime(2026, 3, 3, 17, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 30, 2 },
                    { 53, new DateTime(2026, 3, 23, 20, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 2, 2 },
                    { 54, new DateTime(2026, 3, 13, 9, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 22, 2 },
                    { 55, new DateTime(2026, 3, 7, 16, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 1, 2 },
                    { 56, new DateTime(2026, 3, 7, 13, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 33, 2 },
                    { 57, new DateTime(2026, 3, 3, 21, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 29, 2 },
                    { 58, new DateTime(2026, 3, 15, 10, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 2, 2 },
                    { 59, new DateTime(2026, 3, 8, 19, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 15, 2 },
                    { 60, new DateTime(2026, 3, 9, 17, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 17, 2 },
                    { 61, new DateTime(2026, 3, 23, 9, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 26, 2 },
                    { 62, new DateTime(2026, 3, 6, 16, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 24, 2 },
                    { 63, new DateTime(2026, 3, 24, 8, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 22, 2 },
                    { 64, new DateTime(2026, 3, 24, 10, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 16, 2 },
                    { 65, new DateTime(2026, 3, 22, 20, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 25, 2 },
                    { 66, new DateTime(2026, 3, 4, 14, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 14, 2 },
                    { 67, new DateTime(2026, 3, 13, 19, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 34, 2 },
                    { 68, new DateTime(2026, 3, 6, 16, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 27, 2 },
                    { 69, new DateTime(2026, 3, 7, 19, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 39, 2 },
                    { 70, new DateTime(2026, 3, 9, 12, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 40, 2 },
                    { 71, new DateTime(2026, 3, 18, 12, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 13, 2 },
                    { 72, new DateTime(2026, 3, 13, 8, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 40, 2 },
                    { 73, new DateTime(2026, 3, 6, 13, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 40, 2 },
                    { 74, new DateTime(2026, 3, 5, 17, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 22, 2 },
                    { 75, new DateTime(2026, 3, 10, 18, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 29, 2 },
                    { 76, new DateTime(2026, 3, 13, 21, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 1, 2 },
                    { 77, new DateTime(2026, 3, 8, 11, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 37, 2 },
                    { 78, new DateTime(2026, 3, 20, 12, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 26, 2 },
                    { 79, new DateTime(2026, 3, 8, 21, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 14, 2 },
                    { 80, new DateTime(2026, 3, 18, 11, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 29, 2 },
                    { 81, new DateTime(2026, 3, 22, 11, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 31, 2 },
                    { 82, new DateTime(2026, 3, 18, 10, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 20, 2 },
                    { 83, new DateTime(2026, 3, 20, 9, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 15, 2 },
                    { 84, new DateTime(2026, 3, 2, 21, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 38, 2 },
                    { 85, new DateTime(2026, 3, 5, 16, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 15, 2 },
                    { 86, new DateTime(2026, 3, 1, 19, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 16, 2 },
                    { 87, new DateTime(2026, 3, 24, 20, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 35, 2 },
                    { 88, new DateTime(2026, 3, 16, 12, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 1, 2 },
                    { 89, new DateTime(2026, 3, 14, 11, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 15, 2 },
                    { 90, new DateTime(2026, 3, 4, 15, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 12, 2 },
                    { 91, new DateTime(2026, 3, 10, 9, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 40, 2 },
                    { 92, new DateTime(2026, 3, 17, 21, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 20, 2 },
                    { 93, new DateTime(2026, 3, 14, 19, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 10, 2 },
                    { 94, new DateTime(2026, 3, 8, 11, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 19, 2 },
                    { 95, new DateTime(2026, 3, 3, 21, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 34, 2 },
                    { 96, new DateTime(2026, 3, 23, 20, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 8, 2 },
                    { 97, new DateTime(2026, 3, 3, 20, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 36, 2 },
                    { 98, new DateTime(2026, 3, 8, 15, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 33, 2 },
                    { 99, new DateTime(2026, 3, 21, 14, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 38, 2 },
                    { 100, new DateTime(2026, 3, 11, 10, 0, 0, 0, DateTimeKind.Utc), "Оплачен", 16, 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "MenuItemId", "OrderId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 23, 1, 1, 150m },
                    { 2, 1, 1, 1, 450m },
                    { 3, 6, 1, 2, 400m },
                    { 4, 3, 2, 3, 420m },
                    { 5, 17, 2, 1, 650m },
                    { 6, 24, 3, 1, 200m },
                    { 7, 16, 4, 1, 700m },
                    { 8, 18, 4, 3, 950m },
                    { 9, 23, 4, 1, 150m },
                    { 10, 13, 5, 3, 650m },
                    { 11, 23, 6, 1, 150m },
                    { 12, 22, 7, 1, 250m },
                    { 13, 1, 7, 2, 450m },
                    { 14, 2, 8, 1, 350m },
                    { 15, 11, 8, 1, 950m },
                    { 16, 19, 8, 1, 400m },
                    { 17, 27, 9, 3, 230m },
                    { 18, 1, 10, 1, 450m },
                    { 19, 14, 11, 1, 750m },
                    { 20, 23, 11, 1, 150m },
                    { 21, 8, 12, 2, 700m },
                    { 22, 14, 13, 3, 750m },
                    { 23, 12, 14, 1, 1050m },
                    { 24, 16, 14, 1, 700m },
                    { 25, 17, 14, 2, 650m },
                    { 26, 13, 15, 2, 650m },
                    { 27, 15, 15, 2, 850m },
                    { 28, 19, 16, 2, 400m },
                    { 29, 7, 16, 2, 350m },
                    { 30, 7, 17, 2, 350m },
                    { 31, 24, 17, 1, 200m },
                    { 32, 1, 17, 1, 450m },
                    { 33, 24, 18, 2, 200m },
                    { 34, 15, 18, 1, 850m },
                    { 35, 7, 19, 2, 350m },
                    { 36, 21, 19, 3, 450m },
                    { 37, 20, 19, 2, 420m },
                    { 38, 15, 20, 3, 850m },
                    { 39, 6, 20, 1, 400m },
                    { 40, 1, 21, 2, 450m },
                    { 41, 17, 21, 2, 650m },
                    { 42, 4, 21, 3, 480m },
                    { 43, 3, 22, 2, 420m },
                    { 44, 14, 22, 1, 750m },
                    { 45, 26, 22, 1, 220m },
                    { 46, 27, 23, 2, 230m },
                    { 47, 23, 23, 2, 150m },
                    { 48, 6, 24, 2, 400m },
                    { 49, 13, 24, 3, 650m },
                    { 50, 5, 25, 3, 450m },
                    { 51, 3, 26, 2, 420m },
                    { 52, 18, 26, 1, 950m },
                    { 53, 8, 27, 2, 700m },
                    { 54, 18, 28, 1, 950m },
                    { 55, 3, 28, 1, 420m },
                    { 56, 13, 29, 2, 650m },
                    { 57, 17, 29, 1, 650m },
                    { 58, 5, 29, 2, 450m },
                    { 59, 27, 30, 2, 230m },
                    { 60, 10, 30, 1, 1500m },
                    { 61, 13, 31, 1, 650m },
                    { 62, 8, 31, 3, 700m },
                    { 63, 30, 31, 1, 80m },
                    { 64, 24, 32, 1, 200m },
                    { 65, 26, 32, 3, 220m },
                    { 66, 10, 32, 1, 1500m },
                    { 67, 5, 33, 2, 450m },
                    { 68, 23, 34, 2, 150m },
                    { 69, 10, 34, 2, 1500m },
                    { 70, 10, 35, 3, 1500m },
                    { 71, 19, 36, 3, 400m },
                    { 72, 16, 37, 1, 700m },
                    { 73, 6, 38, 3, 400m },
                    { 74, 19, 38, 3, 400m },
                    { 75, 18, 39, 1, 950m },
                    { 76, 3, 39, 1, 420m },
                    { 77, 16, 40, 2, 700m },
                    { 78, 25, 40, 2, 250m },
                    { 79, 24, 40, 2, 200m },
                    { 80, 22, 41, 1, 250m },
                    { 81, 27, 41, 3, 230m },
                    { 82, 7, 41, 2, 350m },
                    { 83, 16, 42, 2, 700m },
                    { 84, 24, 42, 1, 200m },
                    { 85, 1, 42, 3, 450m },
                    { 86, 17, 43, 3, 650m },
                    { 87, 20, 43, 3, 420m },
                    { 88, 24, 44, 2, 200m },
                    { 89, 10, 44, 1, 1500m },
                    { 90, 22, 45, 3, 250m },
                    { 91, 26, 45, 3, 220m },
                    { 92, 6, 46, 1, 400m },
                    { 93, 19, 46, 2, 400m },
                    { 94, 18, 46, 3, 950m },
                    { 95, 13, 47, 1, 650m },
                    { 96, 6, 48, 1, 400m },
                    { 97, 3, 48, 1, 420m },
                    { 98, 25, 49, 2, 250m },
                    { 99, 3, 49, 3, 420m },
                    { 100, 28, 50, 3, 100m },
                    { 101, 19, 50, 2, 400m },
                    { 102, 5, 50, 2, 450m },
                    { 103, 17, 51, 3, 650m },
                    { 104, 24, 51, 2, 200m },
                    { 105, 17, 52, 3, 650m },
                    { 106, 14, 52, 2, 750m },
                    { 107, 2, 53, 3, 350m },
                    { 108, 7, 53, 1, 350m },
                    { 109, 23, 53, 3, 150m },
                    { 110, 22, 54, 1, 250m },
                    { 111, 17, 54, 1, 650m },
                    { 112, 8, 55, 3, 700m },
                    { 113, 12, 55, 2, 1050m },
                    { 114, 14, 55, 3, 750m },
                    { 115, 14, 56, 2, 750m },
                    { 116, 23, 56, 2, 150m },
                    { 117, 15, 57, 1, 850m },
                    { 118, 25, 57, 1, 250m },
                    { 119, 9, 57, 2, 400m },
                    { 120, 1, 58, 2, 450m },
                    { 121, 20, 59, 1, 420m },
                    { 122, 1, 60, 2, 450m },
                    { 123, 22, 61, 1, 250m },
                    { 124, 5, 62, 3, 450m },
                    { 125, 25, 63, 1, 250m },
                    { 126, 3, 64, 2, 420m },
                    { 127, 4, 64, 2, 480m },
                    { 128, 18, 65, 1, 950m },
                    { 129, 22, 66, 2, 250m },
                    { 130, 26, 67, 1, 220m },
                    { 131, 20, 67, 2, 420m },
                    { 132, 26, 68, 1, 220m },
                    { 133, 1, 69, 3, 450m },
                    { 134, 29, 69, 1, 90m },
                    { 135, 21, 70, 3, 450m },
                    { 136, 23, 70, 2, 150m },
                    { 137, 22, 71, 1, 250m },
                    { 138, 11, 71, 3, 950m },
                    { 139, 13, 72, 1, 650m },
                    { 140, 8, 72, 1, 700m },
                    { 141, 15, 73, 3, 850m },
                    { 142, 15, 74, 2, 850m },
                    { 143, 13, 74, 1, 650m },
                    { 144, 28, 75, 1, 100m },
                    { 145, 30, 75, 2, 80m },
                    { 146, 14, 75, 1, 750m },
                    { 147, 1, 76, 1, 450m },
                    { 148, 21, 76, 1, 450m },
                    { 149, 2, 76, 3, 350m },
                    { 150, 30, 77, 3, 80m },
                    { 151, 6, 77, 2, 400m },
                    { 152, 18, 78, 1, 950m },
                    { 153, 21, 78, 1, 450m },
                    { 154, 28, 78, 3, 100m },
                    { 155, 29, 79, 3, 90m },
                    { 156, 24, 79, 2, 200m },
                    { 157, 25, 79, 1, 250m },
                    { 158, 15, 80, 3, 850m },
                    { 159, 1, 80, 1, 450m },
                    { 160, 5, 81, 2, 450m },
                    { 161, 13, 82, 3, 650m },
                    { 162, 9, 82, 3, 400m },
                    { 163, 12, 82, 1, 1050m },
                    { 164, 11, 83, 1, 950m },
                    { 165, 20, 83, 2, 420m },
                    { 166, 18, 84, 1, 950m },
                    { 167, 10, 84, 3, 1500m },
                    { 168, 28, 85, 1, 100m },
                    { 169, 25, 85, 3, 250m },
                    { 170, 3, 86, 3, 420m },
                    { 171, 19, 87, 2, 400m },
                    { 172, 12, 87, 2, 1050m },
                    { 173, 23, 87, 3, 150m },
                    { 174, 13, 88, 1, 650m },
                    { 175, 21, 89, 3, 450m },
                    { 176, 13, 89, 3, 650m },
                    { 177, 18, 90, 2, 950m },
                    { 178, 20, 90, 2, 420m },
                    { 179, 21, 91, 3, 450m },
                    { 180, 25, 92, 2, 250m },
                    { 181, 2, 92, 2, 350m },
                    { 182, 7, 92, 2, 350m },
                    { 183, 23, 93, 2, 150m },
                    { 184, 18, 94, 1, 950m },
                    { 185, 5, 95, 1, 450m },
                    { 186, 15, 96, 2, 850m },
                    { 187, 18, 96, 2, 950m },
                    { 188, 20, 97, 2, 420m },
                    { 189, 10, 97, 2, 1500m },
                    { 190, 25, 97, 3, 250m },
                    { 191, 24, 98, 3, 200m },
                    { 192, 18, 98, 2, 950m },
                    { 193, 11, 98, 2, 950m },
                    { 194, 13, 99, 1, 650m },
                    { 195, 22, 99, 1, 250m },
                    { 196, 17, 100, 1, 650m },
                    { 197, 13, 100, 2, 650m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishItems_IngredientId",
                table: "DishItems",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_DishItems_MenuItemId",
                table: "DishItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_MenuItemId",
                table: "OrderItems",
                column: "MenuItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Indigriend");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
