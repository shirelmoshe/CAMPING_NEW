using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaigns.Model
{
    public class Sale
    {
        public string buyerName { get; set; }
        public string cellphoneNumber { get; set; }
        public string Email { get; set; }
        public string buyerAddress { get; set; }
        public string CompanyName { get; set; }


        public string Product { get; set; }



        public string Price { get; set; }

        public string CampaignName { get; set; }


        public int productsId { get; set; }
        public int userId { get; set; }





    }
}
