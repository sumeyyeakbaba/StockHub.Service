using StockHub.Services.DTO.CategoryDtos;

namespace StockHub.Services.Services.KategoriServices
{
    public interface IKategoriService
    {
        // Tüm kategorileri listeler
        Task<List<KategoriListeleDto>> TumKategorileriGetirAsync();

        // Yeni kategori ekler
        Task KategoriEkleAsync(KategoriEkleDto kategoriEkleDto);

        // Kategori günceller
        Task KategoriGuncelleAsync(KategoriGuncelleDto kategoriGuncelleDto);

        // Id'ye göre kategoriyi siler
        Task KategoriSilAsync(string id);

        // Id'ye göre tek bir kategori getirir
        Task<KategoriGetirDto> IdyeGoreKategoriGetirAsync(string id);
    }

}
