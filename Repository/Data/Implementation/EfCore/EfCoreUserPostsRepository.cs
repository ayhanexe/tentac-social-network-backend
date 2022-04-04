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
    public class EfCoreUserPostsRepository : EFCoreRepository<UserPosts, int, PostDto, AppDbContext>
    {
        public EfCoreUserPostsRepository(AppDbContext context) : base(context) { }

    }
}
