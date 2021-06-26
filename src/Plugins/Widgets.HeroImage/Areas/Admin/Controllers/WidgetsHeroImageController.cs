using Grand.Business.Common.Extensions;
using Grand.Business.Common.Interfaces.Configuration;
using Grand.Business.Common.Interfaces.Localization;
using Grand.Business.Common.Services.Security;
using Grand.Business.Storage.Interfaces;
using Grand.Web.Common.Controllers;
using Grand.Web.Common.DataSource;
using Grand.Web.Common.Filters;
using Grand.Web.Common.Security.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Widgets.HeroImage.Models;
using Widgets.HeroImage.Services;

namespace Widgets.HeroImage.Areas.Admin.Controllers
{
    [AuthorizeAdmin]
    [Area("Admin")]
    [PermissionAuthorize(PermissionSystemName.Widgets)]
    public class WidgetsHeroImageController : BasePluginController
    {
        private readonly IPictureService _pictureService;
        private readonly ITranslationService _translationService;
        private readonly IHeroImageService _heroImageService;
        private readonly ILanguageService _languageService;
        private readonly HeroImageWidgetSettings _heroImageWidgetSettings;
        private readonly ISettingService _settingService;

        public WidgetsHeroImageController(
            IPictureService pictureService,
            ITranslationService translationService,
            IHeroImageService heroImageService,
            ILanguageService languageService,
            ISettingService settingService,
            HeroImageWidgetSettings heroImageWidgetSettings)
        {
            _pictureService = pictureService;
            _translationService = translationService;
            _heroImageService = heroImageService;
            _languageService = languageService;
            _settingService = settingService;
            _heroImageWidgetSettings = heroImageWidgetSettings;
        }
        public IActionResult Configure()
        {
            var model = new ConfigurationModel();
            model.DisplayOrder = _heroImageWidgetSettings.DisplayOrder;
            model.CustomerGroups = _heroImageWidgetSettings.LimitedToGroups?.ToArray();
            model.Stores = _heroImageWidgetSettings.LimitedToStores?.ToArray();
            return View(model);
        }

        [HttpPost]
        public IActionResult Configure(ConfigurationModel model)
        {
            _heroImageWidgetSettings.DisplayOrder = model.DisplayOrder;
            _heroImageWidgetSettings.LimitedToGroups = model.CustomerGroups == null ? new List<string>() : model.CustomerGroups.ToList();
            _heroImageWidgetSettings.LimitedToStores = model.Stores == null ? new List<string>() : model.Stores.ToList();
            _settingService.SaveSetting(_heroImageWidgetSettings);
            return Json("Ok");
        }

        [HttpPost]
        public async Task<IActionResult> List(DataSourceRequest command)
        {
            var HeroImages = await _heroImageService.GetPictureHeroImages();

            var items = new List<HeroImageListModel>();
            foreach (var x in HeroImages)
            {
                var model = x.ToListModel();
                var picture = await _pictureService.GetPictureById(x.PictureId);
                if (picture != null)
                {
                    model.PictureUrl = await _pictureService.GetPictureUrl(picture, 150);
                }
                items.Add(model);
            }
            var gridModel = new DataSourceResult {
                Data = items,
                Total = HeroImages.Count
            };
            return Json(gridModel);
        }

        public async Task<IActionResult> Create()
        {
            var model = new HeroImageModel();
            //locales
            await AddLocales(_languageService, model.Locales);

            return View(model);
        }
        [HttpPost, ArgumentNameFilter(KeyName = "save-continue", Argument = "continueEditing")]
        public async Task<IActionResult> Create(HeroImageModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var pictureHeroImage = model.ToEntity();
                pictureHeroImage.Locales = model.Locales.ToLocalizedProperty();

                await _heroImageService.InsertPictureHeroImage(pictureHeroImage);

                Success(_translationService.GetResource("Widgets.HeroImage.Added"));
                return continueEditing ? RedirectToAction("Edit", new { id = pictureHeroImage.Id }) : RedirectToAction("Configure");

            }
            return View(model);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var slide = await _heroImageService.GetById(id);
            if (slide == null)
                return RedirectToAction("Configure");

            var model = slide.ToModel();

            //locales
            await AddLocales(_languageService, model.Locales, (locale, languageId) =>
            {
                locale.Name = slide.GetTranslation(x => x.Name, languageId, false);
                locale.Description = slide.GetTranslation(x => x.Description, languageId, false);
                locale.HtmlContent = slide.GetTranslation(x => x.HtmlContent, languageId, false);
                locale.AdditionalCss = slide.GetTranslation(x => x.AdditionalCss, languageId, false);
            });

            return View(model);
        }

        [HttpPost, ArgumentNameFilter(KeyName = "save-continue", Argument = "continueEditing")]
        public async Task<IActionResult> Edit(HeroImageModel model, bool continueEditing)
        {
            var pictureHeroImage = await _heroImageService.GetById(model.Id);
            if (pictureHeroImage == null)
                return RedirectToAction("Configure");

            if (ModelState.IsValid)
            {
                pictureHeroImage = model.ToEntity();

                pictureHeroImage.Locales = model.Locales.ToLocalizedProperty();

                await _heroImageService.UpdatePictureHeroImage(pictureHeroImage);

                Success(_translationService.GetResource("Widgets.HeroImage.Edited"));

                return continueEditing ? RedirectToAction("Edit", new { id = pictureHeroImage.Id }) : RedirectToAction("Configure");

            }
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var pictureHeroImage = await _heroImageService.GetById(id);
            if (pictureHeroImage == null)
                return Json(new DataSourceResult { Errors = "This pictureHeroImage not exists" });

            await _heroImageService.DeleteHeroImage(pictureHeroImage);

            return new JsonResult("");
        }
    }
}
