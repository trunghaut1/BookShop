using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public partial class Order
    {
        public Order(int? id, DateTime? time, string address, bool? status, int userId)
        {
            this.OrderDetail = new ObservableCollection<OrderDetail>();
            ID = id ?? 0;
            Time = time;
            Address = address;
            Status = status;
            UserID = userId;
        }
        public Order(Order value)
        {
            this.OrderDetail = new ObservableCollection<OrderDetail>();
            ID = value.ID;
            Time = value.Time;
            Address = value.Address;
            Status = value.Status;
            UserID = value.UserID;
        }
    }
}
