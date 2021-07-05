using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Constants
{
    public class COrderCommodity
    {
        public const int Unknown = 0;       //未定义
        public const int ToBePay = 1;       //待付款
        public const int ToBeShip = 2;      //待发货
        public const int Shipping = 3;      //运送中
        public const int ToBeReceive = 4;   //待收货
        public const int ToBeComment = 5;   //待评价
        public const int Done = 6;          //已完成
        public const int Canceled = 9;      //已取消
        public const int pending = 10;      //待处理
    }
}
