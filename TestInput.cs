using System;

public static class TestInput
{
    public static int GetInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int result) && result >= min && result <= max)
                return result;
            Console.WriteLine($"Ошибка! Введите число от {min} до {max}");
        }
    }

    public static decimal GetDecimal(string prompt, decimal min, decimal max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out decimal result) && result >= min && result <= max)
                return result;
            Console.WriteLine($"Ошибка! Введите число от {min} до {max}");
        }
    }

    public static string GetFilePath(string prompt, bool mustExist = false)
    {
        while (true)
        {
            Console.Write(prompt);
            string path = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Путь не может быть пустым!");
                continue;
            }

            if (!mustExist || File.Exists(path))
                return path;

            Console.WriteLine("Файл не существует!");
        }
    }

    public static string GetString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}