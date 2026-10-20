using Microsoft.AspNetCore.Mvc;
using StockHub.Services.DTO.CategoryDtos;
using StockHub.Services.Services.KategoriServices;

namespace StockHub.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategoriController : ControllerBase
    {
        private readonly IKategoriService _kategoriService;

        public KategoriController(IKategoriService kategoriService)
        {
            _kategoriService = kategoriService;
        }

        // Tüm kategorileri getiren endpoint (GET: api/Kategori)
        [HttpGet]
        public async Task<IActionResult> KategoriListesi()
        {
            var kategoriler = await _kategoriService.TumKategorileriGetirAsync();
            return Ok(kategoriler);
        }

        // Id'ye göre tek kategori getiren endpoint (GET: api/Kategori/id)
        [HttpGet("{id}")]
        public async Task<IActionResult> IdyeGoreKategoriGetir(string id)
        {
            var deger = await _kategoriService.IdyeGoreKategoriGetirAsync(id);
            if (deger == null) return NotFound("Kategori bulunamadı.");
            return Ok(deger);
        }

        // Yeni kategori ekleyen endpoint (POST: api/Kategori)
        [HttpPost]
        public async Task<IActionResult> KategoriEkle(KategoriEkleDto kategoriEkleDto)
        {
            await _kategoriService.KategoriEkleAsync(kategoriEkleDto);
            return Ok("Kategori başarıyla eklendi.");
        }

        // Kategori güncelleyen endpoint (PUT: api/Kategori)
        [HttpPut]
        public async Task<IActionResult> KategoriGuncelle(KategoriGuncelleDto kategoriGuncelleDto)
        {
            await _kategoriService.KategoriGuncelleAsync(kategoriGuncelleDto);
            return Ok("Kategori başarıyla güncellendi.");
        }

        // Kategori silen endpoint (DELETE: api/Kategori/id)
        [HttpDelete("{id}")]
        public async Task<IActionResult> KategoriSil(string id)
        {
            await _kategoriService.KategoriSilAsync(id);
            return Ok("Kategori başarıyla silindi.");
        }
    }
}