﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Entities
{
    public class BaseEntity<T> : IEntity<T>
    {
        public T Id { get; set; }
    }
}
