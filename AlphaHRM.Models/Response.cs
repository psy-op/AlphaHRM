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
        public List<VacationDTO> vacationlist { get; set; }
        public List<UserDTO> userlist { get; set; }

        public T Data { get; set; }
        public ErrorCodes Errocode { get; set; } = 0;
        public string Description { get; set; }


        public Response(ErrorCodes errocode, string description)        {
            Errocode = errocode;
            Description = description;
        }

        public Response(T data)        {
            Data = data;
        }

        public Response(List<UserDTO> data)
        {
            userlist = data;
        }
        public Response(List<VacationDTO> data)
        {
            vacationlist = data;
        }
    }
}