using Caliburn.Micro;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.WindowsUI;
using PropertyChanged;
using Repository.ClientRepository;
using Repository.Helper;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookShop.Admin.ViewModels
{
    [ImplementPropertyChanged]
    public class TimeViewModel : Screen
    {
        private TimeRepository timeRepo;
        private BookRepository bookRepo;

        public string message { get; set; }
        public string txtSearchBook { get; set; }
        public bool chkT2 {get; set;}
        public bool chkT3 { get; set; }
        public bool chkT4 { get; set; }
        public bool chkT5 { get; set; }
        public bool chkT6 { get; set; }
        public bool chkT7 { get; set; }
        public bool chkCN { get; set; }

        public ObservableCollection<TimeRule> timeRules { get; set; }
        public ObservableCollection<Book> books { get; set; }
        public ObservableCollection<Book> bookSearch { get; set; }

        public TimeViewModel()
        {
            timeRepo = new TimeRepository();
            bookRepo = new BookRepository();
            timeRules = new ObservableCollection<TimeRule>();
            books = new ObservableCollection<Book>();
            bookSearch = new ObservableCollection<Book>();

            LoadData();
        }
        private async void LoadData()
        {
            IEnumerable<TimeRule> timeRule = await timeRepo.GetAll();
            if(timeRule != null)
            {
                timeRules = new ObservableCollection<TimeRule>(timeRule);
            }
        }
        public void ClearSelected(GridControl grid)
        {
            grid.SelectedItem = null;
            books.Clear();
            bookSearch.Clear();
            LoadWeek("");
            message = MessageHelper.Get("+");
        }
        private void LoadWeek(string week)
        {
            chkT2 = week.Contains("1") ? true : false;
            chkT3 = week.Contains("2") ? true : false;
            chkT4 = week.Contains("3") ? true : false;
            chkT5 = week.Contains("4") ? true : false;
            chkT6 = week.Contains("5") ? true : false;
            chkT7 = week.Contains("6") ? true : false;
            chkCN = week.Contains("0") ? true : false;
        }
        public void TimeRuleSelectChange(TimeRule rule)
        {
            if(rule != null)
            {
                LoadWeek(rule.Week);
                books = new ObservableCollection<Book>(rule.TimeBased.Select(o => o.Book));
            }
        }
        public async void SearchBook()
        {
            if (!string.IsNullOrEmpty(txtSearchBook))
            {
                bookSearch.Clear();
                int searchId;
                if (int.TryParse(txtSearchBook, out searchId))
                {
                    if (searchId > 0)
                    {
                        Book book = await bookRepo.GetByID(searchId);
                        if (book != null)
                            bookSearch.Add(book);
                    }
                }
                else
                {
                    IEnumerable<Book> book = await bookRepo.GetByName(txtSearchBook);
                    if (book != null)
                        bookSearch = new ObservableCollection<Book>(book);
                }
            }
        }
        public void btnClearSearch()
        {
            txtSearchBook = null;
            bookSearch.Clear();
        }
        public void AddBook(Book value)
        {
            if (value == null)
            {
                message = "Vui lòng chọn sách cần thêm!";
                return;
            }
            Book exitense = books.Where(o => o.ID == value.ID).FirstOrDefault();
            if (exitense != null)
            {
                message = "Đã tồn tại sách này này!";
                return;
            }
            else
            {
                books.Add(value);
            }
        }
        private string WeekCount()
        {
            string week = "";
            if (chkCN) week += "0";
            if (chkT2) week += "1";
            if (chkT3) week += "2";
            if (chkT4) week += "3";
            if (chkT5) week += "4";
            if (chkT6) week += "5";
            if (chkT7) week += "6";
            return week;
        }
        public void DelBook(Book value)
        {
            if (value != null)
            {
                books.Remove(value);
                message = "Đã xóa!";
            }
            else
            {
                message = "Vui lòng chọn sách cần xóa!";
            }
        }
        public async void AddOrUpdate(int index, int? id, DateTime fromDate, DateTime toDate, object _fromHour, object _toHour,
            bool status)
        {
            TimeSpan fromHour, toHour;
            string week = WeekCount();
            _fromHour = _fromHour ?? new TimeSpan();
            _toHour = _toHour ?? new TimeSpan();
            if(_fromHour.GetType().Equals(typeof(DateTime)))
                fromHour = ((DateTime)_fromHour).TimeOfDay;
            else
                fromHour = (TimeSpan)_fromHour;
            if (_toHour.GetType().Equals(typeof(DateTime)))
                toHour = ((DateTime)_toHour).TimeOfDay;
            else
                toHour = (TimeSpan)_toHour;

            if (fromDate.TimeOfDay > toDate.TimeOfDay)
            {
                message = "Khoảng ngày không hợp lệ!";
                return;
            }
            if (fromHour > toHour)
            {
                message = "Khoảng giờ không hợp lệ!";
                return;
            }
            if (id != null)
            {
                TimeRule value = new TimeRule(id, fromDate, toDate, fromHour, toHour, week, status);
                ObservableCollection<TimeBased> timebased = new ObservableCollection<TimeBased>(books
                        .Select(o => new TimeBased((int)id, o.ID)));
                value.TimeBased = timebased;

                bool result = await timeRepo.Update((int)id, value);
                if(result)
                {
                    value.TimeBased = new ObservableCollection<TimeBased>(books.Select(o => new TimeBased((int)id, o)));
                    timeRules[index] = value;
                    message = MessageHelper.Get("up");
                }
                else
                {
                    message = MessageHelper.Get("upErr");
                }
            }
            else
            {
                TimeRule value = new TimeRule(null, fromDate, toDate, fromHour, toHour, week, status);
                if(books.Count > 0)
                {
                    ObservableCollection<TimeBased> timebased = new ObservableCollection<TimeBased>(books
                        .Select(o => new TimeBased(0, o.ID)));
                    value.TimeBased = timebased;
                }
                id = await timeRepo.Add(value);
                if(id > 0)
                {
                    value.ID = (int)id;
                    ObservableCollection<TimeBased> timebased = new ObservableCollection<TimeBased>(books
                        .Select(o => new TimeBased((int)id, o)));
                    value.TimeBased = timebased;
                    timeRules.Add(value);
                    message = MessageHelper.Get("add");
                }
                else
                {
                    message = MessageHelper.Get("addErr");
                }
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
                    bool del = await timeRepo.Delete((int)id);
                    if (del)
                    {
                        timeRules.RemoveAt(index);
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
