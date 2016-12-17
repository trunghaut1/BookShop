using AspNetCore.Infrastructure;
using BookShop.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.ClientRepository;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Web.Components
{
    public class MenuViewComponent : ViewComponent
    {
        private CatRepository catRepo;
        public MenuViewComponent(CatRepository catRepo)
        {
            this.catRepo = catRepo;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.cartNumber = GetCart().Lines.Count();
            IEnumerable<Cat> cat = await catRepo.GetCatSub();
            return View(cat);
        }
        private Cart GetCart()
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }
    }
}
