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

        var names = new string[]
        {
            "Александр",
            "София",
            "Лиам",
            "Оливия",
            "Ноа",
            "Эмма",
            "Михаил",
            "Ева",
            "Лукас",
            "Миа",
            "Итан",
            "Ава",
            "Оливер",
            "Изабелла",
            "Артём",
            "Анна",
            "Лео",
            "Ария",
            "Джек",
            "Амелия",
            "Мария",
            "Гарри",
            "Шарлотта",
            "Матвей",
            "Лилия",
            "Тео",
            "Грейс",
            "Дмитрий",
            "Хлоя",
            "Максим",
            "Зои",
            "Даниил",
            "Лейла",
            "Иван",
            "Харпер",
            "Кирилл",
            "Эвелин",
            "Роман",
            "Виктория",
            "Илья",
            "Алиса",
            "Амир",
            "Ясмин",
            "Омар",
            "Аиша",
            "Али",
            "Фатима",
            "Муhammad",
            "Зара",
            "Юсуф",
            "Мариам",
            "Кевин",
            "Линда",
            "Эрик",
            "Николь",
            "Марк",
            "Елена",
            "Давид",
            "Сара",
            "Луис",
            "София",
            "Карлос",
            "Мария",
            "Диего",
            "Камила",
            "Хуан",
            "Валентина",
            "Матео",
            "Изабель",
            "Лука",
            "Джулия",
            "Матео",
            "Франческа",
            "Алессандро",
            "Кьяра",
            "Ханс",
            "Эмма",
            "Лукас",
            "Леони",
            "Юрген",
            "Жан",
            "Хлоя",
            "Пьер",
            "Камилла",
            "Антуан",
            "Инес",
            "Такаши",
            "Сакура",
            "Рен",
            "Хина",
            "Харуто",
            "Юи",
            "Минхо",
            "Суджин",
            "Джун",
            "Соён",
            "Рави",
            "Аня",
            "Арджун",
            "Прия",
        };

        var couriers = new List<Courier>(100);
        var vehicleTypes = Enum.GetValues(typeof(VehicleType));
        var random = new Random();

        foreach (var name in names)
        {
            couriers.Add(
                new Courier()
                {
                    Name = name,
                    VehicleType = (VehicleType)
                        vehicleTypes.GetValue(random.Next(vehicleTypes.Length))!,
                }
            );
        }
        await context.Couriers.AddRangeAsync(couriers);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();

        var courierIds = couriers.Select(c => c.Id).ToArray();
        var deliveryStatuses = Enum.GetValues(typeof(DeliveryStatus));

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
                        Status = (DeliveryStatus)
                            deliveryStatuses.GetValue(random.Next(deliveryStatuses.Length))!,
                        CreatedAt = DateTime
                            .UtcNow.AddMonths(-1 * random.Next(1, 12))
                            .AddDays(-1 * random.Next(1, 30))
                            .AddHours(-1 * random.Next(1, 23))
                            .AddMinutes(-1 * random.Next(1, 59))
                            .AddSeconds(-1 * random.Next(1, 59)),
                    }
                );
            }

            await context.Deliveries.AddRangeAsync(deliveries);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
        }
    }
}
