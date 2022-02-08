using BarisTutakli.Week4.WebApi.Business.ViewModels;
using BarisTutakli.Week4.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Business
{
    public interface IProductService
    {
        Task<Response<int>> Add(ProductCreateViewModel productViewModel);
        Task<Response<int>> Delete(ProductDetailQuery query);
        Task<Response<int>> Update(int id, ProductUpdateModel vm);
        Task<Response<List<ProductDetailViewModel>>> GetAll();
        Task<Response<ProductDetailViewModel>> GetById(ProductDetailQuery query);
        Task<IList<Product>> Get(Expression<Func<Product, bool>> filter);
        Task<PagedResponse<List<Product>>> GetAll(PaginationFilter filter, string root);
    }
}
