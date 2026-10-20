namespace StockHub.Services.DTO.ProductDtos
{
    public class UrunGetirDto
    {
        public string UrunId { get; set; } = string.Empty;
        public string UrunAdi { get; set; } = string.Empty;
        public string Barkod { get; set; } = string.Empty;
        public string Kategori { get; set; } = string.Empty;
        public int StokMiktari { get; set; }
        public int KritikStokSeviyesi { get; set; }
        public decimal UrunFiyati { get; set; }
        public string? UrunResimUrl { get; set; }
    }
}