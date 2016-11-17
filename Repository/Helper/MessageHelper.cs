using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Helper
{
    public static class MessageHelper
    {
        private static IDictionary<string, string> message = new Dictionary<string, string>()
        {
           ["+"] = "Nhập đầy đủ thông tin sau đó nhấn lưu!",
           ["add"] = "Thêm thành công!",
           ["up"] = "Cập nhật thành công!",
           ["upErr"] = "Cập nhật thất bại!"
        };

        public static string Get(string mess)
        {
            return message[mess];
        }
    }
}
