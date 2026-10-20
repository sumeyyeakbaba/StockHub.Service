using StockHub.DTOs.StokHareketDtos;

namespace StockHub.Services.Abstract
{
    public interface IStokHareketService
    {
        Task<List<ResultStokHareketDto>> GetAllStokHareketAsync();
        Task<List<ResultStokHareketDto>> GetStokHareketleriByUrunIdAsync(string urunId);
        Task<bool> CreateStokHareketAsync(CreateStokHareketDto createDto);
    }
}