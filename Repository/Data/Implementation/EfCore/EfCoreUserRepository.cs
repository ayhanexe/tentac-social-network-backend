using DomainModels.Dtos;
using DomainModels.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Data.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Data.Implementation.EfCore
{
    public class EfCoreUserRepository : EFCoreRepository<User, string, UserDto, AppDbContext>
    {
        private readonly AppDbContext _context;
        public EfCoreUserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<User> Get(string id)
        {
            var user = await _context.Users
                .Include(u => u.UserStories)
                .ThenInclude(u => u.User)
                .Include(u => u.UserStories)
                .ThenInclude(u => u.Story)
                .Include(u => u.Notifications)
                .Where(u => u.Id == id).FirstOrDefaultAsync();

            return user;
        }

        public override async Task<List<User>> GetAll()
        {
            return await _context.Users
                .Include(u => u.UserStories)
                .ThenInclude(u => u.User)
                .Include(u => u.UserStories)
                .ThenInclude(u => u.Story)
                .Include(u => u.Notifications)
                .ToListAsync();
        }
    }
}
