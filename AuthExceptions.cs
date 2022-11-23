using System;
namespace AuthExceptions
{
    public class UserDoesNotExistsException : Exception // Пользователя не существует
    {
        public UserDoesNotExistsException(string message) : base(message) { }
    }
    public class IncorrectPasswordException : Exception // Пароль неверный
    {
        public IncorrectPasswordException(string message) : base(message) { }
    }
    public class PasswordConfirmMismatchException : Exception // Пароли не совпадают
    {
        public PasswordConfirmMismatchException(string message) : base(message) { }
    }
    public class UserAlreadeExistException : Exception // Пользователь существует
    {
        public UserAlreadeExistException(string message) : base(message) { }
    }
    public class IncorrectLoginException : Exception // Логин некорректный
    {
        public IncorrectLoginException(string message) : base(message) { }
    }
}