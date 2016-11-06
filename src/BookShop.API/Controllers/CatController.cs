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
    public class CatController : Controller
    {
        ICatRepository _catRepo;

        public CatController(ICatRepository catRepo)
        {
            _catRepo = catRepo;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Cat> Get()
        {
            return _catRepo.SelectAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Cat Get(int id)
        {
            return _catRepo.SelectByID(id);
        }

        // POST api/values
        [HttpPost]
        public Cat Post([FromBody]Cat value)
        {
            if (_catRepo.Add(value)) return value;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public Cat Put(int id, [FromBody]Cat value)
        {
            if (_catRepo.Update(value)) return value;
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _catRepo.Delete(id);
        }
    }
}
