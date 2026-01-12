using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CafeAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateDb : Migration
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
                    Available = table.Column<bool>(type: "bit", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
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
                columns: new[] { "MenuItemId", "Available", "CategoryId", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, true, 1, "Классический украинский борщ с мясом, свеклой и сметаной", "Борщ", 450m },
                    { 2, true, 1, "Лёгкий куриный бульон с домашней лапшой", "Куриный суп с лапшой", 350m },
                    { 3, true, 1, "Нежный суп-пюре из лесных грибов с гренками", "Грибной крем-суп", 420m },
                    { 4, true, 2, "Салат с курицей, сыром пармезан и сухариками", "Цезарь с курицей", 480m },
                    { 5, true, 2, "Огурцы, помидоры, фета, оливки, красный лук", "Греческий салат", 450m },
                    { 6, true, 2, "Микс свежих овощей с оливковым маслом", "Витаминный салат", 400m },
                    { 7, true, 3, "Хрустящий хлеб с томатами, базиликом и оливковым маслом", "Брускетта с томатами", 350m },
                    { 8, true, 3, "Тонко нарезанная говядина с лимонным соусом", "Карпаччо из говядины", 700m },
                    { 9, true, 3, "Сыр моцарелла с томатами и базиликом", "Моцарелла с томатами", 400m },
                    { 10, true, 4, "Сочный говяжий стейк, прожарка по желанию", "Стейк рибай", 1500m },
                    { 11, true, 4, "Куриное филе на гриле с пряными травами", "Курица гриль", 950m },
                    { 12, true, 4, "Мясо свинины с соусом BBQ и овощами", "Свинина в соусе барбекю", 1050m },
                    { 13, true, 5, "Томатный соус, сыр моцарелла, базилик", "Маргарита", 650m },
                    { 14, true, 5, "Пицца с пепперони и сыром моцарелла", "Пепперони", 750m },
                    { 15, true, 5, "Моцарелла, горгонзола, пармезан, чеддер", "Четыре сыра", 850m },
                    { 16, true, 6, "Спагетти с мясным соусом и пармезаном", "Спагетти Болоньезе", 700m },
                    { 17, true, 6, "Паста с кремовым соусом и сыром", "Феттучини Альфредо", 650m },
                    { 18, true, 6, "Паста с креветками, кальмарами и чесночным соусом", "Паста с морепродуктами", 950m },
                    { 19, true, 7, "Классический итальянский десерт с маскарпоне и кофе", "Тирамису", 400m },
                    { 20, true, 7, "Тёплый шоколадный кекс с жидкой начинкой", "Шоколадный фондан", 420m },
                    { 21, true, 7, "Классический чизкейк с клубничным соусом", "Чизкейк Нью-Йорк", 450m },
                    { 22, true, 8, "Эспрессо с горячим молоком и пенкой", "Капучино", 250m },
                    { 23, true, 8, "Классический черный чай", "Чай черный", 150m },
                    { 24, true, 8, "Свежевжатый апельсиновый сок", "Сок апельсиновый", 200m },
                    { 25, true, 9, "Хрустящий картофель фри", "Картофель фри", 250m },
                    { 26, true, 9, "Отварной рис с овощами", "Рис с овощами", 220m },
                    { 27, true, 9, "Сезонные овощи на пару", "Овощи на пару", 230m },
                    { 28, true, 10, "Сладко-пряный соус для мяса", "Соус BBQ", 100m },
                    { 29, true, 10, "Соус из томатов для пиццы и пасты", "Томатный соус", 90m },
                    { 30, true, 10, "Нежный соус со сметаной и зеленью", "Сметанный соус", 80m }
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
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Payments");

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
