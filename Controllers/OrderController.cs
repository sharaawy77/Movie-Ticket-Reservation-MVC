using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie.Data.Services;
using Movie.Models;

namespace Movie.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderservice orderservice;
        private readonly UserManager<IdentityUser> userManager;

        public OrderController(IOrderservice orderservice,UserManager<IdentityUser> userManager)
        {
            this.orderservice = orderservice;
            this.userManager = userManager;
        }
        [HttpGet]
        public  IActionResult CheckOut()
        {
            if (userManager.GetUserAsync(User).Result!=null)
            {
                ViewBag.user=userManager.GetUserAsync(User).Result;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CheckOut(Order model)
        {
            if (ModelState.IsValid)
            {
                await orderservice.StoreOrderAsync(model);
                return RedirectToAction("Index","Movies");

            }
            return View(model);
        }
    }
}
