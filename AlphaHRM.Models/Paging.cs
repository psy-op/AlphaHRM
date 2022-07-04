using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.Models
{
    public class Paging
    {
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;


        public Paging(int totalcount, int currentpage, int pagesize)
        {
            TotalCount = totalcount;
            CurrentPage = currentpage;
            TotalPages = (int)Math.Ceiling(totalcount/(double)pagesize);
        }
    }
}
