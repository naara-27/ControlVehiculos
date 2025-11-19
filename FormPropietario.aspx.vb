Imports System.Web.Services.Description
Imports ControlVehiculos.Utils
Public Class FormPropietario
    Inherits System.Web.UI.Page
    Public propietario As New Propietario()
    Protected dbPropietario As New dbPropietario()
    Protected dbVehiculo As New dbVehiculo()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarPersonas()
            CargarVehiculos()
        End If
    End Sub

    Private Sub CargarPersonas()
        Dim tabla As DataTable = dbPropietario.Consulta()
        ddlPersonas.DataSource = tabla
        ddlPersonas.DataTextField = "NombreCompleto"
        ddlPersonas.DataValueField = "IdPersona"
        ddlPersonas.DataBind()
        ddlPersonas.Items.Insert(0, New ListItem("Seleccione una persona", ""))
    End Sub

    Private Sub CargarVehiculos()
        Dim tabla As DataTable = dbVehiculo.Consulta()
        Dim sinPropietario = tabla.Select("IdPropietario IS NULL")
        If sinPropietario.Length > 0 Then
            ddlVehiculos.DataSource = sinPropietario.CopyToDataTable()
            ddlVehiculos.DataTextField = "Placa"
            ddlVehiculos.DataValueField = "IdVehiculo"
            ddlVehiculos.DataBind()
        Else
            ddlVehiculos.Items.Clear()
        End If
        ddlVehiculos.Items.Insert(0, New ListItem("Seleccione un vehículo", ""))
    End Sub

    Protected Sub btnAsignar_Click(sender As Object, e As EventArgs)
        Dim idPersona = ddlPersonas.SelectedValue
        Dim idVehiculo = ddlVehiculos.SelectedValue

        If idPersona = "" OrElse idVehiculo = "" Then
            SwalUtils.ShowSwalError(Me, "Campos incompletos", "Debe seleccionar una persona y un vehículo.")
            Return
        End If

        ' Crear propietario
        Dim mensajePropietario = dbPropietario.create(Convert.ToInt32(idPersona))
        If mensajePropietario.Contains("Error") Then
            SwalUtils.ShowSwalError(Me, "Error al registrar propietario", mensajePropietario)
            Return
        End If

        ' Obtener el último IdPropietario creado
        Dim propietarios = dbPropietario.Consulta()
        Dim ultimo = propietarios.Select("IdPersona = " & idPersona).LastOrDefault()
        If ultimo Is Nothing Then
            SwalUtils.ShowSwalError(Me, "Error", "No se pudo recuperar el propietario recién creado.")
            Return
        End If

        Dim idPropietario = Convert.ToInt32(ultimo("IdPropietario"))

        ' Actualizar vehículo con el nuevo propietario
        Dim vehiculoActualizado As New Vehiculo(idVehiculo, "", "", "", idPropietario)
        Dim mensajeVehiculo = dbVehiculo.update(vehiculoActualizado)

        If mensajeVehiculo.Contains("Error") Then
            SwalUtils.ShowSwalError(Me, "Error al asignar vehículo", mensajeVehiculo)
        Else
            SwalUtils.ShowSwal(Me, "Propietario asignado correctamente")
            ddlPersonas.ClearSelection()
            ddlVehiculos.ClearSelection()
        End If
    End Sub
End Class