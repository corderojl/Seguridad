using IncidentesADO;
using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IncidentesBL
{
    public class TB_CondicionInvolucradaBL
    {
        TB_CondicionInvolucradaADO _TB_CondicionInvolucradaADO = new TB_CondicionInvolucradaADO();

        public DataTable ListarTB_CondicionInvolucrada_All()
        {
            return _TB_CondicionInvolucradaADO.ListarTB_CondicionInvolucrada_All();
        }
        public DataTable ListarTB_CondicionInvolucrada_Act()
        {
            return _TB_CondicionInvolucradaADO.ListarTB_CondicionInvolucrada_Act();
        }
        public List<TB_CondicionInvolucradaBE> ListarTB_CondicionInvolucradaO_Act()
        {
            return _TB_CondicionInvolucradaADO.ListarTB_CondicionInvolucradaO_Act();
        }
    }
}
