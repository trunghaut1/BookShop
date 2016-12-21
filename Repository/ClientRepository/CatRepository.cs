using Repository.Helper;
using Repository.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.ClientRepository
{
    public class CatRepository : GenericRepository<Cat>
    {
        public CatRepository()
        {
            url = "cat";
        }
        public async Task<IEnumerable<Cat>> GetCatSub()
        {
            string json = await APIHelper.Get($"{url}/catsub");
            if (!string.IsNullOrEmpty(json))
            {
                return JsonHelper.Json2List<Cat>(json);
            }
            return null;
        }
        public async Task<IEnumerable<Cat>> GetByBook(int id)
        {
            string json = await APIHelper.Get($"{url}/book/{id}");
            if (!string.IsNullOrEmpty(json))
            {
                return JsonHelper.Json2List<Cat>(json);
            }
            return null;
        }
    }
}
