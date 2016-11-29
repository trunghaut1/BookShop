using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Admin.ViewModels
{
    public class BaseViewModel : Conductor<Screen>
    {
        public BaseViewModel(Type T,string title)
        {
            object viewmodel = Activator.CreateInstance(T);
            ActivateItem((Screen)viewmodel);
            DisplayName = title;
        }
    }
}
