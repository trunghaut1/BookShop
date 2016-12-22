using Repository.Model;
using Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServerRepository
{
    public class EFOrderRepository : EFGenericRepository<Order>, IOrderRepository
    {
        public EFOrderRepository(BookEntities db) : base(db) { }

        public ListPaging<Order> GetPage(int pageSize, int page)
        {
            Paging paging = new Paging()
            {
                totalItem = table.Count(),
                pageSize = pageSize,
                pageIndex = page
            };
            var order = table.OrderBy(o => o.ID).Skip(pageSize * (page - 1)).Take(pageSize).ToList()
                .Select(o => new Order(o, true));
            ListPaging<Order> bookPaging = new ListPaging<Order>()
            {
                list = order,
                paging = paging
            };
            return bookPaging;
        }
        public ListPaging<Order> GetByUserPage(int id, int pageSize, int page)
        {
            var order = table.Where(o => o.UserID == id).OrderBy(o => o.ID)
                .Skip(pageSize * (page - 1)).Take(pageSize).ToList()
                .Select(o => new Order(o, true));
            Paging paging = new Paging()
            {
                totalItem = table.Where(o => o.UserID == id).Count(),
                pageSize = pageSize,
                pageIndex = page
            };
            ListPaging<Order> bookPaging = new ListPaging<Order>()
            {
                list = order,
                paging = paging
            };
            return bookPaging;
        }
    }
}
