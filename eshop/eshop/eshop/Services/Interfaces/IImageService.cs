using eshop.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Services.Interfaces
{
    public interface IImageService
    {
        byte[] ConvertImageToByteArray(Image img);
        byte[] ConvertFileToByteArrayImage(IFormFile file);
        IList<Pictures> CreateRangePictures(ICollection<IFormFile> files);
        void JoinPicturesToArticle(Articles article, IList<Pictures> pictures);
    };
}
