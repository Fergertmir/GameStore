using GameStore.Domain.Abstract;
using GameStore.Domain.EF;
using GameStore.Domain.Concrete;
using GameStore.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace GameStore.Domain.Concrete
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private AppIdentityDbContext db;

        private AppUserManager userManager;
        private AppRoleManager roleManager;
        private IClientManager clientManager;

        public IdentityUnitOfWork(string connectionString)
        {
            db = new AppIdentityDbContext(connectionString);
            userManager = new AppUserManager(new UserStore<AppUser>(db));
            roleManager = new AppRoleManager(new RoleStore<AppRole>(db));
            clientManager = new AppClientManager(db);
        }

        public AppUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public AppRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
