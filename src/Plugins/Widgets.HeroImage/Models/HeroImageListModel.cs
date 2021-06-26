using Grand.Infrastructure.Models;

namespace Widgets.HeroImage.Models
{
    public class HeroImageListModel : BaseModel
    {
        public string Id { get; set; }
        public string PictureUrl { get; set; }
        public string Name { get; set; }
        public string DisplayOrder { get; set; }
        public string Link { get; set; }
        public bool Published { get; set; }
        public string ObjectType { get; set; }

    }
}
