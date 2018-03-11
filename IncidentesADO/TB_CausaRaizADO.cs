using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IncidentesADO
{
    public class TB_CausaRaizADO
    {
        ConexionADO MiConexion = new ConexionADO();
        SqlConnection cnx = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataView dtv = new DataView();

        public DataTable ListarTB_CausaRaiz_All()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_CausaRaiz_All";
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
        public List<TB_CausaRaizBE> ListarTB_CausaRaizO_Act()
        {
            string conexion = MiConexion.GetCnx();
            List<TB_CausaRaizBE> lTB_CausaRaizBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarTB_CausaRaizBE_Act", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                lTB_CausaRaizBE = new List<TB_CausaRaizBE>();
                int posCausaRaiz_id = drd.GetOrdinal("CausaRaiz_id");
                int posCausaRaiz_desc = drd.GetOrdinal("CausaRaiz_desc");
                TB_CausaRaizBE obeCausaRaizBE = null;
                while (drd.Read())
                {
                    obeCausaRaizBE = new TB_CausaRaizBE();
                    obeCausaRaizBE.CausaRaiz_id = drd.GetInt16(posCausaRaiz_id);
                    obeCausaRaizBE.CausaRaiz_desc = drd.GetString(posCausaRaiz_desc);
                    lTB_CausaRaizBE.Add(obeCausaRaizBE);
                }
                drd.Close();
            }
            con.Close();
            return (lTB_CausaRaizBE);
        }
        public DataTable ListarTB_CausaRaiz_Act()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_CausaRaiz_Act";
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
