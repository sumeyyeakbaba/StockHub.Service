using Microsoft.AspNetCore.Mvc;
using StockHub.Web.Models;
using StockHub.Web.Services;

namespace StockHub.Web.Controllers
{
    public class WebUrunController : Controller
    {
        private readonly IApiService _apiService;

        public WebUrunController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // GET: /WebUrun veya /WebUrun/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            var urunler = await _apiService.GetAsync<List<UrunListViewModel>>("api/Urun");

            return View("~/Views/Urun/Index1.cshtml", urunler ?? new List<UrunListViewModel>());
        }
        // GET: /WebUrun/Ekle
        [HttpGet]
        public IActionResult Ekle()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            return View("~/Views/Urun/Ekle.cshtml", new UrunEkleModel());
        }

        // POST: /WebUrun/Ekle
        [HttpPost]
        public async Task<IActionResult> Ekle(UrunEkleModel model)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
                return View("~/Views/Urun/Ekle.cshtml", model);

            var (basarili, hata) = await _apiService.PostWithDetailAsync("api/Urun", model);
            if (basarili)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hata = hata;
            return View("~/Views/Urun/Ekle.cshtml", model);
        }

        // GET: /WebUrun/Guncelle?id=...
        [HttpGet]
        public async Task<IActionResult> Guncelle(string id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            var urun = await _apiService.GetAsync<UrunGuncelleModel>($"api/Urun/{id}");
            if (urun == null)
            {
                var liste = await _apiService.GetAsync<List<UrunGuncelleModel>>($"api/Urun/{id}");
                urun = liste?.FirstOrDefault();
            }

            if (urun == null)
                return RedirectToAction(nameof(Index));

            return View("~/Views/Urun/Guncelle.cshtml", urun);
        }

        // POST: /WebUrun/Guncelle
        [HttpPost]
        public async Task<IActionResult> Guncelle(UrunGuncelleModel model)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
                return View("~/Views/Urun/Guncelle.cshtml", model);

            var (basarili, hata) = await _apiService.PutWithDetailAsync("api/Urun", model);
            if (basarili)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hata = hata;
            return View("~/Views/Urun/Guncelle.cshtml", model);
        }

        // GET: /WebUrun/Sil?id=...
        [HttpGet]
        public async Task<IActionResult> Sil(string id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (!string.IsNullOrEmpty(id))
            {
                await _apiService.DeleteWithDetailAsync($"api/Urun/{id}");
            }

            return RedirectToAction(nameof(Index));
        }
    }
}