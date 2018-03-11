using IncidentesADO;
using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IncidentesBL
{
    public class TB_CausaInmediataBL
    {
        TB_CausaInmediataADO _TB_CausaInmediataADO = new TB_CausaInmediataADO();

        public DataTable ListarTB_CausaInmediata_All()
        {
            return _TB_CausaInmediataADO.ListarTB_CausaInmediata_All();
        }
        public DataTable ListarTB_CausaInmediata_Act()
        {
            return _TB_CausaInmediataADO.ListarTB_CausaInmediata_Act();
        }
        public List<TB_CausaInmediataBE> ListarTB_CausaInmediataO_Act()
        {
            return _TB_CausaInmediataADO.ListarTB_CausaInmediataO_Act();
        }
    }
}
