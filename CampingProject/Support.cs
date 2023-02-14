using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Campaigns.Model;
using Campaigns.Entities;

namespace CampingProject
{
    public static class Support
    {
        [FunctionName("Support")]
 
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var supportCampaignDetails = JsonConvert.DeserializeObject<Twitter>(requestBody);

                if (string.IsNullOrWhiteSpace(supportCampaignDetails.associationName) ||
                    string.IsNullOrWhiteSpace(supportCampaignDetails.email) ||
                    string.IsNullOrWhiteSpace(supportCampaignDetails.userName) ||
                    string.IsNullOrWhiteSpace(supportCampaignDetails.hashtag) ||
                    string.IsNullOrWhiteSpace(supportCampaignDetails.CampaignName) ||
                    string.IsNullOrWhiteSpace(supportCampaignDetails.twitterUsername))
                {
                    return new BadRequestObjectResult("One or more of the required fields are missing or empty.");
                }

                MainManager.Instance.Twitter.InsertUserSupportToDb(
                    supportCampaignDetails.associationName,
                    supportCampaignDetails.email,
                    supportCampaignDetails.userName,
                    supportCampaignDetails.hashtag,
                    supportCampaignDetails.CampaignName,
                    supportCampaignDetails.twitterUsername);

                log.LogInformation("Support campaign inserted into database successfully.");
                return new OkObjectResult("Support campaign added successfully.");
            }
            catch (Exception ex)
            {
                log.LogError(ex, "An error occurred while inserting the support campaign into the database.");
                return new BadRequestObjectResult("An error occurred while processing the request. Please try again later.");
            }
        }
    }
}
