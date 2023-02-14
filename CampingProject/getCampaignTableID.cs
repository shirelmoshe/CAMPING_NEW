using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Campaigns.Entities;

namespace CampingProject
{
    public static class getCampaignTableID
    {
        [FunctionName("getCampaignTableID")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "CampaignTableID/{user_Id}")] HttpRequest req,
        string user_Id, ILogger log)
        {
            if (user_Id == null)
            {
                return new BadRequestObjectResult("Please provide a user ID in the URL parameter.");
            }

           
                string responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.NewCampaing.GetShippingFromDbById(user_Id));

            return new OkObjectResult(responseMessage);
        }
    }
}