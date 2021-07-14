using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models.BusinessEntity
{
    public class ActivityView
    {
        public string ActivityId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }
}
