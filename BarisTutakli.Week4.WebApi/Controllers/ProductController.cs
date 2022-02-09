using BarisTutakli.Week4.WebApi.Business;
using BarisTutakli.Week4.WebApi.Business.ViewModels;
using BarisTutakli.Week4.WebApi.Models;
using BarisTutakli.Week4.WebApi.Services.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Controllers
{

    [Route("api/[controller]s")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMemoryCache _memoryCache;

        public ProductController(IProductService productService, IHttpContextAccessor contextAccessor, IMemoryCache memoryCache)
        {
            _productService = productService;
            _contextAccessor = contextAccessor;
            _memoryCache = memoryCache;
        }
       
      

        // Kullanıcılar ürünlerin listesini görüntüleyebilir
        [Authorize(Roles = Roles.User)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var productDetailViewList = _productService.GetAll();

            return Ok(productDetailViewList.Result);
        }


        // Yetkisi olmayan herkes sınırlı sayıda ürüne ulaşabilir
        // In cache memory de demo veri tutldu
        [HttpGet("demo")]
        public IActionResult GetLimitedProducts()
        {
            if (_memoryCache.TryGetValue("Samsung", out Response<List<ProductDetailViewModel>> productList))
            {
                return Ok(productList);
            }
            var restrictedProductDetailViewList = _productService.GetAll().Result;
            restrictedProductDetailViewList.Data = restrictedProductDetailViewList.Data.GetRange(0, 1);
            _memoryCache.Set("Samsung", restrictedProductDetailViewList.Data);

            return Ok(restrictedProductDetailViewList);
        }
        [ResponseCache(Duration =30, NoStore = false, Location = ResponseCacheLocation.Client,VaryByHeader = "unknown user")]
        [HttpGet("help")]
        public IActionResult GetHelp()
        {
            string message = JsonSerializer.Serialize($"Demo endpoint sınırlı bilgi sunar. Diğer end pointlere erişim için üye olmanız ve yetkinizin olması gerekir." +
                $"Üye olmak için Authenticates/Register adresini kullanınız.");
            if (_memoryCache.TryGetValue("Help", out string response))
            {
                return Ok(response);
            }
     
            _memoryCache.Set("Help", message);

            return Ok(message);
        }


        [HttpGet("paging")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            // Base url adresini aldıktan sonra base url adresine göre gerekli sayfalandırma gönlendirmelrini yapıyorum.
            string value = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}" +
                $"{_contextAccessor.HttpContext.Request.Path}";
            
               var PagedResponseList = await _productService.GetAll(filter,value);
            return Ok(PagedResponseList);
        }


        // Sadece admin yetkisine sahip olan üye ürün ekleyebilir
        [TypeFilter(typeof(CreateProductActionFilter))]
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> Add(ProductCreateViewModel vm)
        {
            var affectedRows = await _productService.Add(vm);
            return affectedRows.Data < 1 ? BadRequest(affectedRows) : Ok(affectedRows);

        }

        /// <summary>
        /// Belirtilen id değerine göre ürün getir
        /// api/products/id.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>.</returns>
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var productViewDetailResponse = _productService.GetById(new ProductDetailQuery { Id = id });

            if (productViewDetailResponse is null)
            {
                return NotFound();
            }
            return Ok(productViewDetailResponse.Result);// return 204 
        }


        /// <summary>
        ///  Sadece admin yetkisine sahip olan üye ürün ekleyebilir
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <param name="updateProductModel">The updateProductModel<see cref="ProductUpdateModel"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductUpdateModel updateProductModel)
        {
            var affectedRowsResponse = _productService.Update(id, updateProductModel);

            return affectedRowsResponse.Result.Data < 1 ? StatusCode(StatusCodes.Status400BadRequest, affectedRowsResponse) :
                Ok(affectedRowsResponse);
        }


        /// <summary>
        /// Delete a specific product 
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var affectedRowsResponse = await _productService.Delete(new ProductDetailQuery { Id = id });
            return affectedRowsResponse.Data < 1 ? StatusCode(StatusCodes.Status400BadRequest, affectedRowsResponse) :
             Ok(affectedRowsResponse);

        }



        ///// <summary>
        ///// Update the category of a product
        ///// </summary>
        ///// <param name="id">The id<see cref="int"/>.</param>
        ///// <param name="updateProductCategoryViewModel">The updateProductCategoryViewModel<see cref="UpdateProductCategoryViewModel"/>.</param>
        ///// <returns>The <see cref="IActionResult"/>.</returns>
        //[HttpPatch("{id}")]
        //public IActionResult UpdateProductCategory(int id, [FromBody] UpdateProductCategoryViewModel updateProductCategoryViewModel)
        //{


        //    return Ok();
        //}
    }
}
