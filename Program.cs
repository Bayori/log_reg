using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using AuthExceptions;

namespace log_reg
{
    internal class Program
    {
        public static string path = @"C:\Users\super\OneDrive\Desktop\usernames\usernames.txt";
        public static Dictionary<string, string> credentials = new Dictionary<string, string>();

        static int auth(string login, string password, Dictionary<string, string> credentials)
        {
            if (credentials.ContainsKey(login)) // Проверяем ключ+значение в словаре 
            {
                if (credentials.ContainsValue(password))
                {
                    return 0;
                }
                else
                {
                    throw new IncorrectPasswordException("Пароль введён неверно");
                }
            }
            else throw new UserDoesNotExistsException("Пользователь не найден");
        }
        static void register(Dictionary<string, string> credentials)
        {
            Console.WriteLine("Введите логин: ");
            string userLogin = Console.ReadLine();
            if (!isValidLogin(userLogin))
            {
                throw new IncorrectLoginException("Логин введён некорректно");
            }
            if (credentials.ContainsKey(userLogin))
            {
                throw new UserAlreadeExistException("Пользователь уже существует");
            }

            Console.WriteLine("Введите пароль: ");
            string userPassword = Console.ReadLine();
            if (!isValidPassword(userPassword))
            {
                throw new IncorrectPasswordException("Пароль введён некорректно");
            }

            Console.WriteLine("Введите пароль ещё раз: ");
            string userPasswordConfrim = Console.ReadLine();

            if (userPasswordConfrim != userPassword)
            {
                throw new PasswordConfirmMismatchException("Пароли не совпадают");
            }
            using (StreamWriter vf = new StreamWriter(path, true)) // Путь до файла
            {
                vf.WriteLine("{0}:{1}", userLogin, userPasswordConfrim); // Добавление строки в файл
            }
            Console.WriteLine("Успешная регистрация");
            return;
        }

        static bool isValidLogin(string login)
        {

            if (login.Length < 6 || login.Length > 20) // Длина строки
            {
                return false;
            }

            if (Regex.IsMatch(login, "[A-Z ]")) // Если есть заглавные буквы или пробелы
            {
                return false;
            }
            return true;
        }

        static bool isValidPassword(string password)
        {
            if (password.Length < 6 || password.Length > 20) // Длина строки
            {
                return false;
            }
            if (Regex.IsMatch(password, " ")) // Проверка на пробел.ы
            {
                return false;
            }
            if (!Regex.IsMatch(password, "[A-Z]")) // Проверка на верхний регистр
            {
                return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            // Коды:
            // 0 - Логина не существует
            // 1 - Пароль введен неверно
            // 2 - Успешная авторизация

            using (StreamReader sr = new StreamReader(path)) // Вносим каждую строчку в словарь, предварительно поделив ключ и значение через ':'
            {
                string[] data;
                while (!sr.EndOfStream)
                {
                    data = sr.ReadLine().Split(':');
                    credentials.Add(data[0], data[1]); // Возможно сократить в одну строчку, но будет крайне не читабельно
                }
            }

            int menu = -1;
            do
            {
                try
                {
                    Console.WriteLine("Введите логин:");
                    string login = Console.ReadLine();
                    Console.WriteLine("Введите пароль:");
                    string password = Console.ReadLine();
                    int loginResultCode = auth(login, password, credentials);
                }
                catch (UserDoesNotExistsException ex)
                {
                    Console.WriteLine(ex.Message+ " [0 - для регистрации, 1 - для повторного ввода]");
                    menu = Convert.ToInt32(Console.ReadLine());
                }
                catch (IncorrectPasswordException ex)
                {
                    Console.WriteLine(ex.Message+ " [0 - для регистрации, 1 - для повторного ввода]");
                    menu = Convert.ToInt32(Console.ReadLine());
                }
            } while (menu == 1);

            string menuNavigation = " [0 - для выхода, 1 - для повторного ввода]";
            if (menu == 0)
            {
                menu = 1;
                do
                {
                    Console.WriteLine("Регистрация...");
                    try
                    {
                        register(credentials);
                    }
                    catch (UserAlreadeExistException ex)
                    {
                        Console.WriteLine(ex.Message + menuNavigation);
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (PasswordConfirmMismatchException ex)
                    {
                        Console.WriteLine(ex.Message + menuNavigation);
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (IncorrectPasswordException ex)
                    {
                        Console.WriteLine(ex.Message + menuNavigation);
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (IncorrectLoginException ex)
                    {
                        Console.WriteLine(ex.Message + menuNavigation);
                        menu = Convert.ToInt32(Console.ReadLine());
                    }
                } while (menu == 1);
            }
        }
    }
}
