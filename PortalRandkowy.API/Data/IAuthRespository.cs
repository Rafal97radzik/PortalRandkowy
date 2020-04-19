using PortalRandkowy.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalRandkowy.API.Data
{
    public interface IAuthRespository
    {
        Task<User> Login(string username, string password);

        Task<User> Register(User user, string password);

        Task<bool> UserExists(string username);
    }
}
