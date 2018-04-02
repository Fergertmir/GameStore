using Microsoft.AspNet.Identity.EntityFramework;

namespace GameStore.Domain.Entities
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base() { }

        public AppRole(string name)
            : base(name)
        { }
    }
}
