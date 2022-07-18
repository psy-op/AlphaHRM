using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AlphaHRM.Models.Enums;

namespace AlphaHRM.Models
{
    public class VacationUpdate
    {
        public Guid ID { get; }
        public VacationType Type { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }
        public VacationStatus Status { get; set; }
        public string? Note { get; set; }
        public Drafted IsDraft { get; set; }
    }
}
