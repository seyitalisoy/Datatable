using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class LinkValidator : AbstractValidator<Link>
    {
        public LinkValidator()
        {
            RuleFor(l => l.Url).NotEmpty().Must(validUrl)
                .WithMessage("Geçerli bir Url giriniz.");

            RuleFor(l => l.Name).NotEmpty().WithMessage("İsim alanı boş geçilemez.");
            RuleFor(l => l.Name).MaximumLength(90)
                .WithMessage("İsim alanı 90 karakterden fazla olamaz");

            RuleFor(l => l.Description).MaximumLength(200)
                .WithMessage("Açıklama 200 karakterden fazla olamaz");

            //RuleFor(l => l.CreatedAt).NotEmpty()
            //    .WithMessage("Link oluşturulma tarihi boş geçilemez");
        }

        private bool validUrl(string link)
        {
            return Uri.IsWellFormedUriString(link, UriKind.Absolute);
        }
    }
}
