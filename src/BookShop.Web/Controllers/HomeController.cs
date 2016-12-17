using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.ClientRepository;
using Repository.Model;

namespace BookShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private BookRepository bookRepo;
        public HomeController(BookRepository bookRepo)
        {
            this.bookRepo = bookRepo;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> book = await bookRepo.tGetByNumber(8);
            return View(book);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
