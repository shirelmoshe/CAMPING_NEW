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
using System.Collections.Generic;

namespace CampingProject
{
    public static class getCompanyOwnerUser
    {
        [FunctionName("getCompanyOwnerUser")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "CompanyOwnerUser/{user_id?}")] HttpRequest req, string user_id,
        ILogger log)
        {
            if (user_id != null)
            {
                string responseMessage4 = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.userNew.GetUserSocialActivistFromDbById(user_id));
                // Return an "OkObjectResult" with the serialized data
                return new OkObjectResult(responseMessage4);
            }
            else
            {
                return new NotFoundResult();
            }
        }
    }
}