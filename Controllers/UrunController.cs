using Microsoft.AspNetCore.Mvc;
using StockHub.Web.Models;
using StockHub.Web.Services;

namespace StockHub.Web.Controllers
{
    public class UrunController : Controller
    {
        private readonly IApiService _apiService;

        public UrunController(IApiService apiService)
        {
            _apiService = apiService;
        }

        // 404 hatasını çözen ana metod burası:
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            // Backend API'deki [HttpGet] api/Urun endpoint'inden listeyi çekiyoruz
            var urunler = await _apiService.GetAsync<List<UrunListViewModel>>("Urun");

            return View(urunler ?? new List<UrunListViewModel>());
        }

        [HttpGet]
        public IActionResult Ekle()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            return View(new UrunEkleModel());
        }

        [HttpPost]
        public async Task<IActionResult> Ekle(UrunEkleModel model)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
                return View(model);

            var (basarili, hata) = await _apiService.PostWithDetailAsync("Urun", model);

            if (basarili)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Hata = hata;
            return View(model);
        }
        // GET: /Urun/Guncelle?id=...
        [HttpGet]
        public async Task<IActionResult> Guncelle(string id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (string.IsNullOrEmpty(id))
                return RedirectToAction(nameof(Index));

            var urun = await _apiService.GetAsync<UrunGuncelleModel>($"Urun/{id}");
            if (urun == null)
            {
                var liste = await _apiService.GetAsync<List<UrunGuncelleModel>>($"Urun/{id}");
                urun = liste?.FirstOrDefault();
            }

            if (urun == null)
                return RedirectToAction(nameof(Index));

            return View(urun);
        }

        // POST: /Urun/Guncelle
        [HttpPost]
        public async Task<IActionResult> Guncelle(UrunGuncelleModel model)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (!ModelState.IsValid)
                return View(model);

            var (basarili, hata) = await _apiService.PutWithDetailAsync("Urun", model);
            if (basarili)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Hata = hata;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Sil(string id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Login", "Auth");

            if (!string.IsNullOrEmpty(id))
            {
                await _apiService.DeleteWithDetailAsync($"Urun/{id}");
            }

            return RedirectToAction(nameof(Index));
        }
    }

}