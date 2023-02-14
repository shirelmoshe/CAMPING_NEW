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

namespace CampingProject
{
    public static class CreatingCampaign
    {
        [FunctionName("creatingCampaign")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route ="creatingCampaign")] HttpRequest req,
            ILogger log)
        {
            return await NewMethod2(req);
        }

        private static async Task<IActionResult> NewMethod2(HttpRequest req)
        {
            string requestBody1 = await new StreamReader(req.Body).ReadToEndAsync();
            Campaign newCampaignDetails = System.Text.Json.JsonSerializer.Deserialize<Campaign>(requestBody1);
            if (newCampaignDetails.associationName != null && newCampaignDetails.email != null && newCampaignDetails.uri != null && newCampaignDetails.hashtag != null && newCampaignDetails.CampaignName != null)
            {
                MainManager.Instance.NewCampaing.InsertUserMessageToDb(newCampaignDetails.associationName, newCampaignDetails.email, newCampaignDetails.uri, newCampaignDetails.hashtag, newCampaignDetails.CampaignName);

                return new OkObjectResult("This POST request executed successfully");
            }

            return new BadRequestObjectResult("Failed POST Request");
        }

    }
}
