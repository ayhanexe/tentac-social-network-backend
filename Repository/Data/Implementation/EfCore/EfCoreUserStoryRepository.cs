using DomainModels.Dtos;
using DomainModels.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Implementation.EfCore
{
    public class EfCoreUserStoryRepository : EFCoreRepository<UserStories, int, StoryDto, AppDbContext>
    {
        private readonly AppDbContext _context;
        public EfCoreUserStoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<UserStories> Get(int id)
        {
            return await _context.UserStories.Include(us => us.User).Include(us => us.Story).Where(us => us.Id == id).FirstOrDefaultAsync();
        }

        public override async Task<List<UserStories>> GetAll()
        {
            return await _context.UserStories.Include(us => us.User).Include(us => us.Story).ToListAsync();
        }
    }


}
