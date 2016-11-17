using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Admin.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private string _mess;

        public string message
        {
            get
            {
                return _mess;
            }
            set
            {
                if (_mess != value)
                {
                    _mess = value;
                    OnPropertyChanged("message");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
