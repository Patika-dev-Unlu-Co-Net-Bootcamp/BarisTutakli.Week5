using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Services.Abstract
{
    public interface ILoggerService
    {
        public void Write(string message);
    }
}
