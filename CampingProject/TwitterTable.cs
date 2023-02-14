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
using System.Collections.Generic;
using Campaigns.Model;

namespace CampingProject
{
    public static class TwitterTable
    {
        [FunctionName("TwitterTable")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "TwitterTable")] HttpRequest req)
        {
            List<Twitter> getTwitter = MainManager.Instance.Twitter.TwitterTableDetailsfromSQL();
            string getTwitterTable = System.Text.Json.JsonSerializer.Serialize(getTwitter);
            return new OkObjectResult(getTwitterTable);
        }
    }
}
