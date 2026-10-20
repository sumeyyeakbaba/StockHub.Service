using System.ComponentModel.DataAnnotations;

namespace StockHub.Web.Models
{
    public class StokHareketEkleViewModel
    {
        [Required]
        public string UrunId { get; set; } = string.Empty;

        public string? UrunAdi { get; set; }

        [Required(ErrorMessage = "Miktar alanı zorunludur.")]
        [Range(1, int.MaxValue, ErrorMessage = "Miktar en az 1 olmalıdır.")]
        public int Miktar { get; set; }

        [Required(ErrorMessage = "Hareket türü seçilmelidir.")]
        public string HareketTuru { get; set; } = "Giris"; // "Giris" veya "Cikis"

        public string? Aciklama { get; set; }
    }
}