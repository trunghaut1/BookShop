using Repository.Model;

namespace Repository.ServerRepository
{
    public interface IEFUserRepository : IEFGenericRepository<User>
    {
        User GetByEmail(string value);
    }
}
