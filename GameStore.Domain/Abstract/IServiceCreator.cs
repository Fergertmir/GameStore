namespace GameStore.Domain.Abstract
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(string connection);
    }
}
