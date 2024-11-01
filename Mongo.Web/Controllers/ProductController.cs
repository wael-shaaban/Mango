using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mongo.Web.Services.IServices;
using Newtonsoft.Json;

namespace Mongo.Web.Controllers
{
    public class ProductController(IProductService productService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDTO> Products = new();
            GeneralResponseDTO result = await productService?.GetAllProductsAsync();
            if (result is not null)
            {
                if (result.Success)
                {
                    TempData["success"] = "Getting all Products successfully!";
                    Products = JsonConvert.DeserializeObject<List<ProductDTO>>(Convert.ToString(result.Data));
                }
                else
                    TempData["error"] = result.Message;
            }
            return View(Products);
        }
        [HttpGet]
        public async Task<IActionResult> ProductCreate() => View();
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDTO ProductDTO)
        {
            if (ModelState.IsValid)
            {
                var response = await productService.CreateProductAsync(ProductDTO);
                if (response is not null)
                {
                    if (response.Success)
                    {
                        TempData["success"] = "Creating New Product successfully!";
                        return RedirectToAction(nameof(ProductIndex));
                    }
                    else
                        TempData["error"] = response.Message;
                }
            }
            return View(ProductDTO);
        }
       
        [HttpGet]
        public async Task<IActionResult> ProductDelete(int productId)
        {
            ProductDTO Product = new();
            GeneralResponseDTO result = await productService.GetProductByIdAsync(productId);
            if (result is not null)
            {
                if (result.Success)
                {
                    TempData["success"] = "getting Product successfully!";
                    Product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(result.Data));
                }
                else TempData["error"] = result.Message;
                return View(Product);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDTO ProductDTO)
        {
            GeneralResponseDTO result = await productService.DeleteProductAsync(ProductDTO.ProductId);
            if (result is not null)
            {
                if (result.Success)
                {
                    TempData["success"] = "Deleting Product successfully!";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else TempData["error"] = result.Message;
            }
            return View(ProductDTO);
        }
        [HttpGet]
        public async Task<IActionResult> ProductEdit(int productId)
        {
            ProductDTO Product = new();
            GeneralResponseDTO result = await productService.GetProductByIdAsync(productId);
            if (result is not null)
            {
                if (result.Success)
                {
                    TempData["success"] = "getting Product successfully!";
                    Product = JsonConvert.DeserializeObject<ProductDTO>(Convert.ToString(result.Data));
                }
                else TempData["error"] = result.Message;
                return View(Product);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDTO ProductDTO)
        {
            GeneralResponseDTO result = await productService.UpdateProductAsync(ProductDTO);
            if (result is not null)
            {
                if (result.Success)
                {
                    TempData["success"] = "Updating Product successfully!";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else TempData["error"] = result.Message;
            }
            return View(ProductDTO);
        }

        //[HttpGet("debug")]
        //[Authorize]
        //public IActionResult DebugUserRoles()
        //{
        //    var claims = User.Claims.Select(c => new { c.Type, c.Value });
        //    return Ok(claims);
        //}
    }
}