using IncidentesBE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace IncidentesADO
{
    public class TB_DepartamentoADO
    {
        ConexionADO MiConexion = new ConexionADO();
        SqlConnection cnx = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataView dtv = new DataView();
        //public List<TB_DepartamentoBE> ListarTB_Departamento_All()
        //{
        //    string conexion = MiConexion.GetCnx();
        //    List<TB_DepartamentoBE> lTB_DepartamentoBE = null;
        //    SqlConnection con = new SqlConnection(conexion);
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("sp_ListarTB_Departamento_All", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

        //    if (drd != null)
        //    {
        //        lTB_DepartamentoBE = new List<TB_DepartamentoBE>();
        //        int posDepartamento_id = drd.GetOrdinal("Departamento_id");
        //        int posDepartamento_desc = drd.GetOrdinal("Departamento_desc");
        //        TB_DepartamentoBE obeDepartamentoBE = null;
        //        while (drd.Read())
        //        {
        //            obeDepartamentoBE = new TB_DepartamentoBE();
        //            obeDepartamentoBE.Departamento_id = drd.GetInt16(posDepartamento_id);
        //            obeDepartamentoBE.Departamento_desc = drd.GetString(posDepartamento_desc);
        //            lTB_DepartamentoBE.Add(obeDepartamentoBE);
        //        }
        //        drd.Close();

        //    }
        //    con.Close();
        //    return (lTB_DepartamentoBE);
        //}

        public List<TB_DepartamentoBE> ListarTB_DepartamentoO_Act(string Sistema_id)
        {
            string conexion = MiConexion.GetCnx();
            List<TB_DepartamentoBE> lTB_DepartamentoBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarTB_Departamento_Act", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter par1 = cmd.Parameters.Add("@Sistema_id", SqlDbType.VarChar,8);
            par1.Direction = ParameterDirection.Input;
            par1.Value = Sistema_id;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            
            if (drd != null)
            {
                lTB_DepartamentoBE = new List<TB_DepartamentoBE>();
                int posDepartamento_id = drd.GetOrdinal("Departamento_id");
                int posDepartamento_desc = drd.GetOrdinal("Departamento_desc");
                TB_DepartamentoBE obeDepartamentoBE = null;
                while (drd.Read())
                {
                    obeDepartamentoBE = new TB_DepartamentoBE();
                    obeDepartamentoBE.Departamento_id = drd.GetInt16(posDepartamento_id);
                    obeDepartamentoBE.Departamento_desc = drd.GetString(posDepartamento_desc);
                    lTB_DepartamentoBE.Add(obeDepartamentoBE);
                }
                drd.Close();

            }
            con.Close();
            return (lTB_DepartamentoBE);
        }
        public DataTable ListarTB_Departamento_Act()
        {
            DataSet dts = new DataSet();
            try
            {
                cnx.ConnectionString = MiConexion.GetCnx();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_ListarTB_Departamento_Act";
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

        public List<TB_DepartamentoBE> ListarTB_DepartamentoSuperiorO_Act(short _Superior)
        {
            string conexion = MiConexion.GetCnx();
            List<TB_DepartamentoBE> lTB_DepartamentoBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarTB_DepartamentoSuperior_Act", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter par1 = cmd.Parameters.Add("@Superior", SqlDbType.SmallInt);
            par1.Direction = ParameterDirection.Input;
            par1.Value = _Superior;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);

            if (drd != null)
            {
                lTB_DepartamentoBE = new List<TB_DepartamentoBE>();
                int posDepartamento_id = drd.GetOrdinal("Departamento_id");
                int posDepartamento_desc = drd.GetOrdinal("Departamento_desc");
                TB_DepartamentoBE obeDepartamentoBE = null;
                while (drd.Read())
                {
                    obeDepartamentoBE = new TB_DepartamentoBE();
                    obeDepartamentoBE.Departamento_id = drd.GetInt16(posDepartamento_id);
                    obeDepartamentoBE.Departamento_desc = drd.GetString(posDepartamento_desc);
                    lTB_DepartamentoBE.Add(obeDepartamentoBE);
                }
                drd.Close();

            }
            con.Close();
            return (lTB_DepartamentoBE);
        }

        public List<TB_DepartamentoBE> ListarTB_Departamento_All(string Sistema_id)
        {
            string conexion = MiConexion.GetCnx();
            List<TB_DepartamentoBE> lTB_DepartamentoBE = null;
            SqlConnection con = new SqlConnection(conexion);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_ListarTB_Departamento_All", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter par1 = cmd.Parameters.Add("@Sistema_id", SqlDbType.VarChar, 8);
            par1.Direction = ParameterDirection.Input;
            par1.Value = Sistema_id;
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
          
            if (drd != null)
            {
                lTB_DepartamentoBE = new List<TB_DepartamentoBE>();
                int posDepartamento_id = drd.GetOrdinal("Departamento_id");
                int posDepartamento_desc = drd.GetOrdinal("Departamento_desc");
                TB_DepartamentoBE obeDepartamentoBE = null;
                while (drd.Read())
                {
                    obeDepartamentoBE = new TB_DepartamentoBE();
                    obeDepartamentoBE.Departamento_id = drd.GetInt16(posDepartamento_id);
                    obeDepartamentoBE.Departamento_desc = drd.GetString(posDepartamento_desc);
                    lTB_DepartamentoBE.Add(obeDepartamentoBE);
                }
                drd.Close();

            }
            con.Close();
            return (lTB_DepartamentoBE);
        }
    }
}
