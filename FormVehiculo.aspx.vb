Imports ControlVehiculos.dbVehiculo
Imports ControlVehiculos.Utils

Public Class FormVehiculo
    Inherits System.Web.UI.Page

    Public vehiculo As New Vehiculo()
    Protected dbHelper As New dbVehiculo()
    Private dbQuery As New DbHelper() ' Para consultas directas como ExecuteQuery

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim dbPersona As New dbPersona()
            Dim dt As DataTable = dbPersona.ConsultaActivas()
            ddlPropietario.DataSource = dt
            ddlPropietario.DataTextField = "NombreCompleto"
            ddlPropietario.DataValueField = "IdPersona"
            ddlPropietario.DataBind()
            ddlPropietario.Items.Insert(0, New ListItem("Seleccione propietario", ""))
            CargarAsignados()
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Try
            If String.IsNullOrEmpty(ddlPropietario.SelectedValue) Then
                SwalUtils.ShowSwalError(Me, "Error", "Debe seleccionar una persona para convertirla en propietario")
                Exit Sub
            End If

            Dim idPersona As Integer = Convert.ToInt32(ddlPropietario.SelectedValue)
            Dim dbProp As New dbPropietario()

            Dim dtVehiculoExistente As DataTable = dbHelper.ConsultaPorPersona(idPersona)
            If dtVehiculoExistente.Rows.Count > 0 Then
                SwalUtils.ShowSwalError(Me, "Asignación inválida", "Esta persona ya tiene un vehículo asignado.")
                Exit Sub
            End If

            Dim mensajeProp = dbProp.create(idPersona)
            If mensajeProp.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error al crear propietario", mensajeProp)
                Exit Sub
            End If

            Dim idPropietario As Integer = dbProp.GetLastInsertedId(idPersona)
            If idPropietario = 0 Then
                SwalUtils.ShowSwalError(Me, "Error", "No se pudo obtener el Id del propietario")
                Exit Sub
            End If

            vehiculo.Placa = txtPlaca.Text
            vehiculo.Marca = ddlMarca.SelectedValue
            vehiculo.Modelo = ddlModelo.SelectedValue
            vehiculo.IdPropietario = idPropietario

            Dim mensaje = dbHelper.create(vehiculo)
            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error", mensaje)
            Else
                SwalUtils.ShowSwal(Me, mensaje)
            End If

            txtPlaca.Text = ""
            ddlMarca.SelectedIndex = 0
            ddlModelo.SelectedIndex = 0
            ddlPropietario.SelectedIndex = 0
            gvVehiculo.DataBind()
            CargarAsignados()

        Catch ex As Exception
            lblMensaje.Text = "Error al guardar el vehículo: " & ex.Message
            SwalUtils.ShowSwalError(Me, "Error al guardar el vehículo", ex.Message)
        End Try
    End Sub

    Private Sub CargarAsignados()
        Try
            Dim sql As String = "
                SELECT 
                    p.IdPropietario,
                    CONCAT(pe.Nombre, ' ', pe.Apellido1, ' ', pe.Apellido2) AS NombreCompleto,
                    v.Placa,
                    v.Marca,
                    v.Modelo
                FROM Propietarios p
                INNER JOIN Personas pe ON p.IdPersona = pe.IdPersona
                INNER JOIN Vehiculos v ON p.IdPropietario = v.IdPropietario"
            Dim dt As DataTable = dbQuery.ExecuteQuery(sql)
            gvAsignados.DataSource = dt
            gvAsignados.DataBind()
        Catch ex As Exception
            lblMensaje.Text = "Error al cargar asignados: " & ex.Message
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
            CargarAsignados()
        Catch ex As Exception
            lblMensaje.Text = "Error al eliminar el vehículo: " & ex.Message
            SwalUtils.ShowSwalError(Me, "Error al eliminar el vehículo", ex.Message)
        End Try
    End Sub

    Protected Sub gvVehiculo_RowEditing(sender As Object, e As GridViewEditEventArgs)
        gvVehiculo.EditIndex = e.NewEditIndex
        gvVehiculo.DataBind()
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
            gvVehiculo.EditIndex = -1
            gvVehiculo.DataBind()
            CargarAsignados()
        Catch ex As Exception
            SwalUtils.ShowSwalError(Me, "Error al actualizar el vehículo", ex.Message)
        End Try
    End Sub

    Protected Sub gvVehiculo_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = gvVehiculo.SelectedRow()
        txtPlaca.Text = row.Cells(3).Text
        ddlMarca.SelectedValue = row.Cells(4).Text
        ddlModelo.SelectedValue = row.Cells(5).Text
    End Sub

    Protected Sub btnActualizar_Click(sender As Object, e As EventArgs)
        Dim vehiculo As Vehiculo = New Vehiculo With {
            .Placa = txtPlaca.Text(),
            .Marca = ddlMarca.SelectedValue,
            .Modelo = ddlModelo.SelectedValue,
            .IdPropietario = Convert.ToInt32(ddlPropietario.SelectedValue),
            .IdVehiculo = editando.Value()
        }
        dbHelper.update(vehiculo)
        gvVehiculo.DataBind()
        gvVehiculo.EditIndex = -1
        CargarAsignados()
    End Sub

    Protected Sub gvAsignados_RowEditing(sender As Object, e As GridViewEditEventArgs)
        gvAsignados.EditIndex = e.NewEditIndex
        CargarAsignados()
    End Sub

    Protected Sub gvAsignados_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvAsignados.EditIndex = -1
        CargarAsignados()
    End Sub

    Protected Sub gvAsignados_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Try
            Dim idPropietario As Integer = Convert.ToInt32(gvAsignados.DataKeys(e.RowIndex).Value)
            Dim row As GridViewRow = gvAsignados.Rows(e.RowIndex)

            Dim marca As String = CType(row.Cells(4).Controls(0), TextBox).Text
            Dim modelo As String = CType(row.Cells(5).Controls(0), TextBox).Text

            Dim mensaje = dbHelper.ActualizarMarcaModeloPorPropietario(idPropietario, marca, modelo)

            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error al actualizar", mensaje)
            Else
                SwalUtils.ShowSwal(Me, "Vehículo actualizado correctamente")
            End If

            gvAsignados.EditIndex = -1
            CargarAsignados()
        Catch ex As Exception
            SwalUtils.ShowSwalError(Me, "Error inesperado", ex.Message)
        End Try
    End Sub

    Protected Sub gvAsignados_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Try
            Dim idPropietario As Integer = Convert.ToInt32(gvAsignados.DataKeys(e.RowIndex).Value)
            Dim mensaje = dbHelper.DesasignarVehiculo(idPropietario)

            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error al eliminar", mensaje)
            Else
                SwalUtils.ShowSwal(Me, "Vehículo desasignado correctamente")
            End If

            CargarAsignados()
        Catch ex As Exception
            SwalUtils.ShowSwalError(Me, "Error inesperado", ex.Message)
        End Try
    End Sub
End Class