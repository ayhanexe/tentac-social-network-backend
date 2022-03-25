using Authentication.Config;
using Authentication.Dtos;
using Authentication.Models;
using Authentication.Utils;
using Constants;
using DomainModels.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.DAL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;

        public AuthService(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<LoginResponseDto> Login(LoginModelDto model)
        {
            var authModel = new LoginResponseDto();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.Email);

                if (user == null)
                {
                    authModel.IsAuthenticated = false;
                    authModel.Message = $"No Accounts Registered with {model.Email}";
                    return authModel;
                }
            }

            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authModel.Email = user.Email;
                authModel.Username = user.UserName;
                authModel.id = user.Id;
                authModel.Name = user.Name;
                authModel.Surname = user.Surname;
                var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authModel.Roles = roleList.ToList<string>();
                return authModel;
            }

            authModel.IsAuthenticated = false;
            authModel.Message = $"Incorrect credentials for user!";
            return authModel;
        }

        public async Task<Response> Register(RegisterModelDto model)
        {
            var response = new Response()
            {
                Message = null
            };
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                response.HasError = true;
                response.Message = $"This email already registered! `{model.Email}`";
                return response;
            }
            else
            {
                user = await _userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    response.HasError = true;
                    response.Message = $"This username already registered! `{model.Username}`";
                    return response;

                }
            }

            IResponse emailValidation = model.Email.isValidEmail();
            IResponse usernameValidation = model.Username.isValidUsername();
            IResponse passwordValidation = model.Password.isValidPassword();

            if (emailValidation.HasError)
            {
                response.HasError = true;
                response.Message = $"{emailValidation.Message}";
                return response;

            }

            if (usernameValidation.HasError)
            {
                response.HasError = true;

                response.Message = $"{usernameValidation.Message}";

                return response;

            }


            if (passwordValidation.HasError)
            {
                response.HasError = true;

                response.Message = $"{passwordValidation.Message}";

                return response;

            }

            var userModel = new User()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                UserName = model.Username
            };

            IdentityResult result = await _userManager.CreateAsync(userModel, model.Password);
            await _userManager.AddToRoleAsync(userModel, RoleConstants.User);
            await _context.SaveChangesAsync();

            var wall = new UserWall
            {
                UserId = userModel.Id,
                User = userModel,
                Photo = ConfigConstants.DefaultWallPhotoName,
                isDeleted = false,
                CreateDate = DateTime.Now,
            };

            await _context.UserWalls.AddAsync(wall);
            await _context.SaveChangesAsync();

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    response.Message = item.Description;
                }
            }

            return new Response()
            {
                HasError = false,
                Message = null
            };
        }

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
