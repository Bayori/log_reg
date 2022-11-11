using System;
using System.Collections.Generic;
using System.IO;

namespace log_reg
{
    internal class Program
    {
        static int auth(string login, string password, Dictionary<string, string> usernames)
        {
            if (usernames.ContainsKey(login)) // Проверяем ключ+значение в словаре 
            {
                if (usernames.ContainsValue(password))
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
            else return 0;
        }
        static void register(string path, Dictionary<string, string> usernames)
        {

            int i = 0;
            Console.WriteLine("Введите логин: ");
            string writeLogin = Console.ReadLine();
            while (usernames.ContainsKey(writeLogin))
            {
                Console.WriteLine("Такой логин уже существует!");
                writeLogin = Console.ReadLine();
            }

            Console.WriteLine("Введите пароль: ");
            string writePassword = Console.ReadLine();
            Console.WriteLine("Введите пароль ещё раз: ");
            string writePassword2 = Console.ReadLine();

            while (writePassword2 != writePassword)
            {
                Console.WriteLine("Повторите повторный ввод пароля [1 - для выхода]");
                writePassword2 = Console.ReadLine();
                if (writePassword2 == "1") return;

            }
            using (StreamWriter vf = new StreamWriter(path, true)) // Путь до файла
            {
                vf.WriteLine("\n{0}:{1}", writeLogin, writePassword2); // Добавление строки в файл
            }
            Console.WriteLine("Успешная регистрация");
        }

        static void Main(string[] args)
        {
            // Коды:
            // 0 - Логина не существует
            // 1 - Пароль введен неверно
            // 2 - Успешная авторизация

            string path = @"C:\Users\super\OneDrive\Desktop\usernames\usernames.txt"; // Путь к файлу

            Dictionary<string, string> usernames = new Dictionary<string, string>(); // Создаём словарь
            string[] data;
            using (StreamReader sr = new StreamReader(path)) // Вносим каждую строчку в словарь, предварительно поделив ключ и значение через ':'
            {
                while (!sr.EndOfStream)
                {
                    data = sr.ReadLine().Split(':');
                    usernames.Add(data[0], data[1]); // Возможно сократить в одну строчку, но будет крайне не читабельно
                }
            }
            Console.WriteLine("Введите логин:");
            string login = Console.ReadLine();
            Console.WriteLine("Введите пароль:");
            string password = Console.ReadLine();
            int code = auth(login, password, usernames);
            if (code == 2)
            {
                Console.WriteLine("Успешная авторизация");
                return;
            }
            else if (code == 1)
            {
                Console.WriteLine("Пароль неверный");
                return;
            }
            else if (code == 0)
            {
                Console.WriteLine("Логина не существует, зарегистрируйтесь");
                register(path, usernames);
            }
        }
    }
}
