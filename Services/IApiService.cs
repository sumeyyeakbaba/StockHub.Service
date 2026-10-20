namespace StockHub.Web.Services
{
    public interface IApiService
    {
        Task<bool> PostAsync<T>(string endpoint, T data);
        Task<string?> GirisYapAsync(string kullaniciAdi, string sifre);
        Task<T?> GetAsync<T>(string endpoint);
        Task<(bool Basarili, string HataMesaji)> PostWithDetailAsync<T>(string endpoint, T data);
        Task<(bool Basarili, string HataMesaji)> PutWithDetailAsync<T>(string endpoint, T data);
        Task<(bool Basarili, string HataMesaji)> DeleteWithDetailAsync(string endpoint);
    }
}