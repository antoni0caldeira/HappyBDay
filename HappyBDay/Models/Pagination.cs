using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HappyBDay.Models
{
    public class Pagination
    {
        public const int DEFAULT_PAGE_SIZE = 10; 
        
        public const int NUMBER_PAGES_SHOW_BEFORE_AFTER = 3;
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; } = DEFAULT_PAGE_SIZE;

        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
    }
}
