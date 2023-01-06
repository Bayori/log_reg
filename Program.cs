using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using AuthExceptions;
using Properties;
namespace log_reg
{
    internal class Program
    {
        User user = new User();
        public static string path = @"C:\Users\юрка\Desktop\usernames\usernames.txt";
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
            User user = new User();

            Console.WriteLine("Введите логин: ");
            string login = Console.ReadLine();
            user.propLogin = login;
            if (string.IsNullOrEmpty(user.propLogin))
            {
                throw new IncorrectLoginException("Логин введён некорректно");
            }
            if (credentials.ContainsKey(user.propLogin))
            {
                throw new UserAlreadeExistException("Пользователь уже существует");
            }

            Console.WriteLine("Введите пароль: ");
            user.propPassword = Console.ReadLine();
            if (string.IsNullOrEmpty(user.propPassword))
            {
                throw new IncorrectPasswordException("Пароль введён некорректно");
            }

            Console.WriteLine("Введите пароль ещё раз: ");
            string userPasswordConfrim = Console.ReadLine();

            if (userPasswordConfrim != user.propPassword)
            {
                throw new PasswordConfirmMismatchException("Пароли не совпадают");
            }
            using (StreamWriter vf = new StreamWriter(path, true)) // Путь до файла
            {
                vf.WriteLine("{0}:{1}", user.propLogin, userPasswordConfrim); // Добавление строки в файл
            }
            Console.WriteLine("Успешная регистрация");
            return;
        }

        static void Main(string[] args)
        {
            User user = new User();
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
                        return;
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
