using FluentValidation;
using Grand.Business.Common.Interfaces.Localization;
using Grand.Infrastructure.Validators;
using System.Collections.Generic;
using Widgets.HeroImage.Domain;
using Widgets.HeroImage.Models;

namespace Widgets.HeroImage.Validators
{
    public class HeroImageValidator : BaseGrandValidator<HeroImageModel>
    {
        public HeroImageValidator(IEnumerable<IValidatorConsumer<HeroImageModel>> validators,
            ITranslationService translationService) : base(validators)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(translationService.GetResource("Widgets.HeroImage.Name.Required"));
            RuleFor(x => x.HeroImageTypeId == (int)HeroImageType.Category && string.IsNullOrEmpty(x.CategoryId)).Equal(false).WithMessage(translationService.GetResource("Widgets.HeroImage.Category.Required"));
            RuleFor(x => x.HeroImageTypeId == (int)HeroImageType.Collection && string.IsNullOrEmpty(x.CollectionId)).Equal(false).WithMessage(translationService.GetResource("Widgets.HeroImage.Collection.Required"));
        }
    }
}