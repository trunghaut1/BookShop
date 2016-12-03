using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.ServerRepository;
using Repository.ViewModel;
using Repository.Model;
using System.Collections.ObjectModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookShop.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        IEFOrderRepository orderRepo;

        public OrderController(IEFOrderRepository orderRepo)
        {
            this.orderRepo = orderRepo;
        }
        // GET: api/values
        [HttpGet]
        public dynamic Get()
        {
            return orderRepo.GetAll().Select(o => new { o.ID, o.Time, o.Status, o.UserID, o.Address}); //Old
        }
        [HttpGet("page/{pageSize}/{page}")]
        public ListPaging<Order> GetPage(int pageSize, int page)
        {
            if(pageSize != 0 && page != 0)
                return orderRepo.GetPage(pageSize, page);
            return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            Order value = orderRepo.GetByID(id);
            return value != null ? new Order(value) : null;
        }

        // POST api/values
        [HttpPost]
        public int? Post([FromBody]Order value)
        {
            value.OrderDetail = new ObservableCollection<OrderDetail>(value.OrderDetail.Select(o => new OrderDetail(o, true)));
            if (orderRepo.Add(value)) return value.ID;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]Order value)
        {
            return orderRepo.Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return orderRepo.Delete(id);
        }
    }
}
