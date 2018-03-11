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
    public partial class RegistrarComportamiento : System.Web.UI.Page
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

        COM_ComportamientoBE _COM_ComportamientoBE = new COM_ComportamientoBE();

        COM_ComportamientoBL _COM_ComportamientoBL = new COM_ComportamientoBL();
        TB_DepartamentoBL _TB_DepartamentoBL = new TB_DepartamentoBL();
        TB_GuardiaBL _TB_GuardiaBL = new TB_GuardiaBL();
        Fnc_FuncionariosBL _Fnc_FuncionariosBL = new Fnc_FuncionariosBL();
        COM_FormatosBL _COM_FormatosBL = new COM_FormatosBL();
        COM_CategoriasBL _COM_CategoriasBL = new COM_CategoriasBL();
        COM_SubCategoriasBL _COM_SubCategoriasBL = new COM_SubCategoriasBL();
        TB_AreaBL _TB_AreaBL = new TB_AreaBL();


        protected DataView dvEstatusOperacional;
        List<TB_DepartamentoBE> lTTB_DepartamentoBE;
        List<TB_GuardiaBE> lTTB_GuardiaBE;
        List<TB_AreaBE> lTTB_AreaBE;
        List<Fnc_FuncionariosBE> lTTB_Fnc_FuncionariosBE;
        List<COM_FormatosBE> lTCOM_FormatosBE;
        List<COM_CategoriasBE> lTCOM_CategoriasBE;
        List<COM_SubCategoriasBE> lTCOM_SubCategoriasBE;
        private List<TB_GuardiaBE> lbeFiltroGuardia;
        private List<TB_AreaBE> lbeFiltroArea;
        private List<COM_FormatosBE> lbeFiltroFormato;
        private List<COM_CategoriasBE> lbeFiltroCa;
        private List<COM_SubCategoriasBE> lbeFiltroSu;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (this.IsPostBack)
                {
                    
                    
                    GenerarTabla(((Fnc_FuncionariosBE)Session["FNC_Funcionarios"]).Funcionario_Id);
                }
                else
                {

                    //Session["FUNCIONARIO_ID"] = "71046";
                    DateTime Hoy = DateTime.Today;
                    fecha_actual = Hoy.ToString("dd/MM/yyyy");
                    txtFecha.Text = fecha_actual;

                    lTTB_GuardiaBE = _TB_GuardiaBL.ListarTB_GuardiaO_Act();
                    Session["Guardias"] = lTTB_GuardiaBE;
                    lTTB_AreaBE = _TB_AreaBL.ListarTB_AreaO_Act();
                    Session["Areas"] = lTTB_AreaBE;
                    lTCOM_CategoriasBE = _COM_CategoriasBL.ListarCOM_CategoriasO_Act();
                    Session["Categoria"] = lTCOM_CategoriasBE;
                    lTCOM_FormatosBE = _COM_FormatosBL.ListarCOM_FormatosO_Act();
                    Session["Formatos"] = lTCOM_FormatosBE;
                    lTCOM_SubCategoriasBE = _COM_SubCategoriasBL.ListarCOM_SubCategoriasO_Act();
                    Session["SubCategoria"] = lTCOM_SubCategoriasBE;
                    LlenarComboDepartamento();
                    ddlDepartamento.SelectedValue = ((Fnc_FuncionariosBE)Session["Fnc_Funcionarios"]).Area_Id.ToString();
                    LlenarComboAuditor();
                    
                    LlenarComboFormato();
                    //Session["Guardias"] = lTTB_GuardiaBE;
                    LlenarComboGuardia();
                    LlenarComboArea();

                    ddlCategoria.Visible = false;
                    //LlenarComboCategoria();
                    //LlenarComboSubCategoria();
                    GenerarTabla(((Fnc_FuncionariosBE)Session["FNC_Funcionarios"]).Funcionario_Id);
                   
                    
                }
                
            }
            catch (Exception x)
            {
                // Response.Redirect("error.aspx");
            }
        }

        private void GenerarTabla(Int16 _Originador)
        {
            DataTable Resultados = _COM_ComportamientoBL.BuscarCOM_ComportamientoByOriginador(_Originador);
            rpComportamiento.DataSource = Resultados;
            rpComportamiento.DataBind();
            
        }
        private bool buscarFormato(COM_FormatosBE obeFormato)
        {
            bool exitoIdFormato = true;
            if (!ddlDepartamento.SelectedValue.Equals("0")) exitoIdFormato = (obeFormato.Departamento_id.ToString().Equals(ddlDepartamento.SelectedValue));
            return (exitoIdFormato);
        }
        private bool buscarGuardias(TB_GuardiaBE obeGuardia)
        {
            bool exitoIdGuardia = true;
            if (!ddlDepartamento.SelectedValue.Equals("0")) exitoIdGuardia = (obeGuardia.Departamento_id.ToString().Equals(ddlDepartamento.SelectedValue));
            return (exitoIdGuardia);
        }
        private bool buscarArea(TB_AreaBE obeArea)
        {
            bool exitoIdArea = true;
            if (!ddlDepartamento.SelectedValue.Equals("0")) exitoIdArea = (obeArea.Departamento_id.ToString().Equals(ddlDepartamento.SelectedValue));
            return (exitoIdArea);
        }
        private bool buscarCategorias(COM_CategoriasBE obeCategoria)
        {
            bool exitoIdCategoria = true;
            if (!ddlFormato.SelectedValue.Equals("0")) exitoIdCategoria = (obeCategoria.Formato_id.ToString().Equals(ddlFormato.SelectedValue));
            return (exitoIdCategoria);
        }
        private bool buscarSubCategorias(COM_SubCategoriasBE obeSubCategoria)
        {
            bool exitoIdSubCategoria = true;
            if (!ddlCategoria.SelectedValue.Equals("0")) exitoIdSubCategoria = (obeSubCategoria.Categoria_id.ToString().Equals(ddlCategoria.SelectedValue));
            return (exitoIdSubCategoria);
        }
        private void LlenarComboAuditor()
        {
            lTTB_Fnc_FuncionariosBE = _Fnc_FuncionariosBL.ListarFnc_FuncionariosO_Act();
            ddlAuditor.DataSource = lTTB_Fnc_FuncionariosBE;
            ddlAuditor.DataValueField = "Funcionario_id";
            ddlAuditor.DataTextField = "Funcionario_Nome";
            ddlAuditor.SelectedValue = ((Fnc_FuncionariosBE)Session["Fnc_Funcionarios"]).Funcionario_Id.ToString();
            ddlAuditor.DataBind();

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

        private void LlenarComboGuardia()
        {
            lTTB_GuardiaBE = (List<TB_GuardiaBE>)Session["Guardias"];
            Predicate<TB_GuardiaBE> pred = new Predicate<TB_GuardiaBE>(buscarGuardias);
            lbeFiltroGuardia = lTTB_GuardiaBE.FindAll(pred);
            // Session["Filtro"] = lbeFiltro;

            ddlGuardia.DataSource = lbeFiltroGuardia;
            ddlGuardia.DataValueField = "Guardia_id";
            ddlGuardia.DataTextField = "Guardia_desc";
            ddlGuardia.DataBind();
            ddlGuardia.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));
        }
        private void LlenarComboArea()
        {
            lTTB_AreaBE= (List<TB_AreaBE>)Session["Areas"];
            Predicate<TB_AreaBE> pred = new Predicate<TB_AreaBE>(buscarArea);
            lbeFiltroArea = lTTB_AreaBE.FindAll(pred);
            // Session["Filtro"] = lbeFiltro;

            ddlArea.DataSource = lbeFiltroArea;
            ddlArea.DataValueField = "Area_id";
            ddlArea.DataTextField = "Area_desc";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));
        }
        private void LlenarComboFormato()
        {
            lTCOM_FormatosBE = (List<COM_FormatosBE>)Session["Formatos"];
            Predicate<COM_FormatosBE> pred = new Predicate<COM_FormatosBE>(buscarFormato);
            lbeFiltroFormato = lTCOM_FormatosBE.FindAll(pred);
            // Session["Filtro"] = lbeFiltro;
            ddlFormato.DataSource = lbeFiltroFormato;
            ddlFormato.DataValueField = "Formato_id";
            ddlFormato.DataTextField = "Formato_desc";
            ddlFormato.DataBind();
            ddlFormato.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));
            LlenarComboCategoria();
        }
        private void LlenarComboCategoria()
        {
            lTCOM_CategoriasBE = (List<COM_CategoriasBE>)Session["Categoria"];
            Predicate<COM_CategoriasBE> pred = new Predicate<COM_CategoriasBE>(buscarCategorias);
            lbeFiltroCa = lTCOM_CategoriasBE.FindAll(pred);
            //Session["FiltroCa"] = lbeFiltroCa;

            ddlCategoria.DataSource = lbeFiltroCa;
            ddlCategoria.DataValueField = "Categoria_id";
            ddlCategoria.DataTextField = "Categoria_desc";
            ddlCategoria.DataBind();
            LlenarComboSubCategoria();
        }
        private void LlenarComboSubCategoria()
        {
            COM_CategoriasBE _COM_CategoriasBE=new COM_CategoriasBE();
            lTCOM_SubCategoriasBE = (List<COM_SubCategoriasBE>)Session["SubCategoria"];
            Predicate<COM_SubCategoriasBE> pred = new Predicate<COM_SubCategoriasBE>(buscarSubCategorias);
            lbeFiltroSu = lTCOM_SubCategoriasBE.FindAll(pred);
            Session["FiltroSu"] = lbeFiltroSu;

            ddlSubCategoria.DataSource = lbeFiltroSu;
            ddlSubCategoria.DataValueField = "SubCategoria_id";
            ddlSubCategoria.DataTextField = "SubCategoria_desc";
            ddlSubCategoria.DataBind();
            ddlSubCategoria.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));
            _COM_CategoriasBE=_COM_CategoriasBL.TraerCOM_CategoriaById(int.Parse(ddlCategoria.SelectedValue));
            if (_COM_CategoriasBE.Grado==1)
                ddlCategoria.Visible = false;
            else
                ddlCategoria.Visible = true;

        }
        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboGuardia();
            LlenarComboArea();
            LlenarComboFormato();
        }

        protected void ddlFormato_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboCategoria();
        }

        protected void ddlCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboSubCategoria();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int status;
                var _mietq_etiqueta = _COM_ComportamientoBE;
                //_miempl.Emp_id = "";
                _mietq_etiqueta.Auditor = Convert.ToInt16(ddlAuditor.SelectedValue);
                _mietq_etiqueta.Fecha_Comportamiento = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", null);
                _mietq_etiqueta.Guardia = Convert.ToInt16(ddlGuardia.SelectedValue);
                _mietq_etiqueta.Tipo_emp = Convert.ToInt16(ddlTipoEmpleado.SelectedValue);
                _mietq_etiqueta.Formato_id = Convert.ToInt16(ddlFormato.SelectedValue);
                _mietq_etiqueta.SubCategoria_id = Convert.ToInt16(ddlSubCategoria.SelectedValue);
                _mietq_etiqueta.Valor = ddlValor.SelectedValue;
                _mietq_etiqueta.Descripcion = txtDescripcion.Text;
                _mietq_etiqueta.Departamento = Convert.ToInt16(ddlDepartamento.SelectedValue);
                _mietq_etiqueta.Area_id = Convert.ToInt16(ddlArea.SelectedValue);
                _mietq_etiqueta.Turno = 1;
                _mietq_etiqueta.Originador = ((Fnc_FuncionariosBE)Session["Fnc_Funcionarios"]).Funcionario_Id;
                _mietq_etiqueta.Estado = 1;
                int vexito = _COM_ComportamientoBL.InsertarCOM_Comportamiento(_COM_ComportamientoBE);
                if (vexito != 0)
                {
                    ltlTabla.Text = "";
                    GenerarTabla(((Fnc_FuncionariosBE)Session["FNC_Funcionarios"]).Funcionario_Id);
                }
                else
                {
                    String mensaje = "<script language='JavaScript'>window.alert('error, no se pudo registrar el comportamiento')";
                    mensaje += Environment.NewLine;
                    this.Page.Response.Write(mensaje);
                }


            }
            catch (Exception ex)
            {
                String mensaje = "<script language='JavaScript'>window.alert('error, no se pudo registrar el comportamiento " + ex.Message + "')";
                mensaje += Environment.NewLine;
                this.Page.Response.Write(mensaje);
            }
        }

        protected void ibnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibn = (ImageButton)sender;
            RepeaterItem fila = (RepeaterItem)ibn.Parent;
            int _Comportamiento_id = int.Parse(((Label)fila.Controls[1]).Text);
            bool obeRespuesta = _COM_ComportamientoBL.EliminarCOM_Comportamiento(_Comportamiento_id);
            if (!obeRespuesta)
            {
                String mensaje = "<script language='JavaScript'>window.alert('error, no se pudo eliminar el registro')";
                mensaje += Environment.NewLine;
                this.Page.Response.Write(mensaje);
            }
            else
            {
            }
            GenerarTabla(((Fnc_FuncionariosBE)Session["FNC_Funcionarios"]).Funcionario_Id);
        }



    }
}