using Repository.Model;

namespace Repository.ClientRepository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository()
        {
            url = "user";
        }
    }
}
