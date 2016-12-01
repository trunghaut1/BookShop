using Flurl.Http;
using System.Threading.Tasks;

namespace Repository.Helper
{
    public class APIHelper
    {
        private static string hostname = "http://localhost:5000/api/";

        private static string host(string url)
        {
            return hostname + url;
        }
        public static async Task<string> Get(string url)
        {
            try
            {
                var response = await host(url).GetStringAsync();
                return response;
            }
            catch
            {
                return null;
            }
        }
        public static async Task<string> Post(string url, object o)
        {
            try
            {
                var response = await host(url).PostJsonAsync(o).ReceiveString();
                return response;
            }
            catch
            {
                return null;
            }
        }
        public static async Task<string> Put(string url, object o)
        {
            try
            {
                var response = await host(url).PutJsonAsync(o).ReceiveString();
                return response;
            }
            catch
            {
                return null;
            }
        }
        public static async Task<string> Delete(string url)
        {
            try
            {
                var response = await host(url).DeleteAsync().ReceiveString();
                return response;
            }
            catch
            {
                return null;
            }
        }
    }
}
