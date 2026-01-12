using CafeAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CafeAPI.Data
{
    public class CafeDbContext : DbContext
    {
        public CafeDbContext(DbContextOptions<CafeDbContext> options) 
            : base(options) 
        { }

        public DbSet<Category> Category { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<MenuItem> MenuItems { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;

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
                // Супы
                new MenuItem { MenuItemId = 1, Name = "Борщ", Description = "Классический украинский борщ с мясом, свеклой и сметаной", Price = 450, CategoryId = 1, Available = true },
                new MenuItem { MenuItemId = 2, Name = "Куриный суп с лапшой", Description = "Лёгкий куриный бульон с домашней лапшой", Price = 350, CategoryId = 1, Available = true },
                new MenuItem { MenuItemId = 3, Name = "Грибной крем-суп", Description = "Нежный суп-пюре из лесных грибов с гренками", Price = 420, CategoryId = 1, Available = true },

                // Салаты
                new MenuItem { MenuItemId = 4, Name = "Цезарь с курицей", Description = "Салат с курицей, сыром пармезан и сухариками", Price = 480, CategoryId = 2, Available = true },
                new MenuItem { MenuItemId = 5, Name = "Греческий салат", Description = "Огурцы, помидоры, фета, оливки, красный лук", Price = 450, CategoryId = 2, Available = true },
                new MenuItem { MenuItemId = 6, Name = "Витаминный салат", Description = "Микс свежих овощей с оливковым маслом", Price = 400, CategoryId = 2, Available = true },

                // Закуски
                new MenuItem { MenuItemId = 7, Name = "Брускетта с томатами", Description = "Хрустящий хлеб с томатами, базиликом и оливковым маслом", Price = 350, CategoryId = 3, Available = true },
                new MenuItem { MenuItemId = 8, Name = "Карпаччо из говядины", Description = "Тонко нарезанная говядина с лимонным соусом", Price = 700, CategoryId = 3, Available = true },
                new MenuItem { MenuItemId = 9, Name = "Моцарелла с томатами", Description = "Сыр моцарелла с томатами и базиликом", Price = 400, CategoryId = 3, Available = true },

                // Основные блюда
                new MenuItem { MenuItemId = 10, Name = "Стейк рибай", Description = "Сочный говяжий стейк, прожарка по желанию", Price = 1500, CategoryId = 4, Available = true },
                new MenuItem { MenuItemId = 11, Name = "Курица гриль", Description = "Куриное филе на гриле с пряными травами", Price = 950, CategoryId = 4, Available = true },
                new MenuItem { MenuItemId = 12, Name = "Свинина в соусе барбекю", Description = "Мясо свинины с соусом BBQ и овощами", Price = 1050, CategoryId = 4, Available = true },

                // Пицца
                new MenuItem { MenuItemId = 13, Name = "Маргарита", Description = "Томатный соус, сыр моцарелла, базилик", Price = 650, CategoryId = 5, Available = true },
                new MenuItem { MenuItemId = 14, Name = "Пепперони", Description = "Пицца с пепперони и сыром моцарелла", Price = 750, CategoryId = 5, Available = true },
                new MenuItem { MenuItemId = 15, Name = "Четыре сыра", Description = "Моцарелла, горгонзола, пармезан, чеддер", Price = 850, CategoryId = 5, Available = true },

                // Паста
                new MenuItem { MenuItemId = 16, Name = "Спагетти Болоньезе", Description = "Спагетти с мясным соусом и пармезаном", Price = 700, CategoryId = 6, Available = true },
                new MenuItem { MenuItemId = 17, Name = "Феттучини Альфредо", Description = "Паста с кремовым соусом и сыром", Price = 650, CategoryId = 6, Available = true },
                new MenuItem { MenuItemId = 18, Name = "Паста с морепродуктами", Description = "Паста с креветками, кальмарами и чесночным соусом", Price = 950, CategoryId = 6, Available = true },

                // Десерты
                new MenuItem { MenuItemId = 19, Name = "Тирамису", Description = "Классический итальянский десерт с маскарпоне и кофе", Price = 400, CategoryId = 7, Available = true },
                new MenuItem { MenuItemId = 20, Name = "Шоколадный фондан", Description = "Тёплый шоколадный кекс с жидкой начинкой", Price = 420, CategoryId = 7, Available = true },
                new MenuItem { MenuItemId = 21, Name = "Чизкейк Нью-Йорк", Description = "Классический чизкейк с клубничным соусом", Price = 450, CategoryId = 7, Available = true },

                // Напитки
                new MenuItem { MenuItemId = 22, Name = "Капучино", Description = "Эспрессо с горячим молоком и пенкой", Price = 250, CategoryId = 8, Available = true },
                new MenuItem { MenuItemId = 23, Name = "Чай черный", Description = "Классический черный чай", Price = 150, CategoryId = 8, Available = true },
                new MenuItem { MenuItemId = 24, Name = "Сок апельсиновый", Description = "Свежевжатый апельсиновый сок", Price = 200, CategoryId = 8, Available = true },

                // Гарниры
                new MenuItem { MenuItemId = 25, Name = "Картофель фри", Description = "Хрустящий картофель фри", Price = 250, CategoryId = 9, Available = true },
                new MenuItem { MenuItemId = 26, Name = "Рис с овощами", Description = "Отварной рис с овощами", Price = 220, CategoryId = 9, Available = true },
                new MenuItem { MenuItemId = 27, Name = "Овощи на пару", Description = "Сезонные овощи на пару", Price = 230, CategoryId = 9, Available = true },

                // Соусы
                new MenuItem { MenuItemId = 28, Name = "Соус BBQ", Description = "Сладко-пряный соус для мяса", Price = 100, CategoryId = 10, Available = true },
                new MenuItem { MenuItemId = 29, Name = "Томатный соус", Description = "Соус из томатов для пиццы и пасты", Price = 90, CategoryId = 10, Available = true },
                new MenuItem { MenuItemId = 30, Name = "Сметанный соус", Description = "Нежный соус со сметаной и зеленью", Price = 80, CategoryId = 10, Available = true }
            );
        }



    }
}
