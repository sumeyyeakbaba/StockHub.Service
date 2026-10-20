using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StockHub.Web.Models;
using StockHub.Web.Services;

namespace StockHub.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiService _apiService;

        public HomeController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Backend'deki Dashboard endpoint'inden canlı MongoDB sayılarını alıyoruz
            var ozet = await _apiService.GetAsync<DashboardOzetModel>("Dashboard");

            return View(ozet ?? new DashboardOzetModel());
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
    }
}