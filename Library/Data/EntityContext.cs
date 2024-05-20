using Library.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class EntityContext : IdentityDbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        { }
        public DbSet<AppUser> APPUSER { get; set; }
        public DbSet<AppuserPermission> AppUserPermission { get; set; }
    }
}
