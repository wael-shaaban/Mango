using AutoMapper;
using Mango.Services.ShoppingCartAPI.Data;
using Mango.Services.ShoppingCartAPI.DTOs;
using Mango.Services.ShoppingCartAPI.Models;
using Mango.Services.ShoppingCartAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace Mango.Services.ShoppingCartAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/cart")]
    [ApiController]
    public class CartAPIController(AppDbContext dbContext, IMapper mapper,
        IProductService productService, ICouponService couponService) : ControllerBase
    {
        protected GeneralResponseDTO generalResponseDTO = new GeneralResponseDTO();
        [HttpGet("GetCart/{userId}")]
        public async Task<GeneralResponseDTO> GetCartByUserId(string userId)
        {
            try
            {
                CartDto cart = new();
                var cartHeader = await dbContext.CartHeaders.FirstOrDefaultAsync(c => c.UserId == userId);
                if (cartHeader is not null)
                {
                    var cartDetailsCollection = dbContext.CartDetails.Where(c => c.CartHeaderId == cartHeader.CartHeaderId).ToList();
                    if (cartDetailsCollection is not null)
                    {
                        cart.CartHeader = mapper.Map<CartHeaderDto>(cartHeader);
                        cart.CartDetails = mapper.Map<List<CartDetailsDto>>(cartDetailsCollection);
                        var products = await productService.GetProductsAsync();
                        foreach (var item in cart?.CartDetails)
                        {
                            item.Product = products.FirstOrDefault(p => p.ProductId == item.ProductId);
                            if (item.Product is not null)
                            {
                                item.CartHeader.CartTotal += (decimal)item?.Product?.Price * item.Count;
                            }
                        }
                    }
                    //apply coupon if any
                    if (!string.IsNullOrEmpty(cart.CartHeader.CouponCode))
                    {
                        var coupon = await couponService.GetCoupon(cart.CartHeader.CouponCode);
                        if (coupon is not null && cart.CartHeader.CartTotal > coupon.MinAmount)
                        {
                            cart.CartHeader.CartTotal -= (decimal)coupon.DiscountAmount;
                            cart.CartHeader.Discount = (decimal)coupon.DiscountAmount;
                        }
                    }
                    generalResponseDTO.Success = true;
                    generalResponseDTO.Data = cart;
                }
                else
                {
                    generalResponseDTO.Message = "Item not found!";
                }
            }
            catch (Exception ex)
            {
                generalResponseDTO.Message = ex.Message;
            }
            return generalResponseDTO;
        }
        [HttpPost("CartUpsert")]
        public async Task<GeneralResponseDTO> CartUpsert(CartDto cartDto)
        {
            try
            {
                //var cartDetails = await dbContext.CartHeaders.AsNoTracking()
                //  .FirstOrDefaultAsync(h => h.CartHeaderId == cartDto.CartHeader.CartHeaderId);
                var cartHeaderDto = await dbContext.CartHeaders.AsNoTracking()
                    .FirstOrDefaultAsync(h => h.UserId == cartDto.CartHeader.UserId);
                //adding newtodatabase
                if (cartHeaderDto is null)
                {
                    // create cart header & details
                    var cartHeader = mapper.Map<CartHeaderModel>(cartDto.CartHeader);
                    dbContext.CartHeaders.Add(cartHeader);
                    await dbContext.SaveChangesAsync();
                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                    dbContext.CartDetails.Add(mapper.Map<CartDetailsModel>(cartDto.CartDetails.First()));
                    await dbContext.SaveChangesAsync();
                }
                //updating
                else
                {
                    var cartdetailfromdb = await dbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync
                        (
                          c => c.ProductId == cartDto.CartDetails.First().ProductId &&
                          c.CartHeaderId == cartDto.CartHeader.CartHeaderId
                        );
                    if (cartdetailfromdb is null)
                    {
                        //add details
                        cartDto.CartDetails.First().CartHeaderId = cartdetailfromdb.CartHeaderId;
                        dbContext.CartDetails.Add(mapper.Map<CartDetailsModel>(cartDto.CartDetails.First()));
                        await dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        //update details
                        cartDto.CartDetails.First().Count += cartdetailfromdb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartdetailfromdb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartdetailfromdb.CartDetailsId;
                        dbContext.CartDetails.Update(mapper.Map<CartDetailsModel>(cartDto.CartDetails.First()));
                        await dbContext.SaveChangesAsync();
                    }
                }
                generalResponseDTO.Success = true;
                generalResponseDTO.Data = cartDto;
            }
            catch (Exception ex)
            {
                generalResponseDTO.Message = ex.Message;
            }
            return generalResponseDTO;
        }
        [HttpPost("RemoveCart")]
        public async Task<GeneralResponseDTO> RemoveCart([FromBody] int cartDetailsId)
        {
            try
            {
                var cartDetails = await dbContext.CartDetails.FirstOrDefaultAsync(c => c.CartDetailsId == cartDetailsId);
                if (cartDetails is not null)
                {
                    var carItemCount = dbContext.CartDetails.Where(c => c.CartHeaderId == cartDetails.CartHeaderId).Count();
                    dbContext.CartDetails.Remove(cartDetails);
                    if (carItemCount == 1)
                    {
                        var header = await dbContext.CartHeaders.
                            FirstOrDefaultAsync(c => c.CartHeaderId == cartDetails.CartHeaderId);
                        dbContext.CartHeaders.Remove(header);
                    }
                    await dbContext.SaveChangesAsync();
                    generalResponseDTO.Success = true;
                }
                else
                    generalResponseDTO.Message = "Item not found!";
            }
            catch (Exception ex)
            {
                generalResponseDTO.Message = ex.Message;
            }
            return generalResponseDTO;
        }
        [HttpPost("ApplyCoupon")]
        public async Task<GeneralResponseDTO> ApplyCoupon(CartDto cartDto)
        {
            try
            {
                var cartHeader = await dbContext.CartHeaders.FirstOrDefaultAsync(c => c.UserId == cartDto.CartHeader.UserId);
                if (cartHeader is not null)
                {
                    cartHeader.CouponCode = cartDto.CartHeader.CouponCode;
                    dbContext.CartHeaders.Update(cartHeader);
                    await dbContext.SaveChangesAsync();
                    generalResponseDTO.Success = true;
                }
                else
                    generalResponseDTO.Message = "Item not found!";
            }
            catch (Exception ex)
            {
                generalResponseDTO.Message = ex.Message;
            }
            return generalResponseDTO;
        }
        /*
        [HttpPost("RemoveCoupon")]
        public async Task<GeneralResponseDTO> RemoveCoupon(CartDto cartDto)
        {
            try
            {
                var cartHeader = await dbContext.CartHeaders.FirstOrDefaultAsync(c=>c.UserId == cartDto.CartHeader.UserId);
                if(cartHeader is not null)
                {
                    cartHeader.CouponCode = string.Empty;
                    dbContext.CartHeaders.Update(cartHeader);
                    await dbContext.SaveChangesAsync();
                    generalResponseDTO.Success = true;  
                }
                else
                    generalResponseDTO.Message = "Item not found!";
            }
            catch (Exception ex)
            {
                generalResponseDTO.Message = ex.Message;
            }
            return generalResponseDTO;
        }
        */
    }
}