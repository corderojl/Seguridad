using IncidentesADO;
using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IncidentesBL
{
    public class LUP_CategoriaBL
    {
        LUP_CategoriaADO _LUP_CategoriaADO = new LUP_CategoriaADO();

        public List<LUP_CategoriaBE> ListarLUP_CategoriaO_Act()
        {
            return _LUP_CategoriaADO.ListarLUP_CategoriaO_Act();
        }
    }
}
