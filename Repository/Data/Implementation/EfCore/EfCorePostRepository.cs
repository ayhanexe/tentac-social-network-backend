using DomainModels.Dtos;
using DomainModels.Entities;
using Repository.DAL;
using Repository.Data.Abstraction;
using System;
using System.Threading.Tasks;

namespace Repository.Data.Implementation.EfCore
{
    public class EfCorePostRepository : EFCoreRepository<Post, int, PostDto, AppDbContext>
    {
        private readonly AppDbContext _context;

        public EfCorePostRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async override Task<Post> Add(Post entity)
        {
            try
            {

                await _context.Set<Post>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                return null;
            }
        }
    }
}
