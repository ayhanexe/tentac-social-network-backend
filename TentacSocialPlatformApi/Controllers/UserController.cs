using Authentication.Config;
using Authentication.Dtos;
using Authentication.Services;
using AutoMapper;
using Constants;
using DomainModels.Dtos;
using DomainModels.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Data.Implementation.EfCore;
using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;

        public UserController(EfCoreUserRepository repository, IMapper mapper, IAuthService authService) : base(repository, mapper)
        {
            _authService = authService;
            _userRepository = repository;
            _mapper = mapper;
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
                
                if(response.HasError)
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
