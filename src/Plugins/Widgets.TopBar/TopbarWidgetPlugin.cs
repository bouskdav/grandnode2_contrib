using Grand.Business.Common.Extensions;
using Grand.Business.Common.Interfaces.Configuration;
using Grand.Business.Common.Interfaces.Localization;
using Grand.Infrastructure.Plugins;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Widgets.Topbar
{
    /// <summary>
    /// Live person provider
    /// </summary>
    public class TopbarPlugin : BasePlugin, IPlugin
    {
        private readonly ITranslationService _translationService;
        private readonly ILanguageService _languageService;
        private readonly ISettingService _settingService;

        public TopbarPlugin(ITranslationService translationService, ILanguageService languageService, ISettingService settingService)
        {
            _translationService = translationService;
            _settingService = settingService;
            _languageService = languageService;
        }

        /// <summary>
        /// Gets a configuration page URL
        /// </summary>
        public override string ConfigurationUrl()
        {
            return TopbarWidgetDefaults.ConfigurationUrl;
        }

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string>
            {
                "topbar_before"
            };
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override async Task Install()
        {
            var settings = new TopbarWidgetSettings {
                DisplayOrder = 10,
                ShowTopbar = true,
                TopbarHtml = "<span class=\"test\">viola</span>"
            };
            await _settingService.SaveSetting(settings);

            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.Topbar.ShowTopbar", "Show topbar");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.Topbar.TopbarHtml", "Html content");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.Topbar.DisplayOrder", "Display order");

            await base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override async Task Uninstall()
        {
            //settings
            await _settingService.DeleteSetting<TopbarWidgetSettings>();

            //locales
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.Topbar.ShowTopbar");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.Topbar.TopbarHtml");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.Topbar.DisplayOrder");

            await base.Uninstall();
        }

    }
}
