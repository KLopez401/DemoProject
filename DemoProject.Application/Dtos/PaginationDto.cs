using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Application.Dtos
{
    /// <summary>
    /// pagination query that is input by the user
    /// </summary>
    public class PaginationDto
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize { get; set; } = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}
