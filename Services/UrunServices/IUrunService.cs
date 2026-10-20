using StockHub.Services.DTO;
using StockHub.Services.DTO.ProductDtos; // veya UrunDtos

namespace StockHub.Services.Services.UrunServices
{
    public interface IUrunService
    {
        Task<List<UrunListeleDto>> TumUrunleriGetirAsync();
        Task UrunEkleAsync(UrunEkleDto urunEkleDto);
        Task UrunGuncelleAsync(UrunGuncelleDto urunGuncelleDto);
        Task UrunSilAsync(string id);
        Task<UrunGetirDto> IdyeGoreUrunGetirAsync(string id);
        // Sayfalama ve Filtreleme Metodu
        Task<PagedResultDto<UrunListeleDto>> UrunleriFiltreliGetirAsync(UrunFiltreDto filtreDto);
    }
}