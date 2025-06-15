using LAB14;
using System;
using System.Collections.Generic;
using System.Linq;
using WatchLibrary;
using LAB12;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"Часть 1");
        
        // 1. Создаём сеть магазинов (List<Dictionary<string, Watch>>)
        var watchShopNetwork = new List<Dictionary<string, Watch>>();

        // 2. Заполняем магазины часами
        var shop1 = new Dictionary<string, Watch>
        {
            { "Model-001", new ElectronicWatch("Casio", 2020, "LCD") },
            { "Model-002", new AnalogWatch("Rolex", 2019, "Люкс") },
            { "Model-003", new SmartWatch("Apple", 2023, "OLED", "WatchOS", true) },
            { "Model-004", new ElectronicWatch("Garmin", 2022, "AMOLED") }
        };

        var shop2 = new Dictionary<string, Watch>
        {
            { "Model-005", new AnalogWatch("Omega", 2021, "Дайверский") },
            { "Model-006", new SmartWatch("Samsung", 2023, "AMOLED", "Tizen", false) },
            { "Model-007", new ElectronicWatch("Casio", 2021, "LED") }
        };

        watchShopNetwork.Add(shop1);
        watchShopNetwork.Add(shop2);

        // 3. Вывод всех часов в сети (для проверки)
        Console.WriteLine("=== Все часы в сети магазинов ===");
        foreach (var shop in watchShopNetwork)
        {
            foreach (var pair in shop)
            {
                Console.WriteLine($"[ID: {pair.Key}] {pair.Value}");
            }
        }
        Console.WriteLine();

        // =============================================
        // LINQ-запросы (двумя способами)
        // =============================================

        // Запрос 1: Выборка данных (Where) — электронные часы с OLED-дисплеем
        Console.WriteLine("1. Электронные часы с OLED-дисплеем:");
        // Способ 1: LINQ-синтаксис
        var oledWatches1 =
            from shop in watchShopNetwork
            from pair in shop
            where pair.Value is ElectronicWatch ew && ew.TypeOfDisplay == "OLED"
            select pair.Value;
        Console.WriteLine("[LINQ] " + string.Join(", ", oledWatches1));

        // Способ 2: Методы расширения
        var oledWatches2 = watchShopNetwork
            .SelectMany(shop => shop)
            .Where(pair => pair.Value is ElectronicWatch ew && ew.TypeOfDisplay == "OLED")
            .Select(pair => pair.Value);
        Console.WriteLine("[Методы] " + string.Join(", ", oledWatches2));
        Console.WriteLine();

        // Запрос 2: Операции над множествами (Union) — объединение часов из двух магазинов
        Console.WriteLine("2. Все уникальные часы из двух магазинов:");
        // Способ 1: LINQ-синтаксис
        var unionWatches1 =
            (from pair in watchShopNetwork[0] select pair.Value)
            .Union(from pair in watchShopNetwork[1] select pair.Value);
        Console.WriteLine("[LINQ] Кол-во: " + unionWatches1.Count());

        // Способ 2: Методы расширения
        var unionWatches2 = watchShopNetwork[0].Values.Union(watchShopNetwork[1].Values);
        Console.WriteLine("[Методы] Кол-во: " + unionWatches2.Count());
        Console.WriteLine();

        // Запрос 3: Агрегирование (Average) — средний год выпуска умных часов
        Console.WriteLine("3. Средний год выпуска умных часов:");
        // Способ 1: LINQ-синтаксис
        var avgYear1 =
            (from shop in watchShopNetwork
             from pair in shop
             where pair.Value is SmartWatch
             select pair.Value.YearOfManufacture)
            .Average();
        Console.WriteLine("[LINQ] " + avgYear1);

        // Способ 2: Методы расширения
        var avgYear2 = watchShopNetwork
            .SelectMany(shop => shop.Values)
            .OfType<SmartWatch>()
            .Average(sw => sw.YearOfManufacture);
        Console.WriteLine("[Методы] " + avgYear2);
        Console.WriteLine();

        // Запрос 4: Группировка (GroupBy) — часы по брендам
        Console.WriteLine("4. Группировка по брендам:");
        // Способ 1: LINQ-синтаксис
        var groupByBrand1 =
            from shop in watchShopNetwork
            from pair in shop
            group pair.Value by pair.Value.Brand into brandGroup
            select brandGroup;
        foreach (var group in groupByBrand1)
        {
            Console.WriteLine($"[LINQ] Бренд: {group.Key}, Кол-во: {group.Count()}");
        }

        // Способ 2: Методы расширения
        var groupByBrand2 = watchShopNetwork
            .SelectMany(shop => shop.Values)
            .GroupBy(watch => watch.Brand);
        foreach (var group in groupByBrand2)
        {
            Console.WriteLine($"[Методы] Бренд: {group.Key}, Кол-во: {group.Count()}");
        }
        Console.WriteLine();

        // Запрос 5: Соединение (Select в новый тип) — ID + информация о часах
        Console.WriteLine("5. Связка ID и часов:");
        // Способ 1: LINQ-синтаксис
        var watchWithId1 =
            from shop in watchShopNetwork
            from pair in shop
            select new { ID = pair.Key, WatchInfo = pair.Value.ToString() };
        foreach (var item in watchWithId1)
        {
            Console.WriteLine($"[LINQ] ID: {item.ID}, {item.WatchInfo}");
        }

        // Способ 2: Методы расширения
        var watchWithId2 = watchShopNetwork
            .SelectMany(shop => shop)
            .Select(pair => new { ID = pair.Key, WatchInfo = pair.Value.ToString() });
        foreach (var item in watchWithId2)
        {
            Console.WriteLine($"[Методы] ID: {item.ID}, {item.WatchInfo}");
        }
        Console.WriteLine();

        // Запрос 6: Без LINQ (for) — найти все часы Rolex
        Console.WriteLine("6. Все часы Rolex (без LINQ):");
        var rolexWatches = new List<Watch>();
        foreach (var shop in watchShopNetwork)
        {
            foreach (var pair in shop)
            {
                if (pair.Value.Brand == "Rolex")
                    rolexWatches.Add(pair.Value);
            }
        }
        Console.WriteLine(string.Join(", ", rolexWatches));
        
        Console.WriteLine($"\nЧасть 2");
        // 1. Создаем коллекцию часов (10 элементов)
        var watchCollection = new MyCollection<Watch>(10);

        // 2. Вывод всех элементов для проверки
        Console.WriteLine("=== Все часы в коллекции ===");
        foreach (var watch in watchCollection)
        {
            Console.WriteLine(watch);
        }
        Console.WriteLine();

        // =============================================
        // LINQ-запросы (двумя способами)
        // =============================================

        // Запрос 1: Выборка данных (Where) — электронные часы
        Console.WriteLine("1. Электронные часы:");
        // Способ 1: LINQ-синтаксис
        var electronicWatches1 =
            from watch in watchCollection
            where watch is ElectronicWatch
            select watch;
        Console.WriteLine("[LINQ] " + string.Join("\n", electronicWatches1));

        // Способ 2: Методы расширения
        var electronicWatches2 = watchCollection
            .Where(watch => watch is ElectronicWatch);
        Console.WriteLine("[Методы] " + string.Join("\n", electronicWatches2));
        Console.WriteLine();

        // Запрос 2: Получение счетчика (Count) — количество аналоговых часов
        Console.WriteLine("2. Количество аналоговых часов:");
        // Способ 1: LINQ-синтаксис
        var analogCount1 =
            (from watch in watchCollection
             where watch is AnalogWatch
             select watch).Count();
        Console.WriteLine("[LINQ] " + analogCount1);

        // Способ 2: Методы расширения
        var analogCount2 = watchCollection
            .Count(watch => watch is AnalogWatch);
        Console.WriteLine("[Методы] " + analogCount2);
        Console.WriteLine();

        // Запрос 3: Агрегирование (Max, Min, Average) — статистика по годам выпуска
        Console.WriteLine("3. Статистика по годам выпуска:");
        // Способ 1: LINQ-синтаксис
        var maxYear1 =
            (from watch in watchCollection
             select watch.YearOfManufacture).Max();
        var minYear1 =
            (from watch in watchCollection
             select watch.YearOfManufacture).Min();
        var avgYear11 =
            (from watch in watchCollection
             select watch.YearOfManufacture).Average();
        Console.WriteLine($"[LINQ] Макс: {maxYear1}, Мин: {minYear1}, Среднее: {avgYear11:F1}");

        // Способ 2: Методы расширения
        var maxYear2 = watchCollection.Max(watch => watch.YearOfManufacture);
        var minYear2 = watchCollection.Min(watch => watch.YearOfManufacture);
        var avgYear22 = watchCollection.Average(watch => watch.YearOfManufacture);
        Console.WriteLine($"[Методы] Макс: {maxYear2}, Мин: {minYear2}, Среднее: {avgYear22:F1}");
        Console.WriteLine();

        // Запрос 4: Группировка (GroupBy) — часы по типам
        Console.WriteLine("4. Группировка по типам часов:");
        // Способ 1: LINQ-синтаксис
        var groupByType1 =
            from watch in watchCollection
            group watch by watch.GetType().Name into typeGroup
            select typeGroup;
        foreach (var group in groupByType1)
        {
            Console.WriteLine($"[LINQ] Тип: {group.Key}, Кол-во: {group.Count()}");
        }

        // Способ 2: Методы расширения
        var groupByType2 = watchCollection
            .GroupBy(watch => watch.GetType().Name);
        foreach (var group in groupByType2)
        {
            Console.WriteLine($"[Методы] Тип: {group.Key}, Кол-во: {group.Count()}");
        }
    }
}