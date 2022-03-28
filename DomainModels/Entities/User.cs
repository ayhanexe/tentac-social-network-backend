using Microsoft.AspNetCore.Identity;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
=======
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
>>>>>>> 60667940fbbce0c24193e76f0a1733ac50d9ad2b

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
        public List<UserPhoto> ProfilePhotos { get; set; }
        public List<UserWall> UserWalls { get; set; }
    }
}
