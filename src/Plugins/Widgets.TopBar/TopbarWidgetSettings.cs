using Grand.Domain.Configuration;
using System.Collections.Generic;

namespace Widgets.Topbar
{
    public class TopbarWidgetSettings : ISettings
    {
        public bool ShowTopbar { get; set; }

        public string TopbarHtml { get; set; }

        public int DisplayOrder { get; set; }
    }
}
