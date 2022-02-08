using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Business.ViewModels
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
