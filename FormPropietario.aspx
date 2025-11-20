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
                <asp:DropDownList ID="ddlPersonas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPersonas_SelectedIndexChanged" CssClass="form-select border-pink text-center">
                    <asp:ListItem Text="Seleccione una persona" Value="" />
                </asp:DropDownList>

                <!-- Datos personales -->
                <div class="row">
                    <div class="col-md-4">
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control text-center" ReadOnly="True" placeholder="Nombre" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtApellido1" runat="server" CssClass="form-control text-center" ReadOnly="True" placeholder="Apellido 1" />
                    </div>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtApellido2" runat="server" CssClass="form-control text-center" ReadOnly="True" placeholder="Apellido 2" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtNacionalidad" runat="server" CssClass="form-control text-center" ReadOnly="True" placeholder="Nacionalidad" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtFechaNacimiento" runat="server" CssClass="form-control text-center" ReadOnly="True" placeholder="Nacimiento" />
                    </div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control text-center" ReadOnly="True" placeholder="Teléfono" />
                    </div>
                </div>

                <hr />

                <!-- Vehículo asignado -->
                <label class="fw-semibold">Vehículo asignado</label>
                <asp:TextBox ID="txtPlaca" runat="server" CssClass="form-control text-center" ReadOnly="True" placeholder="Placa" />
                <div class="row">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control text-center" ReadOnly="True" placeholder="Marca" />
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox ID="txtModelo" runat="server" CssClass="form-control text-center" ReadOnly="True" placeholder="Modelo" />
                    </div>
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="fw-bold text-danger text-center" />

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