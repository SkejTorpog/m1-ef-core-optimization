using Microsoft.EntityFrameworkCore;

namespace m1_ef_core_optimization.Models;

public static class SeedData
{
    public static async Task InitializeAsync(AppDbContext context)
    {
        if (await context.Deliveries.AnyAsync())
        {
            return;
        }

        var arr = Array.CreateInstance(typeof(int), 0);

        var names = new string[]
        {
            "Александр", "София", "Лиам", "Оливия", "Ноа", "Эмма", "Михаил", "Ева", "Лукас", "Миа", "Итан", "Ава",
            "Оливер", "Изабелла", "Артём", "Анна", "Лео", "Ария", "Джек", "Амелия", "Мария", "Гарри", "Шарлотта",
            "Матвей", "Лилия", "Тео", "Грейс", "Дмитрий", "Хлоя", "Максим", "Зои", "Даниил", "Лейла", "Иван", "Харпер",
            "Кирилл", "Эвелин", "Роман", "Виктория", "Илья", "Алиса", "Амир", "Ясмин", "Омар", "Аиша", "Али", "Фатима",
            "Муhammad", "Зара", "Юсуф", "Мариам", "Кевин", "Линда", "Эрик", "Николь", "Марк", "Елена", "Давид", "Сара",
            "Луис", "София", "Карлос", "Мария", "Диего", "Камила", "Хуан", "Валентина", "Матео", "Изабель", "Лука",
            "Джулия", "Матео", "Франческа", "Алессандро", "Кьяра", "Ханс", "Эмма", "Лукас", "Леони", "Юрген", "Жан",
            "Хлоя", "Пьер", "Камилла", "Антуан", "Инес", "Такаши", "Сакура", "Рен", "Хина", "Харуто", "Юи", "Минхо",
            "Суджин", "Джун", "Соён", "Рави", "Аня", "Арджун", "Прия",
        };
        var couriers = new List<Courier>(100);
        var vehicleTypes = Enum.GetValues<VehicleType>();

        var random = new Random();

        foreach (var name in names)
        {
            couriers.Add(
                new Courier()
                {
                    Name = name,
                    VehicleType = vehicleTypes[random.Next(vehicleTypes.Length)],
                }
            );
        }
        await context.Couriers.AddRangeAsync(couriers);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        var courierIds = couriers.Select(c => c.Id).ToArray();
        var deliveryStatuses = Enum.GetValues<DeliveryStatus>();

        var date = DateTime.UtcNow;
        for (int i = 0; i < 30; i++)
        {
            var deliveries = new List<Delivery>(5000);

            for (int j = 0; j < 5000; j++)
            {
                var courierId = courierIds[random.Next(courierIds.Length)];
                deliveries.Add(
                    new Delivery()
                    {
                        CourierId = courierId,
                        Distance = random.NextDouble() * 7,
                        Duration = random.Next(5, 180),
                        Fare = random.Next(1000, 31000) / 100m,
                        Status = deliveryStatuses[random.Next(deliveryStatuses.Length)],
                        CreatedAt = date.AddSeconds(-1 * random.Next(0, 31536000)),
                    }
                );
            }

            await context.Deliveries.AddRangeAsync(deliveries);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
        }
    }
}
