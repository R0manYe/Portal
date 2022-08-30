using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models.EmOpov
{
    public class EmOpov
    {
        [Key]
        public int ID { get; set; }
        public string ID_SPR { get; set; }
        public string NAIM { get; set; }
        public string ID_SP { get; set; }
        public string NAIM_FULL { get; set; }

        public virtual Station_ppgt Stations { get; set; }
        
    }
    public class Station_ppgt
    {
        [Key]
        public string ID_SP { get; set; }
        public String NAIM_FULL { get; set; }
        public virtual ICollection<EmOpov> EmOpovs { get; set; }
    }
}
