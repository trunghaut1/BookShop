using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Repository.ClientRepository;
using BookShop.Web.Models.ViewModels;
using Repository.Model;
using BookShop.Web.Infrastructure;
using Repository.ViewModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace BookShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private UserRepository uRepo;
        private OrderRepository orderRepo;
        int pageSize = 5;
        public AccountController(UserRepository uRepo, OrderRepository orderRepo)
        {
            this.uRepo = uRepo;
            this.orderRepo = orderRepo;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(int page = 1)
        {
            User u = HttpContext.Session.GetJson<User>("User");
            if (u != null)
            {
                ListPaging<Order> order = await orderRepo.GetByUserPage(u.ID, pageSize, page);
                return View(order);
            }
            else
            {
                TempData["Message"] = new[] { "danger", "Vui lòng đăng nhập" };
                return Redirect("/");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            User u = await uRepo.GetByEmail(model.Name);
            if(u != null)
            {
                if(u.Pass == model.Password)
                {
                    HttpContext.Session.SetJson("User", u);
                    return Redirect(model.ReturnUrl);
                }
                else
                {
                    TempData["Message"] = new[] { "danger", "Mật khẩu không đúng" };
                    return Redirect(model.ReturnUrl);
                }
            }
            else
            {
                TempData["Message"] = new[] { "danger", "Địa chỉ email không tồn tại" };
                return Redirect(model.ReturnUrl);
            }
        }
        public RedirectResult Logout(string returnUrl = "/")
        {
            HttpContext.Session.Remove("User");
            if (returnUrl.Contains("Account"))
            {
                return Redirect("/");
            }
            return Redirect(returnUrl);
        }
        [HttpPost]
        public async Task<IActionResult> Signup(User user, string url = "/")
        {
            User u = await uRepo.GetByEmail(user.Email);
            if (u == null)
            {
                user.IsAdmin = false;
                int result = await uRepo.Add(user);
                if (result > 0)
                {
                    TempData["Message"] = new[] { "success", "Đã tạo tài khoản" };
                }
                else
                    TempData["Message"] = new[] { "danger", "Lỗi tạo tài khoản, kiểm tra lại thông tin" };
            }
            else
            {
                TempData["Message"] = new[] { "danger", "Tài khoản đã tồn tại" };
            }
            return Redirect(url);
        }
        public IActionResult Info()
        {
            User u = HttpContext.Session.GetJson<User>("User");
            if(u != null)
            {
                return View(u);
            }
            else
            {
                return View(new User());
            }
        }
    }
}
