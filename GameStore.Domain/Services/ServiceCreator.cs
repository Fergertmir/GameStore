using GameStore.Domain.Abstract;
using GameStore.Domain.Concrete;

namespace GameStore.Domain.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }
    }
}
