using IncidentesADO;
using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IncidentesBL
{
    public class TB_RiesgoIvolucradoBL
    {
        TB_RiesgoInvolucradoADO _TB_RiesgoInvolucradoADO = new TB_RiesgoInvolucradoADO();

        public DataTable ListarTB_RiesgoInvolucrado_All()
        {
            return _TB_RiesgoInvolucradoADO.ListarTB_RiesgoInvolucrado_All();
        }
        public DataTable ListarTB_RiesgoInvolucrado_Act()
        {
            return _TB_RiesgoInvolucradoADO.ListarTB_RiesgoInvolucrado_Act();
        }
        public List<TB_RiesgoInvolucradoBE> ListarTB_RiesgoInvolucradoO_Act()
        {
            return _TB_RiesgoInvolucradoADO.ListarTB_RiesgoInvolucradoO_Act();
        }
    }
}
