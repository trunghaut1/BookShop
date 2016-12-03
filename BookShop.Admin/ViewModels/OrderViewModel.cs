using Caliburn.Micro;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.WindowsUI;
using Repository.ClientRepository;
using Repository.Helper;
using Repository.Model;
using Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookShop.Admin.ViewModels
{
    public class OrderViewModel : Screen
    {
        private int pageSize = 5;

        private OrderRepository orderRepo;
        private BookRepository bookRepo;
        private UserRepository userRepo;
        public string message { get; set; }
        public bool IsAdd { get; set; } = false;
        public DateTime? dpTime { get; set; }
        public Visibility orderUpdateVisible { get; set; } = Visibility.Visible;
        public Visibility orderAddVisible { get; set; } = Visibility.Collapsed;
        public string txtSearchUser { get; set; }
        public string txtSearchBook { get; set; }
        public ObservableCollection<Order> orders { get; set; }
        public ObservableCollection<Book> books { get; set; }
        public ObservableCollection<OrderDetail> orderDetailAdd { get; set; }
        public int orderDetailSum { get; set; } = 0;
        public ObservableCollection<User> users { get; set; }
        public Paging paging { get; set; }
        public ObservableCollection<int> pageList { get; set; }

        public OrderViewModel()
        {
            orderRepo = new OrderRepository();
            bookRepo = new BookRepository();
            userRepo = new UserRepository();
            orders = new ObservableCollection<Order>();
            books = new ObservableCollection<Book>();
            orderDetailAdd = new ObservableCollection<OrderDetail>();
            users = new ObservableCollection<User>();
            pageList = new ObservableCollection<int>();
            paging = new Paging();
            dpTime = DateTime.Now;
            LoadData(1);
        }
        private async void LoadData(int page)
        {
            ListPaging<Order> orderPaging = await orderRepo.GetPage(pageSize, page);
            if(orderPaging?.list != null)
            {
                orders = new ObservableCollection<Order>(orderPaging.list);
                paging = orderPaging.paging;
                ObservableCollection<int> _pageList = new ObservableCollection<int>();
                for (int i = 1; i <= paging.totalPage; i++)
                {
                    _pageList.Add(i);
                }
                pageList = _pageList;
            }
        }
        public void ChangePage(int page)
        {
            if (page > 0)
            {
                LoadData(page);
            }
        }
        public void OrderSelectChange(Order order)
        {
            if(order != null)
            {
                IsAdd = false;
                dpTime = order.Time;
                orderUpdateVisible = Visibility.Visible;
                orderAddVisible = Visibility.Collapsed;
                txtSearchUser = null;
                users.Clear();
                users.Add(order.User);
            }
        }
        public void OrderDetailSelectChange(Book book)
        {
            if (book != null)
            {
                txtSearchBook = null;
                books.Clear();
                books.Add(book);
            }
        }
        public void ClearSelected(GridControl grid)
        {
            grid.SelectedItem = null;
            IsAdd = true;
            orderUpdateVisible = Visibility.Collapsed;
            orderAddVisible = Visibility.Visible;
            txtSearchUser = null;
            txtSearchBook = null;
            users.Clear();
            books.Clear();
            orderDetailAdd.Clear();
            orderDetailSum = 0;
            dpTime = DateTime.Now;
            message = MessageHelper.Get("+");
        }
        public async void SearchUser()
        {
            if (!string.IsNullOrEmpty(txtSearchUser))
            {
                users.Clear();
                int searchId;
                if (int.TryParse(txtSearchUser, out searchId))
                {
                    if (searchId > 0)
                    {
                        User user = await userRepo.GetByID(searchId);
                        if (user != null)
                            users.Add(user);
                    }
                }
                else
                {
                    IEnumerable<User> user = await userRepo.GetByName(txtSearchUser);
                    if (user != null)
                        users = new ObservableCollection<User>(user);
                }
            }
        }
        public async void SearchBook()
        {
            if (!string.IsNullOrEmpty(txtSearchBook))
            {
                books.Clear();
                int searchId;
                if (int.TryParse(txtSearchBook, out searchId))
                {
                    if (searchId > 0)
                    {
                        Book book = await bookRepo.GetByID(searchId);
                        if (book != null)
                            books.Add(book);
                    }
                }
                else
                {
                    IEnumerable<Book> book = await bookRepo.GetByName(txtSearchBook);
                    if (book != null)
                        books = new ObservableCollection<Book>(book);
                }
            }
        }
        public async void AddOrUpdate(int index, int? id, DateTime time, string address, bool status, User user)
        {
            if (id != null) // Update
            {
                Order order = new Order(id, time, address, status, user.ID);
                bool result = await orderRepo.Update((int)id, order);
                if(result)
                {
                    orders[index].Time = time;
                    orders[index].Address = address;
                    orders[index].Status = status;
                    message = MessageHelper.Get("up");
                }
                else
                {
                    message = MessageHelper.Get("upErr");
                }
            }
            else // Add
            {
                if(user == null)
                {
                    message = "Vui lòng chọn khách hàng!";
                    return;
                }
                if (orderDetailAdd.Count == 0)
                {
                    message = "Vui lòng thêm ít nhất một quyển sách!";
                    return;
                }
                Order order = new Order(null, time, address, status, user.ID);
                order.OrderDetail = orderDetailAdd;
                id = await orderRepo.Add(order);
                if(id != 0)
                {
                    order.ID = (int)id;
                    order.User = user;
                    orders.Add(order);
                    message = MessageHelper.Get("add");
                }
                else
                {
                    message = MessageHelper.Get("addErr");
                }
            }
        }
        public void AddBookOrder(Book book, int _quantity)
        {
            if(book == null)
            {
                message = "Vui lòng chọn sách!";
                return;
            }
            else
            {
                int quantity = _quantity > 0 ? _quantity : 1;
                OrderDetail exitense = orderDetailAdd.Where(o => o.BookID == book.ID).SingleOrDefault();
                if(exitense != null)
                {
                    exitense.Quantity += quantity;
                }
                else
                {
                    orderDetailAdd.Add(new OrderDetail(0, book.ID, (int)book.Price, quantity, book));
                }
                orderDetailSum = orderDetailAdd.Sum(o => o.Price * o.Quantity);
            }
        }
        public void DelBookOrder(OrderDetail orderDetail)
        {
            if(orderDetail != null)
            {
                orderDetailAdd.Remove(orderDetail);
                orderDetailSum = orderDetailAdd.Sum(o => o.Price * o.Quantity);
            }
        }

        public async void Delete(int index, int? id)
        {
            if (id != null)
            {
                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                var result = WinUIMessageBox.Show(window,
                "Bạn có muốn xóa giá trị này?", "Xác nhận",
                MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None, FloatingMode.Window);
                if (result == MessageBoxResult.Yes)
                {
                    bool del = await orderRepo.Delete((int)id);
                    if (del)
                    {
                        orders.RemoveAt(index);
                        message = MessageHelper.Get("del");
                    }
                    else
                    {
                        message = MessageHelper.Get("delErr");
                    }
                }
            }
            else
            {
                message = MessageHelper.Get("delNoti");
            }
        }
    }
}
