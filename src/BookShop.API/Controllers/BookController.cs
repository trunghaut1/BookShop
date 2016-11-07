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
    public class BookController : Controller
    {
        IBookRepository _bookRepo;

        public BookController(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _bookRepo.SelectAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return _bookRepo.SelectByID(id);
        }

        [HttpGet("publisher/{id}")]
        public Book GetByPublisher(int id)
        {
            return _bookRepo.SelectByPublisher(id);
        }

        [HttpGet("cat/{id}")]
        public IEnumerable<Book> GetByCat(int id)
        {
            return _bookRepo.SelectByCat(id);
        }

        [HttpGet("subcat/{id}")]
        public IEnumerable<Book> GetBySubCat(int id)
        {
            return _bookRepo.SelectBySubCat(id);
        }

        // POST api/values
        [HttpPost]
        public Book Post([FromBody]Book value)
        {
            if (_bookRepo.Add(value)) return value;
            return value;
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
