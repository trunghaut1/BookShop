using Repository.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ClientRepository
{
    public class GenericRepository<T> where T : class
    {
        public string url { get; set; }
        public async Task<int> Add(T obj)
        {
            string json = await APIHelper.Post(url, obj);
            if(!string.IsNullOrEmpty(json))
            {
                return int.Parse(json);
            }
            return 0;
        }

        public async Task<bool> Delete(int id)
        {
            string json = await APIHelper.Delete($"{url}/{id}");
            if (!string.IsNullOrEmpty(json))
            {
                return bool.Parse(json);
            }
            return false;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            string json = await APIHelper.Get(url);
            if (!string.IsNullOrEmpty(json))
            {
                return JsonHelper.Json2List<T>(json);
            }
            return null;
        }

        public T GetByID(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(int id, T obj)
        {
            string json = await APIHelper.Put($"{url}/{id}", obj);
            return !string.IsNullOrEmpty(json) ? true : false;
        }
    }
}
