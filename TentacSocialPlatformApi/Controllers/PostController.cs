using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Data.Implementation.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TentacSocialPlatformApi.Controllers.Abstraction;
using TentacSocialPlatformApi.PostModels;

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
            var posts = await _context.Posts.Where(p => !p.isDeleted)
                .Include(p => p.User)

                .Include(p => p.PostLikes)
                .Include(p => p.PostReplies)
                .ThenInclude(p => p.User)
                .Include(p => p.PostReplies)
                .ThenInclude(p => p.PostLikes)
                .ToListAsync();
            return posts;
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<Post>> Get(int id)
        {
            var post = await _context.Posts.Where(p => !p.isDeleted && p.Id == id)
                .Include(p => p.User)
                .Include(p => p.PostLikes)
                .Include(p => p.PostReplies)

                .ThenInclude(p => p.User)
                .Include(p => p.PostReplies)
                .ThenInclude(p => p.PostLikes)
                .FirstOrDefaultAsync();
            return post;
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
            try
            {
                var userPosts = await _context.UserPosts.Include(up => up.Post).Where(up => up.Post.Id == id).FirstOrDefaultAsync();
                var post = await _context.Posts.Include(p => p.PostReplies).Where(up => up.Id == id).FirstOrDefaultAsync();
                var replies = await _context.PostReplies.Include(p => p.Post).Where(p => p.Post.Id == id).ToListAsync();
                var likes = await _context.PostLikes.Include(p => p.Post).Where(p => p.Post.Id == id).ToListAsync();

                foreach (var reply in replies)
                {
                    _context.PostReplies.Remove(reply);
                }

                foreach (var like in likes)
                {
                    _context.PostLikes.Remove(like);
                }

                if (post == null)
                {
                    return NotFound();
                }

                _context.UserPosts.Remove(userPosts);
                _context.Posts.Remove(post);

                await _context.SaveChangesAsync();

                return Ok(post);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("like/{id}")]
        public async Task<IActionResult> Like(int id, [FromBody] PostLikeModel model)
        {
            var post = await _context.Posts.Where(p => !p.isDeleted && p.Id == id).FirstOrDefaultAsync();
            var user = await _context.Users.FindAsync(model.userId);
            var existingPostLike = await _context.PostLikes
                .Include(pl => pl.Post)
                .Include(pl => pl.User)
                .Where(pl => pl.Post.Id == id && pl.User.Id == model.userId).FirstOrDefaultAsync();

            if (existingPostLike == null)
            {
                var postLike = new PostLike
                {
                    Post = post,
                    User = user
                };

                await _context.PostLikes.AddAsync(postLike);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("dislike/{id}")]
        public async Task<IActionResult> Dislike(int id, [FromBody] PostLikeModel model)
        {
            var existingPostLike = await _context.PostLikes
                .Include(pl => pl.Post)
                .Include(pl => pl.User)
                .Where(pl => pl.Post.Id == id && pl.User.Id == model.userId).FirstOrDefaultAsync();

            if (existingPostLike != null)
            {
                _context.PostLikes.Remove(existingPostLike);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("{id}/Reply")]
        public async Task<IActionResult> Reply(int id, [FromBody] PostReplyModel model)
        {
            var post = await _context.Posts.Where(p => !p.isDeleted && p.Id == id).FirstOrDefaultAsync();
            var user = await _context.Users.FindAsync(model.UserId);

            var reply = new PostReplies
            {
                Post = post,
                User = user,
                Text = model.Text
            };

            await _context.PostReplies.AddAsync(reply);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{id}/deleteReply")]
        public async Task<IActionResult> DeleteReply(int id, [FromBody] PostDeleteReplyModel model)
        {
            var postReply = await _context.PostReplies
                .Include(p => p.Post)
                .Where(p => p.Post.Id == id && p.Id == model.replyId).FirstOrDefaultAsync();

            _context.PostReplies.Remove(postReply);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPost("likeReply/{id}")]
        public async Task<IActionResult> ReplyLike(int id, [FromBody] ReplyLikeModel model)
        {
            try
            {

                var post = await _context.Posts.Where(p => !p.isDeleted && p.Id == id).FirstOrDefaultAsync();
                var user = await _context.Users.FindAsync(model.userId);
                var reply = await _context.PostReplies.Include(p => p.Post).Where(p => p.Id == model.replyId && p.Post.Id == id).FirstOrDefaultAsync();
                var test = await _context.ReplyLikes.ToListAsync();
                var existingReplyLike = await _context.ReplyLikes
                    .Include(pl => pl.User)
                    .Include(pl => pl.PostReply)
                    .Where(pl => pl.User.Id == model.userId && pl.PostReply.Id == model.replyId).FirstOrDefaultAsync();

                if (existingReplyLike == null)
                {
                    var replyLike = new ReplyLikes
                    {
                        PostReply = reply,
                        User = user
                    };

                    await _context.ReplyLikes.AddAsync(replyLike);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("dislikeReply/{id}")]
        public async Task<IActionResult> Dislike(int id, [FromBody] ReplyLikeModel model)
        {
            var existingReplyLike = await _context.ReplyLikes
                .Include(pl => pl.User)
                .Include(pl => pl.PostReply)
                .Where(pl => pl.User.Id == model.userId && pl.PostReply.Id == model.replyId).FirstOrDefaultAsync();

            if (existingReplyLike != null)
            {
                _context.ReplyLikes.Remove(existingReplyLike);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
