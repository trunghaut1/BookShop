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
    public class CatController : Controller
    {
        IEFCatRepository catRepo;

        public CatController(IEFCatRepository catRepo)
        {
            this.catRepo = catRepo;
        }
        // GET: api/values
        [HttpGet]
        public dynamic Get()
        {
            return catRepo.GetAll().Select(o => new { o.ID, o.Name });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Cat Get(int id)
        {
            var value = catRepo.GetByID(id);
            return new Cat(value);
        }

        // POST api/values
        [HttpPost]
        public int? Post([FromBody]Cat value)
        {
            if (catRepo.Add(value)) return value.ID;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Cat Put(int id, [FromBody]Cat value)
        {
            if (catRepo.Update(value)) return new Cat(value);
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return catRepo.Delete(id);
        }
    }
}
