using System.Threading.Tasks;
using GameStore.Domain.Concrete;
using System;

namespace GameStore.Domain.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        AppUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        AppRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
