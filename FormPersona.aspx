<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormPersona.aspx.vb" Inherits="ControlVehiculos.FormPersona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="editando" runat="server" />

    <div class="container d-flex flex-column mb-3 gap-2">

        <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Nombre" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtApellido1" CssClass="form-control" placeholder="Apellido1" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtApellido2" CssClass="form-control" placeholder="Apellido2" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtNacionalidad" CssClass="form-control" placeholder="Nacionalidad" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtfechaNacimiento" CssClass="form-control" placeholder="FechaNacimiento" runat="server" TextMode="Date"></asp:TextBox>
        <asp:TextBox ID="txtTelefono" CssClass="form-control" placeholder="Telefono" runat="server"></asp:TextBox>


        <asp:Button ID="btnMostrar" runat="server" CssClass="btn btn-primary btn-hover-move" Text="Guardar" OnClick="btn_guardar" />
        <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-primary btn-hover-move" Text="Actualizar" OnClick="btnActualizar_Click" />

        <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>

    </div>
    <asp:GridView ID="gvPersonas" CssClass="table table-striped table-hover table-success" runat="server" AutoGenerateColumns="False"
        DataSourceID="SqlDataSource1" DataKeyNames="idPersona"
        OnRowDeleting="gvPersonas_RowDeleting"
        OnRowEditing="gvPersonas_RowEditing"
        OnRowCancelingEdit="gvPersonas_RowCancelingEdit"
        OnRowUpdating="gvPersonas_RowUpdating"
        OnSelectedIndexChanged="gvPersonas_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-success" />
            <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-primary" />
            <asp:BoundField DataField="idPersona" HeaderText="ID" Visible="False" ReadOnly="True" SortExpression="idPersona" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
            <asp:BoundField DataField="Apellido1" HeaderText="Primer Apellido" SortExpression="Apellido1" />
            <asp:BoundField DataField="Apellido2" HeaderText="Segundo Apellido" SortExpression="Apellido2" />
            <asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad" SortExpression="Nacionalidad" />
            <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nacimiento" SortExpression="FechaNacimiento" />
            <asp:BoundField DataField="Telefono" HeaderText="Telefono" SortExpression="Telefono" />
            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:II46_P3ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:II46_P3ConnectionString.ProviderName %>" 
        SelectCommand="SELECT * FROM [Personas]"></asp:SqlDataSource>


</asp:Content>
