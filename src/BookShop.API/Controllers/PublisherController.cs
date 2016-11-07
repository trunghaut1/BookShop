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
    public class PublisherController : Controller
    {
        IPublisherRepository _pubRepo;

        public PublisherController(IPublisherRepository pubRepo)
        {
            _pubRepo = pubRepo;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Publisher> Get()
        {
            return _pubRepo.SelectAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Publisher Get(int id)
        {
            return _pubRepo.SelectByID(id);
        }

        // POST api/values
        [HttpPost]
        public Publisher Post([FromBody]Publisher value)
        {
            if (_pubRepo.Add(value)) return value;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Publisher Put(int id, [FromBody]Publisher value)
        {
            if (_pubRepo.Update(value)) return value;
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _pubRepo.Delete(id);
        }
    }
}
