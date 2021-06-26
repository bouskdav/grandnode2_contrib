using Grand.Domain;
using Grand.Domain.Localization;
using Grand.Domain.Stores;
using System.Collections.Generic;

namespace Widgets.HeroImage.Domain
{
    public partial class PictureHeroImage : BaseEntity, ITranslationEntity, IStoreLinkEntity
    {
        public PictureHeroImage()
        {
            Stores = new List<string>();
            Locales = new List<TranslationEntity>();
        }
        public string PictureId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AdditionalCss { get; set; }
        public string HtmlContent { get; set; }
        public string Link { get; set; }
        public int DisplayOrder { get; set; }
        public bool FullWidth { get; set; }
        public bool Published { get; set; }
        public string ObjectEntry { get; set; }
        public HeroImageType HeroImageTypeId { get; set; }
        public bool LimitedToStores { get; set; }
        public IList<string> Stores { get; set; }
        public IList<TranslationEntity> Locales { get; set; }
    }
}
