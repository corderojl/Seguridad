﻿using IncidentesADO;
using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace IncidentesBL
{
    public class TB_PilarBL
    {
        TB_PilarADO _TB_PilarADO = new TB_PilarADO();

        public List<TB_PilarBE> ListarTB_Pilar_Act()
        {
            return _TB_PilarADO.ListarTB_PilarO_Act();
        }
    }
}
