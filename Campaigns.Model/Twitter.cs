using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaigns.Model
{
    public class Twitter
    {
        public Twitter() { }




        public int userId { get; set; }
        public string associationName { get; set; }
        public string hashtag { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string twitterUsername { get; set; }
        public string CampaignName { get; set; }

        public int userMoney
        {
            get;
            set;

        }

        public DateTime lastCheckedDateTime { get; set; }




    }
}
