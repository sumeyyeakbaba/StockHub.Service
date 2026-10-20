using AutoMapper;
using MongoDB.Driver;
using StockHub.DTOs.StokHareketDtos;
using StockHub.Entities;
using StockHub.Services.Abstract;
using StockHub.Services.Entities;
using StockHub.Services.Exceptions;

namespace StockHub.Services.Services
{
    public class StokHareketService : IStokHareketService
    {
        private readonly IMongoCollection<StokHareket> _stokHareketCollection;
        private readonly IMongoCollection<Product> _urunCollection;
        private readonly IMapper _mapper;

        public StokHareketService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _stokHareketCollection = database.GetCollection<StokHareket>("StokHareketler");
            _urunCollection = database.GetCollection<Product>("Products");
            _mapper = mapper;
        }

        public async Task<List<ResultStokHareketDto>> GetAllStokHareketAsync()
        {
            var values = await _stokHareketCollection.Find(_ => true).SortByDescending(x => x.Tarih).ToListAsync();
            return _mapper.Map<List<ResultStokHareketDto>>(values);
        }

        public async Task<List<ResultStokHareketDto>> GetStokHareketleriByUrunIdAsync(string urunId)
        {
            var values = await _stokHareketCollection.Find(x => x.UrunId == urunId).SortByDescending(x => x.Tarih).ToListAsync();
            return _mapper.Map<List<ResultStokHareketDto>>(values);
        }

        public async Task<bool> CreateStokHareketAsync(CreateStokHareketDto createDto)
        {
            // 1. Ürün kontrolü
            var urun = await _urunCollection.Find(x => x.ProductId == createDto.UrunId).FirstOrDefaultAsync();

            if (urun == null)
            {
                throw new KeyNotFoundException($"'{createDto.UrunId}' ID değerine sahip ürün bulunamadı.");
            }

            // 2. Çıkış yapılacaksa stok miktarı kontrolü
            if (createDto.HareketTuru == StokHareketTuru.Cikis && urun.StockQuantity < createDto.Miktar)
            {
                throw new YetersizStokException(
                    $"Yetersiz stok! Mevcut stok: {urun.StockQuantity}, Çıkılmak istenen miktar: {createDto.Miktar}"
                );
            }

            // 3. Stok miktarı güncellemesi
            if (createDto.HareketTuru == StokHareketTuru.Giris)
            {
                urun.StockQuantity += createDto.Miktar;
            }
            else if (createDto.HareketTuru == StokHareketTuru.Cikis)
            {
                urun.StockQuantity -= createDto.Miktar;
            }

            var updateResult = await _urunCollection.ReplaceOneAsync(x => x.ProductId == createDto.UrunId, urun);

            if (!updateResult.IsAcknowledged || updateResult.MatchedCount == 0)
            {
                throw new InvalidOperationException("Ürün stok bilgisi güncellenemedi.");
            }

            // 4. Hareketi kaydet
            var hareket = _mapper.Map<StokHareket>(createDto);
            hareket.Tarih = DateTime.UtcNow;

            await _stokHareketCollection.InsertOneAsync(hareket);

            return true;
        }
    }
}