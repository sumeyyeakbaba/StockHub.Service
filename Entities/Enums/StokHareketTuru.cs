using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StockHub.Entities
{
    public enum StokHareketTuru
    {
        Giris = 1,
        Cikis = 2
    }

    public class StokHareket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UrunId { get; set; } = string.Empty;

        public StokHareketTuru HareketTuru { get; set; }

        public int Miktar { get; set; }

        public decimal BirimFiyat { get; set; }

        public string? Aciklama { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Tarih { get; set; } = DateTime.UtcNow;
    }
}