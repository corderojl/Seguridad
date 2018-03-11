using IncidentesBE;
using IncidentesBL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IncidentesWEB.Comportamiento
{
    public partial class registrarFormato : System.Web.UI.Page
    {
        string fecha_actual;
        protected DataView dvLocaliza;
        protected DataView dvComponentes;
        protected DataView dvTipos;
        protected DataView dvEncontrado;
        protected DataView dvResuelto;
        protected DataView dvCategoria;
        protected DataView dvDispositivos;
        protected DataView dvPartes;
        protected DataView dvGrupos;
        //datatable
        protected DataTable dtFuncionario;
        protected DataTable dtComponente;
        protected DataTable dtPartes;
        protected DataTable dtDueno;

        TB_DepartamentoBL _TB_DepartamentoBL = new TB_DepartamentoBL();
        TB_GuardiaBL _TB_GuardiaBL = new TB_GuardiaBL();
        Fnc_FuncionariosBL _Fnc_FuncionariosBL = new Fnc_FuncionariosBL();
        COM_FormatosBL _COM_FormatosBL = new COM_FormatosBL();
        COM_CategoriasBL _COM_CategoriasBL = new COM_CategoriasBL();
        COM_CategoriasBE _COM_CategoriasBE = new COM_CategoriasBE();


        protected DataView dvEstatusOperacional;
        List<TB_DepartamentoBE> lTTB_DepartamentoBE;

        List<COM_FormatosBE> lTCOM_FormatosBE;
        List<COM_CategoriasBE> lTCOM_CategoriasBE;
        private List<COM_FormatosBE> lbeFiltroFormato;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.IsPostBack)
                {

                }
                else
                {

                    //Session["FUNCIONARIO_ID"] = "71046";
                    DateTime Hoy = DateTime.Today;
                    fecha_actual = Hoy.ToString("dd/MM/yyyy");



                    lTCOM_FormatosBE = _COM_FormatosBL.ListarCOM_FormatosO_Act();

                    LlenarComboDepartamento();
                    Session["Formatos"] = lTCOM_FormatosBE;
                    LlenarComboFormato();
                }

            }
            catch (Exception x)
            {
                // Response.Redirect("error.aspx");
            }
        }
        private void LlenarComboDepartamento()
        {
            lTTB_DepartamentoBE = _TB_DepartamentoBL.ListarTB_Departamento_All("1");
            ddlDepartamento.DataSource = lTTB_DepartamentoBE;
            ddlDepartamento.DataValueField = "Departamento_id";
            ddlDepartamento.DataTextField = "Departamento_desc";
            ddlDepartamento.SelectedValue = ((Fnc_FuncionariosBE)Session["Fnc_Funcionarios"]).Area_Id.ToString();
            ddlDepartamento.DataBind();
            ddlDepartamento.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));
        }
        private void LlenarComboFormato()
        {
            lTCOM_FormatosBE = (List<COM_FormatosBE>)Session["Formatos"];
            Predicate<COM_FormatosBE> pred = new Predicate<COM_FormatosBE>(buscarFormato);
            lbeFiltroFormato = lTCOM_FormatosBE.FindAll(pred);

            ddlFormato.DataSource = lbeFiltroFormato;
            ddlFormato.DataValueField = "Formato_id";
            ddlFormato.DataTextField = "Formato_desc";
            ddlFormato.DataBind();
            ddlFormato.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));
            //LlenarComboCategoria();
        }
        private bool buscarFormato(COM_FormatosBE _COM_FormatosBE)
        {
            bool exitoIdFormato = true;
            if (!ddlDepartamento.SelectedValue.Equals("0")) exitoIdFormato = (_COM_FormatosBE.Departamento_id.ToString().Equals(ddlDepartamento.SelectedValue));
            return (exitoIdFormato);
        }

        private void GenerarTabla(Int16 _Formato_id)
        {

            lTCOM_CategoriasBE = _COM_CategoriasBL.ListarCOM_CategoriasByFormato(_Formato_id);
            rpEmpleado.DataSource = lTCOM_CategoriasBE;
            rpEmpleado.DataBind();
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboFormato();
        }

        protected void ibnActualizar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibn = (ImageButton)sender;
            RepeaterItem fila = (RepeaterItem)ibn.Parent;
            Int16 _Categoria_id = Int16.Parse(((Label)fila.Controls[1]).Text);
            var _miObj = _COM_CategoriasBE;
            //_miempl.Emp_id = "";
            _miObj.Categoria_desc = ((TextBox)fila.Controls[3]).Text;
            _miObj.Categoria_id = Int16.Parse(((Label)fila.Controls[1]).Text);
            bool obeRespuesta = _COM_CategoriasBL.ActualizarCOM_Categoria(_COM_CategoriasBE);
            if (!obeRespuesta)
            {
                String mensaje = "<script language='JavaScript'>window.alert('error, no se pudo eliminar el registro')";
                mensaje += Environment.NewLine;
                this.Page.Response.Write(mensaje);
            }
            else
            {
            }
           // GenerarTabla(Convert.ToInt16(Request.QueryString["Categoria_id"]));
        }

        protected void ibnEliminar_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ddlFormato_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarTabla(Int16.Parse(ddlFormato.SelectedValue));
        }
    }
}