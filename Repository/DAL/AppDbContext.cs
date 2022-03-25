﻿using DomainModels.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserPhoto> UserPhotos { get; set; }
        public DbSet<UserWall> UserWalls { get; set; }
    }
}
