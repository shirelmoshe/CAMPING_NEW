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

namespace CampingProject
{
    public static class getShipping
    {
        [FunctionName("getShipping")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Shipping/{user_id?}")] HttpRequest req, string user_id,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string responseMessage6 = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.NewSales.GetmoneyShappingDbById(user_id));
            // Return an "OkObjectResult" with the serialized data
            return new OkObjectResult(responseMessage6);
        }
    }
}
