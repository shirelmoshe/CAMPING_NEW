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
    public static class GetDonation
    {
        [FunctionName("Donation")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "donation/{userId?}")] HttpRequest req,
            string userId, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var newDonation = JsonConvert.DeserializeObject<Donation>(requestBody);

                if (string.IsNullOrWhiteSpace(newDonation.CompanyName) ||
                    string.IsNullOrWhiteSpace(newDonation.Product) ||
                    string.IsNullOrWhiteSpace(newDonation.Email) ||
                    string.IsNullOrWhiteSpace(newDonation.Price) ||
                    string.IsNullOrWhiteSpace(newDonation.CampaignName))
                // string.IsNullOrWhiteSpace(newDonation.Quantity))
                {
                    return new BadRequestObjectResult("One or more of the required fields are missing or empty.");
                }

                MainManager.Instance.NewDonorDetail.CreateDonation(
                    newDonation.CompanyName,
                    newDonation.CampaignName,
                    newDonation.Product,
                    newDonation.Email,
                    newDonation.Price,
                    newDonation.Quantity);

                return new OkObjectResult("Donation added successfully.");
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult("An error occurred while processing the request. Please try again later.");
            }
        }
    }
}
