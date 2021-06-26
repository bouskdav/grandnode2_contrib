using Grand.Business.Cms.Interfaces;
using Grand.Business.Common.Interfaces.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Widgets.Topbar
{
    /// <summary>
    /// Live person provider
    /// </summary>
    public class TopbarWidgetProvider : IWidgetProvider
    {
        private readonly ITranslationService _translationService;
        private readonly TopbarWidgetSettings _topbarWidgetSettings;

        public TopbarWidgetProvider(
            ITranslationService translationService,
            TopbarWidgetSettings topbarWidgetSettings)
        {
            _translationService = translationService;
            _topbarWidgetSettings = topbarWidgetSettings;
        }

        public string ConfigurationUrl => TopbarWidgetDefaults.ConfigurationUrl;

        public string SystemName => TopbarWidgetDefaults.ProviderSystemName;

        public string FriendlyName => _translationService.GetResource(TopbarWidgetDefaults.FriendlyName);

        public int Priority => _topbarWidgetSettings.DisplayOrder;

        public IList<string> LimitedToStores => new List<string>();

        public IList<string> LimitedToGroups => new List<string>();

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public async Task<IList<string>> GetWidgetZones()
        {
            return await Task.FromResult(new List<string>
            {
                "topbar_before"
            });
        }

        public Task<string> GetPublicViewComponentName(string widgetZone)
        {
            return Task.FromResult("WidgetsTopbar");
        }
    }
}
