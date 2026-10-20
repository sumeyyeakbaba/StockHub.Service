using MongoDB.Driver;
using StockHub.Entities;
using StockHub.Services.Abstract;
using StockHub.Services.DTO;
using StockHub.Services.Entities;
using StockHub.Services.Services.UrunServices;

namespace StockHub.Services.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUrunService _urunService;
        private readonly IMongoCollection<StokHareket> _stokHareketCollection;

        public DashboardService(IUrunService urunService, IDatabaseSettings databaseSettings)
        {
            _urunService = urunService;

            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _stokHareketCollection = database.GetCollection<StokHareket>("StokHareketler");
        }

        public async Task<DashboardDto> GetDashboardStatisticsAsync()
        {
            // 1. Ürünleri ve DTO üzerindeki StokMiktari / UrunFiyati alanlarını doğrudan alıyoruz
            var tumUrunler = await _urunService.TumUrunleriGetirAsync();

            var toplamUrunSayisi = tumUrunler.Count;
            var toplamStokMiktari = tumUrunler.Sum(u => u.StokMiktari);
            var toplamStokDegeri = tumUrunler.Sum(u => (decimal)u.StokMiktari * u.UrunFiyati);
            var kritikStokUrunSayisi = tumUrunler.Count(u => u.StokMiktari <= 10);

            // 2. Stok Hareket Sayımları
            var tumHareketler = await _stokHareketCollection.Find(_ => true).ToListAsync();
            var toplamGiris = tumHareketler.Count(h => h.HareketTuru.ToString() == "Giris");
            var toplamCikis = tumHareketler.Count(h => h.HareketTuru.ToString() == "Cikis");

            return new DashboardDto
            {
                ToplamUrunSayisi = toplamUrunSayisi,
                ToplamStokMiktari = toplamStokMiktari,
                ToplamStokDegeri = toplamStokDegeri,
                KritikStokUrunSayisi = kritikStokUrunSayisi,
                ToplamGirisHareketSayisi = toplamGiris,
                ToplamCikisHareketSayisi = toplamCikis
            };
        }
    }
}