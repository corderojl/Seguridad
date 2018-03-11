using IncidentesADO;
using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IncidentesBL
{
    public class TB_CausaRaizBL
    {
        TB_CausaRaizADO _TB_CausaRaizADO = new TB_CausaRaizADO();

        public DataTable ListarTB_CausaRaiz_All()
        {
            return _TB_CausaRaizADO.ListarTB_CausaRaiz_All();
        }
        public DataTable ListarTB_CausaRaiz_Act()
        {
            return _TB_CausaRaizADO.ListarTB_CausaRaiz_Act();
        }
        public List<TB_CausaRaizBE> ListarTB_CausaRaizO_Act()
        {
            return _TB_CausaRaizADO.ListarTB_CausaRaizO_Act();
        }
    }
}
