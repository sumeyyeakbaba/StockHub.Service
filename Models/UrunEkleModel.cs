using System.ComponentModel.DataAnnotations;

namespace StockHub.Web.Models
{
    public class UrunEkleModel
    {
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        public string UrunAdi { get; set; } = null!;

        [Required(ErrorMessage = "Barkod zorunludur.")]
        public string Barkod { get; set; } = null!;

        [Required(ErrorMessage = "Kategori zorunludur.")]
        public string Kategori { get; set; } = null!;

        [Required(ErrorMessage = "Ürün görsel bağlantısı zorunludur.")]
        public string UrunResimUrl { get; set; } = "https://placehold.co/150";

        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı 0 veya daha büyük olmalıdır.")]
        public int StokMiktari { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Kritik seviye 0 veya daha büyük olmalıdır.")]
        public int KritikStokSeviyesi { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır.")]
        public decimal UrunFiyati { get; set; }
    }
}