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
    public static class GetAdmin
    {
        [FunctionName("GetAdmin")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getadmin/{user_id?}")] HttpRequest req,
            string user_id, ILogger log)
        {
            if (user_id == null)
            {
                return new BadRequestObjectResult("Please provide a user ID in the URL parameter.");
            }

            string responseMessage = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.userNew.GetUserSocialActivistFromDbById(user_id));
            return new OkObjectResult(responseMessage);
        }
    }
}
