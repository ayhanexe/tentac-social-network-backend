using Authentication.Utils;
using AutoMapper;
using Constants;
using DomainModels.Dtos;
using DomainModels.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Data.Implementation.EfCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TentacSocialPlatformApi.Controllers.Abstraction;

namespace TentacSocialPlatformApi.Controllers
{
    [Route("api/Stories")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class StoryController : BaseController<UserStories, int, StoryDto, EfCoreUserStoryRepository>
    {
        private readonly AppDbContext _context;
        public StoryController(EfCoreUserStoryRepository repository, IMapper mapper, AppDbContext context) : base(repository, mapper)
        {
            _context = context;
        }

        public async override Task<IActionResult> Add([FromForm] StoryDto entity)
        {
            try
            {
                var user = await _context.Users.FindAsync(entity.UserId);

                var newFileName = await Utils.CopyFile(entity.Image, ConfigConstants.StoryImagesRootPath);

                if (user != null)
                {
                    var model = new Story
                    {
                        Image = newFileName,
                        Text = entity.Text
                    };

                    _context.Stories.Add(model);


                    var userStoryModel = new UserStories
                    {
                        Story = model,
                        User = user
                    };

                    _context.UserStories.Add(userStoryModel);

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

        [HttpDelete("{id}")]
        public async override Task<IActionResult> Delete(int id)
        {
            try
            {
                var story = await _context.Stories.FindAsync(id);
                var userStory = await _context.UserStories
                    .Include(u => u.Story)
                    .Where(us => us.Story.Id == story.Id)
                    .FirstOrDefaultAsync();

                if (story != null)
                {
                    var path = Path.Combine(ConfigConstants.StoryImagesRootPath, story.Image);
                    var isDeleted = Utils.DeleteFile(path);
                    if (isDeleted)
                    {
                        _context.Stories.Remove(story);
                    }
                }

                if (userStory != null)
                {
                    _context.UserStories.Remove(userStory);
                }

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
