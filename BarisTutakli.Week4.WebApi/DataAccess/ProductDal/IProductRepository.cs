using BarisTutakli.Week4.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.DataAccess.ProductDal
{
    public interface IProductRepository
    {
        Task<int> Add(Product product);
        Task<int> Delete(Product product);
        Task<int> Update(Product product);
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<IList<Product>> Get(Expression<Func<Product, bool>> filter);
        Task<PagedResponse<List<Product>>> Paging(PaginationFilter filter, string root);

    }
}
