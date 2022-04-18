using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Data.Implementation.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TentacSocialPlatformApi.Controllers.Abstraction;

namespace TentacSocialPlatformApi.Controllers
{
    public class UserFriendRequestsController : BaseController<UserFriendRequests, string, UserFriendDto, EfCoreUserFriendRequestsRepository>
    {

        private readonly AppDbContext _context;
        private readonly EfCoreUserFriendRequestsRepository _repository;

        public UserFriendRequestsController(EfCoreUserFriendRequestsRepository repo, IMapper maper, AppDbContext context) : base(repo, maper)
        {
            _context = context;
            _repository = repo;
        }

        public async override Task<ActionResult<IEnumerable<UserFriendRequests>>> Get()
        {
            return await _context.UserFirendRequests.Include(u => u.User).Include(u => u.FriendRequestedUser).ToListAsync();
        }

        public async override Task<ActionResult<UserFriendRequests>> Get(string id)
        {
            return await _context.UserFirendRequests.Include(u => u.User).Include(u => u.FriendRequestedUser).Where(u => u.User.Id == id).FirstOrDefaultAsync();
        }

        public override async Task<IActionResult> Add(UserFriendDto entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                var existing = await _context.UserFirendRequests
                    .Include(r => r.User)
                    .Include(r => r.FriendRequestedUser)
                    .Where(r => r.User.Id == entity.UserId && r.FriendRequestedUser.Id == entity.FriendId)
                    .FirstOrDefaultAsync();

                if (existing == null)
                {
                    var user = await _context.Users.FindAsync(entity.UserId);
                    var requestUser = await _context.Users.FindAsync(entity.FriendId);

                    if (user != null)
                    {
                        if (requestUser == null) return BadRequest();
                        var model = new UserFriendRequests
                        {
                            User = user,
                            FriendRequestedUser = requestUser
                        };

                        await _repository.Add(model);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(entity);
        }

        public async override Task<IActionResult> Delete(string id)
        {
            var request = await _context.UserFirendRequests
                .Where(r => r.Id.ToString() == id).FirstOrDefaultAsync();
            if (request != null)
            {
                _context.UserFirendRequests.Remove(request);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Decline/{id}")]
        public async Task<IActionResult> Decline(string id)
        {
            var request = _context.UserFirendRequests.Where(r => r.Id.ToString() == id).FirstOrDefault();

            if (request != null)
            {
                _context.UserFirendRequests.Remove(request);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Accept/{id}")]
        public async Task<IActionResult> Accept(string id)
        {
            var request = _context.UserFirendRequests
                .Include(request => request.User)
                .Include(request => request.FriendRequestedUser)
                .Where(r => r.Id.ToString() == id)
                .FirstOrDefault();

            if (request != null)
            {
                _context.UserFirendRequests.Remove(request);
                var user = await _context.Users.Where(u => u.Id == request.User.Id).FirstOrDefaultAsync();
                var friend = await _context.Users.Where(u => u.Id == request.FriendRequestedUser.Id).FirstOrDefaultAsync();
                var model = new UserFriends
                {
                    Friend = friend,
                    User = user
                };

                _context.UserFriends.Add(model);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }




}
