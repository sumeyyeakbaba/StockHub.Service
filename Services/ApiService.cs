using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using StockHub.Web.Models;

namespace StockHub.Web.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<bool> PostAsync<T>(string endpoint, T data)
        {
            var client = _httpClientFactory.CreateClient("StockHubApi");

            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonIcerik = new StringContent(
                JsonSerializer.Serialize(data),
                Encoding.UTF8,
                "application/json"
            );

            var yanit = await client.PostAsync(endpoint, jsonIcerik);

            if (!yanit.IsSuccessStatusCode)
            {
                var hataMesaji = await yanit.Content.ReadAsStringAsync();
                Console.WriteLine($"[API POST HATASI] Kod: {yanit.StatusCode} | Detay: {hataMesaji}");
            }

            return yanit.IsSuccessStatusCode;
        }

        // Login olup Token alma
        public async Task<string?> GirisYapAsync(string kullaniciAdi, string sifre)
        {
            var client = _httpClientFactory.CreateClient("StockHubApi");
            var istekIcerigi = new StringContent(
                JsonSerializer.Serialize(new { kullaniciAdi, sifre }),
                Encoding.UTF8,
                "application/json"
            );

            var yanit = await client.PostAsync("Auth/login", istekIcerigi);
            if (!yanit.IsSuccessStatusCode)
                return null;

            var jsonMetin = await yanit.Content.ReadAsStringAsync();
            var tokenSonuc = JsonSerializer.Deserialize<TokenModel>(jsonMetin, _jsonOptions);

            return tokenSonuc?.Token;
        }

        // Token ekleyerek GET isteği atma
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            var client = _httpClientFactory.CreateClient("StockHubApi");

            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var yanit = await client.GetAsync(endpoint);
            if (!yanit.IsSuccessStatusCode)
                return default;

            var icerik = await yanit.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(icerik, _jsonOptions);
        }

        public async Task<(bool Basarili, string HataMesaji)> PostWithDetailAsync<T>(string endpoint, T data)
        {
            var client = _httpClientFactory.CreateClient("StockHubApi");

            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);

            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonBody = JsonSerializer.Serialize(data);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return (true, string.Empty);
            }

            var hataIcerigi = await response.Content.ReadAsStringAsync();
            return (false, $"Durum Kodu: {(int)response.StatusCode} {response.StatusCode} | Detay: {hataIcerigi}");
        }

        // Ürün Güncelleme için PUT Metodu
        public async Task<(bool Basarili, string HataMesaji)> PutWithDetailAsync<T>(string endpoint, T data)
        {
            var client = _httpClientFactory.CreateClient("StockHubApi");

            var request = new HttpRequestMessage(HttpMethod.Put, endpoint);

            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var jsonBody = JsonSerializer.Serialize(data);
            request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return (true, string.Empty);
            }

            var hataIcerigi = await response.Content.ReadAsStringAsync();
            return (false, $"Durum Kodu: {(int)response.StatusCode} {response.StatusCode} | Detay: {hataIcerigi}");
        }

        // Ürün Silme için DELETE Metodu
        public async Task<(bool Basarili, string HataMesaji)> DeleteWithDetailAsync(string endpoint)
        {
            var client = _httpClientFactory.CreateClient("StockHubApi");

            var request = new HttpRequestMessage(HttpMethod.Delete, endpoint);

            var token = _httpContextAccessor.HttpContext?.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return (true, string.Empty);
            }

            var hataIcerigi = await response.Content.ReadAsStringAsync();
            return (false, $"Durum Kodu: {(int)response.StatusCode} {response.StatusCode} | Detay: {hataIcerigi}");
        }
    }
}