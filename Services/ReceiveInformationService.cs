using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using InternetMall.Models.ControllerModels;
using System.Collections.Generic;
using System.Linq;

namespace InternetMall.Services
{
    /// <summary>
    /// 收货信息
    /// </summary>
    public class ReceiveInformationService : IReceiveInformationService
    {
        // 构造函数
        private readonly ModelContext _context;

        public ReceiveInformationService(ModelContext context)
        {
            _context = context;
        }

        // 查看收货地址
        public List<ReceiveInformation> GetReceiveInformation(string buyerid)
        {
            return _context.ReceiveInformations.Where(x => x.BuyerId == buyerid).ToList();
        }

        // 创建收货信息
        // public string createReceiveInformation(string buyerid, string receiverName, string phone, string country, string province, string city, string receivedId, string district, string detailAddr)
        public string createReceiveInformation(string buyerid, string receiverName, string phone, string detailAddr, string tag)
        {
            // 收货信息Id生成
            CreateIdCount receivedCount = new CreateIdCount(_context);
            string receivedid = receivedCount.GetOrderCount();

            ReceiveInformation receiveInformation = new ReceiveInformation
            {
                ReceivedId = receivedid,
                Phone = phone,
                ReceiverName = receiverName,
                BuyerId = buyerid,
                Country = "1",
                Province = "2",
                City = "3",
                District = "4",
                DetailAddr = detailAddr,
                Tag = tag
            };

            _context.ReceiveInformations.Add(receiveInformation);

            if (_context.SaveChanges() > 0)
                return receivedid;
            else
                return null;
        }

        // 删除收货信息
        public bool deleteReceiveInformation(string receiveid)
        {
            ReceiveInformation receiveInformation = _context.ReceiveInformations.Where(x => x.ReceivedId == receiveid).FirstOrDefault();

            _context.ReceiveInformations.Remove(receiveInformation);

            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        // 更新收货地址
        public bool updateReceiveInformation(string receiveId, string newName, string newPhone, string newAddress, string newTag)
        {
            ReceiveInformation receiveInformation = _context.ReceiveInformations.Where(x => x.ReceivedId == receiveId).FirstOrDefault();
            if (receiveInformation != null)
            {
                receiveInformation.ReceiverName = newName;
                receiveInformation.Phone = newPhone;
                receiveInformation.DetailAddr = newAddress;
                receiveInformation.Tag = newTag;
                _context.ReceiveInformations.Update(receiveInformation);
                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
    }
}

