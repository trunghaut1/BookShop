using Repository.Helper;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ClientRepository
{
    public class RecommendRepository : GenericRepository<Recommend>
    {
        public RecommendRepository()
        {
            url = "recommend";
        }
        public async Task<IEnumerable<Recommend>> GetListByID(int id)
        {
            string json = await APIHelper.Get($"{url}/{id}");
            if (!string.IsNullOrEmpty(json))
            {
                return JsonHelper.Json2List<Recommend>(json);
            }
            return null;
        }
        public async Task<int> CountRecommend(int id)
        {
            string json = await APIHelper.Get($"{url}/count/{id}");
            if (!string.IsNullOrEmpty(json))
            {
                return int.Parse(json);
            }
            return 0;
        }
        public async Task<bool> UpdateList(int id, IEnumerable<Recommend> value)
        {
            string json = await APIHelper.Put($"{url}/{id}", value);
            if (!string.IsNullOrEmpty(json))
            {
                return bool.Parse(json);
            }
            return false;
        }
    }
}
