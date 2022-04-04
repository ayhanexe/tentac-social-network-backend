using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Data.Implementation.EfCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TentacSocialPlatformApi.Controllers.Abstraction;

namespace TentacSocialPlatformApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PostController : BaseController<Post, int, PostDto, EfCorePostRepository>
    {
        private readonly EfCorePostRepository _postRepository;
        private readonly EfCoreUserPostsRepository _userPostsRepository;
        private readonly AppDbContext _context;

        public PostController(EfCorePostRepository repository, EfCoreUserPostsRepository userPostsRepository, IMapper mapper, AppDbContext context) : base(repository, mapper)
        {
            _postRepository = repository;
            _userPostsRepository = userPostsRepository;
            _context = context;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            return await _context.Posts.Where(p => !p.isDeleted).Include(p => p.User).Include(p => p.PostLikes).ToListAsync();
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<Post>> Get(int id)
        {
            return await _context.Posts.Include(p => p.User).Include(p => p.PostLikes).Where(p => !p.isDeleted && p.Id == id).FirstOrDefaultAsync();
        }


        [HttpPost]
        [Authorize(Roles = "superuser, admin, moderator, user")]
        public override async Task<IActionResult> Add([FromBody] PostDto entity)
        {
            var user = await _context.Users.FindAsync(entity.UserId);

            if (user != null)
            {
                var post = new Post
                {
                    Text = entity.Text,
                    User = user
                };
                var userPost = new UserPosts
                {
                    Post = post,
                    User = user
                };

                await _postRepository.Add(post);
                await _userPostsRepository.Add(userPost);

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public override async Task<IActionResult> Delete(int id)
        {
            var userPosts = await _context.UserPosts.Include(up => up.Post).Where(up => up.Post.Id == id).FirstOrDefaultAsync();
            var post = await _context.Posts.Where(up => up.Id == id).FirstOrDefaultAsync();


            if (post == null)
            {
                return NotFound();
            }

            _context.UserPosts.Remove(userPosts);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(post);
        }
    }
}
