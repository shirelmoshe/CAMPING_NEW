using Campaigns.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campaigns.Model;
using System.Data.SqlClient;
using System.Security.Policy;
using Campaigns.data.sql;
using Utilitis;

namespace Campaigns.Entities
{
    public class MoneyTrackings:BasePromotion
    {

        public MoneyTrackings(Logger Log) : base(Log)
        {
        }

        //  private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public List<moneyTracking> GetmoneyTrackingsFromDbById(string id)
        {
            // Logger.LogEvent("GetMoneyTrackingsFromDbById function called reader: " + id, DateTime.Now);
            data.sql.moneyTrackingSql userFromSql = new data.sql.moneyTrackingSql(log);
            List<moneyTracking> moneyTrackingsList = userFromSql.LoadAllmoneyTrackings(id);
            return moneyTrackingsList;
        }

    }
}
