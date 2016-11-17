using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
