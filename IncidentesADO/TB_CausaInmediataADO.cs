using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using IncidentesBE;

namespace IncidentesADO
{
    public class TB_CausaInmediataADO
    {
        ConexionADO MiConexion = new ConexionADO();
        SqlConnection cnx = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataView dtv = new DataView();

        public DataTable ListarTB_CausaInmediata_All()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_CausaInmediata_All";
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
        public List<TB_CausaInmediataBE> ListarTB_CausaInmediataO_Act()
        {
            string conexion = MiConexion.GetCnx();
            List<TB_CausaInmediataBE> lTB_CausaInmediataBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarTB_CausaInmediata_Act", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                lTB_CausaInmediataBE = new List<TB_CausaInmediataBE>();
                int posCausainmediata_id = drd.GetOrdinal("Causainmediata_id");
                int posCausainmediata_desc = drd.GetOrdinal("Causainmediata_desc");
                TB_CausaInmediataBE obeCausaInmediataBE = null;
                while (drd.Read())
                {
                    obeCausaInmediataBE = new TB_CausaInmediataBE();
                    obeCausaInmediataBE.Causainmediata_id = drd.GetInt16(posCausainmediata_id);
                    obeCausaInmediataBE.Causainmediata_desc = drd.GetString(posCausainmediata_desc);
                    lTB_CausaInmediataBE.Add(obeCausaInmediataBE);
                }
                drd.Close();
            }
            con.Close();
            return (lTB_CausaInmediataBE);
        }
        public DataTable ListarTB_CausaInmediata_Act()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_CausaInmediata_Act";
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
