using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Activity
    {
        public string ActivityId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Name { get; set; }
        public string description  { get; set; }
        public int? Category { get; set; }
    }
}
