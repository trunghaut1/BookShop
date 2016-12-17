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
        IBookRepository bookRepo;

        public BookController(IBookRepository bookRepo)
        {
            this.bookRepo = bookRepo;
        }
        // GET: api/values
        [HttpGet]
        public dynamic Get()
        {
            return bookRepo.GetAll().Select(o => new { o.ID, o.Name, o.Author, o.Summary, o.Image, o.Price, o.Quantity });
        }
        [HttpGet("name/{name}")]
        public dynamic GetByName(string name)
        {
            return bookRepo.GetByName(name).Select(o => new { o.ID, o.Name, o.Author, o.Summary, o.Image, o.Price, o.Quantity });
        }
        [HttpGet("page/{pageSize}/{page}")]
        public ListPaging<Book> GetPage(int pageSize, int page)
        {
            if(pageSize != 0 && page != 0)
                return bookRepo.GetPage(pageSize, page);
            return null;
        }
        [HttpGet("name/{name}/page/{pageSize}/{page}")]
        public ListPaging<Book> GetSearchPage(string name, int pageSize, int page)
        {
            if (pageSize != 0 && page != 0)
                return bookRepo.SearchPage(name, pageSize, page);
            return null;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            Book value = bookRepo.GetByID(id);
            return value != null ? new Book(value) : null;
        }

        // POST api/values
        [HttpPost]
        public int? Post([FromBody]Book value)
        {
            if (bookRepo.Add(value)) return value.ID;
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody]Book value)
        {
            return bookRepo.Update(value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return bookRepo.Delete(id);
        }
        // Time
        [HttpGet("time/getbynumber/{number}")]
        public dynamic tGetByNumber(int number)
        {
            return bookRepo.tGetByNumber(number).Select(o => new { o.ID, o.Name, o.Author, o.Summary, o.Image, o.Price, o.Quantity });
        }
        [HttpGet("time/getbyid/{id}")]
        public Book tGetByID(int id)
        {
            Book value = bookRepo.tGetByID(id);
            return value != null ? new Book(value) : null;
        }
    }
}
