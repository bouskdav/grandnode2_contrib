using Grand.Business.Common.Interfaces.Stores;
using Grand.Web.Common.Localization;
using Grand.Web.Common.Link;
using Grand.Web.Common.Models;
using Grand.Domain.Localization;
using Grand.Domain.Stores;
using Grand.Infrastructure.Mapper;
using Grand.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Widgets.HeroImage.Domain;
using Widgets.HeroImage.Models;

namespace Widgets.HeroImage
{
    public static class MyExtensions
    {
        public static HeroImageModel ToModel(this PictureHeroImage entity)
        {
            return entity.MapTo<PictureHeroImage, HeroImageModel>();
        }

        public static PictureHeroImage ToEntity(this HeroImageModel model)
        {
            return model.MapTo<HeroImageModel, PictureHeroImage>();
        }


        public static HeroImageListModel ToListModel(this PictureHeroImage entity)
        {
            return entity.MapTo<PictureHeroImage, HeroImageListModel>();
        }

        public static List<TranslationEntity> ToLocalizedProperty<T>(this IList<T> list) where T : ILocalizedModelLocal
        {
            var local = new List<TranslationEntity>();
            foreach (var item in list)
            {
                Type[] interfaces = item.GetType().GetInterfaces();
                PropertyInfo[] props = item.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
                foreach (var prop in props)
                {
                    bool insert = true;

                    foreach (var i in interfaces)
                    {
                        if (i.HasProperty(prop.Name))
                        {
                            insert = false;
                        }
                    }

                    if (insert && prop.GetValue(item) != null)
                        local.Add(new TranslationEntity()
                        {
                            LanguageId = item.LanguageId,
                            LocaleKey = prop.Name,
                            LocaleValue = prop.GetValue(item).ToString(),
                        });
                }
            }
            return local;
        }
        public static bool HasProperty(this Type obj, string propertyName)
        {
            return obj.GetProperty(propertyName) != null;
        }
    }


}