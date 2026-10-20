using StockHub.Entities;

namespace StockHub.DTOs.StokHareketDtos
{
    public class ResultStokHareketDto
    {
        public string Id { get; set; } = string.Empty;
        public string UrunId { get; set; } = string.Empty;
        public StokHareketTuru HareketTuru { get; set; }
        public int Miktar { get; set; }
        public decimal BirimFiyat { get; set; }
        public string? Aciklama { get; set; }
        public DateTime Tarih { get; set; }
    }
}