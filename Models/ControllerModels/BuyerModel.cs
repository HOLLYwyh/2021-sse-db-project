using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models
{
    public class BuyerModel
    {
        public string BuyerId { get; set; }
    }
    public class BuyerPhone
    {
        public string BuyerId { get; set; }
        public string OldNo { get; set; }
        public string NewNo { get; set; }
    }
    public class BuyerPasswd
    {
        public string BuyerId { get; set; }
        public string OldPasswd { get; set; }
        public string NewPasswd { get; set; }
    }
    public class BuyerInfo
    {
        public string BuyerId { get; set; }
        public string UpdatedNickname { get; set; }
        public int UpdatedGender { get; set; }
        public DateTime UpdatedBirth { get; set; }
        public string UpdatedUrl { get; set; }
       
    }
    /************************ 买家收货信息 *******************************/
    public class AddReceiveInformation                                 // 添加收货信息
    {
        public string BuyerId { get; set; }
        public string Phone { get; set; }
        public string ReceiverName { get; set; }

        // public string Country { get; set; }
        // public string Province { get; set; }
        // public string City { get; set; }
        // public string District { get; set; }
        public string DetailAddr { get; set; }
        public string Tag { get; set; }
    }

    public class UpdateReceiveInformation                            // 更新收货信息
    {
        public string ReceivedId { get; set; }
        public string Phone { get; set; }
        public string ReceiverName { get; set; }

        // public string Country { get; set; }
        // public string Province { get; set; }
        // public string City { get; set; }
        // public string District { get; set; }
        public string DetailAddr { get; set; }
        public string Tag { get; set; }
    }


    public class DeleteReceiveInformation                           // 删除收货地址
    {
        public string ReceiveId { get; set; }
    }
}
