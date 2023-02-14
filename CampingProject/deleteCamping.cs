using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Campaigns.Entities;

namespace CampingProject
{
    public static class DeleteCamping
    {
        [FunctionName("deleteCamping")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "deleteCamping/{userId?}")]
            HttpRequest req, string userId, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

           

            if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out int campingId))
            {
             /*   var deleted = MainManager.Instance.Newcampaing.DeleteAProductByCampingID(campingId);

                if (deleted)
               {
                    return new OkResult();
                }
                else
                {
                    return new NotFoundObjectResult("User not found with provided id");
                }
             */
            }

            return new BadRequestObjectResult("User ID is required");
        }
    }
}
