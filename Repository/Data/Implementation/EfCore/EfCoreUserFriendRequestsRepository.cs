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
    public class EfCoreUserFriendRequestsRepository : EFCoreRepository<UserFriendRequests, string, UserFriendRequestDto, AppDbContext>
    {
        private readonly AppDbContext _context;
        public EfCoreUserFriendRequestsRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}