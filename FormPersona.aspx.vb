Imports System.Web.Services.Description
Imports ControlVehiculos.Utils

Public Class FormPersona
    Inherits System.Web.UI.Page
    Public persona As New Persona()
    Protected dbHelper As New dbPersona()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            btn_guardar.Visible = True
            btnActualizar.Visible = False
            btn_regresar.Visible = False
        End If
    End Sub

    Protected Sub btn_guardar_Click(sender As Object, e As EventArgs)
        Try
            persona.Nombre = txtNombre.Text
            persona.Apellido1 = txtApellido1.Text
            persona.Apellido2 = txtApellido2.Text
            persona.Nacionalidad = ddlNacionalidad.SelectedValue
            persona.FechaNacimiento = txtfechaNacimiento.Text
            persona.Telefono = txtTelefono.Text

            Dim mensaje = dbHelper.create(persona)
            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error", mensaje)
            Else
                SwalUtils.ShowSwal(Me, mensaje)
            End If

            limpiarCampos()
            gvPersonas.DataBind()

            btn_guardar.Visible = True
            btnActualizar.Visible = False
            btn_regresar.Visible = False
        Catch ex As Exception
            lblMensaje.Text = "Error al guardar la persona: " & ex.Message
            SwalUtils.ShowSwalError(Me, "Error al guardar la persona", ex.Message)
        End Try
    End Sub

    Protected Sub gvPersonas_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Try
            Dim id As Integer = Convert.ToInt32(gvPersonas.DataKeys(e.RowIndex).Value)
            Dim mensaje = dbHelper.delete(id)

            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error", mensaje)
            Else
                SwalUtils.ShowSwal(Me, mensaje)
            End If

            e.Cancel = True
            gvPersonas.DataBind()
        Catch ex As Exception
            lblMensaje.Text = "Error al eliminar la persona: " & ex.Message
            SwalUtils.ShowSwalError(Me, "Error al eliminar la persona", ex.Message)
        End Try
    End Sub

    Protected Sub gvPersonas_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gvPersonas.SelectedRow()
        editando.Value = gvPersonas.DataKeys(row.RowIndex).Value

        txtNombre.Text = row.Cells(2).Text
        txtApellido1.Text = row.Cells(3).Text
        txtApellido2.Text = row.Cells(4).Text
        ddlNacionalidad.SelectedValue = Server.HtmlDecode(row.Cells(5).Text)
        txtfechaNacimiento.Text = Convert.ToDateTime(row.Cells(6).Text).ToString("yyyy-MM-dd")
        txtTelefono.Text = row.Cells(7).Text

        btn_guardar.Visible = False
        btnActualizar.Visible = True
        btn_regresar.Visible = True
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)
        Try
            Dim persona As Persona = New Persona With {
                .IdPersona = editando.Value(),
                .Nombre = txtNombre.Text,
                .Apellido1 = txtApellido1.Text,
                .Apellido2 = txtApellido2.Text,
                .Nacionalidad = ddlNacionalidad.SelectedValue,
                .FechaNacimiento = txtfechaNacimiento.Text,
                .Telefono = txtTelefono.Text
            }

            Dim mensaje = dbHelper.update(persona)
            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error", mensaje)
            Else
                SwalUtils.ShowSwal(Me, mensaje)
            End If

            gvPersonas.DataBind()
            limpiarCampos()

            btn_guardar.Visible = True
            btnActualizar.Visible = False
            btn_regresar.Visible = False
        Catch ex As Exception
            SwalUtils.ShowSwalError(Me, "Error al actualizar la persona", ex.Message)
        End Try
    End Sub

    Protected Sub btn_regresar_Click(sender As Object, e As EventArgs)
        limpiarCampos()
        gvPersonas.SelectedIndex = -1
        btn_guardar.Visible = True
        btnActualizar.Visible = False
        btn_regresar.Visible = False
        lblMensaje.Text = "Edición cancelada."
    End Sub

    Private Sub limpiarCampos()
        txtNombre.Text = ""
        txtApellido1.Text = ""
        txtApellido2.Text = ""
        ddlNacionalidad.SelectedIndex = 0
        txtfechaNacimiento.Text = ""
        txtTelefono.Text = ""
    End Sub
End Class