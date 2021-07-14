using InternetMall.Models;
using InternetMall.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    public interface IAdminActionService
    {
        // 发布活动
        public bool releaseActivity(string name, string category, DateTime startTime, DateTime endTime, string description);

        // 查看指定活动
        public List<ActivityView> getOneActivity(string activityID);

        // 查看所有活动
        public List<ActivityView> getActivities();

        // 删除活动
        public bool deleteOneActivity(string activityID);
    }
}
