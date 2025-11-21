<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormVehiculo.aspx.vb" Inherits="ControlVehiculos.FormVehiculo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="editando" runat="server" />

    <div class="container mt-4 d-flex justify-content-center">
        <div class="card shadow-sm border-0 w-100" style="max-width: 600px; background-color: #fff0f5;">
            <div class="card-header text-white text-center" style="background-color: #d63384;">
                <h4 class="mb-0">Formulario de Vehículo</h4>
            </div>
            <div class="card-body d-flex flex-column gap-3 align-items-center">

                <asp:TextBox ID="txtPlaca" CssClass="form-control border-pink text-center" placeholder="Placa" runat="server" />

                <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select border-pink text-center">
                    <asp:ListItem Text="Seleccione una marca" Value="" />
                    <asp:ListItem Text="Toyota" Value="Toyota" />
                    <asp:ListItem Text="Hyundai" Value="Hyundai" />
                    <asp:ListItem Text="Kia" Value="Kia" />
                    <asp:ListItem Text="Mazda" Value="Mazda" />
                    <asp:ListItem Text="Nissan" Value="Nissan" />
                </asp:DropDownList>

                <asp:DropDownList ID="ddlModelo" runat="server" CssClass="form-select border-pink text-center">
                    <asp:ListItem Text="Seleccione un modelo" Value="" />
                    <asp:ListItem Text="Sedán" Value="Sedán" />
                    <asp:ListItem Text="SUV" Value="SUV" />
                    <asp:ListItem Text="Pick-up" Value="Pick-up" />
                    <asp:ListItem Text="Hatchback" Value="Hatchback" />
                    <asp:ListItem Text="Van" Value="Van" />
                </asp:DropDownList>

                <asp:DropDownList ID="ddlPropietario" runat="server" CssClass="form-select border-pink text-center">
                    <asp:ListItem Text="Seleccione propietario" Value="" />
                </asp:DropDownList>

                <div class="d-flex gap-2">
                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-pink text-white fw-semibold" Text="Guardar" OnClick="btnGuardar_Click" />
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="fw-bold text-danger" />
            </div>
        </div>
    </div>
      <asp:GridView ID="gvVehiculos" runat="server"
    AutoGenerateColumns="False"
    ShowHeaderWhenEmpty="True"
    EmptyDataText="No hay vehículos registrados."
    CssClass="table table-striped table-hover border border-pink"
    DataKeyNames="IdVehiculo"
    OnRowEditing="gvVehiculos_RowEditing"
    OnRowUpdating="gvVehiculos_RowUpdating"
    OnRowCancelingEdit="gvVehiculos_RowCancelingEdit"
    OnRowDeleting="gvVehiculos_RowDeleting">
    <Columns>
        <asp:CommandField ShowEditButton="True" ControlStyle-CssClass="btn btn-rose-dark" />
        <asp:BoundField DataField="IdVehiculo" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="Placa" HeaderText="Placa" />
        <asp:BoundField DataField="Marca" HeaderText="Marca" />
        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
        <asp:BoundField DataField="NombrePropietario" HeaderText="Propietario" ReadOnly="True" />
        <asp:CommandField ShowDeleteButton="True" DeleteText="Eliminar / Desasignar" ControlStyle-CssClass="btn btn-danger" />
    </Columns>
</asp:GridView>

    <!-- SweetAlert Script -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- Estilos rosados personalizados -->
    <style>
        .border-pink { border: 1px solid #d63384 !important; }
        .btn-pink { background-color: #d63384; border: none; }
        .btn-pink:hover { background-color: #c2185b; }
        .btn-outline-pink { border: 1px solid #d63384; color: #d63384; background-color: transparent; }
        .btn-outline-pink:hover { background-color: #d63384; color: white; }
        .btn-rose-light { background-color: #f8b7d4; color: white; border: none; }
        .btn-rose-light:hover { background-color: #f48fb1; }
        .btn-rose-dark { background-color: #c2185b; color: white; border: none; }
        .btn-rose-dark:hover { background-color: #ad1457; }
    </style>
</asp:Content>