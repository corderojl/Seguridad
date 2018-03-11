using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IncidentesADO
{
    public class TB_ResponsablePilarADO
    {
        ConexionADO MiConexion = new ConexionADO();
        SqlConnection cnx = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataView dtv = new DataView();

        public DataTable ListarTB_ResponsablePilarByPilar(short Pilar_id)
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_ResponsablePilarByPilar";
                SqlParameter par1 = cmd.Parameters.Add("@Pilar_id", SqlDbType.SmallInt);
                par1.Direction = ParameterDirection.Input;
                par1.Value = Pilar_id;
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

        public List<TB_ResponsablePilarBE> ListarTB_ResponsablePilarO_Act()
        {
            string conexion = MiConexion.GetCnx();
            List<TB_ResponsablePilarBE> lTB_ResponsablePilarBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarTB_ResponsablePilar_Act", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                lTB_ResponsablePilarBE = new List<TB_ResponsablePilarBE>();
                int posPilar_id = drd.GetOrdinal("Pilar_id");
                int posFuncionario_id = drd.GetOrdinal("Funcionario_id");
                TB_ResponsablePilarBE obeResponsablePilarBE = null;
                while (drd.Read())
                {
                    obeResponsablePilarBE = new TB_ResponsablePilarBE();
                    obeResponsablePilarBE.Pilar_id = drd.GetInt16(posPilar_id);
                    obeResponsablePilarBE.Funcionario_id = drd.GetInt16(posFuncionario_id);
                    lTB_ResponsablePilarBE.Add(obeResponsablePilarBE);
                }
                drd.Close();
            }
            con.Close();
            return (lTB_ResponsablePilarBE);
        }

    }
}
