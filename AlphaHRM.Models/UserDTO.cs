using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.Models
{
    public class UserDTO
    {
        public Guid ID { get;  }
        public string Name { get;  }
        public int Phone { get;  }
        public string Email { get;  }
        public string Job { get;  }
        public int Type { get;  }
        public string Password { get; }
        public Guid ManagerId { get; }
    }
}
