using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using StockHub.Services.DTO;
using StockHub.Services.DTO.ProductDtos;
using StockHub.Services.Entities;

namespace StockHub.Services.Services.UrunServices
{
    public class UrunService : IUrunService
    {
        private readonly IMongoCollection<Product> _urunKoleksiyonu;
        private readonly IMapper _mapper;

        public UrunService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _urunKoleksiyonu = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _mapper = mapper;
        }

        public async Task<List<UrunListeleDto>> TumUrunleriGetirAsync()
        {
            var urunler = await _urunKoleksiyonu.Find(urun => true).ToListAsync();
            return _mapper.Map<List<UrunListeleDto>>(urunler);
        }

        public async Task UrunEkleAsync(UrunEkleDto urunEkleDto)
        {
            var yeniUrun = _mapper.Map<Product>(urunEkleDto);
            await _urunKoleksiyonu.InsertOneAsync(yeniUrun);
        }

        public async Task UrunGuncelleAsync(UrunGuncelleDto urunGuncelleDto)
        {
            var guncellenecekUrun = _mapper.Map<Product>(urunGuncelleDto);
            await _urunKoleksiyonu.FindOneAndReplaceAsync(u => u.ProductId == urunGuncelleDto.UrunId, guncellenecekUrun);
        }

        public async Task<UrunGetirDto> IdyeGoreUrunGetirAsync(string id)
        {
            var urun = await _urunKoleksiyonu.Find(u => u.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<UrunGetirDto>(urun);
        }

        public async Task UrunSilAsync(string id)
        {
            await _urunKoleksiyonu.DeleteOneAsync(u => u.ProductId == id);
        }
        //  Arama ve Filtreleme Metodu
        public async Task<PagedResultDto<UrunListeleDto>> UrunleriFiltreliGetirAsync(UrunFiltreDto filtreDto)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;

            // 1. Ürün Adına Göre Arama (Büyük/Küçük harf duyarsız regex)
            if (!string.IsNullOrWhiteSpace(filtreDto.AramaKelimesi))
            {
                var searchFilter = builder.Regex(u => u.ProductName, new BsonRegularExpression(filtreDto.AramaKelimesi, "i"));
                filter &= searchFilter;
            }

            // 2. Kategori ID'sine Göre Filtreleme
            if (!string.IsNullOrWhiteSpace(filtreDto.AramaKelimesi))
            {
                var searchFilter = builder.Regex(u => u.ProductName, new BsonRegularExpression(filtreDto.AramaKelimesi, "i"));
                filter &= searchFilter;
            }

            // 3. Filtreye Uyan Toplam Kayıt Sayısı
            var totalCount = (int)await _urunKoleksiyonu.CountDocumentsAsync(filter);

            // 4. Sayfalama (Skip & Limit)
            var skipCount = (filtreDto.PageIndex - 1) * filtreDto.PageSize;
            var urunler = await _urunKoleksiyonu.Find(filter)
                                               .Skip(skipCount)
                                               .Limit(filtreDto.PageSize)
                                               .ToListAsync();

            var dtoList = _mapper.Map<List<UrunListeleDto>>(urunler);

            return new PagedResultDto<UrunListeleDto>
            {
                Items = dtoList,
                TotalCount = totalCount,
                PageIndex = filtreDto.PageIndex,
                PageSize = filtreDto.PageSize
            };
        }
        }
}