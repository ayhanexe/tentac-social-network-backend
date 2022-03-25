using Authentication.Config;
using Authentication.Dtos;
using Authentication.Services;
using AutoMapper;
using DomainModels.Entities;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : BaseController<User, string, RegisterModelDto, EfCoreUserRepository>
    {
        private readonly EfCoreUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly AppDbContext _context;

        public UserController(EfCoreUserRepository repository, IMapper mapper, IAuthService authService, AppDbContext context) : base(repository, mapper)
        {
            _authService = authService;
            _userRepository = repository;
            _context = context;
        }

        [HttpGet]
        public override async Task<ActionResult<IEnumerable<User>>> Get()
        {
            List<User> entities = await _context.Users.Include(u => u.UserWalls).Include(u => u.ProfilePhotos).ToListAsync();

            return entities;
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<User>> Get(string id)
        {
            User entity = await _context.Users.Include(u => u.UserWalls).Include(u => u.ProfilePhotos).Where(u => u.Id == id).FirstOrDefaultAsync();

            if (entity == null) return null;

            return entity;

        }

        [HttpPost]
        [Authorize(Roles = "superuser, admin, moderator ")]
        public override async Task<IActionResult> Add(RegisterModelDto entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            try
            {
                Response response = await _authService.Register(entity);

                if (response.HasError)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, response.Message);
                }

                return Ok();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "superuser, admin, moderator ")]
        public override async Task<IActionResult> Put(string id, User entity)
        {
            User _entity = await _userRepository.Get(id);

            if (_entity == null)
            {
                return NotFound();
            }

            try
            {
                await _userRepository.Update(entity);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "superuser, admin, moderator ")]
        public override async Task<IActionResult> Delete(string id)
        {
            var entity = await _userRepository.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
    }
}
