using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IncidentesADO
{
    public class TB_PilarADO
    {
        ConexionADO MiConexion = new ConexionADO();
        SqlConnection cnx = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataView dtv = new DataView();

        public List<TB_PilarBE> ListarTB_PilarO_Act()
        {
            string conexion = MiConexion.GetCnx();
            List<TB_PilarBE> lTB_PilarBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarTB_Pilar_Act", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                lTB_PilarBE = new List<TB_PilarBE>();
                int posPilar_id = drd.GetOrdinal("Pilar_id");
                int posPilar_desc = drd.GetOrdinal("Pilar_desc");
                TB_PilarBE obePilarBE = null;
                while (drd.Read())
                {
                    obePilarBE = new TB_PilarBE();
                    obePilarBE.Pilar_id = drd.GetInt16(posPilar_id);
                    obePilarBE.Pilar_desc = drd.GetString(posPilar_desc);
                    lTB_PilarBE.Add(obePilarBE);
                }
                drd.Close();
            }
            con.Close();
            return (lTB_PilarBE);
        }
    }
}
