using BarisTutakli.Week4.WebApi.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Services
{
    public class DBLogger : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine($"[DBLogger]: {message}");
        }
    }
}
