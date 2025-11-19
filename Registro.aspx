<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Registro.aspx.vb" Inherits="ControlVehiculos.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-4">
        <div class="card shadow-sm border-0" style="background-color:#fff0f5;">
            <div class="card-header text-white" style="background-color:#d63384;">
                <h4 class="mb-0">Registro de Usuario</h4>
            </div>
            <div class="card-body">
                <asp:Label ID="lblMensaje" runat="server" CssClass="d-block mb-3 fw-bold" ForeColor="Red" />

                <div class="mb-3">
                    <label class="form-label text-pink fw-semibold">Usuario:</label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control border-pink" />
                </div>

                <div class="mb-3">
                    <label class="form-label text-pink fw-semibold">Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control border-pink" TextMode="Email" />
                </div>

                <div class="mb-3">
                    <label class="form-label text-pink fw-semibold">Contraseña:</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control border-pink" TextMode="Password" />
                </div>

                <div class="mb-3">
                    <label class="form-label text-pink fw-semibold">Confirmar Contraseña:</label>
                    <asp:TextBox ID="txtConfirmarPassword" runat="server" CssClass="form-control border-pink" TextMode="Password" />
                </div>

                <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" CssClass="btn btn-pink text-white fw-bold mt-2" />
            </div>
        </div>
    </div>

    
    <style>
        .text-pink {
            color: #d63384;
        }
        .border-pink {
            border: 1px solid #d63384;
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
