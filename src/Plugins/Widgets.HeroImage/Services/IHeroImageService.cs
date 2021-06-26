using Widgets.HeroImage.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Widgets.HeroImage.Services
{
    public partial interface IHeroImageService
    {
        /// <summary>
        /// Delete a HeroImage
        /// </summary>
        /// <param name="slide">Slide</param>
        Task DeleteHeroImage(PictureHeroImage slide);

        /// <summary>
        /// Gets all 
        /// </summary>
        /// <returns>Picture HeroImages</returns>
        Task<IList<PictureHeroImage>> GetPictureHeroImages();

        /// <summary>
        /// Gets by type 
        /// </summary>
        /// <returns>Picture HeroImages</returns>
        Task<IList<PictureHeroImage>> GetPictureHeroImages(HeroImageType HeroImageType, string objectEntry = "");

        /// <summary>
        /// Gets by type 
        /// </summary>
        /// <returns>Picture HeroImage</returns>
        Task<PictureHeroImage> GetPictureHeroImage(HeroImageType HeroImageType, string objectEntry = "");

        /// <summary>
        /// Gets a tax rate
        /// </summary>
        /// <param name="slideId">Slide identifier</param>
        /// <returns>Tax rate</returns>
        Task<PictureHeroImage> GetById(string slideId);

        /// <summary>
        /// Inserts a slide
        /// </summary>
        /// <param name="slide">Picture HeroImage</param>
        Task InsertPictureHeroImage(PictureHeroImage slide);

        /// <summary>
        /// Updates slide
        /// </summary>
        /// <param name="slide">Picture HeroImage</param>
        Task UpdatePictureHeroImage(PictureHeroImage slide);

        /// <summary>
        /// Delete slide
        /// </summary>
        /// <param name="slide">Picture HeroImage</param>
        Task DeletePictureHeroImage(PictureHeroImage slide);
    }
}
