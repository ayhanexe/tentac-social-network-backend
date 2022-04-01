using DomainModels.Dtos;
using DomainModels.Entities;
using Repository.DAL;
using Repository.Data.Abstraction;

namespace Repository.Data.Implementation.EfCore
{
    public class EfCoreUserRepository : EFCoreRepository<User, string, UserDto, AppDbContext>
    {
        public EfCoreUserRepository(AppDbContext context) : base(context) {}
    }
}
