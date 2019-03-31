using eshop.Models.Core;
using System;
using System.Collections.Generic;

namespace eshop.Models
{
    public partial class ArticlesPictures : ModelBase<ArticlesPictures>
    {
        public string AcArtId { get; set; }
        public string AcPicId { get; set; }

        public virtual Articles AcArt { get; set; }
        public virtual Pictures AcPic { get; set; }
    }
}
