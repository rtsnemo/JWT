﻿using JWT.Models;
using Microsoft.EntityFrameworkCore;

namespace JWT.Data
{
    public class UserContext : DbContext
    {
        public UserContext() { }
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
