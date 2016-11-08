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
    public class UserController : Controller
    {
        IUserRepository _uRepo;

        public UserController(IUserRepository uRepo)
        {
            _uRepo = uRepo;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _uRepo.SelectAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _uRepo.SelectByID(id);
        }

        [HttpGet("email/{email}")]
        public User Get(string email)
        {
            return _uRepo.SelectByEmail(email);
        }

        // POST api/values
        [HttpPost]
        public User Post([FromBody]User value)
        {
            if (_uRepo.Add(value)) return value;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public User Put(int id, [FromBody]User value)
        {
            if (_uRepo.Update(value)) return value;
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _uRepo.Delete(id);
        }
    }
}
