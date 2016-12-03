using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [ImplementPropertyChanged]
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
        public Order(Order value, bool includeUserOrder)
        {
            this.OrderDetail = new ObservableCollection<OrderDetail>();

            ID = value.ID;
            Time = value.Time;
            Address = value.Address;
            Status = value.Status;
            UserID = value.UserID;
            User = new User(value.User);
            var orderDetail = value.OrderDetail.Select(o => new OrderDetail(o.OrderID, o.BookID, o.Price, o.Quantity, o.Book));
            if(orderDetail != null)
                this.OrderDetail = new ObservableCollection<OrderDetail>(orderDetail);
        }
    }
}
