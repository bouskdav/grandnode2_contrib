using Grand.Business.Common.Extensions;
using Grand.Business.Common.Interfaces.Localization;
using Grand.Business.Storage.Interfaces;
using Grand.Domain.Data;
using Grand.Infrastructure.Plugins;
using Grand.SharedKernel.Extensions;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Widgets.HeroImage.Domain;

namespace Widgets.HeroImage
{
    /// <summary>
    /// PLugin
    /// </summary>
    public class HeroImageWidgetPlugin : BasePlugin, IPlugin
    {
        private readonly IPictureService _pictureService;
        private readonly IRepository<PictureHeroImage> _pictureHeroImageRepository;
        private readonly ITranslationService _translationService;
        private readonly ILanguageService _languageService;
        private readonly IDatabaseContext _databaseContext;

        public HeroImageWidgetPlugin(IPictureService pictureService,
            IRepository<PictureHeroImage> pictureHeroImageRepository,
            ITranslationService translationService,
            ILanguageService languageService,
            IDatabaseContext databaseContext)
        {
            _pictureService = pictureService;
            _pictureHeroImageRepository = pictureHeroImageRepository;
            _translationService = translationService;
            _languageService = languageService;
            _databaseContext = databaseContext;
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override async Task Install()
        {
            //Create index
            await _databaseContext.CreateIndex(_pictureHeroImageRepository, OrderBuilder<PictureHeroImage>.Create().Ascending(x => x.HeroImageTypeId).Ascending(x => x.DisplayOrder), "HeroImageTypeId_DisplayOrder");

            //pictures
            var sampleImagesPath = CommonPath.MapPath("Plugins/Widgets.HeroImage/Assets/heroimages/sample-images/");
            var byte1 = File.ReadAllBytes(sampleImagesPath + "hero1.jpg");
            //var byte2 = File.ReadAllBytes(sampleImagesPath + "hero1.jpg");

            var pictureHeroImage1 = new PictureHeroImage()
            {
                DisplayOrder = 0,
                Link = "",
                Name = "Sample HeroImage 1",
                FullWidth = true,
                Published = true,
                Description = "<div class=\"row HeroImageow justify-content-start\"><div class=\"col-lg-6 d-flex flex-column justify-content-center align-items-center\"><div><div class=\"animate-top animate__animated animate__backInDown\" >exclusive - modern - elegant</div><div class=\"animate-center-title animate__animated animate__backInLeft animate__delay-05s\">Smart watches</div><div class=\"animate-center-content animate__animated animate__backInLeft animate__delay-1s\">Go to collection and see more...</div><a href=\"/smartwatches\" class=\"animate-bottom btn btn-info animate__animated animate__backInUp animate__delay-15s\"> SHOP NOW </a></div></div></div>"
            };

            var pic1 = await _pictureService.InsertPicture(byte1, "image/png", "banner_1",reference: Grand.Domain.Common.Reference.Widget, objectId: pictureHeroImage1.Id, validateBinary: false);
            pictureHeroImage1.PictureId = pic1.Id;
            await _pictureHeroImageRepository.InsertAsync(pictureHeroImage1);


            //var pictureHeroImage2 = new PictureHeroImage()
            //{
            //    DisplayOrder = 1,
            //    Link = "https://grandnode.com",
            //    Name = "Sample HeroImage 2",
            //    FullWidth = true,
            //    Published = true,
            //    Description = "<div class=\"row HeroImageow\"><div class=\"col-md-6 offset-md-6 col-12 offset-0 d-flex flex-column justify-content-center align-items-start px-0 pr-md-3\"><div class=\"slide-title text-dark animate__animated animate__fadeInRight animate__delay-05s\"><h2 class=\"mt-0\">Redmi Note 9</h2></div><div class=\"slide-content animate__animated animate__fadeInRight animate__delay-1s\"><p class=\"mb-0\"><span>Equipped with a high-performance octa-core processor <br/> with a maximum clock frequency of 2.0 GHz.</span></p></div><div class=\"slide-price animate__animated animate__fadeInRight animate__delay-15s d-inline-flex align-items-center justify-content-start w-100 mt-2\"><p class=\"actual\">$249.00</p><p class=\"old-price\">$399.00</p></div><div class=\"slide-button animate__animated animate__fadeInRight animate__delay-2s mt-3\"><a class=\"btn btn-outline-info\" href=\"/redmi-note-9\">BUY REDMI NOTE 9</a></div></div></div>",
            //};
            //var pic2 = await _pictureService.InsertPicture(byte2, "image/png", "banner_2", reference: Grand.Domain.Common.Reference.Widget, objectId: pictureHeroImage2.Id, validateBinary: false);
            //pictureHeroImage2.PictureId = pic2.Id;

            //await _pictureHeroImageRepository.InsertAsync(pictureHeroImage2);

            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Fields.DisplayOrder", "Display order");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Fields.LimitedToGroups", "Limited to groups");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Fields.LimitedToStores", "Limited to stores");


            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.FriendlyName", "Widget HeroImage");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Added", "HeroImage added");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Addnew", "Add new HeroImage");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.AvailableStores", "Available stores");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.AvailableStores.Hint", "Select stores for which the HeroImage will be shown.");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Backtolist", "Back to list");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Category", "Category");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Category.Hint", "Select the category where HeroImage should appear.");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Category.Required", "Category is required");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Description", "Description");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Description.Hint", "Enter the description of the HeroImage");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.DisplayOrder", "Display Order");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.DisplayOrder.Hint", "The HeroImage display order. 1 represents the first item in the list.");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Edit", "Edit HeroImage");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Edited", "HeroImage edited");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Fields.Displayorder", "Display Order");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Fields.Link", "Link");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Fields.ObjectType", "HeroImage type");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Fields.Picture", "Picture");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Fields.Published", "Published");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Fields.Title", "Title");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.FullWidth", "Full width");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.FullWidth.hint", "Full width");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Info", "Info");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.LimitedToStores", "Limited to stores");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.LimitedToStores.Hint", "Determines whether the HeroImage is available only at certain stores.");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Link", "URL");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Link.Hint", "Enter URL. Leave empty if you don't want this picture to be clickable.");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Manage", "Manage Bootstrap HeroImage");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Collection", "Collection");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Collection.Hint", "Select the collection where HeroImage should appear.");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Collection.Required", "Collection is required");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Brand", "Brand");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Brand.Hint", "Select the brand where HeroImage should appear.");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Brand.Required", "Brand is required");

            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Name", "Name");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Name.Hint", "Enter the name of the HeroImage");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Name.Required", "Name is required");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Picture", "Picture");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Picture.Required", "Picture is required");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Published", "Published");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Published.Hint", "Specify it should be visible or not");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.HeroImageType", "HeroImage type");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.HeroImageType.Hint", "Choose the HeroImage type. Home page, category or collection page.");
            await this.AddOrUpdatePluginTranslateResource(_translationService, _languageService, "Widgets.HeroImage.Stores", "Stores");


            await base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override async Task Uninstall()
        {

            //clear repository
            await _pictureHeroImageRepository.DeleteAsync(_pictureHeroImageRepository.Table.ToList());

            //locales
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Added");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Addnew");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.AvailableStores");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.AvailableStores.Hint");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Backtolist");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Category");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Category.Hint");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Category.Required");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Description");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Description.Hint");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.DisplayOrder");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.DisplayOrder.Hint");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Edit");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Edited");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Fields.Displayorder");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Fields.Link");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Fields.ObjectType");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Fields.Picture");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Fields.Published");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Fields.Title");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Info");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.LimitedToStores");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.LimitedToStores.Hint");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Link");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Link.Hint");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Manage");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Collection");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Collection.Hint");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Collection.Required");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Brand");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Brand.Hint");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Brand.Required");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Name");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Name.Hint");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Name.Required");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Picture");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Picture.Required");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Published");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Published.Hint");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.HeroImageType");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.HeroImageType.Hint");
            await this.DeletePluginTranslationResource(_translationService, _languageService, "Widgets.HeroImage.Stores");

            await base.Uninstall();
        }

        public override string ConfigurationUrl()
        {
            return HeroImageWidgetDefaults.ConfigurationUrl;
        }
    }
}
