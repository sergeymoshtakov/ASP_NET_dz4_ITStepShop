using ITStepShop.Data;
using Microsoft.AspNetCore.Http;
using ITStepShop.Models;
using Microsoft.AspNetCore.Mvc;
using ITStepShop.Utility;

namespace ITStepShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ShopingCart> shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart) ?? new List<ShopingCart>();

            List<Product> productsInCart = _db.Product.Where(p => shopingCartList.Any(item => item.ProductId == p.Id)).ToList();

            double totalAmount = productsInCart.Sum(p => p.Price);

            ViewData["ProductsInCart"] = productsInCart;
            ViewData["TotalAmount"] = totalAmount;

            return View();
        }
    }
}
