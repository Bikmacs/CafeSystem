using CafeAPI.Models;
using CafeAPI.Models.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI.Data
{
    public class CafeDbContext(DbContextOptions<CafeDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Indigriend> Indigriend { get; set; }
        public DbSet<DishItems> DishItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, Name = "Admin" },
                new Role { RoleId = 2, Name = "Waiter" },
                new Role { RoleId = 3, Name = "Cook" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    FullName = "Admin",
                    Login = "admin",
                    PasswordHash = "$2a$12$.gijsKvNhylDhZfxAknuDesvmZnx13DhA2NVKk9LZH32YiRAQM8YW",
                    RoleId = 1,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    UserId = 2,
                    FullName = "Waiter",
                    Login = "waiter",
                    PasswordHash = "$2a$12$C2Ek4ejvfw.so/k2AezYpuflw5YAaQ4vmHqU0xq0Gmz85Z.I3bSyG",
                    RoleId = 2,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new User
                {
                    UserId = 3,
                    FullName = "Cook",
                    Login = "cook",
                    PasswordHash = "$2a$12$9iwlfcfL1S1uYa7BLe3xZO03pZ7mOTQzZDDGWOP7h/Xq4GaCSNBCm",
                    RoleId = 3,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Супы" },
                new Category { CategoryId = 2, Name = "Салаты" },
                new Category { CategoryId = 3, Name = "Закуски" },
                new Category { CategoryId = 4, Name = "Основные блюда" },
                new Category { CategoryId = 5, Name = "Пицца" },
                new Category { CategoryId = 6, Name = "Паста" },
                new Category { CategoryId = 7, Name = "Десерты" },
                new Category { CategoryId = 8, Name = "Напитки" },
                new Category { CategoryId = 9, Name = "Гарниры" },
                new Category { CategoryId = 10, Name = "Соусы" }
            );

            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem
                {
                    MenuItemId = 1, 
                    Name = "Борщ",
                    Description = "Классический украинский борщ с мясом, свеклой и сметаной", Price = 450,
                    CategoryId = 1, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 2, 
                    Name = "Куриный суп с лапшой",
                    Description = "Лёгкий куриный бульон с домашней лапшой", Price = 350, CategoryId = 1,
                    Available = true
                },
                new MenuItem
                {
                    MenuItemId = 3, 
                    Name = "Грибной крем-суп",
                    Description = "Нежный суп-пюре из лесных грибов с гренками", Price = 420, CategoryId = 1,
                    Available = true
                },
                new MenuItem
                {
                    MenuItemId = 4,
                    Name = "Цезарь с курицей",
                    Description = "Салат с курицей, сыром пармезан и сухариками", Price = 480, CategoryId = 2,
                    Available = true
                },
                new MenuItem
                {
                    MenuItemId = 5, 
                    Name = "Греческий салат",
                    Description = "Огурцы, помидоры, фета, оливки, красный лук", Price = 450, CategoryId = 2,
                    Available = true
                },
                new MenuItem
                {
                    MenuItemId = 6, 
                    Name = "Витаминный салат", Description = "Микс свежих овощей с оливковым маслом",
                    Price = 400, CategoryId = 2, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 7, 
                    Name = "Брускетта с томатами",
                    Description = "Хрустящий хлеб с томатами, базиликом и оливковым маслом", Price = 350,
                    CategoryId = 3, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 8, 
                    Name = "Карпаччо из говядины",
                    Description = "Тонко нарезанная говядина с лимонным соусом", Price = 700, CategoryId = 3,
                    Available = true
                },
                new MenuItem
                {
                    MenuItemId = 9, 
                    Name = "Моцарелла с томатами", Description = "Сыр моцарелла с томатами и базиликом",
                    Price = 400, CategoryId = 3, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 10, 
                    Name = "Стейк рибай", Description = "Сочный говяжий стейк, прожарка по желанию",
                    Price = 1500, CategoryId = 4, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 11, 
                    Name = "Курица гриль", Description = "Куриное филе на гриле с пряными травами",
                    Price = 950, CategoryId = 4, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 12, 
                    Name = "Свинина в соусе барбекю",
                    Description = "Мясо свинины с соусом BBQ и овощами", Price = 1050, CategoryId = 4, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 13,
                    Name = "Маргарита", Description = "Томатный соус, сыр моцарелла, базилик",
                    Price = 650, CategoryId = 5, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 14, 
                    Name = "Пепперони", Description = "Пицца с пепперони и сыром моцарелла",
                    Price = 750, CategoryId = 5, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 15,
                    Name = "Четыре сыра", Description = "Моцарелла, горгонзола, пармезан, чеддер",
                    Price = 850, CategoryId = 5, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 16,
                    Name = "Спагетти Болоньезе", Description = "Спагетти с мясным соусом и пармезаном",
                    Price = 700, CategoryId = 6, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 17,
                    Name = "Феттучини Альфредо", Description = "Паста с кремовым соусом и сыром",
                    Price = 650, CategoryId = 6, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 18, 
                    Name = "Паста с морепродуктами",
                    Description = "Паста с креветками, кальмарами и чесночным соусом", Price = 950, CategoryId = 6,
                    Available = true
                },
                new MenuItem
                {
                    MenuItemId = 19, 
                    Name = "Тирамису",
                    Description = "Классический итальянский десерт с маскарпоне и кофе", Price = 400, CategoryId = 7,
                    Available = true
                },
                new MenuItem
                {
                    MenuItemId = 20,
                    Name = "Шоколадный фондан",
                    Description = "Тёплый шоколадный кекс с жидкой начинкой", Price = 420, CategoryId = 7,
                    Available = true
                },
                new MenuItem
                {
                    MenuItemId = 21, 
                    Name = "Чизкейк Нью-Йорк",
                    Description = "Классический чизкейк с клубничным соусом", Price = 450, CategoryId = 7,
                    Available = true
                },
                new MenuItem
                {
                    MenuItemId = 22,
                    Name = "Капучино", Description = "Эспрессо с горячим молоком и пенкой",
                    Price = 250, CategoryId = 8, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 23,
                    Name = "Чай черный", Description = "Классический черный чай", Price = 150,
                    CategoryId = 8, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 24,
                    Name = "Сок апельсиновый", Description = "Свежевжатый апельсиновый сок",
                    Price = 200, CategoryId = 8, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 25,
                    Name = "Картофель фри", Description = "Хрустящий картофель фри", Price = 250,
                    CategoryId = 9, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 26,
                    Name = "Рис с овощами", Description = "Отварной рис с овощами", Price = 220,
                    CategoryId = 9, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 27,
                    Name = "Овощи на пару", Description = "Сезонные овощи на пару", Price = 230,
                    CategoryId = 9, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 28, 
                    Name = "Соус BBQ", Description = "Сладко-пряный соус для мяса", Price = 100,
                    CategoryId = 10, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 29, 
                    Name = "Томатный соус", Description = "Соус из томатов для пиццы и пасты",
                    Price = 90, CategoryId = 10, Available = true
                },
                new MenuItem
                {
                    MenuItemId = 30, 
                    Name = "Сметанный соус", Description = "Нежный соус со сметаной и зеленью",
                    Price = 80, CategoryId = 10, Available = true
                }
            );
            
            modelBuilder.Entity<Indigriend>().HasData(
                new Indigriend { Id = 1, Name = "Говядина" },
                new Indigriend { Id = 2, Name = "Свинина" },
                new Indigriend { Id = 3, Name = "Курица" },
                new Indigriend { Id = 4, Name = "Индейка" },
                new Indigriend { Id = 5, Name = "Фарш" },
                new Indigriend { Id = 6, Name = "Молоко" },
                new Indigriend { Id = 7, Name = "Сметана" },
                new Indigriend { Id = 8, Name = "Творог" },
                new Indigriend { Id = 9, Name = "Сыр" },
                new Indigriend { Id = 10, Name = "СливочноеМасло" },
                new Indigriend { Id = 11, Name = "Мука" },
                new Indigriend { Id = 12, Name = "Сахар" },
                new Indigriend { Id = 13, Name = "Соль" },
                new Indigriend { Id = 14, Name = "ПодсолнечноеМасло" },
                new Indigriend { Id = 15, Name = "Рис" },
                new Indigriend { Id = 16, Name = "Гречка" },
                new Indigriend { Id = 17, Name = "МакаронныеИзделия" },
                new Indigriend { Id = 18, Name = "Картофель" },
                new Indigriend { Id = 19, Name = "ЛукРепчатый" },
                new Indigriend { Id = 20, Name = "Морковь" },
                new Indigriend { Id = 21, Name = "Капуста" },
                new Indigriend { Id = 22, Name = "Свекла" },
                new Indigriend { Id = 23, Name = "ХлебПшеничный" },
                new Indigriend { Id = 24, Name = "ХлебРжаной" },
                new Indigriend { Id = 25, Name = "Батон" }
            );

            modelBuilder.Entity<DishItems>().HasData(
                // 1. Борщ
                new DishItems { Id = 1, MenuItemId = 1, IngredientId = 1, Amount = 150m, UnitType = UnitTypes.грамм }, // Говядина
                new DishItems { Id = 2, MenuItemId = 1, IngredientId = 22, Amount = 80m, UnitType = UnitTypes.грамм }, // Свекла
                new DishItems { Id = 3, MenuItemId = 1, IngredientId = 21, Amount = 50m, UnitType = UnitTypes.грамм }, // Капуста
                new DishItems { Id = 4, MenuItemId = 1, IngredientId = 18, Amount = 100m, UnitType = UnitTypes.грамм }, // Картофель
                new DishItems { Id = 5, MenuItemId = 1, IngredientId = 7, Amount = 30m, UnitType = UnitTypes.грамм }, // Сметана
                new DishItems { Id = 6, MenuItemId = 1, IngredientId = 13, Amount = 5m, UnitType = UnitTypes.грамм }, // Соль

                // 2. Куриный суп с лапшой
                new DishItems { Id = 7, MenuItemId = 2, IngredientId = 3, Amount = 150m, UnitType = UnitTypes.грамм }, // Курица
                new DishItems { Id = 8, MenuItemId = 2, IngredientId = 17, Amount = 50m, UnitType = UnitTypes.грамм }, // Макароны
                new DishItems { Id = 9, MenuItemId = 2, IngredientId = 20, Amount = 40m, UnitType = UnitTypes.грамм }, // Морковь
                new DishItems { Id = 10, MenuItemId = 2, IngredientId = 13, Amount = 5m, UnitType = UnitTypes.грамм }, // Соль

                // 3. Грибной крем-суп (адаптировано из доступных продуктов)
                new DishItems { Id = 11, MenuItemId = 3, IngredientId = 18, Amount = 150m, UnitType = UnitTypes.грамм }, // Картофель
                new DishItems { Id = 12, MenuItemId = 3, IngredientId = 6, Amount = 0.15m, UnitType = UnitTypes.литр }, // Молоко
                new DishItems { Id = 13, MenuItemId = 3, IngredientId = 10, Amount = 20m, UnitType = UnitTypes.грамм }, // Сливочное масло
                new DishItems { Id = 14, MenuItemId = 3, IngredientId = 13, Amount = 5m, UnitType = UnitTypes.грамм }, // Соль

                // 4. Цезарь с курицей
                new DishItems { Id = 15, MenuItemId = 4, IngredientId = 3, Amount = 120m, UnitType = UnitTypes.грамм }, // Курица
                new DishItems { Id = 16, MenuItemId = 4, IngredientId = 9, Amount = 40m, UnitType = UnitTypes.грамм }, // Сыр (Пармезан)
                new DishItems { Id = 17, MenuItemId = 4, IngredientId = 21, Amount = 100m, UnitType = UnitTypes.грамм }, // Капуста (вместо салата)
                new DishItems { Id = 18, MenuItemId = 4, IngredientId = 25, Amount = 0.5m, UnitType = UnitTypes.штук }, // Батон (на сухарики)

                // 5. Греческий салат
                new DishItems { Id = 19, MenuItemId = 5, IngredientId = 9, Amount = 80m, UnitType = UnitTypes.грамм }, // Сыр (Фета)
                new DishItems { Id = 20, MenuItemId = 5, IngredientId = 19, Amount = 30m, UnitType = UnitTypes.грамм }, // Лук
                new DishItems { Id = 21, MenuItemId = 5, IngredientId = 14, Amount = 0.03m, UnitType = UnitTypes.литр }, // Масло
                new DishItems { Id = 22, MenuItemId = 5, IngredientId = 13, Amount = 3m, UnitType = UnitTypes.грамм }, // Соль

                // 6. Витаминный салат
                new DishItems { Id = 23, MenuItemId = 6, IngredientId = 21, Amount = 150m, UnitType = UnitTypes.грамм }, // Капуста
                new DishItems { Id = 24, MenuItemId = 6, IngredientId = 20, Amount = 80m, UnitType = UnitTypes.грамм }, // Морковь
                new DishItems { Id = 25, MenuItemId = 6, IngredientId = 14, Amount = 0.02m, UnitType = UnitTypes.литр }, // Масло

                // 7. Брускетта с томатами
                new DishItems { Id = 26, MenuItemId = 7, IngredientId = 25, Amount = 0.2m, UnitType = UnitTypes.штук }, // Батон
                new DishItems { Id = 27, MenuItemId = 7, IngredientId = 9, Amount = 30m, UnitType = UnitTypes.грамм }, // Сыр
                new DishItems { Id = 28, MenuItemId = 7, IngredientId = 14, Amount = 0.01m, UnitType = UnitTypes.литр }, // Масло

                // 8. Карпаччо из говядины
                new DishItems { Id = 29, MenuItemId = 8, IngredientId = 1, Amount = 150m, UnitType = UnitTypes.грамм }, // Говядина
                new DishItems { Id = 30, MenuItemId = 8, IngredientId = 9, Amount = 30m, UnitType = UnitTypes.грамм }, // Сыр
                new DishItems { Id = 31, MenuItemId = 8, IngredientId = 14, Amount = 0.02m, UnitType = UnitTypes.литр }, // Масло
                new DishItems { Id = 32, MenuItemId = 8, IngredientId = 13, Amount = 4m, UnitType = UnitTypes.грамм }, // Соль

                // 9. Моцарелла с томатами
                new DishItems { Id = 33, MenuItemId = 9, IngredientId = 9, Amount = 120m, UnitType = UnitTypes.грамм }, // Сыр (Моцарелла)
                new DishItems { Id = 34, MenuItemId = 9, IngredientId = 14, Amount = 0.02m, UnitType = UnitTypes.литр }, // Масло

                // 10. Стейк рибай
                new DishItems { Id = 35, MenuItemId = 10, IngredientId = 1, Amount = 300m, UnitType = UnitTypes.грамм }, // Говядина
                new DishItems { Id = 36, MenuItemId = 10, IngredientId = 14, Amount = 0.02m, UnitType = UnitTypes.литр }, // Масло
                new DishItems { Id = 37, MenuItemId = 10, IngredientId = 13, Amount = 6m, UnitType = UnitTypes.грамм }, // Соль

                // 11. Курица гриль
                new DishItems { Id = 38, MenuItemId = 11, IngredientId = 3, Amount = 250m, UnitType = UnitTypes.грамм }, // Курица
                new DishItems { Id = 39, MenuItemId = 11, IngredientId = 14, Amount = 0.02m, UnitType = UnitTypes.литр }, // Масло
                new DishItems { Id = 40, MenuItemId = 11, IngredientId = 13, Amount = 5m, UnitType = UnitTypes.грамм }, // Соль

                // 12. Свинина в соусе барбекю
                new DishItems { Id = 41, MenuItemId = 12, IngredientId = 2, Amount = 250m, UnitType = UnitTypes.грамм }, // Свинина
                new DishItems { Id = 42, MenuItemId = 12, IngredientId = 12, Amount = 10m, UnitType = UnitTypes.грамм }, // Сахар (для BBQ)
                new DishItems { Id = 43, MenuItemId = 12, IngredientId = 13, Amount = 5m, UnitType = UnitTypes.грамм }, // Соль

                // 13. Маргарита
                new DishItems { Id = 44, MenuItemId = 13, IngredientId = 11, Amount = 200m, UnitType = UnitTypes.грамм }, // Мука
                new DishItems { Id = 45, MenuItemId = 13, IngredientId = 9, Amount = 150m, UnitType = UnitTypes.грамм }, // Сыр
                new DishItems { Id = 46, MenuItemId = 13, IngredientId = 13, Amount = 4m, UnitType = UnitTypes.грамм }, // Соль
                new DishItems { Id = 47, MenuItemId = 13, IngredientId = 14, Amount = 0.02m, UnitType = UnitTypes.литр }, // Масло

                // 14. Пепперони
                new DishItems { Id = 48, MenuItemId = 14, IngredientId = 11, Amount = 200m, UnitType = UnitTypes.грамм }, // Мука
                new DishItems { Id = 49, MenuItemId = 14, IngredientId = 9, Amount = 100m, UnitType = UnitTypes.грамм }, // Сыр
                new DishItems { Id = 50, MenuItemId = 14, IngredientId = 2, Amount = 80m, UnitType = UnitTypes.грамм }, // Свинина (Колбаса)
                new DishItems { Id = 51, MenuItemId = 14, IngredientId = 13, Amount = 5m, UnitType = UnitTypes.грамм }, // Соль

                // 15. Четыре сыра
                new DishItems { Id = 52, MenuItemId = 15, IngredientId = 11, Amount = 200m, UnitType = UnitTypes.грамм }, // Мука
                new DishItems { Id = 53, MenuItemId = 15, IngredientId = 9, Amount = 250m, UnitType = UnitTypes.грамм }, // Сыр
                new DishItems { Id = 54, MenuItemId = 15, IngredientId = 14, Amount = 0.02m, UnitType = UnitTypes.литр }, // Масло

                // 16. Спагетти Болоньезе
                new DishItems { Id = 55, MenuItemId = 16, IngredientId = 17, Amount = 150m, UnitType = UnitTypes.грамм }, // Макароны
                new DishItems { Id = 56, MenuItemId = 16, IngredientId = 5, Amount = 120m, UnitType = UnitTypes.грамм }, // Фарш
                new DishItems { Id = 57, MenuItemId = 16, IngredientId = 9, Amount = 30m, UnitType = UnitTypes.грамм }, // Сыр
                new DishItems { Id = 58, MenuItemId = 16, IngredientId = 13, Amount = 4m, UnitType = UnitTypes.грамм }, // Соль

                // 17. Феттучини Альфредо
                new DishItems { Id = 59, MenuItemId = 17, IngredientId = 17, Amount = 150m, UnitType = UnitTypes.грамм }, // Макароны
                new DishItems { Id = 60, MenuItemId = 17, IngredientId = 10, Amount = 40m, UnitType = UnitTypes.грамм }, // Сливочное масло
                new DishItems { Id = 61, MenuItemId = 17, IngredientId = 9, Amount = 60m, UnitType = UnitTypes.грамм }, // Сыр
                new DishItems { Id = 62, MenuItemId = 17, IngredientId = 13, Amount = 3m, UnitType = UnitTypes.грамм }, // Соль

                // 18. Паста с морепродуктами (используем базовые продукты для основы)
                new DishItems { Id = 63, MenuItemId = 18, IngredientId = 17, Amount = 150m, UnitType = UnitTypes.грамм }, // Макароны
                new DishItems { Id = 64, MenuItemId = 18, IngredientId = 10, Amount = 30m, UnitType = UnitTypes.грамм }, // Сливочное масло
                new DishItems { Id = 65, MenuItemId = 18, IngredientId = 9, Amount = 30m, UnitType = UnitTypes.грамм }, // Сыр
                new DishItems { Id = 66, MenuItemId = 18, IngredientId = 13, Amount = 4m, UnitType = UnitTypes.грамм }, // Соль

                // 19. Тирамису
                new DishItems { Id = 67, MenuItemId = 19, IngredientId = 8, Amount = 120m, UnitType = UnitTypes.грамм }, // Творог (вместо Маскарпоне)
                new DishItems { Id = 68, MenuItemId = 19, IngredientId = 12, Amount = 40m, UnitType = UnitTypes.грамм }, // Сахар
                new DishItems { Id = 69, MenuItemId = 19, IngredientId = 6, Amount = 0.05m, UnitType = UnitTypes.литр }, // Молоко

                // 20. Шоколадный фондан
                new DishItems { Id = 70, MenuItemId = 20, IngredientId = 11, Amount = 60m, UnitType = UnitTypes.грамм }, // Мука
                new DishItems { Id = 71, MenuItemId = 20, IngredientId = 10, Amount = 50m, UnitType = UnitTypes.грамм }, // Сливочное масло
                new DishItems { Id = 72, MenuItemId = 20, IngredientId = 12, Amount = 50m, UnitType = UnitTypes.грамм }, // Сахар

                // 21. Чизкейк Нью-Йорк
                new DishItems { Id = 73, MenuItemId = 21, IngredientId = 8, Amount = 200m, UnitType = UnitTypes.грамм }, // Творог
                new DishItems { Id = 74, MenuItemId = 21, IngredientId = 11, Amount = 50m, UnitType = UnitTypes.грамм }, // Мука
                new DishItems { Id = 75, MenuItemId = 21, IngredientId = 12, Amount = 60m, UnitType = UnitTypes.грамм }, // Сахар
                new DishItems { Id = 76, MenuItemId = 21, IngredientId = 10, Amount = 40m, UnitType = UnitTypes.грамм }, // Сливочное масло

                // 22. Капучино
                new DishItems { Id = 77, MenuItemId = 22, IngredientId = 6, Amount = 0.2m, UnitType = UnitTypes.литр }, // Молоко
                new DishItems { Id = 78, MenuItemId = 22, IngredientId = 12, Amount = 10m, UnitType = UnitTypes.грамм }, // Сахар

                // 23. Чай черный
                new DishItems { Id = 79, MenuItemId = 23, IngredientId = 12, Amount = 15m, UnitType = UnitTypes.грамм }, // Сахар

                // 24. Сок апельсиновый
                new DishItems { Id = 80, MenuItemId = 24, IngredientId = 12, Amount = 5m, UnitType = UnitTypes.грамм }, // Сахар

                // 25. Картофель фри
                new DishItems { Id = 81, MenuItemId = 25, IngredientId = 18, Amount = 250m, UnitType = UnitTypes.грамм }, // Картофель
                new DishItems { Id = 82, MenuItemId = 25, IngredientId = 14, Amount = 0.1m, UnitType = UnitTypes.литр }, // Подсолнечное масло (фритюр)
                new DishItems { Id = 83, MenuItemId = 25, IngredientId = 13, Amount = 5m, UnitType = UnitTypes.грамм }, // Соль

                // 26. Рис с овощами
                new DishItems { Id = 84, MenuItemId = 26, IngredientId = 15, Amount = 150m, UnitType = UnitTypes.грамм }, // Рис
                new DishItems { Id = 85, MenuItemId = 26, IngredientId = 20, Amount = 50m, UnitType = UnitTypes.грамм }, // Морковь
                new DishItems { Id = 86, MenuItemId = 26, IngredientId = 19, Amount = 30m, UnitType = UnitTypes.грамм }, // Лук
                new DishItems { Id = 87, MenuItemId = 26, IngredientId = 13, Amount = 4m, UnitType = UnitTypes.грамм }, // Соль

                // 27. Овощи на пару
                new DishItems { Id = 88, MenuItemId = 27, IngredientId = 21, Amount = 100m, UnitType = UnitTypes.грамм }, // Капуста
                new DishItems { Id = 89, MenuItemId = 27, IngredientId = 20, Amount = 60m, UnitType = UnitTypes.грамм }, // Морковь
                new DishItems { Id = 90, MenuItemId = 27, IngredientId = 22, Amount = 60m, UnitType = UnitTypes.грамм }, // Свекла
                new DishItems { Id = 91, MenuItemId = 27, IngredientId = 13, Amount = 3m, UnitType = UnitTypes.грамм }, // Соль

                // 28. Соус BBQ
                new DishItems { Id = 92, MenuItemId = 28, IngredientId = 12, Amount = 15m, UnitType = UnitTypes.грамм }, // Сахар
                new DishItems { Id = 93, MenuItemId = 28, IngredientId = 13, Amount = 3m, UnitType = UnitTypes.грамм }, // Соль

                // 29. Томатный соус
                new DishItems { Id = 94, MenuItemId = 29, IngredientId = 12, Amount = 10m, UnitType = UnitTypes.грамм }, // Сахар
                new DishItems { Id = 95, MenuItemId = 29, IngredientId = 13, Amount = 4m, UnitType = UnitTypes.грамм }, // Соль

                // 30. Сметанный соус
                new DishItems { Id = 96, MenuItemId = 30, IngredientId = 7, Amount = 80m, UnitType = UnitTypes.грамм }, // Сметана
                new DishItems { Id = 97, MenuItemId = 30, IngredientId = 13, Amount = 3m, UnitType = UnitTypes.грамм } // Соль
            );
        }
    }
}