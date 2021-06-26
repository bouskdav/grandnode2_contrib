using Grand.Infrastructure.ModelBinding;
using Grand.Infrastructure.Models;
using Grand.Web.Common.Link;
using Grand.Web.Common.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Widgets.HeroImage.Models
{
    public partial class HeroImageModel : BaseEntityModel, ILocalizedModel<HeroImageLocalizedModel>, IStoreLinkModel
    {
        public HeroImageModel()
        {
            Locales = new List<HeroImageLocalizedModel>();
        }

        [GrandResourceDisplayName("Widgets.HeroImage.Name")]
        public string Name { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.Description")]
        public string Description { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.HtmlContent")]
        public string HtmlContent { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.AdditionalCss")]
        public string AdditionalCss { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.Link")]
        public string Link { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.Published")]
        public bool Published { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.FullWidth")]
        public bool FullWidth { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.HeroImageType")]
        public int HeroImageTypeId { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.Picture")]
        [UIHint("Picture")]
        public string PictureId { get; set; }

        public IList<HeroImageLocalizedModel> Locales { get; set; }

        //Store acl
        [GrandResourceDisplayName("Widgets.HeroImage.LimitedToStores")]
        [UIHint("Stores")]
        public string[] Stores { get; set; }

        [UIHint("Category")]
        [GrandResourceDisplayName("Widgets.HeroImage.Category")]
        public string CategoryId { get; set; }
        [UIHint("Collection")]
        [GrandResourceDisplayName("Widgets.HeroImage.Collection")]
        public string CollectionId { get; set; }

        [UIHint("Brand")]
        [GrandResourceDisplayName("Widgets.HeroImage.Brand")]
        public string BrandId { get; set; }

    }

    public partial class HeroImageLocalizedModel : ILocalizedModelLocal
    {
        public string LanguageId { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.Name")]
        public string Name { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.Description")]
        public string Description { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.HtmlContent")]
        public string HtmlContent { get; set; }

        [GrandResourceDisplayName("Widgets.HeroImage.AdditionalCss")]
        public string AdditionalCss { get; set; }

    }
}
