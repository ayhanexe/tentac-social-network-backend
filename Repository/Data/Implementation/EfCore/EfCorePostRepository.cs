using DomainModels.Dtos;
using DomainModels.Entities;
using Repository.DAL;
using Repository.Data.Abstraction;
using System.Threading.Tasks;

namespace Repository.Data.Implementation.EfCore
{
    public class EfCorePostRepository : EFCoreRepository<Post, string, PostDto, AppDbContext>
    {
        private readonly EfCoreUserPostsRepository _userRepository;

        public EfCorePostRepository(AppDbContext context, EfCoreUserPostsRepository userPostsRepository) : base(context) {
            _userRepository = userPostsRepository;
        }

        public async override Task<Post> Add(Post entity)
        {
            await base.Add(entity);

            var userPost = new UserPosts
            {
                Post = entity,
                User = entity.User
            };

            await _userRepository.Add(userPost);

            return entity;
        }

    }
}
