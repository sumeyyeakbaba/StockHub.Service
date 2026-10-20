using Microsoft.AspNetCore.Mvc;
using StockHub.Web.Models;
using StockHub.Web.Services;

namespace StockHub.Web.Controllers
{
    public class StokHareketController : Controller
    {
        private readonly IApiService _apiService;

        public StokHareketController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: /StokHareket/Ekle?urunId=...
        [HttpGet]
        public async Task<IActionResult> Ekle(string urunId)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (string.IsNullOrEmpty(urunId))
                return RedirectToAction("Index", "Urun");

            var urun = await _apiService.GetAsync<UrunGuncelleModel>($"Urun/{urunId}");

            var model = new StokHareketEkleViewModel
            {
                UrunId = urunId,
                UrunAdi = urun?.UrunAdi ?? "Seçili Ürün"
            };

            // ~/ kaldırıldı, göreceli yol verildi:
            return View("../Urun/StokIslem", model);
        }
        // POST: /StokHareket/Ekle
        [HttpPost]
        public async Task<IActionResult> Ekle(StokHareketEkleViewModel model)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
                return View("../Urun/StokIslem", model);

            var (basarili, hata) = await _apiService.PostWithDetailAsync("StokHareket", model);
            if (basarili)
            {
                return RedirectToAction("Index", "Urun");
            }

            ViewBag.Hata = hata;
            return View("../Urun/StokIslem", model);
        }
    }
}