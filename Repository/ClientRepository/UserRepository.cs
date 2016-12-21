using Repository.Helper;
using Repository.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.ClientRepository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository()
        {
            url = "user";
        }
        public async Task<User> GetByEmail(string email)
        {
            string json = await APIHelper.Get($"{url}/email/{email}");
            if (!string.IsNullOrEmpty(json))
            {
                return JsonHelper.Json2Object<User>(json);
            }
            return null;
        }
    }
}
