using Grand.Business.Cms.Interfaces;
using Grand.Business.Common.Interfaces.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Widgets.HeroImage
{
    public class HeroImageWidgetProvider : IWidgetProvider
    {
        private readonly ITranslationService _translationService;
        private readonly HeroImageWidgetSettings _HeroImageWidgetSettings;

        public HeroImageWidgetProvider(ITranslationService translationService, HeroImageWidgetSettings HeroImageWidgetSettings)
        {
            _translationService = translationService;
            _HeroImageWidgetSettings = HeroImageWidgetSettings;
        }

        public string ConfigurationUrl => HeroImageWidgetDefaults.ConfigurationUrl;

        public string SystemName => HeroImageWidgetDefaults.ProviderSystemName;

        public string FriendlyName => _translationService.GetResource(HeroImageWidgetDefaults.FriendlyName);

        public int Priority => _HeroImageWidgetSettings.DisplayOrder;

        public IList<string> LimitedToStores => _HeroImageWidgetSettings.LimitedToStores;

        public IList<string> LimitedToGroups => _HeroImageWidgetSettings.LimitedToGroups;

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public async Task<IList<string>> GetWidgetZones()
        {
            return await Task.FromResult(new List<string>
            {
                HeroImageWidgetDefaults.WidgetZoneHomePage,
                HeroImageWidgetDefaults.WidgetZoneCategoryPage,
                HeroImageWidgetDefaults.WidgetZoneCollectionPage,
                HeroImageWidgetDefaults.WidgetZoneBrandPage,
            });
        }

        public Task<string> GetPublicViewComponentName(string widgetZone)
        {
            return Task.FromResult("WidgetHeroImage");
        }

    }
}
