using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaHRM.Models
{
    public class VacationDTO
    {
        public Guid ID { get; }
        public int Type { get; }
        public int Duration { get; }
        public DateTime Date { get;  }
        public int Status { get; }
        public string? Note { get;  }
        public bool IsDraft { get; }
        [ForeignKey("UsersDTO")]
        public Guid UserID { get; }
    }
}
