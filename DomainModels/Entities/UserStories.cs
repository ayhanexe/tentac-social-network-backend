﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class UserStories : BaseEntity<int>
    {
        public User User { get; set; }
        public Story Story { get; set; }
    }
}
