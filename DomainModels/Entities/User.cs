﻿using Microsoft.AspNetCore.Identity;

namespace DomainModels.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}
