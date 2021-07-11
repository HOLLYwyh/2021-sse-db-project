using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models.BusinessEntity
{
    /// <summary>
    /// 订单显示信息类
    /// </summary>
    public class OrderView
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(70)]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [StringLength(24)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Credit number required")]
        //[CreditCard(ErrorMessage ="Credit number not valid")]
        [RegularExpression("^\\d{16}$", ErrorMessage = "Month not valid")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Month required")]
        [RegularExpression("^\\d{2}$", ErrorMessage = "Month not valid")]
        public string Month { get; set; }
        [Required(ErrorMessage = "Year required")]
        [RegularExpression("^\\d{4}$", ErrorMessage = "Year not valid")]
        public string Year { get; set; }
        [Required(ErrorMessage = "Security code required")]
        [RegularExpression("^\\d{3}$", ErrorMessage = "Security code not valid")]
        public string SecurityCode { get; set; }
        [Required(ErrorMessage = "First name required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name required")]
        public string LastName { get; set; }
        public Nullable<decimal> Total { get; set; }
        public System.DateTime OrderDate { get; set; }
        public string AspNetUserId { get; set; }
        public List<OrderView> OrderDetails { get; set; }
        public int TargetId { get; set; }

        // public OrderTypeEnum OrderType;
        public string LastFourCardDigits { get; set; }
        public int OrderTypeInt { get; set; }
        public int OrderStatusId { get; set; }
       // public OrderStatusView OrderStatus { get; set; }
        public OrderView()
        {
            OrderDetails = new List<OrderView>();
        }
    }
}
