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
    
    public class MainManager
        
    {
        Logger Log = null;
       
        public MainManager()
        {
            init();
        }
        public static MainManager Instance { get; } = new MainManager();



        public Campaigns NewCampaing;
        public Users userNew;
        public Entities.Twitters Twitter;
        public sales NewSales;
        public Donation buyNewProduct;
        public Donations NewDonorDetail;
        Campaigns campaigns;
        public MoneyTrackings moneyTracking;
        public void init()
        {
            Log = new Logger("logFile");

            //public sales Shipments = new sales();
            NewCampaing = new Campaigns(Log);
            Twitter = new Twitters(Log);
            NewSales = new sales(Log);
            userNew = new Users(Log);
            NewDonorDetail = new Donations(Log);
            moneyTracking = new MoneyTrackings(Log);
        }


    }



    /*
    public interface ILogger
    {
        void Init();
        void LogEvent(Logger log);
        void LogError(LogItem log);
        void LogException(LogItem log);
        void LogCheckHoseKeeping();
    }
    */

}
