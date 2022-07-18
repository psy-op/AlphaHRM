using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.Models
{

    public class PagingRequest
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }


    public class GetUsersRequest:PagingRequest
    {
        public int ManagerId { get; set; }
    }

    public class GetVacationRequest : PagingRequest
    {
        public int UserId { get; set; }
    }

}
