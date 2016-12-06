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
    public class TimeController : Controller
    {
        private ITimeRepository timeRepo;

        public TimeController(ITimeRepository timeRepo)
        {
            this.timeRepo = timeRepo;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<TimeRule> Get()
        {
            return timeRepo.GetAll().Select(o => new TimeRule(o));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TimeRule Get(int id)
        {
            TimeRule value = timeRepo.GetByID(id);
            return value != null ? new TimeRule(value) : null;
        }

        // POST api/values
        [HttpPost]
        public int? Post([FromBody]TimeRule value)
        {
            if (timeRepo.Add(value)) return value.ID;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]TimeRule value)
        {
            return timeRepo.Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return timeRepo.Delete(id);
        }
    }
}
