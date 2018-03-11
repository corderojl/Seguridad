using IncidentesBE;
using IncidentesBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IncidentesWEB.Comportamiento
{
    public partial class registrarSubCategorias : System.Web.UI.Page
    {
        COM_SubCategoriasBL _COM_SubCategoriasBL = new COM_SubCategoriasBL();
        COM_SubCategoriasBE _COM_SubCategoriasBE = new COM_SubCategoriasBE();
        List<COM_SubCategoriasBE> lTCOM_SubCategoriasBE;

        protected void Page_Load(object sender, EventArgs e)
        {
            Int16 _Categoria_id = Convert.ToInt16(Request.QueryString["Categoria_id"]);
            if (this.IsPostBack)
            {
                lblMensaje.Text = "";
            }
            else
            {
                GenerarTabla(_Categoria_id);
                lblMensaje.Text = "";
            }
        }
        private void GenerarTabla(Int16 _Categoria_id)
        {

            lTCOM_SubCategoriasBE = _COM_SubCategoriasBL.ListarCOM_SubCategoriasByCategoria(_Categoria_id);
            rpEmpleado.DataSource = lTCOM_SubCategoriasBE;
            rpEmpleado.DataBind();
        }

        protected void ibnActualizar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibn = (ImageButton)sender;
            RepeaterItem fila = (RepeaterItem)ibn.Parent;
            Int16 _SubCategoria_id = Int16.Parse(((Label)fila.Controls[1]).Text);
            var _miObj = _COM_SubCategoriasBE;
            //_miempl.Emp_id = "";
            _miObj.SubCategoria_desc = ((TextBox)fila.Controls[3]).Text;
            _miObj.SubCategoria_id = Int16.Parse(((Label)fila.Controls[1]).Text);

            bool obeRespuesta = _COM_SubCategoriasBL.ActualizarCOM_SubCategoria(_COM_SubCategoriasBE);
            if (!obeRespuesta)
            {
                String mensaje = "<script language='JavaScript'>window.alert('error, no se pudo eliminar el registro')";
                mensaje += Environment.NewLine;
                this.Page.Response.Write(mensaje);
            }
            else
            {
            }
            GenerarTabla(Convert.ToInt16(Request.QueryString["Categoria_id"]));
        }

        protected void ibnEliminar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton ibn = (ImageButton)sender;
            RepeaterItem fila = (RepeaterItem)ibn.Parent;
            Int16 _SubCategoria_id = Int16.Parse(((Label)fila.Controls[1]).Text);
            bool obeRespuesta = _COM_SubCategoriasBL.EliminarCOM_SubCategoria(_SubCategoria_id);
            if (!obeRespuesta)
            {
                String mensaje = "<script language='JavaScript'>window.alert('error, no se pudo eliminar el registro')";
                mensaje += Environment.NewLine;
                this.Page.Response.Write(mensaje);
            }
            else
            {
            }
            GenerarTabla(Convert.ToInt16(Request.QueryString["Categoria_id"]));
        }

        protected void ibnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int status;
                var _miObj = _COM_SubCategoriasBE;
                //_miempl.Emp_id = "";
                _miObj.SubCategoria_desc = txtSubCategoria.Text;
                _miObj.Categoria_id = Convert.ToInt16(Request.QueryString["Categoria_id"]);
                _miObj.SubCategoria_codigo = (((Fnc_FuncionariosBE)Session["Fnc_Funcionarios"]).Funcionario_Id).ToString();

                int vexito = _COM_SubCategoriasBL.InsertarCOM_SubCategoria(_COM_SubCategoriasBE);
                if (vexito != 0)
                {
                    GenerarTabla(Convert.ToInt16(Request.QueryString["Categoria_id"]));
                    txtSubCategoria.Text = "";
                }
                else
                {
                    lblMensaje.Text="error, no se pudo registrar la Categoria";
                }


            }
            catch (Exception ex)
            {
                lblMensaje.Text = "error, no se pudo registrar la Categoria" + ex.Message;
            }
        }
    }
}