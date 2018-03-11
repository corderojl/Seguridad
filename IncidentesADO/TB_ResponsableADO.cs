using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IncidentesADO
{
    public class TB_ResponsableADO
    {
        ConexionADO MiConexion = new ConexionADO();
        SqlConnection cnx = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataView dtv = new DataView();

        public DataTable ListarTB_Responsable_All()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_Responsable_All";
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
        public List<TB_ResponsableBE> ListarTB_ResponsableO()
        {
            string conexion = MiConexion.GetCnx();
            List<TB_ResponsableBE> lTB_ResponsableBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarTB_Responsable_All", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                lTB_ResponsableBE = new List<TB_ResponsableBE>();
                int posDepartamento_id = drd.GetOrdinal("Departamento_id");
                int posFuncionario_id = drd.GetOrdinal("Funcionario_id");
                TB_ResponsableBE obeAreaBE = null;
                while (drd.Read())
                {
                    obeAreaBE = new TB_ResponsableBE();
                    obeAreaBE.Departamento_id = drd.GetInt16(posDepartamento_id);
                    obeAreaBE.Funcionario_id = drd.GetInt16(posFuncionario_id);
                    lTB_ResponsableBE.Add(obeAreaBE);
                }
                drd.Close();
            }
            con.Close();
            return (lTB_ResponsableBE);
        }
        public TB_ResponsableBE TraerTB_ResponsableByDepartamento(int _Departamento_id)
        {
            TB_ResponsableBE _TB_ResponsableBE = new TB_ResponsableBE();
            SqlDataReader dtr = default(SqlDataReader);
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_TraerTB_ResponsableByDepartamento";
                cmd.Parameters.Add(new SqlParameter("@Departamento_id", SqlDbType.SmallInt));
                cmd.Parameters["@Departamento_id"].Value = _Departamento_id;
                cnx.Open();
                dtr = cmd.ExecuteReader();
                if (dtr.HasRows == true)
                {
                    dtr.Read();
                    var _with1 = _TB_ResponsableBE;
                    _with1.Departamento_id = Convert.ToInt32(dtr.GetValue(dtr.GetOrdinal("Departamento_id")));
                    _with1.Funcionario_id = Convert.ToInt32(dtr.GetValue(dtr.GetOrdinal("Funcionario_id")));
                    _with1.Funcionario_nome = dtr.GetValue(dtr.GetOrdinal("Funcionario_nome")).ToString();

                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
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
            return (_TB_ResponsableBE);
        }

    }
}
