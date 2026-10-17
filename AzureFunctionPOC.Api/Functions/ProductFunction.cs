using AzureFunctionPOC.Api.Models;
using AzureFunctionPOC.Application.Interfaces;
using AzureFunctionPOC.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace AzureFunctionPOC.Api.Functions
{
    public class ProductFunction
    {
        private readonly ILogger<ProductFunction> _logger;
        private readonly IProductService _productService;
        public ProductFunction(ILogger<ProductFunction> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [Function("GetProducts")]
        public async Task<HttpResponseData> GetProducts(
     [HttpTrigger(AuthorizationLevel.Function, "get")]
    HttpRequestData req)
        {
            try
            {
                var products = await _productService.GetProductsAsync();

                var response = req.CreateResponse(HttpStatusCode.OK);

                await response.WriteAsJsonAsync(new ApiResponse<List<ProductDto>>
                {
                    Success = true,
                    Message = "Products fetched successfully.",
                    Data = products
                });

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse =
                    req.CreateResponse(HttpStatusCode.InternalServerError);

                await errorResponse.WriteAsJsonAsync(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });

                return errorResponse;
            }
        }

        [Function("CreateProduct")]
        public async Task<HttpResponseData> CreateProduct(
       [HttpTrigger(AuthorizationLevel.Function, "post")]
    HttpRequestData req)
        {
            try
            {
                var dto = await req.ReadFromJsonAsync<ProductDto>();

                if (dto == null)
                {
                    var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);

                    await badRequest.WriteAsJsonAsync(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "Request body is required.",
                        Data = null
                    });

                    return badRequest;
                }

                var product = await _productService.CreateProductAsync(dto);

                var response = req.CreateResponse(HttpStatusCode.Created);

                await response.WriteAsJsonAsync(new ApiResponse<ProductDto>
                {
                    Success = true,
                    Message = "Product created successfully.",
                    Data = product
                });

                return response;
            }
            catch (Exception ex)
            {
                var errorResponse =
                    req.CreateResponse(HttpStatusCode.InternalServerError);

                await errorResponse.WriteAsJsonAsync(new ApiResponse<object>
                {
                    Success = false,
                    Message = ex.Message,
                    Data = null
                });

                return errorResponse;
            }
        }

    }
}
