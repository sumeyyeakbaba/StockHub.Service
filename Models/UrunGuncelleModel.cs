using System.ComponentModel.DataAnnotations;

namespace StockHub.Web.Models
{
    public class UrunGuncelleModel
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Ürün adı boş bırakılamaz.")]
        public string UrunAdi { get; set; } = null!;

        [Required(ErrorMessage = "Barkod boş bırakılamaz.")]
        public string Barkod { get; set; } = null!;

        [Required(ErrorMessage = "Kategori boş bırakılamaz.")]
        public string Kategori { get; set; } = null!;

        [Required(ErrorMessage = "Ürün görsel bağlantısı boş bırakılamaz.")]
        public string UrunResimUrl { get; set; } = null!;

        [Range(0, int.MaxValue, ErrorMessage = "Stok miktarı 0 veya daha büyük olmalıdır.")]
        public int StokMiktari { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Kritik seviye 0 veya daha büyük olmalıdır.")]
        public int KritikStokSeviyesi { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0 TL'den büyük olmalıdır.")]
        public decimal UrunFiyati { get; set; }
    }
}