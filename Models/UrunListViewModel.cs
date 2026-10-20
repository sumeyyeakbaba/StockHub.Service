using System.Text.Json.Serialization;

namespace StockHub.Web.Models
{
    public class UrunListViewModel
    {
        [JsonPropertyName("urunId")]
        public string UrunId { get; set; } = string.Empty;

        public string Id
        {
            get => UrunId;
            set => UrunId = value;
        }

        public string ProductId
        {
            get => UrunId;
            set => UrunId = value;
        }

        [JsonPropertyName("urunAdi")]
        public string UrunAdi { get; set; } = string.Empty;

        [JsonPropertyName("barkod")]
        public string Barkod { get; set; } = "-";

        // API'den kategoriAdi veya kategori gelse de yakalaması için:
        [JsonPropertyName("kategoriAdi")]
        public string KategoriAdi { get; set; } = string.Empty;

        public string Kategori
        {
            get => !string.IsNullOrEmpty(KategoriAdi) ? KategoriAdi : _kategori;
            set => _kategori = value;
        }
        private string _kategori = "Genel";

        [JsonPropertyName("stokMiktari")]
        public int StokMiktari { get; set; }

        [JsonPropertyName("kritikStokSeviyesi")]
        public int KritikStokSeviyesi { get; set; } = 10;

        [JsonPropertyName("urunFiyati")]
        public decimal UrunFiyati { get; set; }

        [JsonPropertyName("urunResimUrl")]
        public string UrunResimUrl { get; set; } = "https://placehold.co/45";
    }
}