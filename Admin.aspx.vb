Public Class Admin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Usuario As Usuario = Session("Usuario")
        If Usuario Is Nothing Then
            Response.Redirect("Login.aspx")
            Return
        End If
        If Usuario.Rol <> "2" Then
            Response.Redirect("Home.aspx")
            Return
        End If
        lblUsuario.Text = "Bienvenido, " & Usuario.NombreUsuario
        lblEmail.Text = "Correo electrónico, " & Usuario.Email
    End Sub

End Class