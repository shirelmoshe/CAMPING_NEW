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
    public static class PostSnigUp
    {
        [FunctionName("PostSnigUp")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "SnigUp")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");


            string singUp = await new StreamReader(req.Body).ReadToEndAsync();
            User newUser = System.Text.Json.JsonSerializer.Deserialize<User>(singUp);


            if (newUser.userName != null && newUser.cellphoneNumber != null && newUser.email != null && newUser.UserType != null && newUser.twitterUsername != null && newUser.user_id != null)
            {
                MainManager.Instance.userNew.CreateUsers(newUser.userName, newUser.cellphoneNumber, newUser.email, newUser.UserType, newUser.twitterUsername, newUser.user_id);


                return new OkObjectResult("This POST request executed successfully");
            }

            else return new BadRequestObjectResult("Failed POST Request");
        }
    }
}
