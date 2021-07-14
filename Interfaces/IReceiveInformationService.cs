using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    /// <summary>
    /// 收货详情
    /// </summary>
    public interface IReceiveInformationService
    {
        // 查看收货信息
        List<ReceiveInformation> GetReceiveInformation(string buyerid);

        // 创建收货信息
        public string createReceiveInformation(string buyerid, string receiverName, string phone, string detailAddr, string tag);

        // 删除收货信息
        public bool deleteReceiveInformation(string receiveid);

        // 更新收货地址
        public bool updateReceiveInformation(string receiveId, string newName, string newPhone, string newAddress, string newTag);
    }
}
