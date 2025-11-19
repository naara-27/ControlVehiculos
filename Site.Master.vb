Public Class SiteMaster
    Inherits MasterPage
    Protected Autenticado As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim Usuario As Usuario = Session("Usuario")
        Autenticado = Usuario IsNot Nothing ' Verificar si el usuario está autenticado
        Dim esAdmin As Boolean = Usuario?.Rol = "2" ' Verificar si el usuario es administrador
        liAdmin.Visible = esAdmin ' Mostrar enlace de administración solo para administradores
    End Sub

    Protected Sub LogOut_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Login.aspx")
    End Sub
End Class