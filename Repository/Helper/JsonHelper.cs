using Newtonsoft.Json;
using System.Collections.Generic;

namespace Repository.Helper
{
    public class JsonHelper
    {
        public static IEnumerable<T> Json2List<T> (string json) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            }
            catch
            {
                return null;
            }
        }
        public static object Object2Json(object o)
        {
            try
            {
                return JsonConvert.SerializeObject(o);
            }
            catch
            {
                return null;
            }
        }
        public static T Json2Object<T>(string json) where T : class
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}
