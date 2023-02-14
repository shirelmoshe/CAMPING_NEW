using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Campaigns.Entities;
using Campaigns.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace CampingProject
{
    public static class GetUser
    {
        [FunctionName("getUser")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "User")] HttpRequest req)
        {
            Dictionary<int, User> user = MainManager.Instance.userNew.UserDetailsfromSQL();
            string getUser = System.Text.Json.JsonSerializer.Serialize(user);

           
                return new OkObjectResult(getUser);
        }
    }
}




