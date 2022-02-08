using BarisTutakli.Week4.WebApi.Business.Validators;
using BarisTutakli.Week4.WebApi.Business.ViewModels;
using BarisTutakli.Week4.WebApi.DataAccess.ProductDal;
using BarisTutakli.Week4.WebApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Business
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<int>> Add(ProductCreateViewModel vm)
        {
            ProductCreateViewModelValidator validator = new ProductCreateViewModelValidator();
            validator.ValidateAndThrow(vm);
            var affectedRow = await _repository.Add(new Product { CategoryId = vm.CategoryId, ProductName = vm.ProductName, PublishDate = DateTime.Parse(vm.PublishDate.ToShortDateString()), CreatedAt = DateTime.Parse(DateTime.Now.ToShortDateString()) });

            return affectedRow < 1 ? new Response<int> {Data=-1, Succeeded = false, Message = "Failed" } : new Response<int> { Data = affectedRow, Succeeded = true, Message = "Added a new product" };
        }

        public async Task<Response<int>> Delete(ProductDetailQuery query)
        {
            ProductIdValidator validator = new ProductIdValidator();
            validator.ValidateAndThrow(new ProductDetailQuery { Id = query.Id });
            var selectedItem = _repository.GetById(query.Id);
            var affectedRow = await _repository.Delete(selectedItem.Result);

            return affectedRow < 1 ? new Response<int> { Data = -1, Succeeded = false, Message = "Failed" } : new Response<int> { Data = affectedRow, Succeeded = true, Message = "Deleted  the selected product" };

        }

        public Task<IList<Product>> Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<ProductDetailViewModel>>> GetAll()
        {
            var productList = await _repository.GetAll();
            List<ProductDetailViewModel> vm = new List<ProductDetailViewModel>();
            productList.ForEach(item => vm.Add(new ProductDetailViewModel
            {
                Id = item.Id,
                CategoryId = item.CategoryId,
                CreatedAt = item.CreatedAt,
                ModifiedAt = item.ModifiedAt,
                ProductName = item.ProductName,
                PublishDate = item.PublishDate
            }));

            
            return new Response<List<ProductDetailViewModel>>(vm); ;
        }

        public Task<Response<ProductDetailViewModel>> GetById(ProductDetailQuery query)
        {
            ProductIdValidator validator = new ProductIdValidator();
            validator.ValidateAndThrow(query);
            var product = _repository.GetById(query.Id).Result;


            return Task.FromResult(new Response<ProductDetailViewModel>(new ProductDetailViewModel
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                CreatedAt = product.CreatedAt,
                ModifiedAt = product.ModifiedAt,
                ProductName = product.ProductName,
                PublishDate = product.PublishDate
            }));

        }

        public async Task<Response<int>> Update(int id,ProductUpdateModel vm)
        {
            ProductUpdateValidator validator = new ProductUpdateValidator();
            validator.ValidateAndThrow(vm);
            var product =_repository.GetById(id);
            product.Result.ProductName = vm.ProductName;
            product.Result.PublishDate = DateTime.Parse( vm.PublishDate.ToShortDateString());
            product.Result.CategoryId = vm.CategoryId;
            product.Result.ModifiedAt = DateTime.Parse(DateTime.Now.ToShortDateString());
             var affectedRow = await _repository.Update(product.Result);

            return affectedRow < 1 ? new Response<int> { Data = -1, Succeeded = false, Message = "Failed" } : new Response<int> { Data = affectedRow, Succeeded = true, Message = "Updated" };

        }


        public async Task<PagedResponse<List<Product>>> GetAll(PaginationFilter filter,string root)
        {
           
            var response = await _repository.Paging(filter,root);

            return response;
        }

    }
}
