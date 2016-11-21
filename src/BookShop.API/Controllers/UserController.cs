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
        public IEnumerable<User> Get()
        {
            return userRepo.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return userRepo.GetByID(id);
        }

        [HttpGet("email/{email}")]
        public User Get(string email)
        {
            return userRepo.GetByEmail(email);
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
        public User Put(int id, [FromBody]User value)
        {
            if (userRepo.Update(value)) return value;
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return userRepo.Delete(id);
        }
    }
}
