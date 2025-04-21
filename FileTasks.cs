using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;

public static class FileTasks
{
    private static Random _random = new Random();
    private const string CharPool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";

    // 1 задание
    public static void GenerateNumbersOnePerLine(string filePath, int count)
    {
        try
        {
            // Генерация случайных чисел и запись их в файл построчно
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine(_random.Next(-1000, 1001)); // Числа от -1000 до 1000
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи файла: {ex.Message}");
        }
    }

    public static (int maxNumber, int count) FindMaxNumber(string filePath)
    {
        try
        {
            int max = int.MinValue; 
            int count = 0;

            foreach (string line in File.ReadLines(filePath)) // Чтение файла построчно
            {
                if (int.TryParse(line, out int num)) // Проверка, что строка — это число
                {
                    if (num > max) 
                    {
                        max = num;
                        count = 1; // Сбрасываем счетчик
                    }
                    else if (num == max) 
                    {
                        count++; 
                    }
                }
            }

            return count > 0 ? (max, count) : (int.MinValue, 0); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            return (int.MinValue, 0);
        }
    }

    // 2 задание
    public static void GenerateNumbersMultiplePerLine(string filePath, int totalNumbers)
    {
        try
        {
            // Генерация случайных чисел и запись их в одну строку через пробел
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < totalNumbers; i++)
                {
                    writer.Write(_random.Next(-1000, 1001)); 
                    if (i < totalNumbers - 1) // добавляем пробел после каждого числа кроме последнего
                        writer.Write(" ");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи файла: {ex.Message}");
        }
    }

    public static List<int> FindEvenNumbers(string filePath)
    {
        List<int> evenNumbers = new List<int>();

        try
        {
            string content = File.ReadAllText(filePath); // Чтение всего содержимого файла
            string[] numbers = content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string numStr in numbers) // Перебор всех чисел в строке
            {
                if (int.TryParse(numStr, out int num) && num % 2 == 0) 
                {
                    evenNumbers.Add(num); 
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
        }

        return evenNumbers; 
    }

    // 3 задание
    public static void FilterLinesByCombination(string sourcePath, string targetPath, string combination)
    {
        if (string.IsNullOrWhiteSpace(combination)) // Проверка, что подстрока не пустая
        {
            Console.WriteLine("Ошибка: Подстрока не может быть пустой.");
            return;
        }

        try
        {
            var matchingLines = new List<string>(); // Список для хранения подходящих строк

            foreach (string line in File.ReadLines(sourcePath)) // Чтение файла построчно
            {
                if (line.Contains(combination)) // Проверка, содержит ли строка подстроку
                {
                    matchingLines.Add(line); // Добавление строки в список
                }
            }

            File.WriteAllLines(targetPath, matchingLines); 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке файлов: {ex.Message}");
        }
    }

    // 4 задание
    public static void GenerateBinaryFile(string filePath, int numbersCount)
    {
        try
        {
            // Генерация случайных чисел и запись их в бинарный файл
            using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                for (int i = 0; i < numbersCount; i++)
                {
                    writer.Write(_random.Next(-1000, 1001)); 
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при записи бинарного файла: {ex.Message}");
        }
    }

    public static int CalculateMaxMinDifference(string filePath)
    {
        try
        {
            int min = int.MaxValue; 
            int max = int.MinValue; 

            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length) // пока не достигнут конец файла
                {
                    int number = reader.ReadInt32(); 
                    if (number < min) min = number; 
                    if (number > max) max = number; 
                }
            }

            return max - min; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении бинарного файла: {ex.Message}");
            return -1; 
        }
    }

    // 5 задание
    [Serializable]
    public struct Toy
    {
        public string Name;
        public decimal Price;
        public int MinAge;
        public int MaxAge;

        public override string ToString()
        {
            return $"{Name} (Цена: {Price} руб., Возраст: {MinAge}-{MaxAge} лет)";
        }
    }

    public static void GenerateBinaryToysFile(string filePath, int toysCount)
    {
        try
        {
            Toy[] toys = new Toy[toysCount];
            string[] names = { "Конструктор", "Кукла", "Машинка", "Пазл", "Мяч", "Настольная игра" };

            for (int i = 0; i < toysCount; i++)
            {
                int minAge = _random.Next(0, 5); 
                int maxAge = _random.Next(minAge + 1, 12); // максимальный возраст (всегда больше минимального)

                toys[i] = new Toy
                {
                    Name = names[_random.Next(names.Length)], // Случайное название игрушки
                    Price = _random.Next(100, 5000), // Цена от 100 до 5000 рублей
                    MinAge = minAge,
                    MaxAge = maxAge
                };
            }

            // XML-сериализация данных в MemoryStream
            XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
            using (MemoryStream memoryStream = new MemoryStream())
            {
                serializer.Serialize(memoryStream, toys); // Сериализация массива игрушек
                byte[] xmlData = memoryStream.ToArray(); // Преобразование в массив байтов

                using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
                {
                    writer.Write(xmlData.Length); // Запись длины массива
                    writer.Write(xmlData); // Запись самого массива
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при создании файла игрушек: {ex.Message}");
        }
    }

    public static void FindMostExpensiveToys(string filePath, decimal k)
    {
        try
        {
            byte[] xmlData;
            //чтение длинны массива и его байтов
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int length = reader.ReadInt32(); 
                xmlData = reader.ReadBytes(length);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
            using (MemoryStream memoryStream = new MemoryStream(xmlData))
            {
                Toy[] toys = (Toy[])serializer.Deserialize(memoryStream); // десериализация массива игрушек

                if (toys.Length == 0)
                {
                    Console.WriteLine("Файл не содержит игрушек.");
                    return;
                }

                decimal maxPrice = decimal.MinValue;
                foreach (Toy toy in toys)
                {
                    if (toy.Price > maxPrice)
                    {
                        maxPrice = toy.Price;
                    }
                }

                Console.WriteLine("\nСамые дорогие игрушки:");
                foreach (Toy toy in toys)
                {
                    if (maxPrice - toy.Price <= k) 
                    {
                        Console.WriteLine(toy); 
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла игрушек: {ex.Message}");
        }
    }

    public static void PrintBinaryToysFileContents(string filePath)
    {
        try
        {
            byte[] xmlData;
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                int length = reader.ReadInt32();
                xmlData = reader.ReadBytes(length);
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
            using (MemoryStream memoryStream = new MemoryStream(xmlData))
            {
                Toy[] toys = (Toy[])serializer.Deserialize(memoryStream);

                Console.WriteLine("\nВсе игрушки в файле:");
                foreach (Toy toy in toys)
                {
                    Console.WriteLine(toy);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    // 6 задание
    public static void ProcessListTask()
    {
        try
        {
            List<char> list = GenerateRandomCharList(10); // генерация случайного списка символов

            Console.WriteLine("Исходный список: " + string.Join(" ", list));
            Console.Write("Введите символ E: ");

            if (!char.TryParse(Console.ReadLine(), out char E)) // проверка ввода символа
            {
                Console.WriteLine("Ошибка: Введите один символ.");
                return;
            }

            int index = list.IndexOf(E); // Поиск индекса символа E

            if (index == -1)
            {
                Console.WriteLine($"Символ '{E}' не найден в списке.");
                return;
            }

            list.InsertRange(index + 1, list); // вставка копии списка после символа E
            Console.WriteLine("Результат: " + string.Join(" ", list));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    // 7 задание
    public static void ProcessLinkedListTask()
    {
        try
        {
            // Создаем связанный список с случайными символами
            LinkedList<char> list = new LinkedList<char>(GenerateRandomCharList(10));
            Console.WriteLine("Исходный список: " + LinkedListToString(list));

            // Удаляем элементы, у которых соседи равны
            RemoveElementsWithSameNeighbors(list);

            Console.WriteLine("Результат: " + LinkedListToString(list));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    private static void RemoveElementsWithSameNeighbors(LinkedList<char> list)
    {
        if (list.Count < 2) return; // Если элементов меньше двух, ничего удалять не нужно

        LinkedListNode<char> current = list.First;

        while (current != null)
        {
            // определяем следующий и предыдущий элементы, учитывая цикличность списка
            LinkedListNode<char> next = current.Next ?? list.First; 
            LinkedListNode<char> prev = current.Previous ?? list.Last;

        
            if (prev.Value == next.Value)
            {
                LinkedListNode<char> toRemove = current; // Запоминаем элемент для удаления
                current = current.Next;
                list.Remove(toRemove); 
            }
            else
            {
                current = current.Next;
            }
        }
    }

    // 8 задание
    public static void AnalyzeFurniturePurchases()
    {
        try
        {
            int factoryCount = TestInput.GetInt("Введите количество фабрик: ", 1, 100);
            int buyerCount = TestInput.GetInt("Введите количество покупателей: ", 1, 100);

        List<string> factories = new List<string>();
            for (int i = 1; i <= factoryCount; i++)
            {
                factories.Add($"Фабрика{i}");
            }

            List<string> buyers = new List<string>();
            for (int i = 1; i <= buyerCount; i++)
            {
                buyers.Add($"Покупатель{i}");
            }

            Dictionary<string, HashSet<string>> purchases = new Dictionary<string, HashSet<string>>();

            foreach (var factory in factories)
            {
                var factoryBuyers = new HashSet<string>();
                int buyerCountForFactory = _random.Next(1, buyers.Count + 1);

                while (factoryBuyers.Count < buyerCountForFactory)
                {
                    string buyer = buyers[_random.Next(buyers.Count)];
                    factoryBuyers.Add(buyer);
                }

                purchases[factory] = factoryBuyers;
            }

            // Анализ данных
            HashSet<string> allBuyers = new HashSet<string>(buyers);
            HashSet<string> boughtByAll = new HashSet<string>(allBuyers);
            HashSet<string> boughtBySome = new HashSet<string>();
            HashSet<string> boughtByNone = new HashSet<string>(allBuyers);

            foreach (var factory in purchases.Values)
            {
                boughtByAll.IntersectWith(factory); // пересечение для "купивших у всех"
                boughtBySome.UnionWith(factory);   // объединение для "купивших у некоторых"
            }

            boughtByNone.ExceptWith(boughtBySome); // разница для "не купивших ни у кого"

            // вывод результатов
            Console.WriteLine("\nАнализ покупок по фабрикам:");
            foreach (var factory in purchases)
            {
                Console.WriteLine($"\nФабрика: {factory.Key}");
                Console.WriteLine("Покупатели: " + string.Join(", ", factory.Value));
            }

            Console.WriteLine("\nОбщие результаты:");
            Console.WriteLine("Покупатели, купившие мебель всех фабрик: " +
                (boughtByAll.Count > 0 ? string.Join(", ", boughtByAll) : "нет"));
            Console.WriteLine("Покупатели, купившие мебель хотя бы одной фабрики: " +
                (boughtBySome.Count > 0 ? string.Join(", ", boughtBySome) : "нет"));
            Console.WriteLine("Покупатели, не купившие мебель ни одной фабрики: " +
                (boughtByNone.Count > 0 ? string.Join(", ", boughtByNone) : "нет"));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    // 9 задание
    public static void AnalyzeRussianConsonants(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден!");
                return;
            }

            char[] deafConsonantsArray = { 'п', 'ф', 'к', 'т', 'ш', 'с', 'х', 'ц', 'ч', 'щ' };
            HashSet<char> deafConsonants = new HashSet<char>(deafConsonantsArray);

            string text = File.ReadAllText(filePath).ToLower();
            char[] separators = { ' ', ',', '.', '!', '?', ';', ':', '\n', '\r', '\t', '-', '—', '(', ')', '"' };
            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length < 2)
            {
                Console.WriteLine("Текст должен содержать как минимум 2 слова!");
                return;
            }

            List<HashSet<char>> oddWordsConsonants = new List<HashSet<char>>();
            HashSet<char> evenWordsConsonants = new HashSet<char>();

            for (int i = 0; i < words.Length; i++)
            {
                HashSet<char> currentConsonants = new HashSet<char>();
                foreach (char c in words[i])
                {
                    if (deafConsonants.Contains(c))
                    {
                        currentConsonants.Add(c);
                    }
                }

                if ((i + 1) % 2 == 1)
                {
                    oddWordsConsonants.Add(currentConsonants);
                }
                else
                {
                    foreach (char c in currentConsonants)
                    {
                        evenWordsConsonants.Add(c);
                    }
                }
            }

            HashSet<char> commonInOdd = new HashSet<char>();
            if (oddWordsConsonants.Count > 0)
            {
                commonInOdd = new HashSet<char>(oddWordsConsonants[0]);
                for (int i = 1; i < oddWordsConsonants.Count; i++)
                {
                    HashSet<char> temp = new HashSet<char>();
                    foreach (char c in commonInOdd)
                    {
                        if (oddWordsConsonants[i].Contains(c))
                        {
                            temp.Add(c);
                        }
                    }
                    commonInOdd = temp;
                }
            }

            List<char> result = new List<char>();
            foreach (char c in commonInOdd)
            {
                if (!evenWordsConsonants.Contains(c))
                {
                    result.Add(c);
                }
            }

            result.Sort();

            Console.WriteLine("\nРезультат анализа:");
            if (result.Count > 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    if (i > 0)
                    {
                        Console.Write(", ");
                    }
                    Console.Write(result[i]);
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Нет глухих согласных, удовлетворяющих условиям.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

// 10 задание
public static void AnalyzeCheapestSourCream(string filePath)
{
    try
    {
        // словарь для хранения минимальной цены и количества магазинов для каждой жирности
        Dictionary<int, (int minPrice, int count)> fatToMinPrice = new Dictionary<int, (int, int)>
        {
            { 15, (int.MaxValue, 0) },
            { 20, (int.MaxValue, 0) },
            { 25, (int.MaxValue, 0) }
        };

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        foreach (string line in File.ReadLines(filePath))
        {
            string[] parts = line.Split(' ');

            if (parts.Length != 4)
            {
                Console.WriteLine($"Некорректная строка в файле: {line}");
                continue;
            }

            // парсинг данных
            string firm = parts[0];
            string street = parts[1];

            if (!int.TryParse(parts[2], out int fat) || !int.TryParse(parts[3], out int price))
            {
                Console.WriteLine($"Некорректные данные о жирности или цене в строке: {line}");
                continue;
            }

            if (fat != 15 && fat != 20 && fat != 25 || price < 2000 || price > 5000)
            {
                Console.WriteLine($"Некорректные значения жирности или цены в строке: {line}");
                continue;
            }

            // обновление словаря
            var current = fatToMinPrice[fat];
            if (price < current.minPrice)
            {
                fatToMinPrice[fat] = (price, 1); // новая минимальная цена
            }
            else if (price == current.minPrice)
            {
                fatToMinPrice[fat] = (current.minPrice, current.count + 1); 
            }
        }

        
        Console.WriteLine("Количество магазинов с минимальной ценой:");
        foreach (var fat in new[] { 15, 20, 25 })
        {
            int count = fatToMinPrice[fat].minPrice == int.MaxValue ? 0 : fatToMinPrice[fat].count;
            Console.Write(count + " ");
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Ошибка: {ex.Message}");
    }
}

    // вспомогательное
    private static List<char> GenerateRandomCharList(int length)
    {
        List<char> chars = new List<char>();
        for (int i = 0; i < length; i++)
        {
            chars.Add(CharPool[_random.Next(0, CharPool.Length)]);
        }
        return chars;
    }

    private static string ListToString(List<char> list)
    {
        return string.Join(" ", list);
    }

    private static string LinkedListToString(LinkedList<char> list)
    {
        return string.Join(" ", list);
    }

    public static void PrintFileContents(string filePath)
    {
        try
        {
            if (!File.Exists(filePath)) return;
            Console.WriteLine("\nСодержимое файла:");
            Console.WriteLine(File.ReadAllText(filePath));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    public static void PrintBinaryFileContents(string filePath)
    {
        try
        {
            Console.WriteLine("\nСодержимое бинарного файла:");
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    Console.Write(reader.ReadInt32() + " ");
                }
            }
            Console.WriteLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    public static void PrintToysFileContents(string filePath)
    {
        try
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
            Toy[] toys;
            
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                toys = (Toy[])serializer.Deserialize(fs);
            }

            Console.WriteLine("\nВсе игрушки в файле:");
            foreach (Toy toy in toys)
            {
                Console.WriteLine(toy);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
