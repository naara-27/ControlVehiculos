<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Admin.aspx.vb" Inherits="ControlVehiculos.Admin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-sm border-0" style="background-color:#fff0f5;">
            <div class="card-header text-white" style="background-color:#d63384;">
                <h4 class="mb-0">Panel de Administración</h4>
            </div>
            <div class="card-body">
                <div class="mb-3">
                    <label class="form-label fw-bold text-pink">Usuario:</label>
                    <asp:Label ID="lblUsuario" runat="server" CssClass="form-control-plaintext text-dark fw-semibold" />
                </div>
                <div class="mb-3">
                    <label class="form-label fw-bold text-pink">Email:</label>
                    <asp:Label ID="lblEmail" runat="server" CssClass="form-control-plaintext text-dark fw-semibold" />
                </div>
            </div>
        </div>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script type="text/javascript">
        window.onload = function () {
            Swal.fire({
                title: '¡Bienvenida!',
                text: 'Estás en el panel de administración',
                icon: 'info',
                confirmButtonColor: '#d63384',
                confirmButtonText: 'Entendido'
            });
        };
    </script>
    <style>
        .text-pink {
            color: #d63384;
        }
    </style>
</asp:Content>