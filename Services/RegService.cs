using JWT.Data;
using JWT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JWT.Services
{
    public class RegService
    {
        private readonly UserContext _userContext;
        private readonly PasswordHasher _passwordHasher;

        public RegService(UserContext userContext, PasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _userContext = userContext;
        }

        public async Task<bool> RegisterUserAsync(RegModel user)
        {
            var username = user.Name;
            var email = user.Email;
            var password = _passwordHasher.HashPassword(user.Password);

            // Проверяем, существует ли пользователь с таким именем
            if (await UserExistsAsync(username))
            {
                return false; // Пользователь уже существует
            }

            // Создаем нового пользователя
            var newUser = new User
            {
                Name = username,
                Email = email,
                Password = password.Hash, // В реальном приложении следует хешировать пароль
                Salt = password.Salt
            };

            // Добавляем пользователя в базу данных
            _userContext.Users.Add(newUser);
            await _userContext.SaveChangesAsync();

            return true; // Регистрация успешна
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            // Проверяем, существует ли пользователь с заданным именем
            return await _userContext.Users.AnyAsync(u => u.Name == username);
        }
    }
}
