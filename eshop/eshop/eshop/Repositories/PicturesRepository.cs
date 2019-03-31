using eshop.Models;
using eshop.Repositories.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Repositories
{
    public class PicturesRepository : RepositoryBase<Pictures>
    {
        public PicturesRepository(eshopContext context)
            :base(context)
        {

        }

        public Pictures CreatePicture(byte[] content)
        {
            Pictures newPicture = new Pictures
            {
                PId = Guid.NewGuid().ToString(),
                PContent = content,
                PDatecreation = DateTime.Now
            };

            Create(newPicture);

            return newPicture;
        }

        public IList<Pictures> CreateRangePictures(IList<Pictures> pictures)
        {
            Repo.Pictures.AddRange(pictures);

            return pictures;
        }

    }
}
