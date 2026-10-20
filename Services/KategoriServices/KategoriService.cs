using AutoMapper;
using MongoDB.Driver;
using StockHub.Services.DTO.CategoryDtos;
using StockHub.Services.Entities;

namespace StockHub.Services.Services.KategoriServices
{
    public class KategoriService : IKategoriService
    {
        private readonly IMongoCollection<Category> _kategoriKoleksiyonu;
        private readonly IMapper _mapper;

        public KategoriService(IDatabaseSettings databaseSettings, IMapper mapper)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _kategoriKoleksiyonu = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task KategoriEkleAsync(KategoriEkleDto kategoriEkleDto)
        {
            var kategori = _mapper.Map<Category>(kategoriEkleDto);
            await _kategoriKoleksiyonu.InsertOneAsync(kategori);
        }

        public async Task KategoriGuncelleAsync(KategoriGuncelleDto kategoriGuncelleDto)
        {
            var kategori = _mapper.Map<Category>(kategoriGuncelleDto);
            await _kategoriKoleksiyonu.FindOneAndReplaceAsync(x => x.CategoryId == kategoriGuncelleDto.KategoriId, kategori);
        }

        // Interface: TumKategorileriGetirAsync
        public async Task<List<KategoriListeleDto>> TumKategorileriGetirAsync()
        {
            var kategoriler = await _kategoriKoleksiyonu.Find(x => true).ToListAsync();
            return _mapper.Map<List<KategoriListeleDto>>(kategoriler);
        }

        // Interface: IdyeGoreKategoriGetirAsync ve DTO tipi KategoriGetirDto
        public async Task<KategoriGetirDto> IdyeGoreKategoriGetirAsync(string id)
        {
            var kategori = await _kategoriKoleksiyonu.Find(x => x.CategoryId == id).FirstOrDefaultAsync();
            return _mapper.Map<KategoriGetirDto>(kategori);
        }

        public async Task KategoriSilAsync(string id)
        {
            await _kategoriKoleksiyonu.DeleteOneAsync(x => x.CategoryId == id);
        }
    }
}