using PropertyChanged;
using System.Collections.ObjectModel;

namespace Repository.Model
{
    [ImplementPropertyChanged]
    public partial class User
    {
        public User(int? id, string name, string email, string pass, string phone, bool? admin)
        {
            this.Order = new ObservableCollection<Order>();

            ID = id ?? 0;
            Name = name;
            Email = email;
            Pass = pass;
            Phone = phone;
            IsAdmin = admin;
        }
        public User(User value)
        {
            this.Order = new ObservableCollection<Order>();

            ID = value.ID;
            Name = value.Name;
            Email = value.Email;
            Pass = value.Pass;
            Phone = value.Phone;
            IsAdmin = value.IsAdmin;
        }
    }
}
