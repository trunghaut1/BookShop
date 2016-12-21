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
    public class SubCatController : Controller
    {
        ISubCatRepository subCatRepo;

        public SubCatController(ISubCatRepository subCatRepo)
        {
            this.subCatRepo = subCatRepo;
        }
        // GET: api/values
        [HttpGet]
        public dynamic Get()
        {
            return subCatRepo.GetAll().Select(o => new { o.ID, o.Name, o.CatID, Cat = new { o.Cat.ID, o.Cat.Name } });
        }
        [HttpGet("book/{id}")]
        public dynamic GetByBook(int id)
        {
            return subCatRepo.GetByBook(id).Select(o => new { o.ID, o.Name, o.CatID });
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public SubCat Get(int id)
        {
            var value = subCatRepo.GetByID(id);
            return value != null ? new SubCat(value) : null;
        }

        // POST api/values
        [HttpPost]
        public int? Post([FromBody]SubCat value)
        {
            if (subCatRepo.Add(value)) return value.ID;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]SubCat value)
        {
            return subCatRepo.Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return subCatRepo.Delete(id);
        }
    }
}
