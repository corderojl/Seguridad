<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmReporteCartaAll.aspx.cs" Inherits="IncidentesWEB.Indicadores.frmReporteCartaAll" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte</title>
    <script src="Scripts/jquery-1.4.1-vsdoc.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.core.js" type="text/javascript"></script>
    <script src="Scripts/jquery.ui.widget.js" type="text/javascript"></script>


    <link href="../css/custom_inputs.css" rel="stylesheet" type="text/css" />
    <link href="css/style_cms.css" rel="stylesheet" type="text/css" />
    <link href="../css/reset.css" rel="stylesheet" type="text/css" />
    <link href="../css/style.css" rel="stylesheet" type="text/css" />

    <link href="../css/custom_inputs.css" rel="stylesheet" type="text/css" />
    <link href="../Comportamiento/css/style_cms.css" rel="stylesheet" />
    <link href="Styles/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <link href="Styles/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery.ui.datepicker.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div id="contenido" class="cms">
                <div id="panel" class="custom_form">
                    <div id="Filtro">
                    <table style="width: 100%">
                        <tr>
                            <td align="right" width="80">Departamento:</td>
                            <td>
                                <asp:DropDownList ID="ddlDepartamento" runat="server"  CssClass="form_row">
                                </asp:DropDownList>
                            </td>
                            <td>Lider:</td>
                            <td>
      
                                <asp:DropDownList ID="ddlLider" runat="server" CssClass="form_row">
                                </asp:DropDownList>
      
                            </td>
                            <td align="right" width="80">Clasificación:</td>
                            <td width="300">
                                <asp:DropDownList ID="ddClasificacion" runat="server" CssClass="form_row">
                                    <asp:ListItem Value="%%" Selected="True">(Todos)</asp:ListItem>
                                    <asp:ListItem Value="Sobresaliente">Sobresaliente</asp:ListItem>
                                    <asp:ListItem Value="Cumple Bien">Cumple Bien</asp:ListItem>
                                    <asp:ListItem Value="Cumple Parcial">Cumple Parcial</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" width="80">Año:</td>
                            <td>
                                <asp:DropDownList ID="ddlAnio" runat="server"  CssClass="form_row" >
                                    <asp:ListItem Selected="True">2017</asp:ListItem>
                                    <asp:ListItem>2018</asp:ListItem>
                                    <asp:ListItem>2019</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right" width="80">Período:</td>
                            <td width="300">
                                <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form_row" >
                                    <asp:ListItem Value="1" >Revisión Mitad de Año</asp:ListItem>
                                    <asp:ListItem Value="2" Selected="True">Revisión Final de Año</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right" width="80"></td>
                            <td width="300">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="right" width="80">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td align="right" width="80">&nbsp;</td>
                            <td width="300">&nbsp;</td>
                            <td align="right" width="80">&nbsp;</td>
                            <td width="300">
                                <asp:Button ID="btnExportar" runat="server" Text="Buscar" OnClick="btnExportar_Click" CssClass="botonB"/>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="580px" Width="758px"></rsweb:ReportViewer>
                </div>
            </div>
        </div>
    </form>
</body>
</html>