Imports System.Web.Services.Description
Imports ControlVehiculos.dbVehiculo
Imports ControlVehiculos.Utils

Public Class FormVehiculo
    Inherits System.Web.UI.Page
    Public vehiculo As New Vehiculo()
    Protected dbHelper As New dbVehiculo()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btn_guardar(sender As Object, e As EventArgs)
        Try

            vehiculo.Placa = txtPlaca.Text
            vehiculo.Marca = ddlMarca.SelectedValue
            vehiculo.Modelo = ddlModelo.SelectedValue
            vehiculo.IdPropietario = ""

            Dim mensaje = dbHelper.create(vehiculo)
            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error", mensaje)
            Else
                SwalUtils.ShowSwal(Me, mensaje)
            End If

            txtPlaca.Text = ""
            ddlMarca.SelectedIndex = 0
            ddlModelo.SelectedIndex = 0

            gvVehiculo.DataBind()
        Catch ex As Exception
            lblMensaje.Text = "Error al guardar el vehiculo: " & ex.Message
            SwalUtils.ShowSwalError(Me, "Error al guardar el vehiculo", ex.Message)
        End Try

    End Sub

    Protected Sub gvVehiculo_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Try
            Dim id As Integer = Convert.ToInt32(gvVehiculo.DataKeys(e.RowIndex).Value)

            Dim mensaje = dbHelper.delete(id)
            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error", mensaje)
            Else
                SwalUtils.ShowSwal(Me, mensaje)
            End If
            e.Cancel = True
            gvVehiculo.DataBind()
        Catch ex As Exception
            lblMensaje.Text = "Error al eliminar la persona: " & ex.Message
            SwalUtils.ShowSwalError(Me, "Error al eliminar la persona", ex.Message)
        End Try
    End Sub

    Protected Sub gvVehiculo_RowEditing(sender As Object, e As GridViewEditEventArgs)

    End Sub

    Protected Sub gvVehiculo_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvVehiculo.EditIndex = -1
        gvVehiculo.DataBind()
    End Sub

    Protected Sub gvVehiculo_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Try
            Dim id As Integer = Convert.ToInt32(gvVehiculo.DataKeys(e.RowIndex).Value)
            Dim vehiculo = New Vehiculo With {
                .Placa = e.NewValues("Placa"),
                .Marca = e.NewValues("Marca"),
                .Modelo = e.NewValues("Modelo"),
                .IdVehiculo = id
            }

            Dim mensaje = dbHelper.update(vehiculo)
            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error", mensaje)
            Else
                SwalUtils.ShowSwal(Me, mensaje)
            End If
            gvVehiculo.DataBind()
            e.Cancel = True
            gvVehiculo.EditIndex = -1
        Catch ex As Exception
            SwalUtils.ShowSwalError(Me, "Error al actulizar la persona", ex.Message)
        End Try
    End Sub

    Protected Sub gvVehiculo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gvVehiculo.SelectedRow()
        Dim persona As Persona = New Persona()
        txtPlaca.Text = row.Cells(3).Text
        ddlMarca.SelectedValue = row.Cells(4).Text
        ddlModelo.SelectedValue = row.Cells(5).Text
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)
        Dim vehiculo As Vehiculo = New Vehiculo With {
           .Placa = txtPlaca.Text(),
           .Marca = ddlMarca.SelectedValue,
           .Modelo = ddlModelo.SelectedValue,
           .IdVehiculo = editando.Value()
       }
        dbHelper.update(vehiculo)
        gvVehiculo.DataBind()
        gvVehiculo.EditIndex = -1
    End Sub
End Class