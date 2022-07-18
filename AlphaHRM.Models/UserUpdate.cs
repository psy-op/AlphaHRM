using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaHRM.Models.Enums;

namespace AlphaHRM.Models
{
    public class UserUpdate
    {
        public string Name { get; set; }
        public int Phone { get; set;  }
        public string Email { get; set;  }
        public string Job { get; set;  }
        public Guid? ManagerId { get; set; }
    }
}
