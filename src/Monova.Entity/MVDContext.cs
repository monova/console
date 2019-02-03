using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Monova.Entity
{
    public class MVDContext : IdentityDbContext<MVDUser, IdentityRole<Guid>, Guid>
    {
        public MVDContext(DbContextOptions options) : base(options)
        {
        }
    }
}
