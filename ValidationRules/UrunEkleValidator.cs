using FluentValidation;
using StockHub.Services.DTO.ProductDtos;

namespace StockHub.Services.ValidationRules
{
    public class UrunEkleValidator : AbstractValidator<UrunEkleDto>
    {
        public UrunEkleValidator()
        {
            RuleFor(x => x.UrunAdi)
                .NotEmpty().WithMessage("Ürün adı boş bırakılamaz.")
                .MinimumLength(2).WithMessage("Ürün adı en az 2 karakter olmalıdır.");

            RuleFor(x => x.StokMiktari)
                .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı 0'dan küçük olamaz.");

            RuleFor(x => x.UrunFiyati)
                .GreaterThan(0).WithMessage("Ürün fiyatı 0 TL'den büyük olmalıdır.");

            RuleFor(x => x.UrunResimUrl)
                .NotEmpty().WithMessage("Ürün resim bağlantısı boş geçilemez.");
        }
    }
}