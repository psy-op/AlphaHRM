using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaHRM.Models.Enums;

namespace AlphaHRM.Models
{
    public class Response<T>
    {


        public T Data { get; set; }
        public ErrorCodes ErrorCode { get; set; } = 0;
        public string Description { get; set; }
        public int TotalCount { get; set; }


        public Response(ErrorCodes errorcode, string description)
        {
            ErrorCode = errorcode;
            Description = description;
        }

        public Response(T data)
        {
            Data = data;
        }


    }

    public class PagedResponse<T> : Response<List<T>>
    {

        public int TotalCount { get; set; }

        public PagedResponse(ErrorCodes errorcode, string description) : base(errorcode, description)
        {
        }

        public PagedResponse(List<T> data, int totalCount) : base(data)
        {
            TotalCount = totalCount;
        }


    }

}
    
