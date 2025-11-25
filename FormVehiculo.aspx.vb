Imports ControlVehiculos.dbVehiculo
Imports ControlVehiculos.Utils

Public Class FormVehiculo
    Inherits System.Web.UI.Page

    Public vehiculo As New Vehiculo()
    Protected dbHelper As New dbVehiculo()
    Private dbQuery As New DbHelper()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim tipoUsuario As Integer = Convert.ToInt32(Session("Usuario").Rol)

            If tipoUsuario = 2 Then
                ' Solo administrador carga propietarios
                Dim dbPersona As New dbPersona()
                Dim dt As DataTable = dbPersona.ConsultaActivas()
                ddlPropietario.DataSource = dt
                ddlPropietario.DataTextField = "NombreCompleto"
                ddlPropietario.DataValueField = "IdPersona"
                ddlPropietario.DataBind()
                ddlPropietario.Items.Insert(0, New ListItem("Seleccione propietario", ""))
            Else
                ' Usuario normal: oculta formulario completo
                panelFormulario.Visible = False
                gvVehiculos.Columns(0).Visible = False ' Oculta botón Editar
                gvVehiculos.Columns(6).Visible = False ' Oculta botón Eliminar
            End If

            CargarVehiculos()
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        If Convert.ToInt32(Session("Usuario").Rol) <> 2 Then Exit Sub

        Try
            If String.IsNullOrEmpty(ddlPropietario.SelectedValue) Then
                SwalUtils.ShowSwalError(Me, "Error", "Debe seleccionar una persona para convertirla en propietario")
                Exit Sub
            End If

            Dim idPersona As Integer = Convert.ToInt32(ddlPropietario.SelectedValue)
            Dim dbProp As New dbPropietario()

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

            CargarVehiculos()
        Catch ex As Exception
            lblMensaje.Text = "Error al guardar el vehículo: " & ex.Message
            SwalUtils.ShowSwalError(Me, "Error al guardar el vehículo", ex.Message)
        End Try
    End Sub

    Private Sub CargarVehiculos()
        Try
            Dim sql As String = "
                SELECT 
                    v.IdVehiculo,
                    v.Placa,
                    v.Marca,
                    v.Modelo,
                    ISNULL(CONCAT(p.Nombre, ' ', p.Apellido1, ' ', p.Apellido2), '') AS NombrePropietario
                FROM Vehiculos v
                LEFT JOIN Propietarios pr ON v.IdPropietario = pr.IdPropietario
                LEFT JOIN Personas p ON pr.IdPersona = p.IdPersona"
            Dim dt As DataTable = dbQuery.ExecuteQuery(sql)
            gvVehiculos.DataSource = dt
            gvVehiculos.DataBind()
        Catch ex As Exception
            lblMensaje.Text = "Error al cargar vehículos: " & ex.Message
        End Try
    End Sub

    Protected Sub gvVehiculos_RowEditing(sender As Object, e As GridViewEditEventArgs)
        If Convert.ToInt32(Session("Usuario").Rol) <> 2 Then Exit Sub
        gvVehiculos.EditIndex = e.NewEditIndex
        CargarVehiculos()
    End Sub

    Protected Sub gvVehiculos_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvVehiculos.EditIndex = -1
        CargarVehiculos()
    End Sub

    Protected Sub gvVehiculos_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        If Convert.ToInt32(Session("Usuario").Rol) <> 2 Then Exit Sub

        Try
            Dim idVehiculo As Integer = Convert.ToInt32(gvVehiculos.DataKeys(e.RowIndex).Value)
            Dim row As GridViewRow = gvVehiculos.Rows(e.RowIndex)

            Dim placa As String = CType(row.Cells(2).Controls(0), TextBox).Text
            Dim marca As String = CType(row.Cells(3).Controls(0), TextBox).Text
            Dim modelo As String = CType(row.Cells(4).Controls(0), TextBox).Text

            Dim vehiculo = New Vehiculo With {
                .IdVehiculo = idVehiculo,
                .Placa = placa,
                .Marca = marca,
                .Modelo = modelo
            }

            Dim mensaje = dbHelper.update(vehiculo)
            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error al actualizar", mensaje)
            Else
                SwalUtils.ShowSwal(Me, "Vehículo actualizado correctamente")
            End If

            gvVehiculos.EditIndex = -1
            CargarVehiculos()
        Catch ex As Exception
            SwalUtils.ShowSwalError(Me, "Error inesperado", ex.Message)
        End Try
    End Sub

    Protected Sub gvVehiculos_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        If Convert.ToInt32(Session("Usuario").Rol) <> 2 Then Exit Sub

        Try
            Dim idVehiculo As Integer = Convert.ToInt32(gvVehiculos.DataKeys(e.RowIndex).Value)
            Dim mensaje As String = dbHelper.delete(idVehiculo)

            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error al eliminar", mensaje)
            Else
                SwalUtils.ShowSwal(Me, "Vehículo eliminado correctamente")
            End If

            CargarVehiculos()
        Catch ex As Exception
            SwalUtils.ShowSwalError(Me, "Error inesperado", ex.Message)
        End Try
    End Sub
End Class