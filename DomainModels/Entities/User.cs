using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModels.Entities
{
    public enum Gender
    {
        MALE,
        FEMALE
    }

    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Tel { get; set; }
        public string ProfilePhoto { get; set; }
        public string UserWall { get; set; }
        public ICollection<UserStories> UserStories { get; set; }
    }
}
