using Grand.Domain.Configuration;
using System.Collections.Generic;

namespace Widgets.HeroImage
{
    public class HeroImageWidgetSettings : ISettings
    {
        public HeroImageWidgetSettings()
        {
            LimitedToStores = new List<string>();
            LimitedToGroups = new List<string>();
        }
        public int DisplayOrder { get; set; }
        public IList<string> LimitedToStores { get; set; }

        public IList<string> LimitedToGroups { get; set; }
    }
}
