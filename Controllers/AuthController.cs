using Microsoft.AspNetCore.Mvc;
using StockHub.Web.Models;
using StockHub.Web.Services;

namespace StockHub.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IApiService _apiService;

        public AuthController(IApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(GirisModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var token = await _apiService.GirisYapAsync(model.KullaniciAdi, model.Sifre);

            if (string.IsNullOrEmpty(token))
            {
                ViewBag.Hata = "Kullanıcı adı veya şifre hatalı!";
                return View(model);
            }

            // Alınan JWT Token'ı Session'a yazıyoruz
            HttpContext.Session.SetString("AuthToken", token);
            HttpContext.Session.SetString("KullaniciAdi", model.KullaniciAdi);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}