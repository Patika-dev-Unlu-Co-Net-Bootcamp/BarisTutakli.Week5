using BarisTutakli.Week4.WebApi.Contexts;
using BarisTutakli.Week4.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace BarisTutakli.Week4.WebApi.DataAccess.ProductDal
{
    public class ProductRepository : IProductRepository
    {
        private readonly UnluDBContext _context;
        private readonly DbSet<Product> _dbSet;
        public ProductRepository(UnluDBContext context)
        {
            _context = context;
            _dbSet = _context.Set<Product>();

        }
        public async Task<int> Add(Product product)
        {
            await _dbSet.AddAsync(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(Product product)
        {
            _dbSet.Remove(product);
            return await _context.SaveChangesAsync();

        }

        public async Task<PagedResponse<List<Product>>> Paging(PaginationFilter filter,string root)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _dbSet
                .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
                .Take(validFilter.PageSize)
                .ToListAsync();
            var totalRecords = await _dbSet.CountAsync();
            
            // Sort and filter
            pagedData = FilterProductByDate(filter, pagedData);
            pagedData = FilterProductsByName(filter, pagedData);
            pagedData = SortProduct(filter, pagedData);

            PagedResponse<List<Product>> pagedResponse = new PagedResponse<List<Product>>(pagedData, validFilter.PageNumber, validFilter.PageSize);
            pagedResponse.Message = "Pagination";
            pagedResponse.FirstPage = UriGenerator( validFilter.PageNumber - 1,validFilter.PageSize,root);
            pagedResponse.LastPage = UriGenerator((int)Math.Ceiling(totalRecords / (double)validFilter.PageSize), validFilter.PageSize, root);
            pagedResponse.NextPage = UriGenerator(validFilter.PageNumber+1, validFilter.PageSize, root);
            pagedResponse.PreviousPage= validFilter.PageNumber>1? UriGenerator(validFilter.PageNumber-1, validFilter.PageSize, root) : UriGenerator(1, validFilter.PageSize, root);
            pagedResponse.TotalRecords = totalRecords;
            pagedResponse.TotalPages = (int)Math.Ceiling(totalRecords / (double)validFilter.PageSize);
            
            return pagedResponse;
        }


        private Uri UriGenerator(int pageNumber,int pageSize,string root)
        {

            string uri = $"{root}?PageNumber={pageNumber}&PageSize={pageSize}";
            return new Uri(uri);
        }
        public List<Product> SortProduct(PaginationFilter filter, List<Product> products)
        {
            if (filter.Sort!="asc")
            {
                return filter.SortByName is null? products.OrderByDescending(p => p.Id).ToList(): products.OrderByDescending(p => p.ProductName).ToList();
            }

            return filter.SortByName is null ? products.OrderByDescending(p => p.Id).ToList() :products.OrderByDescending(p => p.ProductName).ToList();

        }


        public List<Product> FilterProductByDate(PaginationFilter filter,List<Product> products)
        {

            return products.Where(p => p.CreatedAt > filter.MinDate && p.CreatedAt < filter.MaxDate).ToList();

        }

        public List<Product> FilterProductsByName(PaginationFilter filter, List<Product> products)
        {
            return filter.ProductName is not null ? products.Where(p => p.ProductName.Contains(filter.ProductName)).ToList() : products;
        }




        public async Task<IList<Product>> Get(Expression<Func<Product, bool>> filter)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }

        public async Task<List<Product>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public Task<Product> GetById(int id)
        {
            return Task.FromResult(_dbSet.SingleOrDefault(e => e.Id == id));
        }

        public async Task<int> Update(Product product)
        {
            _dbSet.Update(product);
            return await _context.SaveChangesAsync();
        }
    }
}
