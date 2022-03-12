using Authentication.Config;
using Authentication.Dtos;
using Authentication.Services;
using AutoMapper;
using DomainModels.Dtos;
using DomainModels.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Data.Implementation.EfCore;
using System.Threading.Tasks;
using TentacSocialPlatformApi.Controllers.Abstraction;

namespace TentacSocialPlatformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModelDto loginDto)
        {
            LoginResponseDto response = await _authService.Login(loginDto);

            if (!response.IsAuthenticated)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDto registerDto)
        {
            Response response = await _authService.Register(registerDto);

            return Ok(response);
        }
    }
}
