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
    public static class GetMoneyTracking
    {
        [FunctionName("GetMoneyTracking")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "MoneyTracking/{user_id}")] HttpRequest req, string user_id,
        ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

          
                if (!string.IsNullOrEmpty(user_id))
            {
                var moneyTracking = MainManager.Instance.moneyTracking.GetmoneyTrackingsFromDbById(user_id);
                if (moneyTracking != null)
                {
                    var response = new
                    {
                        success = true,
                        data = moneyTracking
                    };
                    return new OkObjectResult(response);
                }
                else
                {
                    return new NotFoundResult();
                }
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}




