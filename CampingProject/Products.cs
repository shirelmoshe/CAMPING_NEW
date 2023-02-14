using Campaigns.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;


public class ProductsController
{
    [FunctionName("Products")]
    public static async Task<IActionResult> GetProducts(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Products/{productsId?}")] HttpRequest req, string productsId,
        ILogger log)
    {
        if (productsId == null)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return Product();
        }
        else
        {
            return ProductMethod(productsId);
        }
    }

    private static IActionResult Product()
    {
        try
        {
            var productDetails = MainManager.Instance.NewDonorDetail.ProductsDetailsFromSQL();

            var responseMessage = System.Text.Json.JsonSerializer.Serialize(productDetails);

            return new OkObjectResult(responseMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new BadRequestObjectResult("An error occurred while processing the request. Please try again later.");
        }
    }

    private static IActionResult ProductMethod(string productsId)
    {
        int id;
        if (!int.TryParse(productsId, out id))
        {
            return new BadRequestObjectResult("The products id must be a valid integer");
        }
        var product = MainManager.Instance.NewDonorDetail.GetProductFromDbById(id);
        if (product == null)
        {
            return new NotFoundObjectResult("No product found with the provided id");
        }
        string responseMessage = System.Text.Json.JsonSerializer.Serialize(product);
        return new OkObjectResult(responseMessage);
    }
}
