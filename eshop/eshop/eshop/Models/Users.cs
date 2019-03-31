using eshop.Models.Core;
using System;
using System.Collections.Generic;

namespace eshop.Models
{
    public partial class Users : ModelBase<Users>
    {
        public string UId { get; set; }
        public string ULogin { get; set; }
        public string UPassword { get; set; }
        public string USalt { get; set; }
        public DateTime UDatecreation { get; set; }
        public DateTime? ULastlogin { get; set; }
        public byte[] UAvatar { get; set; }
    }
}
