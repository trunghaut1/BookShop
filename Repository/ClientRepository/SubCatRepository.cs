using Repository.Helper;
using Repository.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.ClientRepository
{
    public class SubCatRepository : GenericRepository<SubCat>
    {
        public SubCatRepository()
        {
            url = "subcat";
        }
        public async Task<IEnumerable<SubCat>> GetByBook(int id)
        {
            string json = await APIHelper.Get($"{url}/book/{id}");
            if (!string.IsNullOrEmpty(json))
            {
                return JsonHelper.Json2List<SubCat>(json);
            }
            return null;
        }
    }
}
