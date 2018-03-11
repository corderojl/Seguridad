using IncidentesADO;
using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IncidentesBL
{
    public class TB_ResponsablePilarBL
    {
        TB_ResponsablePilarADO _TB_ResponsablePilarADO = new TB_ResponsablePilarADO();

        public DataTable ListarTB_ResponsablePilarByPilar(short Pilar_id)
        {
            return _TB_ResponsablePilarADO.ListarTB_ResponsablePilarByPilar(Pilar_id);
        }
        public List<TB_ResponsablePilarBE> ListarTB_ResponsablePilarO_Act(short Pilar_id)
        {
            return _TB_ResponsablePilarADO.ListarTB_ResponsablePilarO_Act();
        }
    }
}
