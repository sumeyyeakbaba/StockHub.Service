namespace StockHub.Web.Models
{
    public class TokenModel
    {
        public string Token { get; set; } = null!;
        public DateTime GecerlilikTarihi { get; set; }
    }
}