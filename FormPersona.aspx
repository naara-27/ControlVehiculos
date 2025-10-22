<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormPersona.aspx.vb" Inherits="ControlVehiculos.FormPersona" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="editando" runat="server"/> 

<div class="container d-flex flex-column mb-3 gap-2">

    <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Nombre" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtApellido" CssClass="form-control" placeholder="Apellido" runat="server"></asp:TextBox>
    <asp:TextBox ID="txtEdad" CssClass="form-control" placeholder="Edad" runat="server"></asp:TextBox>

    <asp:Button ID="btnMostrar" runat="server" CssClass="btn btn-primary btn-hover-move" Text="Guardar" OnClick="btn_guardar" />
    <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-primary btn-hover-move" Text="Actualizar" OnClick="btnActualizar_Click" />

    <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>

</div>
<asp:GridView ID="gvPersonas" CssClass="table table-striped table-hover table-success" runat="server" AutoGenerateColumns="False"
    DataSourceID="SqlDataSource1" DataKeyNames="ID" 
    OnRowDeleting="gvPersonas_RowDeleting" 
    OnRowEditing="gvPersonas_RowEditing" 
    OnRowCancelingEdit="gvPersonas_RowCancelingEdit" 
    OnRowUpdating="gvPersonas_RowUpdating" 
    OnSelectedIndexChanged="gvPersonas_SelectedIndexChanged">

    <Columns>
        <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-success" />
        <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-primary" />
        <asp:BoundField DataField="ID" HeaderText="ID" Visible="False" ReadOnly="True" SortExpression="ID" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
        <asp:BoundField DataField="Apellido" HeaderText="Apellido" SortExpression="Apellido" />
        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" />
    </Columns>
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:II46ConnectionString %>" ProviderName="<%$ ConnectionStrings:II46ConnectionString.ProviderName %>" SelectCommand="SELECT * FROM [Persona]"></asp:SqlDataSource>
 
</asp:Content>
