using CelilCavus.Models.Entites;
using FluentValidation;

namespace CelilCavus.Validator
{
    public class OgrenciValidator : AbstractValidator<Ogrenci>
    {
        public OgrenciValidator()
        {   

            RuleFor(x=>x.ad).NotEmpty().WithMessage("Öğrenci Adi Boş geçilemez");
            RuleFor(x=>x.ad).MinimumLength(3).WithMessage("Öğrenci Adi En Az 3 Karekter Olmalidir");
            RuleFor(x=>x.ad).MaximumLength(20).WithMessage("Öğrenci Adi En Fazla 20 Karekter Olmalidir");


            RuleFor(x=>x.soyad).NotEmpty().WithMessage("Öğrenci Soyadi Boş geçilemez");
            RuleFor(x=>x.soyad).MinimumLength(3).WithMessage("Öğrenci Soyadi En Az 3 Karekter Olmalidir");
            RuleFor(x=>x.soyad).MaximumLength(20).WithMessage("Öğrenci Soyadi En Fazla 20 Karekter Olmalidir");

        }
    }
}