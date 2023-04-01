using CelilCavus.Models.Entites;
using FluentValidation;

namespace CelilCavus.Validator
{
    public class DersValidator : AbstractValidator<Dersler>
    {
        public DersValidator()
        {
            RuleFor(x=>x.ders_kodu).NotEmpty().WithMessage("Ders Kodu Boş geçilemez");
            RuleFor(x=>x.ders_kodu).Length(5).WithMessage("Ders Kodu 5 Karekter Olmalidir");
           

            RuleFor(x=>x.ders_adi).NotEmpty().WithMessage("Ders Adi Boş geçilemez");
            RuleFor(x=>x.ders_adi).MinimumLength(3).WithMessage("Ders Adi En Az 3 Karekter Olmalidir");
            RuleFor(x=>x.ders_adi).MaximumLength(40).WithMessage("Ders Adi En Fazla 40 Karekter Olmalidir");


            RuleFor(x=>x.ders_hocasi).NotEmpty().WithMessage("Ders Hocasi Boş geçilemez");
            RuleFor(x=>x.ders_hocasi).MinimumLength(3).WithMessage("Ders Hocasi En Az 3 Karekter Olmalidir");
            RuleFor(x=>x.ders_hocasi).MaximumLength(40).WithMessage("Ders Hocasi En Fazla 40 Karekter Olmalidir");

        }
    }
}