Public Class FormPersona
    Inherits System.Web.UI.Page
    Public persona As New Persona()
    Protected dbHelper As New dbPersona()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        persona.Nombre = txtNombre.Text
        persona.Apellido1 = txtApellido1.Text
        persona.Apellido2 = txtApellido2.Text
        persona.FechaNacimiento = txtFechaNacimiento.Text

        If dbHelper.create(persona) Then
            lblMensaje.Text = "Persona guardada correctamente."
            txtNombre.Text = ""
            txtApellido1.Text = ""
            txtApellido2.Text = ""
        Else
            lblMensaje.Text = "Error al guardar la persona."
        End If


        gvPersonas.DataBind()
    End Sub

    Protected Sub gvPersonas_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Try
            Dim ID As Integer = Convert.ToInt32(gvPersonas.DataKeys(e.RowIndex).Value)
            dbHelper.delete(ID)
            e.Cancel = True
            gvPersonas.DataBind()
        Catch ex As Exception
            lblMensaje.Text = "Error al eliminar la persona: " & ex.Message
        End Try
    End Sub

    Protected Sub gvPersonas_RowEditing(sender As Object, e As GridViewEditEventArgs)

    End Sub

    Protected Sub gvPersonas_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvPersonas.EditIndex = -1
        gvPersonas.DataBind()
    End Sub

    Protected Sub gvPersonas_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim ID As Integer = Convert.ToInt32(gvPersonas.DataKeys(e.RowIndex).Value)
        Dim persona As Persona = New Persona With {
            .Nombre = e.NewValues("Nombre"),
            .Apellido1 = e.NewValues("Apellido1"),
            .Apellido2 = e.NewValues("Apellido2"),
            .FechaNacimiento = e.NewValues("FechaNacimiento"),
            .IdPersona = ID
        }
        dbHelper.update(persona)
        gvPersonas.DataBind()
        e.Cancel = True
        gvPersonas.EditIndex = -1

    End Sub

    Protected Sub gvPersonas_SelectedIndexChanged(sender As Object, e As EventArgs)

        Dim row As GridViewRow = gvPersonas.SelectedRow()
        Dim ID As Integer = Convert.ToInt32(row.Cells(2).Text)
        Dim persona As Persona = New Persona()

        txtNombre.Text = row.Cells(3).Text
        txtApellido1.Text = row.Cells(4).Text
        txtApellido2.Text = row.Cells(5).Text
        txtFechaNacimiento.Text = row.Cells(6).Text
        editando.Value = ID
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)

        Dim persona As Persona = New Persona With {
            .Nombre = txtNombre.Text(),
            .Apellido1 = txtApellido1.Text(),
            .Apellido2 = txtApellido2.Text(),
            .FechaNacimiento = txtFechaNacimiento.Text(),
            .IdPersona = editando.Value()
        }
        dbHelper.update(persona)
        gvPersonas.DataBind()
        gvPersonas.EditIndex = -1
    End Sub

End Class