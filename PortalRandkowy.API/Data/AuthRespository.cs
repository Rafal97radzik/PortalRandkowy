using PortalRandkowy.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace PortalRandkowy.API.Data
{
    public class AuthRespository : IAuthRespository
    {
        private DataContext context;

        public AuthRespository(DataContext context)
        {
            this.context = context;
        }

        #region method public
        public async Task<User> Login(string username, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(u=>u.Username==username);

            if(user == null)
                return null;

            if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
                return null;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHashSalt(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await context.Users.AnyAsync(x => x.Username == username);
        }
        #endregion

        #region method private
        private void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            } 
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (HMACSHA512 hmac = new HMACSHA512(passwordSalt))
            {
                byte[] computtedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computtedHash.Length; i++)
                {
                    if (computtedHash[i] != passwordHash[i])
                        return false;
                }

                return true;
            }
        }
        #endregion
    }
}
