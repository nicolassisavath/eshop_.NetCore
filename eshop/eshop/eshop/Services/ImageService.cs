using eshop.Models;
using eshop.Repositories.Core;
using eshop.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Services
{
    public class ImageService : IImageService
    {
        private IRepositoryWrapper RepoW;

        public ImageService(IRepositoryWrapper repoW)
        {
            RepoW = repoW;
        }

        public byte[] ConvertImageToByteArray(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] ByteImage = ms.ToArray();

            return ByteImage;
        }

        /*
         * Convert IFormFile to byte[]
         * Utilized to set the byte[] avatar for new users or updated users
         */ 
        public byte[] ConvertFileToByteArrayImage(IFormFile file)
        {
            try
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    // Convert the File image to Image and then byte[]
                    file.CopyTo(stream);
                    Image img = Image.FromStream(stream);
                    byte[] ByteImage = ConvertImageToByteArray(img);

                    return ByteImage;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /*
         * Convert the 
         */ 
        public IList<Pictures> CreateRangePictures(ICollection<IFormFile> files)
        {
            IList<Pictures> pictures = new List<Pictures>();
            IList<Pictures>createdPictures = new List<Pictures>();

            foreach (IFormFile file in files)
            {
                Pictures newPicture = new Pictures();
                newPicture = InitializePicture(newPicture);
                newPicture.PContent = ConvertFileToByteArrayImage(file);
                pictures.Add(newPicture);
            }

            createdPictures = RepoW.PitcuresRep.CreateRangePictures(pictures);

            return createdPictures;
        }

        public Pictures InitializePicture(Pictures picture)
        {
            picture.PId = Guid.NewGuid().ToString();
            picture.PDatecreation = DateTime.Now;

            return picture;
        }

        public void JoinPicturesToArticle(Articles article, IList<Pictures> pictures)
        {
            ArticlesPictures LinkedArticlePicture = new ArticlesPictures();

            foreach (Pictures picture in pictures)
            {
                LinkedArticlePicture = new ArticlesPictures
                {
                    AcArtId = article.AId,
                    AcPicId = picture.PId
                };
                RepoW.Repo.ArticlesPictures.Add(LinkedArticlePicture);
            }
        }
    }
}
