﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ZeroStack.IdentityServer.API.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
