using System.Net;
using System.Text.Json;
using StockHub.Services.Dtos;
using StockHub.Services.Exceptions;
using StockHub.Services.DTO; 

namespace StockHub.Services.Middlewares
{
    public class HataYakalamaMiddleware
    {
        private readonly RequestDelegate _siradakiIslem;
        private readonly ILogger<HataYakalamaMiddleware> _gunlukKaydedici;
        private readonly IHostEnvironment _calismaOrtami;

        public HataYakalamaMiddleware(RequestDelegate siradakiIslem, ILogger<HataYakalamaMiddleware> gunlukKaydedici, IHostEnvironment calismaOrtami)
        {
            _siradakiIslem = siradakiIslem;
            _gunlukKaydedici = gunlukKaydedici;
            _calismaOrtami = calismaOrtami;
        }

        public async Task InvokeAsync(HttpContext baglam)
        {
            try
            {
                // İstek sorunsuzsa yoluna devam etsin (Controller'a gitsin)
                await _siradakiIslem(baglam);
            }
            catch (Exception hata)
            {// İstek yolu ve metodu ile birlikte detaylı loglama
                _gunlukKaydedici.LogError(hata, "[StockHub Hata] {Metot} {Yol} isteğinde hata oluştu: {Mesaj}",
                    baglam.Request.Method,
                    baglam.Request.Path,
                    hata.Message);

                baglam.Response.ContentType = "application/json";

                // Hatanın türüne göre durum kodunu ve mesajını belirliyoruz
                int durumKodu;
                string kullaniciMesaji;

                if (hata is YetersizStokException)
                {
                    durumKodu = (int)HttpStatusCode.BadRequest; // 400 - Geçersiz İstek
                    kullaniciMesaji = hata.Message;
                }
                else if (hata is KeyNotFoundException)
                {
                    durumKodu = (int)HttpStatusCode.NotFound;   // 404 - Bulunamadı
                    kullaniciMesaji = hata.Message;
                }
                else
                {
                    durumKodu = (int)HttpStatusCode.InternalServerError; // 500 - Beklenmeyen Hata
                    kullaniciMesaji = "Sunucu tarafında beklenmeyen bir hata oluştu.";
                }

                baglam.Response.StatusCode = durumKodu;

                // Türkçe modelimizi dolduruyoruz
                var cevap = new HataCevapDto
                {
                    DurumKodu = durumKodu,
                    Mesaj = kullaniciMesaji,
                    Detay = _calismaOrtami.IsDevelopment() ? hata.Message : null
                };

                var jsonAyarlari = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var jsonMetin = JsonSerializer.Serialize(cevap, jsonAyarlari);

                await baglam.Response.WriteAsync(jsonMetin);
            }
        }
    }
}