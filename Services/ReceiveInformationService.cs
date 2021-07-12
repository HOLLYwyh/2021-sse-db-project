using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using InternetMall.Models.ControllerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // 创建收货信息
        public bool createReceiveInformation(string buyerid, string receiverName, string phone, string country, string province, string city, string receivedId, string district, string detailAddr)
        {
            // 收货信息Id生成
            CreateIdCount receivedCount = new CreateIdCount(_context);
            string receivedid = receivedCount.GetOrderCount();

            ReceiveInformation receiveInformation = new ReceiveInformation
            {
                ReceivedId = receivedid,
                Phone = phone,
                BuyerId = buyerid,
                Country = country,
                Province = province,
                City = city,
                District = district,
                DetailAddr = detailAddr
            };

            _context.ReceiveInformations.Add(receiveInformation);

            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
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
    }
}
