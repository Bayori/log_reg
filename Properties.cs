using System;
using System.Text.RegularExpressions;

namespace Properties
{
    public class User // Класс для данных
    {
        private string login, password; // Поля

	public string propLogin // Свойства login
	{
            get // Геттер, возвращает значение
            { 
                return login; 
            } 

            set // Сеттер, проверка данных
	    {
                if (value.Length < 6 || value.Length > 20) // Длина строки
		{ 
                    return;
                }

                if (Regex.IsMatch(value, "[A-Z ]")) // Если есть заглавные буквы или пробелы
                {
                    return;
                }

		login = value; // При успешной проверке - присваивание значения
		}
	     }


		public string propPassword // Свойства password
		{
	    get 
            { 
                return password; 
            } // Геттер, возвращает значение

            set // Сеттер, проверка данных
	    {
                if (value.Length < 6 || value.Length > 20) // Длина строки
                {
                    return;
                }
                if (Regex.IsMatch(value, " ")) // Проверка на пробелы
                {
                    return;
                }
                if (!Regex.IsMatch(value, "[A-Z]")) // Проверка на верхний регистр
                {
                    return;
                }
                password = value; // При успешной проверке - присваивание значения
            }
	}
    }

}
