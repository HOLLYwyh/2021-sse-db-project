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
        // 创建收货信息
        public bool createReceiveInformation(string buyerid, string receiverName, string phone, string country, string province, string city, string receivedId, string district, string detailAddr);

        // 删除收货信息
        public bool deleteReceiveInformation(string receiveid);
    }
}
