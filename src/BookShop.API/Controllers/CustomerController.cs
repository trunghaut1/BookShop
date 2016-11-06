using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookShop.API.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        ICustomerRepository _cusRepo;

        public CustomerController(ICustomerRepository cusRepo)
        {
            _cusRepo = cusRepo;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _cusRepo.SelectAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _cusRepo.SelectByID(id);
        }

        [HttpGet("email/{id}")]
        public Customer Get(string value)
        {
            return _cusRepo.SelectByEmail(value);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
