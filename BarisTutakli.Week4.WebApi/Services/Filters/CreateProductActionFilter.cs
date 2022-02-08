using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Services.Filters
{
    
    public class CreateProductActionFilter :IActionFilter
    {
        private DateTime _requestTime { get; set; }
        private DateTime _responseTime { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _responseTime = DateTime.Now;
            context.HttpContext.Response.Headers.Add("ProcessTime", $"Request time: {_requestTime} Response time: {_responseTime}");
        }

        public  void  OnActionExecuting(ActionExecutingContext context)
        {
            _requestTime = DateTime.Now;
        }

    }
}
