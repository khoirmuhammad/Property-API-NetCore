using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PropertyAPI.Models;

namespace PropertyAPI.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> Authenticate(string username, string passwordText, CancellationToken cancellationToken);
        void Register(string username, string password, CancellationToken cancellationToken);
        Task<bool> CheckUserExist(string username, CancellationToken cancellationToken);
    }
}