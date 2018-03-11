using IncidentesADO;
using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IncidentesBL
{
    public class TB_EstatusOperacionalBL
    {
        TB_EstatusOperacionalADO _TB_EstatusOperacionalADO = new TB_EstatusOperacionalADO();

        public DataTable ListarTB_EstatusOperacional_All()
        {
            return _TB_EstatusOperacionalADO.ListarTB_EstatusOperacional_All();
        }
        public DataTable ListarTB_EstatusOperacional_Act()
        {
            return _TB_EstatusOperacionalADO.ListarTB_EstatusOperacional_Act();
        }
        public List<TB_EstatusOperacionalBE> ListarTB_EstatusOperacionalO_Act()
        {
            return _TB_EstatusOperacionalADO.ListarTB_EstatusOperacionalO_Act();
        }
    }
}
