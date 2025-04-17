class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Максимальное число в файле");
            Console.WriteLine("2. Четные числа в файле");
            Console.WriteLine("3. Поиск строк по комбинации");
            Console.WriteLine("4. Разность max и min в бинарном файле");
            Console.WriteLine("5. Самые дорогие игрушки");
            Console.WriteLine("6. Обработка списка (List)");
            Console.WriteLine("7. Обработка связанного списка (LinkedList)");
            Console.WriteLine("8. Мебельные фабрики (HashSet)");
            Console.WriteLine("9. Глухие согласные (HashSet)");
            Console.WriteLine("10. Сметана");
            Console.WriteLine("0. Выход");

            int choice = TestInput.GetInt("Выберите задание: ", 0, 10);

            if (choice == 0) break;

            switch (choice)
            {
                case 1:
                    ExecuteTask1();
                    break;
                case 2:
                    ExecuteTask2();
                    break;
                case 3:
                    ExecuteTask3();
                    break;
                case 4:
                    ExecuteTask4();
                    break;
                case 5:
                    ExecuteTask5();
                    break;
                case 6:
                    ExecuteTask6();
                    break;
                case 7:
                    ExecuteTask7();
                    break;
                case 8:
                    ExecuteTask8();
                    break;
                case 9:
                    ExecuteTask9();
                    break;
                case 10:
                    ExecuteTask10();
                    break;
            }

            Console.WriteLine("\nНажмите Enter для продолжения...");
            Console.ReadLine();
        }
    }

    static void ExecuteTask1()
    {
        Console.WriteLine("\n=== Задание 1: Максимальное число ===");
        string path = TestInput.GetFilePath("Введите путь к файлу: ");
        int count = TestInput.GetInt("Сколько чисел сгенерировать? ", 1, 1000);
        
        FileTasks.GenerateNumbersOnePerLine(path, count);
        FileTasks.PrintFileContents(path);
        
        var (max, cnt) = FileTasks.FindMaxNumber(path);
        Console.WriteLine($"Максимальное число: {max} (встречается {cnt} раз)");
    }

    static void ExecuteTask2()
    {
        Console.WriteLine("\n=== Задание 2: Четные числа ===");
        string path = TestInput.GetFilePath("Введите путь к файлу: ");
        int count = TestInput.GetInt("Сколько чисел сгенерировать? ", 1, 1000);
        
        FileTasks.GenerateNumbersMultiplePerLine(path, count);
        FileTasks.PrintFileContents(path);
        
        List<int> evens = FileTasks.FindEvenNumbers(path);
        Console.WriteLine($"Четные числа ({evens.Count}): {string.Join(", ", evens)}");
    }

    static void ExecuteTask3()
    {
        Console.WriteLine("\n=== Задание 3: Поиск строк ===");
        string sourcePath = TestInput.GetFilePath("Введите путь к исходному файлу: ", true);
        FileTasks.PrintFileContents(sourcePath);
        
        string combination = TestInput.GetString("Введите комбинацию для поиска: ");
        string targetPath = TestInput.GetFilePath("Введите путь для результата: ");
        
        FileTasks.FilterLinesByCombination(sourcePath, targetPath, combination);
        FileTasks.PrintFileContents(targetPath);
    }

    static void ExecuteTask4()
    {
        Console.WriteLine("\n=== Задание 4: Разность max и min ===");
        string path = TestInput.GetFilePath("Введите путь к бинарному файлу: ");
        int count = TestInput.GetInt("Сколько чисел сгенерировать? ", 1, 1000);
        
        FileTasks.GenerateBinaryFile(path, count);
        FileTasks.PrintBinaryFileContents(path);
        
        int difference = FileTasks.CalculateMaxMinDifference(path);
        Console.WriteLine($"Разность максимального и минимального элементов: {difference}");
    }

    static void ExecuteTask5()
    {
        Console.WriteLine("\n=== Задание 5: Самые дорогие игрушки ===");
        string path = TestInput.GetFilePath("Введите путь к бинарному файлу игрушек: ");
        int count = TestInput.GetInt("Сколько игрушек сгенерировать? ", 1, 100);
        
        FileTasks.GenerateBinaryToysFile(path, count);
        FileTasks.PrintBinaryToysFileContents(path);
        
        decimal k = TestInput.GetDecimal("Введите разницу в цене (k руб.): ", 0, 1000);
        FileTasks.FindMostExpensiveToys(path, k);
    }

    static void ExecuteTask6()
    {
        Console.WriteLine("\n=== Задание 6: Обработка списка (List) ===");
        FileTasks.ProcessListTask();
    }

    static void ExecuteTask7()
    {
        Console.WriteLine("\n=== Задание 7: Обработка связанного списка (LinkedList) ===");
        FileTasks.ProcessLinkedListTask();
    }

    static void ExecuteTask8()
    {
        Console.WriteLine("\n=== Задание 8: Анализ покупок мебели ===");
        FileTasks.AnalyzeFurniturePurchases();
    }   

     static void ExecuteTask9()
    {
        Console.WriteLine("\n=== Задание 9: Анализ глухих согласных ===");
        string path = TestInput.GetFilePath("Введите путь к файлу с русским текстом: ", true);
        FileTasks.AnalyzeRussianConsonants(path);
    }

    public static void ExecuteTask10()
    {
        Console.WriteLine("\n=== Задание 10: Анализ цен на сметану ===");
        string path = TestInput.GetFilePath("Введите путь к файлу с данными: ", true);
        FileTasks.AnalyzeCheapestSourCream(path);
    }
}