﻿using DomainModels.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<UserPosts> UserPosts { get; set; }
    }
}
