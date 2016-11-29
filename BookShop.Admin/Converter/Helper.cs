using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
