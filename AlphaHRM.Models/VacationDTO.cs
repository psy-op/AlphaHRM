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
        public Guid ID { get; set; }
        public int Type { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public string? Note { get; set; }
        public bool IsDraft { get; set; }
        [ForeignKey("UsersDTO")]
        public Guid UserID { get; set; }
    }
}
