using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarisTutakli.Week4.WebApi.Models
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime? MinDate { get; set; } = new DateTime(2000, 01, 01);
        public DateTime? MaxDate { get; set; } = DateTime.Now;
        public string? ProductName { get; set; }

        public string Sort { get; set; } = "asc";
        public string? SortByName { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 10 ? 10 : pageSize;
        }

    }
}
