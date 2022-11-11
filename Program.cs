using System;
using System.Collections.Generic;
using System.IO;

namespace log_reg
{
    internal class Program
    {
        static int auth(string login, string password, Dictionary<string, string> credentials)
        {
            if (credentials.ContainsKey(login)) // Проверяем ключ+значение в словаре 
            {
                if (credentials.ContainsValue(password))
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
        static void register(string path, Dictionary<string, string> credentials)
        {
            Console.WriteLine("Введите логин: ");
            string userLogin = Console.ReadLine();
            while (credentials.ContainsKey(userLogin))
            {
                Console.WriteLine("Такой логин уже существует!");
                userLogin = Console.ReadLine();
            }

            Console.WriteLine("Введите пароль: ");
            string userPassword = Console.ReadLine();
            Console.WriteLine("Введите пароль ещё раз: ");
            string userPasswordConfrim = Console.ReadLine();

            while (userPasswordConfrim != userPassword)
            {
                Console.WriteLine("Повторите ввод пароля [1 - для выхода]");
                userPassword = Console.ReadLine();
                if (userPassword == "1") return;
                Console.WriteLine("Введите пароль ещё раз: ");
                userPasswordConfrim = Console.ReadLine();
                if (userPasswordConfrim == "1") return;

            }
            using (StreamWriter vf = new StreamWriter(path, true)) // Путь до файла
            {
                vf.WriteLine("{0}:{1}", userLogin, userPasswordConfrim); // Добавление строки в файл
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

            Dictionary<string, string> credentials = new Dictionary<string, string>(); // Создаём словарь
            string[] data;
            using (StreamReader sr = new StreamReader(path)) // Вносим каждую строчку в словарь, предварительно поделив ключ и значение через ':'
            {
                while (!sr.EndOfStream)
                {
                    data = sr.ReadLine().Split(':');
                    credentials.Add(data[0], data[1]); // Возможно сократить в одну строчку, но будет крайне не читабельно
                }
            }
            int menuLogin = -1;
            do
            {
                Console.WriteLine("Введите логин:");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль:");
                string password = Console.ReadLine();
                int code = auth(login, password, credentials);
                if (code == 1)
            {
                Console.WriteLine("Пароль неверный [0 - для регистрации, 1 - для повторного ввода]");
                menuLogin = Convert.ToInt32(Console.ReadLine());
            }
                else if (code == 0)
            {
                Console.WriteLine("Логина не существует [0 - для регистрации, 1 - для повторного ввода]");
                menuLogin = Convert.ToInt32(Console.ReadLine());
            }
          
            } while (menuLogin == 1);

            if (menuLogin == 0)
                {
                    Console.WriteLine("Регистрация...");
                register(path, credentials);
                } 
        }
    }
}
