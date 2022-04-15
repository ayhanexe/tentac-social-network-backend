using DomainModels.Dtos;
using DomainModels.Entities;
using Repository.DAL;
using Repository.Data.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Implementation.EfCore
{
    public class EfCoreUserFriendsRepository : EFCoreRepository<UserFriends, int, UserFriendDto, AppDbContext>
    {
        private readonly AppDbContext _context;
        public EfCoreUserFriendsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
