using StockHub.Entities;

namespace StockHub.DTOs.StokHareketDtos
{
    public class CreateStokHareketDto
    {
        public string UrunId { get; set; } = string.Empty;
        public StokHareketTuru HareketTuru { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public string? Aciklama { get; set; }
    }
}