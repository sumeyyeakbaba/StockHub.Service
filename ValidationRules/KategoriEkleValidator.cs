using FluentValidation;
using StockHub.Services.DTO.CategoryDtos;

namespace StockHub.Services.ValidationRules
{
    public class KategoriEkleValidator : AbstractValidator<KategoriEkleDto>
    {
        public KategoriEkleValidator()
        {
            RuleFor(x => x.KategoriAdi)
                .NotEmpty().WithMessage("Kategori adı boş geçilemez.")
                .NotNull().WithMessage("Kategori adı mutlaka girilmelidir.")
                .MinimumLength(2).WithMessage("Kategori adı en az 2 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Kategori adı en fazla 50 karakter olabilir.");
        }
    }
}