using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Models
{
    public class Product:BaseEntity
    {
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
