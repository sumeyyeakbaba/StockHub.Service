using Microsoft.AspNetCore.Mvc;
using StockHub.Services.DTO.ProductDtos;
using StockHub.Services.Services.UrunServices;

namespace StockHub.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrunController : ControllerBase
    {
        private readonly IUrunService _urunService;

        public UrunController(IUrunService urunService)
        {
            _urunService = urunService;
        }

        // Tüm ürünleri getiren endpoint (GET: api/Urun)
        [HttpGet]
        public async Task<IActionResult> TumUrunleriGetir()
        {
            var urunler = await _urunService.TumUrunleriGetirAsync();
            return Ok(urunler);
        }

        // Id'ye göre ürün getiren endpoint (GET: api/Urun/{id})
        [HttpGet("{id}")]
        public async Task<IActionResult> IdyeGoreUrunGetir(string id)
        {
            var urun = await _urunService.IdyeGoreUrunGetirAsync(id);
            if (urun == null)
            {
                return NotFound("Ürün bulunamadı.");
            }
            return Ok(urun);
        }
        [HttpGet("paged")]
        public async Task<IActionResult> UrunleriFiltreliGetir([FromQuery] UrunFiltreDto filtreDto)
        {
            var result = await _urunService.UrunleriFiltreliGetirAsync(filtreDto);
            return Ok(result);
        }

        // Yeni ürün ekleyen endpoint (POST: api/Urun)
        [HttpPost]
        public async Task<IActionResult> UrunEkle(UrunEkleDto urunEkleDto)
        {
            await _urunService.UrunEkleAsync(urunEkleDto);
            return Ok("Ürün başarıyla eklendi.");
        }

        // Ürün güncelleyen endpoint (PUT: api/Urun)
        [HttpPut]
        public async Task<IActionResult> UrunGuncelle(UrunGuncelleDto urunGuncelleDto)
        {
            await _urunService.UrunGuncelleAsync(urunGuncelleDto);
            return Ok("Ürün başarıyla güncellendi.");
        }

        // Ürün silen endpoint (DELETE: api/Urun/{id})
        [HttpDelete("{id}")]
        public async Task<IActionResult> UrunSil(string id)
        {
            await _urunService.UrunSilAsync(id);
            return Ok("Ürün başarıyla silindi.");
        }
    }
}