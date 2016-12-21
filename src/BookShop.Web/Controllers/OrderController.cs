using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.ClientRepository;
using BookShop.Web.Infrastructure;
using Repository.Model;
using BookShop.Web.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookShop.Web.Controllers
{
    public class OrderController : Controller
    {
        private OrderRepository orderRepo;
        private BookRepository bookRepo;
        private Cart cart;
        public OrderController(OrderRepository orderRepo, Cart cartService, BookRepository bookRepo)
        {
            this.orderRepo = orderRepo;
            this.bookRepo = bookRepo;
            cart = cartService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Checkout()
        {
            User u = HttpContext.Session.GetJson<User>("User");
            if(u != null)
            {
                if (cart.Lines.Count() == 0)
                {
                    TempData["Message"] = new[] { "danger", "Giỏ hàng trống" };
                    return Redirect("/Cart");
                }
                ViewBag.cart = cart;
                return View(new Order());
            }
            else
            {
                TempData["Message"] = new[] { "danger", "Vui lòng đăng nhập để thực hiện thanh toán" };
                return Redirect("/Cart");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            User u = HttpContext.Session.GetJson<User>("User");
            order.UserID = u.ID;
            order.Time = DateTime.Now;
            order.Status = false;
            foreach (var value in cart.Lines)
            {
                order.OrderDetail.Add(new OrderDetail()
                {
                    OrderID = 0,
                    BookID = value.Book.ID,
                    Price = (int)value.Book.Price,
                    Quantity = value.Quantity
                });
                await bookRepo.SetQuantity(value.Book.ID, value.Quantity);
            }
            int result = await orderRepo.Add(order);
            if (result > 0)
            {
                cart.Clear();
                TempData["Message"] = new[] { "success", "Đặt hàng thành công" };
                return Redirect("/Account");
            }
            else
            {
                TempData["Message"] = new[] { "danger", "Lỗi đặt hàng" };
                return View(order);
            }
        }
    }
}
