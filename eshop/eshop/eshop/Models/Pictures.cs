using eshop.Models.Core;
using System;
using System.Collections.Generic;

namespace eshop.Models
{
    public partial class Pictures : ModelBase<Pictures>
    {
        public Pictures()
        {
            ArticlesPictures = new HashSet<ArticlesPictures>();
        }

        public string PId { get; set; }
        public byte[] PContent { get; set; }
        public DateTime PDatecreation { get; set; }

        public virtual ICollection<ArticlesPictures> ArticlesPictures { get; set; }
    }
}
