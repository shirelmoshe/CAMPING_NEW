using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using Campaigns.Model;
using System.Collections.Generic;
using Campaigns.Entities;

namespace Camping
{
    public static class Camping
    {
        [FunctionName("UpdateCamping")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdatePage/{userId?}")] HttpRequest req, string userId,
        ILogger log)
        {
            string requestBody;
            log.LogInformation("C# HTTP trigger function processed a request.");

           
                if (!string.IsNullOrEmpty(userId))
            {
                var requestBody1 = await new StreamReader(req.Body).ReadToEndAsync();
                var campaign = System.Text.Json.JsonSerializer.Deserialize<Campaign>(requestBody1);

                // Validate that all necessary parameters are present
                if (string.IsNullOrWhiteSpace(campaign.associationName) ||
                    string.IsNullOrWhiteSpace(campaign.email) ||
                    string.IsNullOrWhiteSpace(campaign.uri) ||
                    string.IsNullOrWhiteSpace(campaign.hashtag) ||
                    string.IsNullOrWhiteSpace(campaign.CampaignName))
                {
                    return new BadRequestObjectResult("One or more of the required fields are missing or empty.");
                }

                try
                {
                    MainManager.Instance.NewCampaing.UpdateCampingInDb(userId, campaign.associationName, campaign.email, campaign.uri, campaign.hashtag, campaign.CampaignName);
                }
                catch (Exception ex)
                {
                    // Log the error message using a logging library
                    log.LogError(ex, "An error occurred while updating the camping");

                    // Return a descriptive error message to the client
                    return new BadRequestObjectResult("An error occurred while processing the request. Please try again later.");
                }

                return new OkObjectResult("Campaign updated successfully.");
            }

            // Return a "NotFoundObjectResult" with the message "User not found with provided id"
            return new NotFoundObjectResult("User not found with provided id");
        }
    }
}