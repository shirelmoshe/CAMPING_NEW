using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Campaigns.Entities;
using Campaigns.Model;
using System.Collections.Generic;

namespace CampingProject
{
    public static class getCampaingsTable
    {
        [FunctionName("getCampaingsTable")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get",  Route= "CampaingsTable")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            Dictionary<int, Campaign> campaings = MainManager.Instance.NewCampaing.campaingsTableDetailsfromSQL();
            string getCampaingTable = System.Text.Json.JsonSerializer.Serialize(campaings);

            return new OkObjectResult(getCampaingTable);


        }
    }
}
