using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.ClientRepository;
using BookShop.Web.Models;
using AspNetCore.Infrastructure;
using Repository.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookShop.Web.Controllers
{
    public class CartController : Controller
    {
        private BookRepository bookRepo;
        public CartController(BookRepository bookRepo)
        {
            this.bookRepo = bookRepo;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(GetCart());
        }
        public IActionResult RefreshCart()
        {
            return ViewComponent("CartSummary");
        }
        public async Task<bool> CheckBook(int id)
        {
            Book value = await bookRepo.tGetByID(id);
            return value != null ? true : false;
        }
        public async Task<bool> AddToCart(int id, int quantity)
        {
            Book book = await bookRepo.GetByID(id);
            if (book != null)
            {
                Cart cart = GetCart();
                cart.AddItem(book, quantity);
                SaveCart(cart);
                return true;
            }
            return false;
        }
        public async Task<bool> RemoveFromCart(int id)
        {
            Book book = await bookRepo.GetByID(id);
            if (book != null)
            {
                Cart cart = GetCart();
                cart.RemoveLine(book);
                SaveCart(cart);
                return true;
            }
            return false;
        }

        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
