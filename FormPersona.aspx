<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FormPersona.aspx.vb" Inherits="ControlVehiculos.FormPersona" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="editando" runat="server" />

    <div class="container mt-4 d-flex justify-content-center">
        <div class="card shadow-sm border-0 w-100" style="max-width:600px; background-color:#fff0f5;">
            <div class="card-header text-white text-center" style="background-color:#d63384;">
                <h4 class="mb-0">Formulario de Persona</h4>
            </div>
            <div class="card-body d-flex flex-column gap-3 align-items-center">

                <asp:ValidationSummary ID="vsPersona" runat="server" ValidationGroup="vgPersona" CssClass="alert alert-warning w-100 text-center" HeaderText="Corrige los siguientes errores:" />

                <asp:TextBox ID="txtNombre" CssClass="form-control border-pink text-center" placeholder="Nombre" runat="server" />
                <asp:RequiredFieldValidator ID="rfNombre" runat="server" ControlToValidate="txtNombre" ValidationGroup="vgPersona" Display="None" ErrorMessage="El nombre es obligatorio" />

                <asp:TextBox ID="txtApellido1" CssClass="form-control border-pink text-center" placeholder="Primer Apellido" runat="server" />
                <asp:RequiredFieldValidator ID="rfApellido1" runat="server" ControlToValidate="txtApellido1" ValidationGroup="vgPersona" Display="None" ErrorMessage="El primer apellido es obligatorio" />

                <asp:TextBox ID="txtApellido2" CssClass="form-control border-pink text-center" placeholder="Segundo Apellido" runat="server" />

                <asp:DropDownList ID="ddlNacionalidad" runat="server" CssClass="form-select border-pink text-center">
                    <asp:ListItem Text="Seleccione nacionalidad" Value="" />
                    <asp:ListItem Text="Costarricense" Value="Costarricense" />
                    <asp:ListItem Text="Salvadoreño" Value="Salvadoreño" />
                    <asp:ListItem Text="Guatemalteco" Value="Guatemalteco" />
                    <asp:ListItem Text="Hondureño" Value="Hondureño" />
                    <asp:ListItem Text="Nicaragüense" Value="Nicaragüense" />
                    <asp:ListItem Text="Panameño" Value="Panameño" />
                    <asp:ListItem Text="Beliceño" Value="Beliceño" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfNacionalidad" runat="server" ControlToValidate="ddlNacionalidad" InitialValue="" ValidationGroup="vgPersona" Display="None" ErrorMessage="Debe seleccionar una nacionalidad" />

                <asp:TextBox ID="txtfechaNacimiento" CssClass="form-control border-pink text-center" placeholder="Fecha de Nacimiento" runat="server" TextMode="Date" />
                <asp:RequiredFieldValidator ID="rfFecha" runat="server" ControlToValidate="txtfechaNacimiento" ValidationGroup="vgPersona" Display="None" ErrorMessage="La fecha de nacimiento es obligatoria" />

                <asp:TextBox ID="txtTelefono" CssClass="form-control border-pink text-center" placeholder="Teléfono" runat="server" />
                <asp:RequiredFieldValidator ID="rfTelefono" runat="server" ControlToValidate="txtTelefono" ValidationGroup="vgPersona" Display="None" ErrorMessage="El teléfono es obligatorio" />

                <div class="d-flex gap-2">
                    <asp:Button ID="btn_guardar" runat="server" CssClass="btn btn-pink text-white fw-semibold" Text="Guardar" OnClick="btn_guardar_Click" ValidationGroup="vgPersona" />
                    <asp:Button ID="btnActualizar" runat="server" CssClass="btn btn-rose-dark fw-semibold" Text="Actualizar" OnClick="btnActualizar_Click" ValidationGroup="vgPersona" />
                    <asp:Button ID="btn_regresar" runat="server" CssClass="btn btn-secondary fw-semibold" Text="Cancelar" OnClick="btn_regresar_Click" CausesValidation="False" />
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="fw-bold text-danger" />
            </div>
        </div>
    </div>

    <div class="container mt-4">
        <asp:GridView ID="gvPersonas" CssClass="table table-striped table-hover border border-pink" runat="server" AutoGenerateColumns="False"
            DataSourceID="SqlDataSource1" DataKeyNames="idPersona"
            OnRowDeleting="gvPersonas_RowDeleting"
            OnSelectedIndexChanged="gvPersonas_SelectedIndexChanged">
            <Columns>
                <asp:CommandField ShowSelectButton="True" ControlStyle-CssClass="btn btn-rose-light" />
                <asp:BoundField DataField="idPersona" HeaderText="ID" Visible="False" ReadOnly="True" SortExpression="idPersona" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" SortExpression="Nombre" />
                <asp:BoundField DataField="Apellido1" HeaderText="Primer Apellido" SortExpression="Apellido1" />
                <asp:BoundField DataField="Apellido2" HeaderText="Segundo Apellido" SortExpression="Apellido2" />
                <asp:BoundField DataField="Nacionalidad" HeaderText="Nacionalidad" SortExpression="Nacionalidad" />
                <asp:BoundField DataField="FechaNacimiento" HeaderText="Fecha Nacimiento" SortExpression="FechaNacimiento" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" SortExpression="Telefono" />
                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger" />
            </Columns>
        </asp:GridView>
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:II46_P3ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:II46_P3ConnectionString.ProviderName %>" 
        SelectCommand="SELECT * FROM [Personas]"></asp:SqlDataSource>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

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
        .btn-outline-pink {
            border: 1px solid #d63384;
            color: #d63384;
            background-color: transparent;
        }
        .btn-outline-pink:hover {
            background-color: #d63384;
            color: white;
        }
        .btn-rose-light {
            background-color: #f8b7d4;
            color: white;
            border: none;
        }
        .btn-rose-light:hover {
            background-color: #f48fb1;
        }
        .btn-rose-dark {
            background-color: #c2185b;
            color: white;
            border: none;
        }
        .btn-rose-dark:hover {
            background-color: #ad1457;
        }
    </style>
</asp:Content>