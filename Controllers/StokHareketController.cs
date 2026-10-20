using Microsoft.AspNetCore.Mvc;
using StockHub.DTOs.StokHareketDtos;
using StockHub.Services.Abstract;
using StockHub.Services.DTO;
using StockHub.Services.Services;

namespace StockHub.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StokHareketController : ControllerBase
    {
        private readonly IStokHareketService _stokHareketService;

        public StokHareketController(IStokHareketService stokHareketService)
        {
            _stokHareketService = stokHareketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStokHareket()
        {
            var values = await _stokHareketService.GetAllStokHareketAsync();
            return Ok(values);
        }

        [HttpGet("GetByUrunId/{urunId}")]
        public async Task<IActionResult> GetStokHareketleriByUrunId(string urunId)
        {
            var values = await _stokHareketService.GetStokHareketleriByUrunIdAsync(urunId);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStokHareket(CreateStokHareketDto createDto)
        {
            await _stokHareketService.CreateStokHareketAsync(createDto);
            return Ok("Stok hareketi başarıyla eklendi ve ürün stoğu güncellendi.");
        }
    }
}