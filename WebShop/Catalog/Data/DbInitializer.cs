using Catalog.Entities;

namespace Catalog.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Перевірка, чи є вже дані в базі
            if (context.Brands.Any() && context.Categories.Any() && context.Products.Any())
            {
                return;   // База вже ініціалізована
            }

            // 1. Додавання брендів
            var brand1 = new Brand { Name = "Apple", Country = "USA" };
            var brand2 = new Brand { Name = "Samsung", Country = "South Korea" };
            context.Brands.AddRange(brand1, brand2);

            // 2. Додавання категорій
            var category1 = new Category { Name = "Smartphones" };
            var category2 = new Category { Name = "Laptops" };
            context.Categories.AddRange(category1, category2);

            // Збереження брендів та категорій
            context.SaveChanges();

            // 3. Додавання продуктів
            var product1 = new ProductEntity
            {
                Title = "iPhone 13",
                Description = "Apple iPhone 13",
                Price = 999.99m,
                CategoryId = category1.Id,
                BrandId = brand1.Id
            };
            var product2 = new ProductEntity
            {
                Title = "Samsung Galaxy S21",
                Description = "Samsung Galaxy S21",
                Price = 799.99m,
                CategoryId = category1.Id,
                BrandId = brand2.Id
            };
            var product3 = new ProductEntity
            {
                Title = "MacBook Pro 13",
                Description = "Apple MacBook Pro 13",
                Price = 1299.99m,
                CategoryId = category2.Id,
                BrandId = brand1.Id
            };
            context.Products.AddRange(product1, product2, product3);

            // Збереження продуктів
            context.SaveChanges();

            // 4. Додавання замовлення
            var order = new OrderEntity
            {
                CustomerName = "Іван Іванов",
                Address = "Вулиця Центральна, 15",
            };
            context.Orders.Add(order);
            context.SaveChanges();  // Зберігаємо, щоб отримати Id замовлення

            // 5. Додавання елементів замовлення
            var orderItem1 = new OrderItem
            {
                OrderId = order.Id,
                ProductId = product1.Id, // Прив'язуємо до iPhone 13
                Quantity = 1,
                Price = product1.Price
            };
            var orderItem2 = new OrderItem
            {
                OrderId = order.Id,
                ProductId = product2.Id, // Прив'язуємо до Samsung Galaxy S21
                Quantity = 2,
                Price = product2.Price
            };
            context.OrderItems.AddRange(orderItem1, orderItem2);

            // Збереження елементів замовлення
            context.SaveChanges();
        }
    }


}
