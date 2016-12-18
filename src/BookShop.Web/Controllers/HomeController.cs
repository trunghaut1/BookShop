using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.ClientRepository;
using Repository.Model;
using Repository.ViewModel;

namespace BookShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private BookRepository bookRepo;
        private CatRepository catRepo;
        private SubCatRepository subCatRepo;
        private int pageSize = 5;
        public HomeController(BookRepository bookRepo, CatRepository catRepo, SubCatRepository subCatRepo)
        {
            this.bookRepo = bookRepo;
            this.catRepo = catRepo;
            this.subCatRepo = subCatRepo;
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
        public async Task<IActionResult> RelatedPartial(int id)
        {
            IEnumerable<Book> book = await bookRepo.tGetRelated(id);
            if (book.Count() > 0)
                return PartialView("RelatedPartial", book);
            else
                return null;
        }
        public async Task<IActionResult> Cat(int id, int page = 1)
        {
            Cat cat = await catRepo.GetByID(id);
            ViewBag.Title = $"Loại: {cat?.Name}";
            ViewBag.CatID = id;
            ListPaging<Book> value = await bookRepo.tGetByCatPage(id, pageSize, page);
            return View("List", value);
        }
        public async Task<IActionResult> SubCat(int id, int page = 1)
        {
            SubCat subcat = await subCatRepo.GetByID(id);
            ViewBag.Title = $"Loại: {subcat?.Name}";
            ViewBag.SubCatID = id;
            ListPaging<Book> value = await bookRepo.tGetBySubCatPage(id, pageSize, page);
            return View("List", value);
        }
        public async Task<IActionResult> List(int page = 1)
        {
            ViewBag.Title = $"Sách";
            ViewBag.List = true;
            ListPaging<Book> value = await bookRepo.tGetPage(pageSize, page);
            return View(value);
        }
    }
}
