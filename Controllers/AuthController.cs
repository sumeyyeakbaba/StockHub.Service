using Microsoft.AspNetCore.Mvc;
using StockHub.Services.DTO.AuthDtos;
using StockHub.Services.Services;

namespace StockHub.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult GirisYap([FromBody] KullaniciGirisDto model)
        {
            // Test amaçlı sabit kullanıcı doğrulaması (Admin girişi)
            if (model.KullaniciAdi == "admin" && model.Sifre == "123456")
            {
                var tokenResult = _tokenService.TokenUret(model.KullaniciAdi, "Admin");
                return Ok(tokenResult);
            }

            return Unauthorized(new { Mesaj = "Kullanıcı adı veya şifre hatalı." });
        }
    }
}