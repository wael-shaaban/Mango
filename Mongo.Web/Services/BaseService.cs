﻿using Mongo.Web.Services.IServices;
using Mongo.Web.ViewModels;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using static Mongo.Web.Utility.SD;

namespace Mongo.Web.Services
{
    public class BaseService(IHttpClientFactory httpClientFactory,ITokeProvider tokeProvider) : IBaseService
    {

        public async Task<GeneralResponseDTO?> SendAsync(RequestDTO requestDTO, bool withJwtBrearer = true)
        {
			try
			{
                var httpClient = httpClientFactory.CreateClient("MangoAPI");
                HttpRequestMessage httpRequestMessage = new();
                httpRequestMessage.RequestUri = new Uri(requestDTO.Url);
                httpRequestMessage.Headers.Add("Accept", "application/json");
                if(withJwtBrearer)
                {
                    var token = tokeProvider.GetToken();
                    httpRequestMessage.Headers.Add("Authorization", $"Bearer {token}");
                }
                if (requestDTO.Data is not null)
                    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
                HttpResponseMessage? httpResponseMessage = null;
                httpRequestMessage.Method = requestDTO.ApiType switch
                {
                    ApiType.GET => HttpMethod.Get,
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => throw new ArgumentOutOfRangeException(nameof(requestDTO.ApiType), $"Unsupported API type: {requestDTO.ApiType}")
                };
                httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                switch (httpResponseMessage.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { Success = false, Message = "Not Found!" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { Success = false, Message = "Access Denied" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { Success = false, Message = "Unauthorized!" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { Success = false, Message = "Internal Server Error!" };
                    default:
                        var apiresponse =await httpResponseMessage.Content.ReadAsStringAsync();
                        var apiresponseDTO = JsonConvert.DeserializeObject<GeneralResponseDTO>(apiresponse);
                        //apiresponseDTO.Success = true;
                        return apiresponseDTO;
                }
            }
			catch (Exception ex)
			{
                return new() { Data = null, Message = ex.Message, Success = false };
			}
        }
    }
}
