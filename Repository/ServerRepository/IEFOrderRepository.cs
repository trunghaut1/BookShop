using Repository.Model;
using Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServerRepository
{
    public interface IEFOrderRepository : IEFGenericRepository<Order>
    {
        ListPaging<Order> GetPage(int pageSize, int page);
    }
}
