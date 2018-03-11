using IncidentesBE;
using IncidentesBL;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IncidentesWEB.Indicadores
{
    public partial class frmReporteCartaAll : System.Web.UI.Page
    {
        Fnc_FuncionariosBL _Fnc_FuncionariosBL = new Fnc_FuncionariosBL();
        TB_DepartamentoBL _TB_DepartamentoBL = new TB_DepartamentoBL();
        List<TB_DepartamentoBE> lTTB_DepartamentoBE;
        string _Clasificacion, _Anio;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                llenarComboLider();
                LlenarComboDepartamentoAll();
                DateTime Hoy = DateTime.Today;
                if (Hoy.AddMonths(-6).Month > 6)
                {
                    ddlTipo.SelectedValue = "2";
                }
                else
                {
                    ddlTipo.SelectedValue = "1";
                }
            }
        }

        private void mostrarReporte(string Clasificacion, string Anio, string _Lider, string _Departamento)
        {
            ReportViewer1.Reset();

            DataTable dt = GetData(Clasificacion, Anio, _Lider, _Departamento);
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            ReportViewer1.LocalReport.DataSources.Add(rds);
            ReportViewer1.LocalReport.ReportPath = "Indicadores\\Reportes\\rptCartaAll.rdlc";

            //ReportParameter[] rptParam = new ReportParameter[] {
            //   new ReportParameter("Evaluacion_id", _Evaluacion_id.ToString()) 
            // };
            ReportViewer1.LocalReport.Refresh();
        }
        private DataTable GetData(string Clasificacion, string Anio, string _Lider, string _Departamento)
        {
            DataTable dt = new DataTable();
            string conn = System.Configuration.ConfigurationManager.ConnectionStrings["DB_IndicadoresConnectionString"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(conn))
            {
                SqlCommand cmd = new SqlCommand("sp_ListarEVA_EvaluacionByCarta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Anio", SqlDbType.VarChar, 7).Value = Anio;
                cmd.Parameters.Add("@lider_id", SqlDbType.VarChar, 5).Value = _Lider;
                cmd.Parameters.Add("@FUNCIONARIO_NOME", SqlDbType.VarChar, 80).Value = "";
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

            }
            return dt;
        }

        private void llenarComboLider()
        {
            List<Fnc_FuncionariosBE> ltFuncionariosLider;
            ltFuncionariosLider = _Fnc_FuncionariosBL.ListarFNC_FuncionariosLideresO_Act();
            Fnc_FuncionariosBE _Fnc_FuncionariosBE = new Fnc_FuncionariosBE();
            _Fnc_FuncionariosBE = (Fnc_FuncionariosBE)Session["Fnc_Funcionarios"];
            ddlLider.DataSource = ltFuncionariosLider;
            ddlLider.DataValueField = "FUNCIONARIO_ID";
            ddlLider.DataTextField = "FUNCIONARIO_NOME";
            ddlLider.DataBind();
            ddlLider.Items.Insert(0, new ListItem("(Todos)", "%%"));
        }
        private void LlenarComboDepartamentoAll()
        {
            lTTB_DepartamentoBE = _TB_DepartamentoBL.ListarTB_Departamento_All("%%");
            ddlDepartamento.DataSource = lTTB_DepartamentoBE;
            ddlDepartamento.DataValueField = "Departamento_id";
            ddlDepartamento.DataTextField = "Departamento_desc";
            ddlDepartamento.DataBind();
            ddlDepartamento.Items.Insert(0, new ListItem("(Todos)", "%%"));
        }

        protected void btnExportar_Click(object sender, EventArgs e)
        {
            string _anio, _lider, _Clasificacion;
            _anio = ddlAnio.SelectedValue + "-" + ddlTipo.SelectedValue;
            _lider = ddlLider.SelectedValue;
            _Clasificacion = ddClasificacion.SelectedValue;
            mostrarReporte(_Clasificacion, _anio, ddlLider.SelectedValue, ddlDepartamento.SelectedValue);

        }
    }
}