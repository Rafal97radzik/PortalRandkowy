using Microsoft.EntityFrameworkCore;
using PortalRandkowy.API.Helpers;
using PortalRandkowy.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalRandkowy.API.Data
{
    public class UserRepository:GenericRepository, IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context):base(context)
        {
            this.context = context;
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            var photo = await context.Photos.Where(p => p.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
            return photo;
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = context.Users.Include(p => p.Photos).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);
            users= users.Where(u => u.Gender == userParams.Gender);

            if (userParams.MinAge!=18 || userParams.MaxAge!=100)
            {
                var minDate = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDate = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDate && u.DateOfBirth <= maxDate);
            }

            if (!string.IsNullOrEmpty(userParams.ZodiacSing))
            {
                users = users.Where(u => u.ZodiacSing == userParams.ZodiacSing);
            }

            return await PagedList<User>.CreateListAsync(users, userParams.PageNumber, userParams.PageSize);
        }
    }
}
