using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.ServerRepository;
using Repository.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookShop.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        IEFUserRepository userRepo;

        public UserController(IEFUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }
        // GET: api/values
        [HttpGet]
        public dynamic Get()
        {
            return userRepo.GetAll().Select(o => new { o.ID, o.Name, o.Email, o.Pass, o.Phone, o.IsAdmin});
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            var value = userRepo.GetByID(id);
            return value != null ? new User(value) : null;
        }

        [HttpGet("email/{email}")]
        public User GetByEmail(string email)
        {
            var value = userRepo.GetByEmail(email);
            return value != null ? new User(value) : null;
        }

        // POST api/values
        [HttpPost]
        public int? Post([FromBody]User value)
        {
            if (userRepo.Add(value)) return value.ID;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]User value)
        {
            return userRepo.Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return userRepo.Delete(id);
        }
    }
}
