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
    public class EfCoreUserRepository : EFCoreRepository<User, string, UserDto, AppDbContext>
    {

        private readonly AppDbContext _context;
        public EfCoreUserRepository(AppDbContext context) : base(context) {
            _context = context;
        }

        public override async Task<User> Get(string id)
        {
            return await _context.Users.Where(u => u.Id == id).Include(u => u.ProfilePhotos.Where(pp => !pp.isDeleted)).Include(u => u.UserWalls.Where(pp => !pp.isDeleted)).FirstOrDefaultAsync();
        }

        public override async Task<List<User>> GetAll()
        {
            return await _context.Users.Include(u => u.ProfilePhotos).Include(u => u.UserWalls).ToListAsync();
        }
    }
}
