namespace StockHub.Services.DTO.ProductDtos
{
    public class UrunEkleDto
    {
        public string UrunAdi { get; set; } = string.Empty;
        public string Barkod { get; set; } = string.Empty;
        public string Kategori { get; set; } = string.Empty; // Web'deki Kategori ismiyle birebir eşleşmeli
        public int StokMiktari { get; set; }
        public int KritikStokSeviyesi { get; set; }
        public decimal UrunFiyati { get; set; }
        public string? UrunResimUrl { get; set; }
    }
}