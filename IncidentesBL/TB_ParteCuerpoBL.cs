using IncidentesADO;
using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IncidentesBL
{
    public class TB_ParteCuerpoBL
    {
        TB_ParteCuerpoADO _TB_ParteCuerpoADO = new TB_ParteCuerpoADO();

        public DataTable ListarTB_ParteCuerpo_All()
        {
            return _TB_ParteCuerpoADO.ListarTB_ParteCuerpo_All();
        }
        public DataTable ListarTB_ParteCuerpo_Act()
        {
            return _TB_ParteCuerpoADO.ListarTB_ParteCuerpo_Act();
        }
        public List<TB_ParteCuerpoBE> ListarTB_ParteCuerpoO_Act()
        {
            return _TB_ParteCuerpoADO.ListarTB_ParteCuerpoO_Act();
        }
    }
}
