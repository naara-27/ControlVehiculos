<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormPropietario.aspx.vb" Inherits="ControlVehiculos.FormPropietario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4 d-flex justify-content-center">
        <div class="card shadow-sm border-0 w-100" style="max-width:600px; background-color:#fff0f5;">
            <div class="card-header text-white text-center" style="background-color:#d63384;">
                <h4 class="mb-0">Asignar Propietario a Vehículo</h4>
            </div>
            <div class="card-body d-flex flex-column gap-3 align-items-center">
                <!-- Selección de Persona -->
                <asp:DropDownList ID="ddlPersonas" runat="server" CssClass="form-select border-pink text-center">
                    <asp:ListItem Text="Seleccione una persona" Value="" />
                </asp:DropDownList>

                <!-- Selección de Vehículo -->
                <asp:DropDownList ID="ddlVehiculos" runat="server" CssClass="form-select border-pink text-center">
                    <asp:ListItem Text="Seleccione un vehículo" Value="" />
                </asp:DropDownList>

                <!-- Botón para asignar -->
                <asp:Button ID="btnAsignar" runat="server" CssClass="btn btn-pink text-white fw-semibold" Text="Asignar Propietario" OnClick="btnAsignar_Click" />

                <asp:Label ID="lblMensaje" runat="server" CssClass="fw-bold text-danger" />
            </div>
        </div>
    </div>

    <!-- SweetAlert Script -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- Estilos rosados personalizados -->
    <style>
        .border-pink {
            border: 1px solid #d63384 !important;
        }
        .btn-pink {
            background-color: #d63384;
            border: none;
        }
        .btn-pink:hover {
            background-color: #c2185b;
        }
    </style>
</asp:Content>