using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Models.BusinessEntity
{
    public class ShopView
    {
        public string shopID { get; set; }
        public string shopName { get; set; }
        public string shopDescription { get; set; }
        public string img { get; set; }
        public short? creditScore { get; set; }
    }
}
