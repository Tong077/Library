using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data
{
    public class EntityContext : IdentityDbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options) : base(options) 
        { }
    }
}
