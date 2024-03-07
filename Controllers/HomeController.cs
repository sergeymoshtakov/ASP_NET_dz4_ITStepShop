using ITStepShop.Data;
using ITStepShop.Models;
using ITStepShop.Models.ViewModel;
using ITStepShop.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ITStepShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Products = _db.Product,
                Categories = _db.Category
            };
            return View(homeVM);
        }

        public IActionResult Details(int id)
        {
            List<ShopingCart> shopingCartList = new List<ShopingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart).Count() > 0)
            {
                shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart);
            }
            DetailsVM detailsVM = new DetailsVM()
            {
                Product = _db.Product.Include(x => x.Category).Where(u => u.Id == id).FirstOrDefault(),
                ExistsInCart = false
            };
            foreach (var item in shopingCartList)
            {
                if (item.ProductId == id)
                {
                    detailsVM.ExistsInCart = true;
                }
            }
            return View(detailsVM);
        }

        [HttpPost, ActionName("Details")]
        public IActionResult DetailsPost(int id)
        {
            List<ShopingCart> shopingCartList = new List<ShopingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart).Count() > 0)
            {
                shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart);
            }
            shopingCartList.Add(new ShopingCart { ProductId = id });
            HttpContext.Session.Set(WC.SessionCart, shopingCartList);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int id)
        {
            List<ShopingCart> shopingCartList = new List<ShopingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart) != null && HttpContext.Session.Get<IEnumerable<ShopingCart>>(WC.SessionCart).Count() > 0)
            {
                shopingCartList = HttpContext.Session.Get<List<ShopingCart>>(WC.SessionCart);
            }
            var itemRemove = shopingCartList.SingleOrDefault(x => x.ProductId == id);
            if (itemRemove != null)
            { 
                shopingCartList.Remove(itemRemove);
            }
            HttpContext.Session.Set(WC.SessionCart, shopingCartList);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        //CKEditor

        //[HttpPost]
        //public ActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        //{
        //    if (upload.Length <= 0) return null;
        //    //if (!upload.IsImage())
        //    //{
        //    //    var NotImageMessage = "please choose a picture";
        //    //    dynamic NotImage = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + NotImageMessage + "\"}}");
        //    //    return Json(NotImage);
        //    //}

        //    var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();

        //    Image image = Image.FromStream(upload.OpenReadStream());
        //    int width = image.Width;
        //    int height = image.Height;
        //    if ((width > 750) || (height > 500))
        //    {
        //        var DimensionErrorMessage = "Custom Message for error";
        //        dynamic stuff = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + DimensionErrorMessage + "\"}}");
        //        return Json(stuff);
        //    }

        //    if (upload.Length > 500 * 1024)
        //    {
        //        var LengthErrorMessage = "Custom Message for error";
        //        dynamic stuff = JsonConvert.DeserializeObject("{ 'uploaded': 0, 'error': { 'message': \"" + LengthErrorMessage + "\"}}");
        //        return Json(stuff);
        //    }

        //    var path = Path.Combine(
        //        Directory.GetCurrentDirectory(), "wwwroot/images/CKEditorImages",
        //        fileName);

        //    using (var stream = new FileStream(path, FileMode.Create))
        //    {
        //        upload.CopyTo(stream);

        //    }

        //    var url = $"{"/images/CKEditorImages/"}{fileName}";
        //    var successMessage = "image is uploaded successfully";
        //    dynamic success = JsonConvert.DeserializeObject("{ 'uploaded': 1,'fileName': \"" + fileName + "\",'url': \"" + url + "\", 'error': { 'message': \"" + successMessage + "\"}}");
        //    return Json(success);
        //}
    }
}
