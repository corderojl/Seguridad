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

namespace IncidentesWEB.admin
{
    public partial class ActualizarIncidente : System.Web.UI.Page
    {

        TB_IncidentesBE _TB_IncidentesBE = new TB_IncidentesBE();

        TB_IncidentesBL _TB_IncidentesBL = new TB_IncidentesBL();
        TB_DepartamentoBL _TB_DepartamentoBL = new TB_DepartamentoBL();
        TB_AreaBL _TB_AreaBL = new TB_AreaBL();
        TB_EstatusOperacionalBL _TB_EstatusOperacionalBL = new TB_EstatusOperacionalBL();
        TB_ClasificacionBL _TB_ClasificacionBL = new TB_ClasificacionBL();
        TB_ParteCuerpoBL _TB_ParteCuerpoBL = new TB_ParteCuerpoBL();
        TB_EquipoAfectadoBL _TB_EquipoAfectadoBL = new TB_EquipoAfectadoBL();
        TB_ContratistaBL _TB_ContratistaBL = new TB_ContratistaBL();
        TB_GuardiaBL _TB_GuardiaBL = new TB_GuardiaBL();
        TB_RolBL _TB_RolBL = new TB_RolBL();
        TB_PlanAccionBL _TB_PlanAccionBL = new TB_PlanAccionBL();
        TB_ResponsableBL _TB_ResponsableBL = new TB_ResponsableBL();

        protected DataView dvEstatusOperacional;
        List<TB_DepartamentoBE> lTTB_DepartamentoBE;
        List<TB_AreaBE> lTTB_AreaBE;
        List<TB_EstatusOperacionalBE> lTTB_EstatusOperacionalBE;
        List<TB_ClasificacionBE> lTTB_ClasificacionBE;
        List<TB_ParteCuerpoBE> lTTB_ParteCuerpoBE;
        List<TB_EquipoAfectadoBE> lTTB_EquipoAfectadoBE;
        List<TB_ContratistaBE> lTTB_ContratistaBE;
        List<TB_GuardiaBE> lTTB_GuardiaBE;
        List<TB_RolBE> lTTB_RolBE;
        private List<TB_GuardiaBE> lbeFiltro;
        private List<TB_AreaBE> lbeFiltroArea;

        int _Incidente_id;
        string _reg;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.IsPostBack)
                {

                }
                else
                {
                    _Incidente_id = Convert.ToInt32(Request.QueryString["Incidente_id"]);
                    if (Request.QueryString["reg"] != null)
                        _reg = Request.QueryString["reg"].ToString();
                    if (_reg == "ver")
                        btnRegistrar.Visible = false;
                    if(_reg == "exito")
                        ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "jAlert", "jAlert('Debe registrar un Plan de Acción');", true);
                    
                    _TB_IncidentesBE = _TB_IncidentesBL.TraerTB_IncidentesById(_Incidente_id);
                    lblIncidente_id.Text = _Incidente_id.ToString();
                    rblTipoPersonal.SelectedValue = _TB_IncidentesBE.Tipo_emp.ToString();
                    txtTitulo.Text = _TB_IncidentesBE.Titulo;
                    txtDescripcion.Text = _TB_IncidentesBE.Descripcion;
                    rblTurno.SelectedValue = _TB_IncidentesBE.Turno.ToString();
                    txtFecha.Text = _TB_IncidentesBE.Fecha_incidente.ToString("dd/MM/yyyy");
                    lTTB_AreaBE = _TB_AreaBL.ListarTB_AreaO_Act();
                    Session["Areas"] = lTTB_AreaBE;
                    lTTB_GuardiaBE = _TB_GuardiaBL.ListarTB_GuardiaO_Act();
                    Session["Guardias"] = lTTB_GuardiaBE;
                    ddlTipoIncidente.SelectedValue = _TB_IncidentesBE.Daño_tipo.ToString();
                    LlenarComboDepartamento();
                    LlenarComboGuardia();
                    LlenarComboArea();
                    LlenarComboEstatusOperacional();
                    LlenarComboClasificacion();
                    LlenarComboParteCuerpo();
                    LlenarComboEquipoAfectado();
                    LlenarComboContratista();
                    GenerarTablaPlanInmediato(_Incidente_id, txtTitulo.Text);
                    GenerarTablaPlanSistemico(_Incidente_id, txtTitulo.Text);
                    ddlDepartamento.SelectedValue = _TB_IncidentesBE.Departamento.ToString();
                    ddlGuardia.SelectedValue = _TB_IncidentesBE.Guardia.ToString();
                    TraerResponsable(Convert.ToInt16(ddlDepartamento.SelectedValue));

                }
            }
            catch (Exception x)
            {
                // Response.Redirect("error.aspx");
            }
        }
        private void TraerResponsable(Int16 _Departamento_id)
        {
            lblResponsable.Text = _TB_ResponsableBL.TraerTB_ResponsableByDepartamento(_Departamento_id).Funcionario_nome;
        }
        private void LlenarComboContratista()
        {
            lTTB_ContratistaBE = _TB_ContratistaBL.ListarTB_ContratistaO_Act();
            ddlContratista.DataSource = lTTB_ContratistaBE;
            ddlContratista.DataValueField = "Contratista_id";
            ddlContratista.DataTextField = "Contratista_desc";
            ddlContratista.SelectedValue = _TB_IncidentesBE.Contratista_id.ToString();
            ddlContratista.DataBind();

        }


        private void LlenarComboArea()
        {
            lTTB_AreaBE = (List<TB_AreaBE>)Session["Areas"];
            Predicate<TB_AreaBE> pred = new Predicate<TB_AreaBE>(buscarAreas);
            lbeFiltroArea = lTTB_AreaBE.FindAll(pred);
            Session["Filtro"] = lbeFiltroArea;

            ddlArea.DataSource = lbeFiltroArea;
            ddlArea.DataValueField = "Area_id";
            ddlArea.DataTextField = "Area_desc";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));
        }

        private void LlenarComboDepartamento()
        {
            ddlDepartamento.DataSource = _TB_DepartamentoBL.ListarTB_Departamento_All("1");
            ddlDepartamento.DataValueField = "Departamento_id";
            ddlDepartamento.DataTextField = "Departamento_desc";
            ddlDepartamento.DataBind();
            ddlDepartamento.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));

        }
        private void LlenarComboEstatusOperacional()
        {
            lTTB_EstatusOperacionalBE = _TB_EstatusOperacionalBL.ListarTB_EstatusOperacionalO_Act();
            ddlEstatusOperacional.DataSource = lTTB_EstatusOperacionalBE;
            ddlEstatusOperacional.DataValueField = "EstatusOperacional_id";
            ddlEstatusOperacional.DataTextField = "EstatusOperacional_desc";
            ddlEstatusOperacional.SelectedValue = _TB_IncidentesBE.Estatus_ope.ToString();
            ddlEstatusOperacional.DataBind();
            ddlEstatusOperacional.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));
        }
        private void LlenarComboClasificacion()
        {
            lTTB_ClasificacionBE = _TB_ClasificacionBL.ListarTB_ClasificacionO_Act();
            ddlClasificacion.DataSource = lTTB_ClasificacionBE;
            ddlClasificacion.DataValueField = "Clasificacion_id";
            ddlClasificacion.DataTextField = "Clasificacion_desc";
            ddlClasificacion.SelectedValue = _TB_IncidentesBE.Clasificacion_id.ToString();
            ddlClasificacion.DataBind();
            ddlClasificacion.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));
        }
        private void LlenarComboParteCuerpo()
        {
            lTTB_ParteCuerpoBE = _TB_ParteCuerpoBL.ListarTB_ParteCuerpoO_Act();
            ddlParteCuerpo.DataSource = lTTB_ParteCuerpoBE;
            ddlParteCuerpo.DataValueField = "ParteCuerpo_id";
            ddlParteCuerpo.DataTextField = "ParteCuerpo_desc";
            ddlParteCuerpo.SelectedValue = _TB_IncidentesBE.ParteCuerpo_id.ToString();
            ddlParteCuerpo.DataBind();
            ddlParteCuerpo.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));
        }
        private void LlenarComboEquipoAfectado()
        {
            lTTB_EquipoAfectadoBE = _TB_EquipoAfectadoBL.ListarTB_EquipoAfectadoO_Act();
            ddlEquipoAfectado.DataSource = lTTB_EquipoAfectadoBE;
            ddlEquipoAfectado.DataValueField = "EquipoAfectado_id";
            ddlEquipoAfectado.DataTextField = "EquipoAfectado_desc";
            ddlEquipoAfectado.SelectedValue = _TB_IncidentesBE.EquipoAfectado_id.ToString();
            ddlEquipoAfectado.DataBind();
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                var _mietq_etiqueta = _TB_IncidentesBE;
                _mietq_etiqueta.Incidente_id = int.Parse(lblIncidente_id.Text);
                _mietq_etiqueta.Titulo = txtTitulo.Text;
                _mietq_etiqueta.Descripcion = txtDescripcion.Text;
                _mietq_etiqueta.Fecha_incidente = DateTime.ParseExact(txtFecha.Text, "dd/MM/yyyy", null);
                _mietq_etiqueta.Fecha_estimada = DateTime.Now;
                _mietq_etiqueta.Turno = Convert.ToInt16(rblTurno.SelectedValue);
                _mietq_etiqueta.Tiempo_ext = 1;
                _mietq_etiqueta.Estatus_ope = Convert.ToInt16(ddlEstatusOperacional.SelectedValue);
                _mietq_etiqueta.Departamento = Convert.ToInt16(ddlDepartamento.SelectedValue);
                _mietq_etiqueta.Guardia = Convert.ToInt16(ddlGuardia.SelectedValue);
                _mietq_etiqueta.Area_id = Convert.ToInt16(ddlArea.SelectedValue);
                _mietq_etiqueta.Tipo_emp = Convert.ToInt16(rblTipoPersonal.SelectedValue);
                _mietq_etiqueta.Contratista_id = Convert.ToInt16(ddlContratista.SelectedValue);
                _mietq_etiqueta.Rol_id = 1;
                _mietq_etiqueta.Rol_tiempo = 1;
                _mietq_etiqueta.Compania_tiempo = 1;
                _mietq_etiqueta.ParteCuerpo_id = Convert.ToInt16(ddlParteCuerpo.SelectedValue);
                _mietq_etiqueta.EquipoAfectado_id = Convert.ToInt16(ddlEquipoAfectado.SelectedValue);
                _mietq_etiqueta.Clasificacion_id = Convert.ToInt16(ddlClasificacion.SelectedValue);
                _mietq_etiqueta.Daño_tipo = Convert.ToInt16(ddlTipoIncidente.SelectedValue);
                _mietq_etiqueta.Originador = ((Fnc_FuncionariosBE)Session["Fnc_Funcionarios"]).Funcionario_Id;
                _mietq_etiqueta.Registro = "urco.ju.1";
                _mietq_etiqueta.Estado = 1;
                _mietq_etiqueta.ParteCuerpo_id = Convert.ToInt16(ddlParteCuerpo.SelectedValue);

                if (_TB_IncidentesBL.ActualizarTB_Incidentes(_TB_IncidentesBE))
                {

                    this.Page.Response.Write("");
                }
                else
                {
                    Response.Redirect("error.aspx?error=");
                }

            }
            catch (Exception ex)
            {
                // lblMensaje.Text = ex.ToString();
            }
        }
        private bool buscarGuardias(TB_GuardiaBE obeGuardia)
        {
            bool exitoIdGuardia = true;
            if (!ddlDepartamento.SelectedValue.Equals("0")) exitoIdGuardia = (obeGuardia.Departamento_id.ToString().Equals(ddlDepartamento.SelectedValue));
            return (exitoIdGuardia);
        }
        private bool buscarAreas(TB_AreaBE obeArea)
        {
            bool exitoIdArea = true;
            if (!ddlDepartamento.SelectedValue.Equals("0")) exitoIdArea = (obeArea.Departamento_id.ToString().Equals(ddlDepartamento.SelectedValue));
            return (exitoIdArea);
        }
        private void LlenarComboGuardia()
        {
            lTTB_GuardiaBE = (List<TB_GuardiaBE>)Session["Guardias"];
            Predicate<TB_GuardiaBE> pred = new Predicate<TB_GuardiaBE>(buscarGuardias);
            lbeFiltro = lTTB_GuardiaBE.FindAll(pred);
            Session["Filtro"] = lbeFiltro;

            ddlGuardia.DataSource = lbeFiltro;
            ddlGuardia.DataValueField = "Guardia_id";
            ddlGuardia.DataTextField = "Guardia_desc";
            ddlGuardia.DataBind();
            ddlGuardia.Items.Insert(0, new ListItem("Elija una Opcion..", "0"));
        }

        protected void ddlDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            LlenarComboGuardia();
            LlenarComboArea();
            TraerResponsable(Convert.ToInt16(ddlDepartamento.SelectedValue));
        }

        protected void rblTipoPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblTipoPersonal.SelectedValue == "1")
            {
                ddlContratista.Enabled = false;
                ddlContratista.SelectedValue = "1";

            }
            else
            {
                ddlContratista.Enabled = true;
            }
        }
        public void GenerarTablaPlanSistemico(int _Incidente_id, string _Titulo)
        {
            DataTable Resultados = _TB_PlanAccionBL.BuscarTB_PlanAccionByIncidenteResponsable(_Incidente_id, 2, 1);
            StringBuilder Tabla = new StringBuilder();

            string _PlanAccion_id;

            int TotalRegistros = Resultados.Rows.Count;
            Tabla.AppendLine("<h2>Plan Sistémico:</h2>");
            Tabla.AppendLine("<table class=\"lista_table\">");
            Tabla.AppendLine("<thead>");
            Tabla.AppendLine("<th><a href=\"#\" onClick=\"PopUp('RegistrarPlanAccion.aspx?tipoPlan=2&Registro_id=" + _Incidente_id + "&Sistema_id=1',20,20,800,400); return false;\">Agregar</th><th> ID </th><th> PLAN </th><th> RESPONSABLE </th><th> ESTADO </th><th> FECHA ESTIMADA </th>");
            Tabla.AppendLine("</thead>");
            Tabla.AppendLine("<tbody>");

            for (int i = 0; i < TotalRegistros; i++)
            {

                _PlanAccion_id = Resultados.Rows[i]["PlanAccion_id"].ToString();
                Tabla.AppendLine("<tr>");
                Tabla.Append("<td>" + "<a href=\"#\" onClick=\"PopUp('ActualizarPlanAccion.aspx?PlanAccion_id=" + _PlanAccion_id + "&Registro_id=" + _Incidente_id + "&Sistema_id=1',20,20,800,400); return false;\"> Editar </a></td>");
                Tabla.Append("<td>" + _PlanAccion_id + "</td>");
                Tabla.Append("<td align=\"left\">" + Resultados.Rows[i]["PlanAccion_desc"].ToString() + "</td>");
                Tabla.Append("<td>" + Resultados.Rows[i]["Funcionario_nome"].ToString() + "</td>");
                if (Convert.ToBoolean(Convert.ToByte(Resultados.Rows[i]["Estado"])) == true)
                {
                    Tabla.Append("<td>Cumplido</td>");
                }
                else
                {
                    Tabla.Append("<td style='color: red;'>No cumplido</td>");
                }
                Tabla.Append("<td>" + Resultados.Rows[i]["fecha"] + "</td>");
                Tabla.Append(Environment.NewLine);
                Tabla.AppendLine("</tr>");
            }

            Tabla.AppendLine("</tbody>");
            Tabla.AppendLine("</table>");

            //lblTitulo.Text = "Comentarios: " + Resultados.Rows[0]["Titulo"];
            ltlPlanSistemico.Text = Tabla.ToString();
        }
        public void GenerarTablaPlanInmediato(int _Incidente_id, string _Titulo)
        {
            DataTable Resultados = _TB_PlanAccionBL.BuscarTB_PlanAccionByIncidenteResponsable(_Incidente_id, 1, 1);
            StringBuilder Tabla = new StringBuilder();

            string _PlanAccion_id;

            int TotalRegistros = Resultados.Rows.Count;
            Tabla.AppendLine("<h2>Plan Inmediato:</h2>");
            Tabla.AppendLine("<table class=\"lista_table\">");
            Tabla.AppendLine("<thead>");
            Tabla.AppendLine("<th><a href=\"#\" onClick=\"PopUp('RegistrarPlanAccion.aspx?tipoPlan=1&Registro_id=" + _Incidente_id + "&Sistema_id=1',20,20,800,400); return false;\">Agregar</th><th> ID </th><th> PLAN </th><th> RESPONSABLE </th><th> ESTADO </th><th> FECHA ESTIMADA </th>");
            Tabla.AppendLine("</thead>");
            Tabla.AppendLine("<tbody>");

            for (int i = 0; i < TotalRegistros; i++)
            {

                _PlanAccion_id = Resultados.Rows[i]["PlanAccion_id"].ToString();
                Tabla.AppendLine("<tr>");
                Tabla.Append("<td>" + "<a href=\"#\" onClick=\"PopUp('ActualizarPlanAccion.aspx?PlanAccion_id=" + _PlanAccion_id + "&Registro_id=" + _Incidente_id + "&Sistema_id=1',20,20,800,400); return false;\"> Editar </a></td>");
                Tabla.Append("<td>" + _PlanAccion_id + "</td>");
                Tabla.Append("<td align=\"left\">" + Resultados.Rows[i]["PlanAccion_desc"].ToString() + "</td>");
                Tabla.Append("<td>" + Resultados.Rows[i]["Funcionario_nome"].ToString() + "</td>");
                if (Convert.ToBoolean(Convert.ToByte(Resultados.Rows[i]["Estado"])) == true)
                {
                    Tabla.Append("<td>Cumplido</td>");
                }
                else
                {
                    Tabla.Append("<td style='color: red;'>No cumplido</td>");
                }
                Tabla.Append("<td>" + Resultados.Rows[i]["fecha"] + "</td>");
                Tabla.Append(Environment.NewLine);
                Tabla.AppendLine("</tr>");
            }

            Tabla.AppendLine("</tbody>");
            Tabla.AppendLine("</table>");

            //lblTitulo.Text = "Comentarios: " + Resultados.Rows[0]["Titulo"];
            ltlPlanInmediato.Text = Tabla.ToString();
        }
    }
}