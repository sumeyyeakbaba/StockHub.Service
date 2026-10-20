using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StockHub.Services.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public string Barcode { get; set; } = string.Empty;

        public string CategoryName { get; set; } = "Genel";

        public int StockQuantity { get; set; }

        public int CriticalStockLevel { get; set; } = 10;

        public decimal ProductPrice { get; set; }

        public string? ProductImageUrl { get; set; }
    }
}