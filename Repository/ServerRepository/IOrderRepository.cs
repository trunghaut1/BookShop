using Repository.Model;
using Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServerRepository
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        ListPaging<Order> GetPage(int pageSize, int page);
        ListPaging<Order> GetByUserPage(int id, int pageSize, int page);
    }
}
