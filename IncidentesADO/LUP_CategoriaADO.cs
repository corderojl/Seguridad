using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IncidentesADO
{
    public class LUP_CategoriaADO
    {
        ConexionADO MiConexion = new ConexionADO();
        SqlConnection cnx = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataView dtv = new DataView();

        public List<LUP_CategoriaBE> ListarLUP_CategoriaO_Act()
        {
            string conexion = MiConexion.GetCnx();
            List<LUP_CategoriaBE> lLUP_CategoriaBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarLUP_Categoria_Act", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                lLUP_CategoriaBE = new List<LUP_CategoriaBE>();
                int posCategoria_id = drd.GetOrdinal("Categoria_id");
                int posCategoria_desc = drd.GetOrdinal("Categoria_desc");
                int posPilar_id = drd.GetOrdinal("Pilar_id");
                LUP_CategoriaBE obeCategoriaBE = null;
                while (drd.Read())
                {
                    obeCategoriaBE = new LUP_CategoriaBE();
                    obeCategoriaBE.Categoria_id = drd.GetInt16(posCategoria_id);
                    obeCategoriaBE.Categoria_desc = drd.GetString(posCategoria_desc);
                    obeCategoriaBE.Pilar_id = drd.GetInt16(posPilar_id);
                    lLUP_CategoriaBE.Add(obeCategoriaBE);
                }
                drd.Close();
            }
            con.Close();
            return (lLUP_CategoriaBE);
        }
    }
}
