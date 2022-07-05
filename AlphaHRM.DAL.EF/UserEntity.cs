using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.DAL.EF
{
    public class UserEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string Email { get; set; }
        public string Job { get; set; }
        public int Type { get; set; }
        public DateTime Created { get; set; }
        public string Password { get; set; }
        public Guid? ManagerId { get; set; }
    }
}
