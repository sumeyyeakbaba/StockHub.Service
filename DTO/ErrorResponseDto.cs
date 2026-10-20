namespace StockHub.Services.DTO
{
    public class ErrorResponseDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Detailed { get; set; } // Geliştirme ortamında hataan detayını görmek için
    }
}
