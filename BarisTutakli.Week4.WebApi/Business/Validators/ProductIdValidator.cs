using BarisTutakli.Week4.WebApi.Business.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Business.Validators
{
    public class ProductIdValidator : AbstractValidator<ProductDetailQuery>
    {
        public ProductIdValidator()
        {
            RuleFor(vm => vm.Id).GreaterThan(0).NotEmpty().NotNull();
        }
       
    }
}
