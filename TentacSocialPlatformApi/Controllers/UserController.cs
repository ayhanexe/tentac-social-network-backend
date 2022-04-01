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
using Authentication.Utils;
using System.IO;
using Constants;

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
            List<User> entities = await _userRepository.GetAll();

            return entities;
        }

        [HttpGet("{id}")]
        public override async Task<ActionResult<User>> Get(string id)
        {
            User entity = await _userRepository.Get(id);

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
        [Authorize(Roles = "superuser, admin, moderator, user")]
        public override async Task<IActionResult> Put(string id, [FromBody] User entity, [FromHeader] string token)
        {
            User _entity = await _userRepository.Get(id);
            var userToken = await _context.UserTokens.Where(ut => ut.UserId == _entity.Id && ut.Value == token).FirstOrDefaultAsync();

            if (userToken != null)
            {

                if (_entity == null)
                {
                    return NotFound();
                }

                try
                {
                    await _userRepository.Update(id, entity);
                    return Ok();
                }
                catch (Exception exception)
                {
                    return BadRequest(exception.Message);
                }
            }
            else
            {
                return BadRequest("Please send valid token with header!");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "superuser, admin, moderator")]
        public override async Task<IActionResult> Delete(string id)
        {
            var entity = await _userRepository.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost("addProfile/{id}")]
        [Authorize(Roles = "superuser, admin, moderator, user")]
        public async Task<IActionResult> AddProfile([FromForm] IFormFile File, string id)
        {
            try
            {
                var user = await _userRepository.Get(id);
                if (user != null)
                {
                    var newFileName = await Utils.CopyFile(File, ConfigConstants.ProfileImagesRootPath);
                    var oldPhotoName = user.ProfilePhoto;
                    if (oldPhotoName != null)
                    {
                        var oldProfilePhotoPath = Path.Combine(ConfigConstants.ProfileImagesRootPath, oldPhotoName);
                        Utils.DeleteFile(oldProfilePhotoPath);
                    }
                    user.ProfilePhoto = newFileName;

                    await _context.SaveChangesAsync();
                    return Ok(newFileName);
                }

                return BadRequest("User not found!");

            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost("addWall/{id}")]
        [Authorize(Roles = "superuser, admin, moderator, user")]
        public async Task<IActionResult> AddWall([FromForm] IFormFile File, string id)
        {
            try
            {
                var user = await _userRepository.Get(id);
                if (user != null)
                {
                    var newFileName = await Utils.CopyFile(File, ConfigConstants.WallImagesRootPath);
                    var oldPhotoName = user.UserWall;
                    if (oldPhotoName != null)
                    {
                        var oldProfilePhotoPath = Path.Combine(ConfigConstants.WallImagesRootPath, oldPhotoName);
                        Utils.DeleteFile(oldProfilePhotoPath);
                    }
                    user.UserWall = newFileName;

                    await _context.SaveChangesAsync();
                    return Ok(newFileName);
                }

                return BadRequest("User not found!");

            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
