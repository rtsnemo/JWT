using JWT.Data;
using JWT.Models;
using Microsoft.AspNetCore.Identity;


namespace JWT.Services
{
    public class AuthService
    {
        private readonly JwtGenerator _jwtGenerator;
        private readonly UserContext _userContext;
        private readonly PasswordHasher _passwordHasher;

        public AuthService(UserContext userContext, JwtGenerator jwtGenerator, PasswordHasher passwordHasher)
        {
            _jwtGenerator = jwtGenerator;
            _userContext = userContext;
            _passwordHasher = passwordHasher;
        }

        public string AuthenticateUser(string username, string enteredPassword)
        {
            var user = _userContext.Users.FirstOrDefault(u => u.Name == username);

            if (IsValidUser(username, user.Password) && _passwordHasher.VerifyPassword(enteredPassword, user.Password, user.Salt))
            {
                var userId = GetUserIdByUsername(username);
                return _jwtGenerator.GenerateJwtToken(userId?.ToString(), username);
            }

            return null; // Возвращаем null в случае неудачной аутентификации
        }

        private bool IsValidUser(string username, string password)
        {
            return _userContext.Users.Any(u => u.Name == username && u.Password == password);
        }

        private int? GetUserIdByUsername(string username)
        {
            var user = _userContext.Users.FirstOrDefault(u => u.Name == username);
            return user?.Id;
        }
    }
}
