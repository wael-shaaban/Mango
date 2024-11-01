using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mongo.Web.Models;
using Mongo.Web.Services;
using Mongo.Web.Services.IServices;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Mongo.Web.Controllers
{
    public class HomeController(IProductService productService,ILogger<HomeController> _logger) : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<ProductDTO> products = new();
            GeneralResponseDTO result = await productService?.GetAllProductsAsync();
            if (result is not null)
            {
                if (result.Success)
                {
                    TempData["success"] = "Getting all products successfully!";
                    products = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(result.Data));
                }
                else
                    TempData["error"] = result.Message;
            }
            return View(products);
        }
        [Authorize]
          public async Task<IActionResult> Details(int productId)
        {
            ProductDTO product = new();
            GeneralResponseDTO result = await productService?.GetProductByIdAsync(productId);
            if (result is not null)
            {
                if (result.Success)
                {
                    TempData["success"] = "Getting all products successfully!";
                    product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(result.Data));
                }
                else
                    TempData["error"] = result.Message;
            }
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
