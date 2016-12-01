using System.Net;
using System.Windows;
using System.Windows.Data;

namespace BookShop.Admin.Converter
{
    public class Helper
    {
        public static Binding SetBinding(string elementName, string path)
        {
            Binding binding = new Binding();
            binding.ElementName = elementName;
            binding.Path = new PropertyPath(path);
            binding.Mode = BindingMode.OneWay;
            return binding;
        }
        public static byte[] Image2Byte(string url)
        {
            try
            {
                var webClient = new WebClient();
                byte[] imageBytes = webClient.DownloadData(url);
                return imageBytes;
            }
            catch
            {
                return null;
            }
        }
    }
}
