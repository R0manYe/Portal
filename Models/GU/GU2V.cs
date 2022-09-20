using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Models.GU
{
    public class GU2V
    {
        public int ID { get; set; }
        public string NUM { get; set; }        
        public string ST_NAIM { get; set; }
        public string PL_DAT { get; set; }
        public string PL_TIME { get; set; }
        public string COL_W { get; set; }       
        public string COL_W_F { get; set; }
    }
    public class LOGI
    {
        public string DATA { get; set; }
        public string SPR { get; set; }
        public string ID_ST { get; set; }
        public string DOC { get; set; }
        public string ID_USER { get; set; }
        public string ID_DOC_COMPLEX { get; set; }
        public string ID_DOC_ETRAN { get; set; }
        public string DATA_ETRAN_INS { get; set; }
        public string EMAIL { get; set; }

    }
    public class Dislokacia
    {
        public int ID { get; set; }
        public string NOM_VAG { get; set; }
        public string NAIM_ROD_VAG { get; set; }
        public string NAIM_STAN_NAZN { get; set; }
        public string GRUZPOL_OKPO { get; set; }
        public string NAIM_GRUZPOL_OKPO { get; set; }
        public string NAIM_GRUZOTPR_OKPO { get; set; }
        public string NAIM_KOD_GRZ { get; set; }
        public string VES_GRZ { get; set; }
        public string DATE_OP { get; set; }
        public string NAIM_STAN_OP { get; set; }
        public string NAIM_KOP_VMD { get; set; }
        public string INDEX_POEZD { get; set; }
        public string NOM_POEZD { get; set; }
        public string NPP_VAG { get; set; }
        public string DATE_DOSTAV { get; set; }
        public string RASST_STAN_NAZN { get; set; }
        public string DOR { get; set; }

    }
}
