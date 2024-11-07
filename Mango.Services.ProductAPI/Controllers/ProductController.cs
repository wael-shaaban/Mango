using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.DTOs;
using Mango.Services.ProductAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Product")]
    [ApiController]
    //[Authorize]
    public class ProductController(AppDbContext _dbContext, IMapper _mapper) : ControllerBase
    {
        protected GeneralResponseDTO generalResponse = new GeneralResponseDTO();
        [HttpGet]
        [Authorize]
        public GeneralResponseDTO GetAllProducts()
        {
            try
            {
                var data = _dbContext.Products.ToList();
                var result = _mapper.Map<IEnumerable<ProductDTO>>(data);
                if (data is null)
                    throw new Exception("Data Not Found");
                generalResponse.Data = result;
                generalResponse.Success = true;
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
        [HttpGet("{id:int}")]
        public GeneralResponseDTO GetById(int id)
        {
            try
            {
                var data = _dbContext.Products.FirstOrDefault(c => c.ProductId == id);
                if (data is null)
                    throw new Exception("Data Not Found");
                var result = _mapper.Map<ProductDTO>(data);
                generalResponse.Data = result;
                generalResponse.Success = true;
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
        [HttpGet("GetByName/{productName:alpha}")]
        public GeneralResponseDTO GetByName(string productName)
        {
            try
            {
                var data = _dbContext.Products.FirstOrDefault(c => c.Name == productName);
                var result = _mapper.Map<ProductDTO>(data);
                if (data is null)
                    throw new Exception("Data Not Found");
                generalResponse.Data = result;
                generalResponse.Success = true;
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public GeneralResponseDTO AddProduct(ProductDTO product)
        {
            try
            {
                if (product is null)
                    throw new Exception("Coupon data is null");
                var result = _mapper.Map<ProductModel>(product);
                if (result == null)
                    throw new Exception("Data Not Valid");
                _dbContext.Products.Add(result);
                _dbContext.SaveChanges();
                generalResponse.Success = true;
                generalResponse.Data = _mapper.Map<ProductDTO>(result);

                // Returns a 201 status code with a location header pointing to the GetById method.
                //return CreatedAtAction(nameof(GetById), new { id = result.CouponId }, generalResponse);
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.Success = false;
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public GeneralResponseDTO EditCoupon(ProductDTO product)
        {
            try
            {
                if (product == null)
                {
                    throw new Exception("Coupon data is null");
                }

                var result = _mapper.Map<ProductModel>(product);
                if (result == null)
                    throw new Exception("Data Not Valid");
                _dbContext.Products.Update(result);
                _dbContext.SaveChanges();
                generalResponse.Success = true;
                generalResponse.Data = _mapper.Map<ProductDTO>(result);

                // Returns a 201 status code with a location header pointing to the GetById method.
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.Success = false;
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public GeneralResponseDTO RemoveCoupon(int id)
        {
            try
            {
                var data = _dbContext.Products.FirstOrDefault(c => c.ProductId == id);
                if (data is null)
                    throw new Exception("Data Not Found");
                _dbContext.Products.Remove(data);
                _dbContext.SaveChanges();
                generalResponse.Success = true;
                return generalResponse;
            }
            catch (Exception ex)
            {
                generalResponse.Message = ex.Message;
                return generalResponse;
            }
        }
    }
}
