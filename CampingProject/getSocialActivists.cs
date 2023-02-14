using Campaigns.Entities;
using Campaigns.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampingProject
{
    public class getSocialActivists
    {
        
        [FunctionName("SocialActivists")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "SocialActivists/{user_id?}")] HttpRequest req,
        string user_id)
        {
           
            if (!string.IsNullOrEmpty(user_id))
            {
                string responseMessage3 = System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.userNew.GetUserSocialActivistFromDbById(user_id));
                // Return an "OkObjectResult" with the serialized data
                return new OkObjectResult(responseMessage3);
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}