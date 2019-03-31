using eshop.Models.Core;
using System;
using System.Collections.Generic;

namespace eshop.Models
{
    public partial class Articles : ModelBase<Articles>
    {
        public Articles()
        {
            ArticlesPictures = new HashSet<ArticlesPictures>();
        }

        public string AId { get; set; }
        public string ATitle { get; set; }
        public string ADescription { get; set; }
        public decimal APriceunit { get; set; }
        public int? AQtity { get; set; }

        public virtual ICollection<ArticlesPictures> ArticlesPictures { get; set; }
    }
}
