<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormPropietario.aspx.vb" Inherits="ControlVehiculos.FormPropietario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4 d-flex justify-content-center">
        <div class="card shadow-sm border-0 w-100" style="max-width: 700px; background-color: #fff0f5;">
            <div class="card-header text-white text-center" style="background-color: #d63384;">
                <h4 class="mb-0">Detalle de Propietario y Vehículo</h4>
            </div>
            <div class="card-body d-flex flex-column gap-3">

                <!-- Selección de Persona -->
                <label class="fw-semibold">Persona</label>
                <asp:DropDownList ID="ddlPersonas" runat="server" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlPersonas_SelectedIndexChanged" 
                    CssClass="form-select border-pink text-center">
                    <asp:ListItem Text="Seleccione una persona" Value="" />
                </asp:DropDownList>

                <hr />

                <!-- Grid de Vehículos por Persona -->
                <asp:GridView ID="gvVehiculosPersona" runat="server"
                    AutoGenerateColumns="False"
                    CssClass="table table-striped table-hover border border-pink"
                    EmptyDataText="Esta persona no tiene vehículos asignados."
                    DataKeyNames="IdVehiculo"
                    OnRowDeleting="gvVehiculosPersona_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="Placa" HeaderText="Placa" />
                        <asp:BoundField DataField="Marca" HeaderText="Marca" />
                        <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                        
                        <asp:CommandField ShowDeleteButton="True" DeleteText="Eliminar Vehiculo" ControlStyle-CssClass="btn btn-warning" />
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>

    <!-- SweetAlert Script -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- Estilos rosados -->
    <style>
        .border-pink { border: 1px solid #d63384 !important; }
        .btn-pink { background-color: #d63384; border: none; }
        .btn-pink:hover { background-color: #c2185b; }
    </style>

</asp:Content>