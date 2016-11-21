using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public partial class User
    {
        public User(int? id, string name, string email, string pass, string phone, bool admin)
        {
            this.Order = new ObservableCollection<Order>();
            ID = id ?? 0;
            Name = name;
            Email = email;
            Pass = pass;
            Phone = phone;
            IsAdmin = admin;
        }
    }
}
