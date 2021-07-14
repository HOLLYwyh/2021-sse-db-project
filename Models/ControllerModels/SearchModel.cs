using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models
{
   public class SearchName
   {
        public string Context { get; set; }
   }
   public class SearchCommodityType
   {
        public string Type { get; set; }
   }

   public class SearchShopType
   {
        public string Type { get; set; }
   }

   public class SearchNameType
   {
        public string Context { get; set; }
        public string Category { get; set; }
   }
}
