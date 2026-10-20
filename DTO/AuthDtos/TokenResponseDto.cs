namespace StockHub.Services.DTO.AuthDtos
{
    public class TokenResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime GecerlilikTarihi { get; set; }
    }
}