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
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(entity);
        }
    }




}
