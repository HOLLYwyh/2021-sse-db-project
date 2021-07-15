using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Activity
    {
        public Activity()
        {
            Coupons = new HashSet<Coupon>();
        }
        public string ActivityId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Name { get; set; }
        public short? Category { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Coupon> Coupons { get; set; }
    }
}
