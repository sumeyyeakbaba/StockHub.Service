using AutoMapper;
using StockHub.DTOs.StokHareketDtos;
using StockHub.Entities;
using StockHub.Services.DTO.CategoryDtos;
using StockHub.Services.DTO.ProductDtos;
using StockHub.Services.Entities;

namespace StockHub.Services.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // Kategori Eşleştirmeleri
            CreateMap<Category, KategoriListeleDto>().ReverseMap();
            CreateMap<Category, KategoriEkleDto>().ReverseMap();
            CreateMap<Category, KategoriGuncelleDto>().ReverseMap();
            CreateMap<Category, KategoriGetirDto>().ReverseMap();

            // Ürün Eşleştirmeleri - Listeleme
            CreateMap<Product, UrunListeleDto>()
                .ForMember(dest => dest.UrunId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.UrunAdi, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Barkod, opt => opt.MapFrom(src => src.Barcode))
                .ForMember(dest => dest.KategoriAdi, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.StokMiktari, opt => opt.MapFrom(src => src.StockQuantity))
                .ForMember(dest => dest.KritikStokSeviyesi, opt => opt.MapFrom(src => src.CriticalStockLevel))
                .ForMember(dest => dest.UrunFiyati, opt => opt.MapFrom(src => src.ProductPrice))
                .ForMember(dest => dest.UrunResimUrl, opt => opt.MapFrom(src => src.ProductImageUrl));

            // Ürün Eşleştirmeleri - Ekleme (Barkod ve Kategori eklendi)
            CreateMap<Product, UrunEkleDto>()
                .ForMember(dest => dest.UrunAdi, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Barkod, opt => opt.MapFrom(src => src.Barcode))
                .ForMember(dest => dest.Kategori, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.StokMiktari, opt => opt.MapFrom(src => src.StockQuantity))
                .ForMember(dest => dest.KritikStokSeviyesi, opt => opt.MapFrom(src => src.CriticalStockLevel))
                .ForMember(dest => dest.UrunFiyati, opt => opt.MapFrom(src => src.ProductPrice))
                .ForMember(dest => dest.UrunResimUrl, opt => opt.MapFrom(src => src.ProductImageUrl))
                .ReverseMap();

            // Ürün Eşleştirmeleri - Güncelleme (Barkod ve Kategori eklendi)
            CreateMap<Product, UrunGuncelleDto>()
                .ForMember(dest => dest.UrunId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.UrunAdi, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Barkod, opt => opt.MapFrom(src => src.Barcode))
                .ForMember(dest => dest.Kategori, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.StokMiktari, opt => opt.MapFrom(src => src.StockQuantity))
                .ForMember(dest => dest.KritikStokSeviyesi, opt => opt.MapFrom(src => src.CriticalStockLevel))
                .ForMember(dest => dest.UrunFiyati, opt => opt.MapFrom(src => src.ProductPrice))
                .ForMember(dest => dest.UrunResimUrl, opt => opt.MapFrom(src => src.ProductImageUrl))
                .ReverseMap();

            // Ürün Eşleştirmeleri - Tekil Getirme
            CreateMap<Product, UrunGetirDto>()
                .ForMember(dest => dest.UrunId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.UrunAdi, opt => opt.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.Barkod, opt => opt.MapFrom(src => src.Barcode))
                .ForMember(dest => dest.Kategori, opt => opt.MapFrom(src => src.CategoryName))
                .ForMember(dest => dest.StokMiktari, opt => opt.MapFrom(src => src.StockQuantity))
                .ForMember(dest => dest.KritikStokSeviyesi, opt => opt.MapFrom(src => src.CriticalStockLevel))
                .ForMember(dest => dest.UrunFiyati, opt => opt.MapFrom(src => src.ProductPrice))
                .ForMember(dest => dest.UrunResimUrl, opt => opt.MapFrom(src => src.ProductImageUrl))
                .ReverseMap();

            // Stok Hareket Eşleştirmeleri
            CreateMap<StokHareket, CreateStokHareketDto>().ReverseMap();
            CreateMap<StokHareket, ResultStokHareketDto>().ReverseMap();
        }
    }
}