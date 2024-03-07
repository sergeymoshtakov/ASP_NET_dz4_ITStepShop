using ITStepShop.Data;
using ITStepShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace ITStepShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;
        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                order.OrderDate = DateTime.Now;
                _db.Orders.Add(order);
                _db.SaveChanges();

                // Перенаправлення на сторінку підтвердження замовлення
                return RedirectToAction("Confirmation");
            }
            return View(order);
        }

        // Сторінка підтвердження замовлення
        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
