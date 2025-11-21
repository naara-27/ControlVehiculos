Imports ControlVehiculos.Utils
Imports ControlVehiculos.dbVehiculo

Public Class FormPropietario
    Inherits System.Web.UI.Page

    Protected dbPropietario As New dbPropietario()
    Private dbHelper As New DbHelper()
    Private dbVehiculoHelper As New dbVehiculo()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarPersonas()
        End If
    End Sub

    Private Sub CargarPersonas()
        Dim tabla As DataTable = dbHelper.ExecuteQuery("
            SELECT IdPersona, CONCAT(Nombre, ' ', Apellido1, ' ', Apellido2) AS NombreCompleto
            FROM Personas
        ")
        ddlPersonas.DataSource = tabla
        ddlPersonas.DataTextField = "NombreCompleto"
        ddlPersonas.DataValueField = "IdPersona"
        ddlPersonas.DataBind()
        ddlPersonas.Items.Insert(0, New ListItem("Seleccione una persona", ""))
    End Sub

    Protected Sub ddlPersonas_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlPersonas.SelectedValue = "" Then
            gvVehiculosPersona.DataSource = Nothing
            gvVehiculosPersona.DataBind()
            Return
        End If

        Dim idPersona As Integer = Convert.ToInt32(ddlPersonas.SelectedValue)
        Dim dt As DataTable = dbPropietario.ConsultaPorPersona(idPersona)

        If dt.Rows.Count > 0 Then
            gvVehiculosPersona.DataSource = dt
            gvVehiculosPersona.DataBind()
        Else
            gvVehiculosPersona.DataSource = Nothing
            gvVehiculosPersona.DataBind()
            SwalUtils.ShowSwalError(Me, "Sin datos", "No se encontró información para esta persona.")
        End If
    End Sub

    Protected Sub gvVehiculosPersona_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        Try
            Dim idVehiculo As Integer = Convert.ToInt32(gvVehiculosPersona.DataKeys(e.RowIndex).Value)
            Dim mensaje = dbVehiculoHelper.delete(idVehiculo)

            If mensaje.Contains("Error") Then
                SwalUtils.ShowSwalError(Me, "Error al eliminar", mensaje)
            Else
                SwalUtils.ShowSwal(Me, "Vehículo eliminado correctamente")
            End If

            ddlPersonas_SelectedIndexChanged(Nothing, Nothing)
        Catch ex As Exception
            SwalUtils.ShowSwalError(Me, "Error inesperado", ex.Message)
        End Try
    End Sub
End Class