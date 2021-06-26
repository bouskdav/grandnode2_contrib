using Grand.Business.Common.Extensions;
using Grand.Business.Storage.Interfaces;
using Grand.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Widgets.HeroImage.Domain;
using Widgets.HeroImage.Models;
using Widgets.HeroImage.Services;

namespace Widgets.HeroImage.ViewComponents
{
    [ViewComponent(Name = "WidgetHeroImage")]
    public class WidgetHeroImageComponent : ViewComponent
    {
        private readonly IPictureService _pictureService;
        private readonly IHeroImageService _heroImageService;
        private readonly IWorkContext _workContext;

        public WidgetHeroImageComponent(
            IPictureService pictureService,
            IHeroImageService heroImageService,
            IWorkContext workContext)
        {
            _pictureService = pictureService;
            _heroImageService = heroImageService;
            _workContext = workContext;
        }

        protected async Task<string> GetPictureUrl(string pictureId)
        {
            var url = await _pictureService.GetPictureUrl(pictureId, showDefaultPicture: false);
            if (url == null)
                url = "";

            return url;
        }

        protected async Task PrepareModel(PictureHeroImage heroImage, PublicInfoModel model)
        {
            if (heroImage == null)
                return;

            model.Image = new PublicInfoModel.HeroImage() {
                Link = heroImage.Link,
                PictureUrl = await GetPictureUrl(heroImage.PictureId),
                Name = heroImage.GetTranslation(x => x.Name, _workContext.WorkingLanguage.Id),
                Description = heroImage.GetTranslation(x => x.Description, _workContext.WorkingLanguage.Id),
                FullWidth = heroImage.FullWidth,
                CssClass = "active",
                AdditionalCss = heroImage.AdditionalCss,
                HtmlContent = heroImage.HtmlContent
            };
        }

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData = null)
        {
            var model = new PublicInfoModel();
            if (widgetZone == HeroImageWidgetDefaults.WidgetZoneHomePage)
            {
                var slides = await _heroImageService.GetPictureHeroImage(HeroImageType.HomePage);
                await PrepareModel(slides, model);
            }
            if (widgetZone == HeroImageWidgetDefaults.WidgetZoneCategoryPage)
            {
                var slides = await _heroImageService.GetPictureHeroImage(HeroImageType.Category, additionalData.ToString());
                await PrepareModel(slides, model);
            }
            if (widgetZone == HeroImageWidgetDefaults.WidgetZoneCollectionPage)
            {
                var slides = await _heroImageService.GetPictureHeroImage(HeroImageType.Collection, additionalData.ToString());
                await PrepareModel(slides, model);
            }
            if (widgetZone == HeroImageWidgetDefaults.WidgetZoneBrandPage)
            {
                var slides = await _heroImageService.GetPictureHeroImage(HeroImageType.Brand, additionalData.ToString());
                await PrepareModel(slides, model);
            }

            if (model.Image == null)
                return Content("");

            return View(model);
        }
    }
}