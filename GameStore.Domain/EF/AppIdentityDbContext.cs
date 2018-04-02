using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using GameStore.Domain.Entities;

namespace GameStore.Domain.EF
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContext(string conectionString) : base(conectionString) { }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
