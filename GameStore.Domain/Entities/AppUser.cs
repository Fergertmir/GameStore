using Microsoft.AspNet.Identity.EntityFramework;

namespace GameStore.Domain.Entities
{
    public class AppUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
