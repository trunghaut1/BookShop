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
           ["addErr"] = "Thêm thất bại!",
           ["up"] = "Cập nhật thành công!",
           ["upErr"] = "Cập nhật thất bại!",
           ["del"] = "Xóa thành công!",
           ["delErr"] = "Xóa thất bại!",
           ["delNoti"] = "Vui lòng chọn 1 đối tượng cần xóa!"
        };

        public static string Get(string mess)
        {
            return message[mess];
        }
    }
}
