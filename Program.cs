using WatchLibrary;
using System;
using LAB12;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine($"\nЗадание 1\n");
        //создаем список из 5 случайных часов
        Point watchList = Point.MakeList(5);
        
        Thread.Sleep(500);

        Console.WriteLine("\nСписок:");
        Point.ShowList(watchList);

        //добавляем умные часы на 3 позицию
        var smartWatch = new SmartWatch();
        smartWatch.RandomInit();
        watchList = Point.AddPoint(watchList, 3, smartWatch);

        Thread.Sleep(500);

        Console.WriteLine("\nСписок после добавления умных часов на 3 позицию:");
        Point.ShowList(watchList);

        Thread.Sleep(500);

        //удаляем часы по бренду
        Console.WriteLine("\nВведите бренд для удаления часов:");
        string brandToRemove = Console.ReadLine();
        watchList = Point.RemoveFirstByBrand(watchList, brandToRemove);

        Thread.Sleep(500);

        Console.WriteLine("\nСписок после удаления:");
        Point.ShowList(watchList);

        //добавляем 2 случайных часов в начало
        watchList = Point.AddElementsToBeginning(watchList, 2);

        Thread.Sleep(500);

        Console.WriteLine("\nСписок после добавления двух случайных часов в начало:");
        Point.ShowList(watchList);

        //клонируем список
        Point clonedList = Point.DeepCloneList(watchList);

        //очищаем оригинальный список
        Point.ClearList(ref watchList);

        Thread.Sleep(500);

        Console.WriteLine("\nОригинальный список после очищения:");
        Point.ShowList(watchList);

        Thread.Sleep(500);

        Console.WriteLine("\nКлонированный список:");
        Point.ShowList(clonedList);

        Console.WriteLine("\nНажмите любую клавишу для продолжения работы...");
        Console.ReadLine();
        Console.WriteLine($"\nЗадание 2\n");

        MyHashSet hashSet = new MyHashSet();

        //добавляем часы разных брендов
        hashSet.Add(new Watch("Rolex", 2020));
        hashSet.Add(new AnalogWatch("Casio", 2021, "Sport"));
        hashSet.Add(new ElectronicWatch("Samsung", 2022, "AMOLED"));
        
        //печать после добавления
        hashSet.PrintHS();
        
        //проверяем наличие бренда
        bool hasCasio = hashSet.Contains("Casio");
        Console.WriteLine($"Содержит Casio: {hasCasio}");

        //удаляем бренд
        bool removed = hashSet.Remove("Casio");
        Console.WriteLine($"Удален Casio: {removed}");

        //печать после удаления
        hashSet.PrintHS();

        //проверяем снова
        hasCasio = hashSet.Contains("Casio");
        Console.WriteLine($"Содержит Casio после удаления: {hasCasio}");

        Console.WriteLine("\nНажмите любую клавишу для продолжения работы...");
        Console.ReadLine();
        Console.WriteLine($"\nЗадание 3\n");
        
        //создание и заполнение дерева
        List<Watch> watches = new List<Watch>
            {
                new Watch("Rolex", 2020),
                new AnalogWatch("Casio", 2018, "Classic"),
                new ElectronicWatch("Samsung", 2022, "AMOLED"),
                new SmartWatch("Apple", 2021, "OLED", "WatchOS", true),
                new Watch("Tissot", 2019),
                new AnalogWatch("Seiko", 2020, "Sport"),
                new ElectronicWatch("Garmin", 2023, "LCD")
            };

        BalancedBinaryTree tree = new BalancedBinaryTree();
        tree.CreateBalancedTree(watches);

        //печать дерева по уровням
        Console.WriteLine("Идеально сбалансированное бинарное дерево:");
        tree.PrintTreeByLevels();

        //поиск часов с минимальным годом выпуска
        try
        {
            Watch minWatch = tree.FindMinYear();
            Console.WriteLine($"\nЧасы с самым ранним годом выпуска: {minWatch}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
        }

        //преобразование в дерево поиска
        Console.WriteLine("\nПреобразованное дерево поиска:");
        BalancedBinaryTree searchTree = tree.ConvertToSearchTree();
        searchTree.PrintTreeByLevels();

        //проверка что это действительно новое дерево
        Console.WriteLine("\nИсходное дерево (не изменилось):");
        tree.PrintTreeByLevels();

        //удаление узла
        Console.WriteLine("\nВведите год выпуска часов для удаления:");
        int yearToDelete = int.Parse(Console.ReadLine());

        searchTree.DeleteNode(yearToDelete);

        Console.WriteLine("\nДерево после удаления:");
        searchTree.PrintTreeByLevels();

        //удаление дерева
        Console.WriteLine("Удаление дерева из памяти...");
        tree.ClearTree();

        //проверка
        Console.WriteLine("Проверка содержимого дерева:");
        tree.PrintTreeByLevels();

        Console.WriteLine("\nНажмите любую клавишу для продолжения работы...");
        Console.ReadLine();
        Console.WriteLine($"\nЗадание 4\n");

        //демонстрация работы коллекций
        Console.WriteLine("Демонстрация MyCollection (список):");
        var list = new MyCollection<Watch>(3);
        foreach (var watch in list)
        {
            Console.WriteLine(watch);
        }
    }
}