using Grand.Infrastructure.ModelBinding;
using Grand.Infrastructure.Models;

namespace Widgets.Topbar.Models
{
    public class ConfigurationModel : BaseModel
    {
        public string StoreScope { get; set; }

        [GrandResourceDisplayName("Widgets.Topbar.ShowTopbar")]
        public bool ShowTopbar { get; set; }

        [GrandResourceDisplayName("Widgets.Topbar.TopbarHtml")]
        public string TopbarHtml { get; set; }

        [GrandResourceDisplayName("Widgets.Topbar.DisplayOrder")]
        public int DisplayOrder { get; set; }
    }
}