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
    public static class Campaigns
    {
        [FunctionName("Campaigns")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Campaigns")] HttpRequest req,
            ILogger log)
        {
            try
            {
                var campaignDetails = MainManager.Instance.NewCampaing.CampaignDetailsFromSQL();
                var responseMessage = JsonConvert.SerializeObject(campaignDetails);

               

                return new OkObjectResult(responseMessage);
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message);
                return new BadRequestObjectResult("An error occurred while processing the request. Please try again later.");
            }
        }
    }
}
