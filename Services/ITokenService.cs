using StockHub.Services.DTO.AuthDtos;

namespace StockHub.Services.Services
{
    public interface ITokenService
    {
        TokenResponseDto TokenUret(string kullaniciAdi, string rol);
    }
}