using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IncidentesADO
{
    public class TB_EstatusOperacionalADO
    {
        ConexionADO MiConexion = new ConexionADO();
        SqlConnection cnx = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataView dtv = new DataView();

        public DataTable ListarTB_EstatusOperacional_All()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_EstatusOperacional_All";
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
        public List<TB_EstatusOperacionalBE> ListarTB_EstatusOperacionalO_Act()
        {
            string conexion = MiConexion.GetCnx();
            List<TB_EstatusOperacionalBE> lTB_EstatusOperacionalBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarTB_EstatusOperacional_Act", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                lTB_EstatusOperacionalBE = new List<TB_EstatusOperacionalBE>();
                int posEstatusOperacional_id = drd.GetOrdinal("EstatusOperacional_id");
                int posEstatusOperacional_desc = drd.GetOrdinal("EstatusOperacional_desc");
                TB_EstatusOperacionalBE obeEstatusOperacionalBE = null;
                while (drd.Read())
                {
                    obeEstatusOperacionalBE = new TB_EstatusOperacionalBE();
                    obeEstatusOperacionalBE.EstatusOperacional_id = drd.GetInt16(posEstatusOperacional_id);
                    obeEstatusOperacionalBE.EstatusOperacional_desc = drd.GetString(posEstatusOperacional_desc);
                    lTB_EstatusOperacionalBE.Add(obeEstatusOperacionalBE);
                }
                drd.Close();
            }
            con.Close();
            return (lTB_EstatusOperacionalBE);
        }
        public DataTable ListarTB_EstatusOperacional_Act()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_EstatusOperacional_Act";
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
