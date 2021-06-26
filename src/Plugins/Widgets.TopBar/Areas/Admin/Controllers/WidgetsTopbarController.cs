using Grand.Business.Common.Interfaces.Configuration;
using Grand.Business.Common.Interfaces.Localization;
using Grand.Business.Common.Interfaces.Logging;
using Grand.Business.Common.Interfaces.Stores;
using Grand.Business.Common.Services.Security;
using Grand.Web.Common.Controllers;
using Grand.Web.Common.Filters;
using Grand.Web.Common.Security.Authorization;
using Grand.Domain.Common;
using Grand.Domain.Customers;
using Grand.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Widgets.Topbar.Models;

namespace Widgets.Topbar.Controllers
{
    [Area("Admin")]
    [AuthorizeAdmin]
    [PermissionAuthorize(PermissionSystemName.Widgets)]
    public class WidgetsTopbarController : BasePluginController
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreService _storeService;
        private readonly ISettingService _settingService;
        private readonly ITranslationService _translationService;

        public WidgetsTopbarController(IWorkContext workContext,
            IStoreService storeService,
            ISettingService settingService,
            ILogger logger,
            ITranslationService translationService)
        {
            _workContext = workContext;
            _storeService = storeService;
            _settingService = settingService;
            _translationService = translationService;
        }
        protected virtual async Task<string> GetActiveStore(IStoreService storeService, IWorkContext workContext)
        {
            var stores = await storeService.GetAllStores();
            if (stores.Count < 2)
                return stores.FirstOrDefault().Id;

            var storeId = workContext.CurrentCustomer.GetUserFieldFromEntity<string>(SystemCustomerFieldNames.AdminAreaStoreScopeConfiguration);
            var store = await storeService.GetStoreById(storeId);

            return store != null ? store.Id : "";
        }

        public async Task<IActionResult> Configure()
        {
            //load settings for a chosen store scope
            var storeScope = await GetActiveStore(_storeService, _workContext);
            var topbarWidgetSettings = _settingService.LoadSetting<TopbarWidgetSettings>(storeScope);
            var model = new ConfigurationModel();
            model.DisplayOrder = topbarWidgetSettings.DisplayOrder;
            model.ShowTopbar = topbarWidgetSettings.ShowTopbar;
            model.TopbarHtml = topbarWidgetSettings.TopbarHtml;
            model.StoreScope = storeScope;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(ConfigurationModel model)
        {
            //load settings for a chosen store scope
            var storeScope = await GetActiveStore(_storeService, _workContext);
            var topbarWidgetSettings = _settingService.LoadSetting<TopbarWidgetSettings>(storeScope);
            topbarWidgetSettings.DisplayOrder = model.DisplayOrder;
            topbarWidgetSettings.ShowTopbar = model.ShowTopbar;
            topbarWidgetSettings.TopbarHtml = model.TopbarHtml;

            await _settingService.SaveSetting(topbarWidgetSettings, storeScope);

            //now clear settings cache
            await _settingService.ClearCache();

            Success(_translationService.GetResource("Admin.Plugins.Saved"));

            return await Configure();
        }
    }
}