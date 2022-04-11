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
            var user = await _context.Users.Where(u => u.Id == id)
                .Include(u => u.UserPosts)
                .ThenInclude(u => u.Post)
                .ThenInclude(u => u.PostLikes)
                .Include(u => u.UserPosts)
                .ThenInclude(u => u.Post)
                .ThenInclude(u => u.PostReplies)
                .ThenInclude(u => u.User)
                .Include(u => u.UserPosts)
                .ThenInclude(u => u.Post)
                .ThenInclude(u => u.PostReplies)
                .ThenInclude(u => u.PostLikes)
                .Include(u => u.UserPosts)
                .ThenInclude(u => u.Post)
                .ThenInclude(u => u.User)
                .FirstOrDefaultAsync();

            return user;
        }

        public override async Task<List<User>> GetAll()
        {
            return await _context.Users
                .Include(u => u.UserPosts)
                .ThenInclude(u => u.Post)
                .ThenInclude(u => u.PostLikes)
                .Include(u => u.UserPosts)
                .ThenInclude(u => u.Post)
                .ThenInclude(u => u.PostReplies)
                .ThenInclude(u => u.User)
                .Include(u => u.UserPosts)
                .ThenInclude(u => u.Post)
                .ThenInclude(u => u.PostReplies)
                .ThenInclude(u => u.PostLikes)
                .Include(u => u.UserPosts)
                .ThenInclude(u => u.Post)
                .ThenInclude(u => u.User).ToListAsync();
        }
    }
}
