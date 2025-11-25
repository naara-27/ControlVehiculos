Public Class SiteMaster
    Inherits MasterPage
    Protected Autenticado As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim Usuario As Usuario = Session("Usuario")
        Autenticado = Usuario IsNot Nothing

        If Autenticado Then
            Dim esAdmin As Boolean = Usuario.Rol = "2"
            liAdmin.Visible = esAdmin
            liPropietario.Visible = esAdmin
            liPersona.Visible = esAdmin
            liVehiculo.Visible = True
            liInicio.Visible = True
        End If
    End Sub

    Protected Sub LogOut_Click(sender As Object, e As EventArgs)
        Session.Clear()
        Session.Abandon()
        Response.Redirect("Login.aspx")
    End Sub
End Class