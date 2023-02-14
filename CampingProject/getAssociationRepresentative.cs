using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Threading.Tasks;
using Campaigns.Entities;
using Campaigns.Model;

namespace CampingProject
{
    public class getAssociationRepresentative
    {
        [FunctionName("AssociationRepresentative")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "AssociationRepresentative/{user_id?}")] HttpRequest req,
        string user_id)
        {
            if (!string.IsNullOrEmpty(user_id))
            {
                User user = MainManager.Instance.userNew.GetUserSocialActivistFromDbById(user_id);

            
                        if (user != null)
                {
                    string responseMessage = System.Text.Json.JsonSerializer.Serialize(user);
                    return new OkObjectResult(responseMessage);
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