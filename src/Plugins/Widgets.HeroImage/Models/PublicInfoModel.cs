using Grand.Infrastructure.Models;
using System.Collections.Generic;

namespace Widgets.HeroImage.Models
{
    public class PublicInfoModel : BaseModel
    {
        public HeroImage Image { get; set; }

        public class HeroImage
        {
            public string PictureUrl { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Link { get; set; }
            public bool FullWidth { get; set; }
            public string CssClass { get; set; }
            public string AdditionalCss { get; set; }
            public string HtmlContent { get; set; }
        }

    }
}