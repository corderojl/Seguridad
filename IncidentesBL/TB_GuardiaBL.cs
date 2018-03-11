using IncidentesADO;
using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IncidentesBL
{
    public class TB_GuardiaBL
    {
        TB_GuardiaADO _TB_GuardiaADO = new TB_GuardiaADO();

        public DataTable ListarTB_Guardia_All()
        {
            return _TB_GuardiaADO.ListarTB_Guardia_All();
        }
        public DataTable ListarTB_Guardia_Act()
        {
            return _TB_GuardiaADO.ListarTB_Guardia_Act();
        }
        public List<TB_GuardiaBE> ListarTB_GuardiaO_Act()
        {
            return _TB_GuardiaADO.ListarTB_GuardiaO_Act();
        }
        public DataTable ConsultarGuardiaId(int vIdGrupo)
        {
            return _TB_GuardiaADO.ConsultarFunc_Grupo_Id(vIdGrupo);
        }
    }
}
