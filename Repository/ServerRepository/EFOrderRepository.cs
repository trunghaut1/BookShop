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
    }
}
