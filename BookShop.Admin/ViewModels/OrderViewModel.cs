using Caliburn.Micro;
using Repository.ClientRepository;
using Repository.Model;
using Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Admin.ViewModels
{
    public class OrderViewModel : Screen
    {
        private int pageSize = 5;

        private OrderRepository orderRepo;
        private BookRepository bookRepo;
        private UserRepository userRepo;
        public string message { get; set; }
        public ObservableCollection<Order> orders { get; set; }
        public ObservableCollection<OrderDetail> orderDetails { get; set; }
        public ObservableCollection<Book> books { get; set; }
        public ObservableCollection<User> users { get; set; }
        public Paging paging { get; set; }
        public ObservableCollection<int> pageList { get; set; }

        public OrderViewModel()
        {
            orderRepo = new OrderRepository();
            bookRepo = new BookRepository();
            userRepo = new UserRepository();
            orders = new ObservableCollection<Order>();
            orderDetails = new ObservableCollection<OrderDetail>();
            books = new ObservableCollection<Book>();
            users = new ObservableCollection<User>();
            pageList = new ObservableCollection<int>();
            paging = new Paging();
            LoadData(1);
        }
        private async void LoadData(int page)
        {
            OrderPaging orderPaging = await orderRepo.GetPage(pageSize, page);
            if(orderPaging?.order != null)
            {
                orders = new ObservableCollection<Order>(orderPaging.order);
                paging = orderPaging.paging;
                ObservableCollection<int> _pageList = new ObservableCollection<int>();
                for (int i = 1; i <= paging.totalPage; i++)
                {
                    _pageList.Add(i);
                }
                pageList = _pageList;
            }
        }
        private async Task LoadUserToOrder()
        {
            IEnumerable<User> user = await userRepo.GetAll();
            if(user != null)
            {
                users = new ObservableCollection<User>(user);
            }
        }
    }
}
