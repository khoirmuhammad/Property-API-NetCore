using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyAPI.Data.Interfaces;
using PropertyAPI.Models;

namespace PropertyAPI.Data.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User?> Authenticate(string username, string passwordText, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(
                f => f.Username.Equals(username), cancellationToken);

            if (user == null)
                return null;

            bool isPasswordMatch = CheckPasswordMatch(passwordText, user.Password, user.PasswordKey);

            if (!isPasswordMatch)
                return null;
            else
                return user;
        }

        public async Task<bool> CheckUserExist(string username, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(a => a.Username.Equals(username), cancellationToken);
        }

        public async void Register(string username, string password, CancellationToken cancellationToken)
        {
            byte[] passwordHash, passwordKey;

            using(var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            await _context.Users.AddAsync(new User{
                Username = username,
                Password = passwordHash,
                PasswordKey = passwordKey
            }, cancellationToken);
        }
    
        private bool CheckPasswordMatch(string passwordText, byte[] passwordHash, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var password = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordText));

                for(int i = 0; i < password.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }
            }

            return true;
        }
    }
}