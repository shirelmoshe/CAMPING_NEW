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
    public static class Sales
    {
        [FunctionName("Sales")]
        public static async Task<IActionResult> Run1(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Sales/{userId?}")] HttpRequest req, string userId,
            ILogger log)
        {
            string requestBody;
            log.LogInformation("C# HTTP trigger function processed a request.");


            var sales = MainManager.Instance.NewSales.GetSalesDetails();
            var json = System.Text.Json.JsonSerializer.Serialize(sales);
            return new OkObjectResult(json);






            // Return a "NotFoundObjectResult" with the message "User not found with provided id"
            return new NotFoundObjectResult("User not found with provided id");
        }
    }
}
