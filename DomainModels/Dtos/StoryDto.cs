using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Dtos
{
    public class StoryDto
    {
        public IFormFile Image { get; set; }
        public string Text { get; set; }
        public string UserId { get; set; }
    }
}
