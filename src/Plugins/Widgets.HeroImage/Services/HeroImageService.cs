using Grand.Business.Common.Interfaces.Security;
using Grand.Domain.Data;
using Grand.Infrastructure;
using Grand.Infrastructure.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Widgets.HeroImage.Domain;

namespace Widgets.HeroImage.Services
{
    public partial class HeroImageService : IHeroImageService
    {
        #region Fields

        private readonly IRepository<PictureHeroImage> _reporistoryPictureHeroImage;
        private readonly IAclService _aclService;
        private readonly IWorkContext _workContext;
        private readonly ICacheBase _cacheBase;

        /// <summary>
        /// Key for HeroImages
        /// </summary>
        /// <remarks>
        /// {0} : Store id
        /// {1} : HeroImage type
        /// {2} : Object entry / categoryId || collectionId
        /// </remarks>
        public const string HeroImageS_MODEL_KEY = "Grand.HeroImage-{0}-{1}-{2}";
        public const string HeroImageS_PATTERN_KEY = "Grand.HeroImage";

        #endregion
        
        public HeroImageService(IRepository<PictureHeroImage> reporistoryPictureHeroImage,
            IWorkContext workContext, IAclService aclService,
            ICacheBase cacheManager)
        {
            _reporistoryPictureHeroImage = reporistoryPictureHeroImage;
            _workContext = workContext;
            _aclService = aclService;
            _cacheBase = cacheManager;
        }
        /// <summary>
        /// Delete a HeroImage
        /// </summary>
        /// <param name="HeroImage">HeroImage</param>
        public virtual async Task DeleteHeroImage(PictureHeroImage slide)
        {
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));

            //clear cache
            await _cacheBase.RemoveByPrefix(HeroImageS_PATTERN_KEY);

            await _reporistoryPictureHeroImage.DeleteAsync(slide);
        }

        /// <summary>
        /// Gets all 
        /// </summary>
        /// <returns>Picture HeroImages</returns>
        public virtual async Task<IList<PictureHeroImage>> GetPictureHeroImages()
        {
            return await Task.FromResult(_reporistoryPictureHeroImage.Table.OrderBy(x => x.HeroImageTypeId).ThenBy(x => x.DisplayOrder).ToList());
        }

        /// <summary>
        /// Gets by type 
        /// </summary>
        /// <returns>Picture HeroImages</returns>
        public virtual async Task<IList<PictureHeroImage>> GetPictureHeroImages(HeroImageType HeroImageType, string objectEntry = "")
        {
            string cacheKey = string.Format(HeroImageS_MODEL_KEY, _workContext.CurrentStore.Id, HeroImageType.ToString(), objectEntry);
            return await _cacheBase.GetAsync(cacheKey, async () =>
            {
                var query = from s in _reporistoryPictureHeroImage.Table
                            where s.HeroImageTypeId == HeroImageType && s.Published
                            select s;

                if (!string.IsNullOrEmpty(objectEntry))
                    query = query.Where(x => x.ObjectEntry == objectEntry);

                var items = query.ToList();
                return await Task.FromResult(items.Where(c => _aclService.Authorize(c, _workContext.CurrentStore.Id)).ToList());
            });
        }

        /// <summary>
        /// Gets by type 
        /// </summary>
        /// <returns>Picture HeroImages</returns>
        public virtual async Task<PictureHeroImage> GetPictureHeroImage(HeroImageType HeroImageType, string objectEntry = "")
        {
            string cacheKey = string.Format(HeroImageS_MODEL_KEY, _workContext.CurrentStore.Id, HeroImageType.ToString(), objectEntry);
            return await _cacheBase.GetAsync(cacheKey, async () =>
            {
                var query = from s in _reporistoryPictureHeroImage.Table
                            where s.HeroImageTypeId == HeroImageType && s.Published
                            select s;

                if (!string.IsNullOrEmpty(objectEntry))
                    query = query.Where(x => x.ObjectEntry == objectEntry);

                var items = query
                    .OrderBy(i => i.DisplayOrder)
                    .ToList();

                //return await Task.FromResult(items.Where(c => _aclService.Authorize(c, _workContext.CurrentStore.Id)).ToList());
                return await Task.FromResult(items.FirstOrDefault(c => _aclService.Authorize(c, _workContext.CurrentStore.Id)));
            });
        }

        /// <summary>
        /// Gets a tax rate
        /// </summary>
        /// <param name="slideId">Slide identifier</param>
        /// <returns>Tax rate</returns>
        public virtual Task<PictureHeroImage> GetById(string slideId)
        {
            return _reporistoryPictureHeroImage.FirstOrDefaultAsync(x => x.Id == slideId);
        }

        /// <summary>
        /// Inserts a slide
        /// </summary>
        /// <param name="slide">Picture HeroImage</param>
        public virtual async Task InsertPictureHeroImage(PictureHeroImage slide)
        {
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));

            //clear cache
            await _cacheBase.RemoveByPrefix(HeroImageS_PATTERN_KEY);

            await _reporistoryPictureHeroImage.InsertAsync(slide);
        }

        /// <summary>
        /// Updates slide
        /// </summary>
        /// <param name="slide">Picture HeroImage</param>
        public virtual async Task UpdatePictureHeroImage(PictureHeroImage slide)
        {
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));

            //clear cache
            await _cacheBase.RemoveByPrefix(HeroImageS_PATTERN_KEY);

            await _reporistoryPictureHeroImage.UpdateAsync(slide);
        }

        /// <summary>
        /// Delete slide
        /// </summary>
        /// <param name="slide">Picture HeroImage</param>
        public virtual async Task DeletePictureHeroImage(PictureHeroImage slide)
        {
            if (slide == null)
                throw new ArgumentNullException(nameof(slide));

            //clear cache
            await _cacheBase.RemoveByPrefix(HeroImageS_PATTERN_KEY);

            await _reporistoryPictureHeroImage.DeleteAsync(slide);
        }

    }
}
