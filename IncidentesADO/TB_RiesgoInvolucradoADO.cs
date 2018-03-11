using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IncidentesADO
{
    public class TB_RiesgoInvolucradoADO
    {
        ConexionADO MiConexion = new ConexionADO();
        SqlConnection cnx = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataView dtv = new DataView();

        public DataTable ListarTB_RiesgoInvolucrado_All()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_RiesgoInvolucrado_All";
                SqlDataAdapter miada = default(SqlDataAdapter);
                miada = new SqlDataAdapter(cmd);
                miada.Fill(dts, "Sistemas");
                dtv = dts.Tables["Sistemas"].DefaultView;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                }
                cmd.Parameters.Clear();
            }
            return dts.Tables["Sistemas"];
        }
        public List<TB_RiesgoInvolucradoBE> ListarTB_RiesgoInvolucradoO_Act()
        {
            string conexion = MiConexion.GetCnx();
            List<TB_RiesgoInvolucradoBE> lTB_RiesgoInvolucradoBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarTB_RiesgoInvolucrado_Act", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                lTB_RiesgoInvolucradoBE = new List<TB_RiesgoInvolucradoBE>();
                int posRiesgo_inv_id = drd.GetOrdinal("Riesgo_inv_id");
                int posRiesgo_inv_desc = drd.GetOrdinal("Riesgo_inv_desc");
                TB_RiesgoInvolucradoBE obeRiesgoInvolucradoBE = null;
                while (drd.Read())
                {
                    obeRiesgoInvolucradoBE = new TB_RiesgoInvolucradoBE();
                    obeRiesgoInvolucradoBE.Riesgo_inv_id = drd.GetInt16(posRiesgo_inv_id);
                    obeRiesgoInvolucradoBE.Riesgo_inv_desc = drd.GetString(posRiesgo_inv_desc);
                    lTB_RiesgoInvolucradoBE.Add(obeRiesgoInvolucradoBE);
                }
                drd.Close();
            }
            con.Close();
            return (lTB_RiesgoInvolucradoBE);
        }
        public DataTable ListarTB_RiesgoInvolucrado_Act()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_RiesgoInvolucrado_Act";
                SqlDataAdapter miada = default(SqlDataAdapter);
                miada = new SqlDataAdapter(cmd);
                miada.Fill(dts, "Sistemas");
                dtv = dts.Tables["Sistemas"].DefaultView;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                }
                cmd.Parameters.Clear();
            }
            return dts.Tables["Sistemas"];
        }
    }
}
