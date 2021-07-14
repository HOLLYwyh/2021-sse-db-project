using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Models.BusinessEntity
{
    public class Good
    {
        public string img { get; set; }
        public string intro { get; set; }
        public string shop { get; set; }
        public string ID { get; set; }
        public string description { get; set; }
        public int Soldnum { get; set; }
        public decimal? price { get; set; }
    }
}
