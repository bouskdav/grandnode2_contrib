using AutoMapper;
using Grand.Infrastructure.Mapper;
using Widgets.HeroImage.Domain;
using Widgets.HeroImage.Models;

namespace Widgets.HeroImage.Infrastructure.Mapper
{
    public class HeroImageMapperConfiguration : Profile, IAutoMapperProfile
    {
        protected string SetObjectEntry(HeroImageModel model)
        {
            if (model.HeroImageTypeId == (int)HeroImageType.HomePage)
                return "";

            if (model.HeroImageTypeId == (int)HeroImageType.Category)
                return model.CategoryId;

            if (model.HeroImageTypeId == (int)HeroImageType.Collection)
                return model.CollectionId;

            if (model.HeroImageTypeId == (int)HeroImageType.Brand)
                return model.BrandId;

            return "";
        }
        protected string GetCategoryId(PictureHeroImage pictureHeroImage)
        {
            if (pictureHeroImage.HeroImageTypeId == HeroImageType.Category)
                return pictureHeroImage.ObjectEntry;

            return "";
        }

        protected string GetCollectionId(PictureHeroImage pictureHeroImage)
        {
            if (pictureHeroImage.HeroImageTypeId == HeroImageType.Collection)
                return pictureHeroImage.ObjectEntry;

            return "";
        }
        protected string GetBrandId(PictureHeroImage pictureHeroImage)
        {
            if (pictureHeroImage.HeroImageTypeId == HeroImageType.Brand)
                return pictureHeroImage.ObjectEntry;

            return "";
        }

        public HeroImageMapperConfiguration()
        {
            CreateMap<HeroImageModel, PictureHeroImage>()
                .ForMember(dest => dest.ObjectEntry, mo => mo.MapFrom(x => SetObjectEntry(x)))
                .ForMember(dest => dest.Locales, mo => mo.Ignore());

            CreateMap<PictureHeroImage, HeroImageModel>()
                .ForMember(dest => dest.CategoryId, mo => mo.MapFrom(x => GetCategoryId(x)))
                .ForMember(dest => dest.CollectionId, mo => mo.MapFrom(x => GetCollectionId(x)))
                .ForMember(dest => dest.BrandId, mo => mo.MapFrom(x => GetBrandId(x)))
                .ForMember(dest => dest.Locales, mo => mo.Ignore());

            CreateMap<HeroImageListModel, PictureHeroImage>()
                .ForMember(dest => dest.Locales, mo => mo.Ignore())
                .ForMember(dest => dest.Stores, mo => mo.Ignore());

            CreateMap<PictureHeroImage, HeroImageListModel>()
                .ForMember(dest => dest.UserFields, mo => mo.Ignore())
                .ForMember(dest => dest.PictureUrl, mo => mo.Ignore());
        }
        public int Order
        {
            get { return 0; }
        }
    }
}
