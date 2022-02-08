using BarisTutakli.Week4.WebApi.Business.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Business.Validators
{
    public  class ProductCreateViewModelValidator: AbstractValidator<ProductCreateViewModel>
    {
        public ProductCreateViewModelValidator()
        {
            RuleFor(vm => vm.CategoryId).GreaterThan(0);
            RuleFor(vm => vm.ProductName).MinimumLength(2).NotEmpty();
            RuleFor(vm => vm.PublishDate.Year).GreaterThan(1800);
        }
    }
}
