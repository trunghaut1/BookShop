using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.ServerRepository;
using Repository.Model;
using Repository.ViewModel;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookShop.API.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        IEFBookRepository bookRepo;

        public BookController(IEFBookRepository bookRepo)
        {
            this.bookRepo = bookRepo;
        }
        // GET: api/values
        [HttpGet]
        public dynamic Get()
        {
            return bookRepo.GetAll().Select(o => new { o.ID, o.Name, o.Author, o.Summary, o.Image, o.Price, o.Quantity });
        }
        [HttpGet("page/{pageSize}/{page}")]
        public BookPaging GetPage(int pageSize, int page)
        {
            return bookRepo.GetPage(pageSize, page);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return bookRepo.GetByID(id);
        }

        [HttpGet("cat/{id}")]
        public IEnumerable<Book> GetByCat(int id)
        {
            return bookRepo.GetByCat(id);
        }

        [HttpGet("subcat/{id}")]
        public IEnumerable<Book> GetBySubCat(int id)
        {
            return bookRepo.GetBySubCat(id);
        }

        // POST api/values
        [HttpPost]
        public Book Post([FromBody]Book value)
        {
            if (bookRepo.Add(value)) return value;
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
