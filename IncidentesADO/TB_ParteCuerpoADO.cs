using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IncidentesADO
{
    public class TB_ParteCuerpoADO
    {
        ConexionADO MiConexion = new ConexionADO();
        SqlConnection cnx = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataView dtv = new DataView();

        public DataTable ListarTB_ParteCuerpo_All()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_ParteCuerpo_All";
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
        public List<TB_ParteCuerpoBE> ListarTB_ParteCuerpoO_Act()
        {
            string conexion = MiConexion.GetCnx();
            List<TB_ParteCuerpoBE> lTB_ParteCuerpoBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarTB_ParteCuerpo_Act", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                lTB_ParteCuerpoBE = new List<TB_ParteCuerpoBE>();
                int posParteCuerpo_id = drd.GetOrdinal("ParteCuerpo_id");
                int posParteCuerpo_desc = drd.GetOrdinal("ParteCuerpo_desc");
                TB_ParteCuerpoBE obeParteCuerpoBE = null;
                while (drd.Read())
                {
                    obeParteCuerpoBE = new TB_ParteCuerpoBE();
                    obeParteCuerpoBE.ParteCuerpo_id = drd.GetInt16(posParteCuerpo_id);
                    obeParteCuerpoBE.ParteCuerpo_desc = drd.GetString(posParteCuerpo_desc);
                    lTB_ParteCuerpoBE.Add(obeParteCuerpoBE);
                }
                drd.Close();
            }
            con.Close();
            return (lTB_ParteCuerpoBE);
        }
        public DataTable ListarTB_ParteCuerpo_Act()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_ParteCuerpo_Act";
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
