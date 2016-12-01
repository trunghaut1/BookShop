using Caliburn.Micro;
using System;

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
