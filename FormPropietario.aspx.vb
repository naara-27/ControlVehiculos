Imports ControlVehiculos.Utils

Public Class FormPropietario
    Inherits System.Web.UI.Page

    Protected dbPropietario As New dbPropietario()
    Private dbHelper As New DbHelper()

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
            LimpiarCampos()
            Return
        End If

        Dim idPersona As Integer = Convert.ToInt32(ddlPersonas.SelectedValue)
        Dim dt As DataTable = dbPropietario.ConsultaPorPersona(idPersona)

        If dt.Rows.Count > 0 Then
            Dim row = dt.Rows(0)
            txtNombre.Text = row("Nombre").ToString()
            txtApellido1.Text = row("Apellido1").ToString()
            txtApellido2.Text = row("Apellido2").ToString()
            txtNacionalidad.Text = row("Nacionalidad").ToString()
            txtFechaNacimiento.Text = Convert.ToDateTime(row("FechaNacimiento")).ToString("yyyy-MM-dd")
            txtTelefono.Text = row("Telefono").ToString()

            If String.IsNullOrEmpty(row("Placa").ToString()) Then
                txtPlaca.Text = ""
                txtMarca.Text = ""
                txtModelo.Text = ""
                SwalUtils.ShowSwalError(Me, "Sin vehículo", "Esta persona no tiene vehículo asignado.")
            Else
                txtPlaca.Text = row("Placa").ToString()
                txtMarca.Text = row("Marca").ToString()
                txtModelo.Text = row("Modelo").ToString()
            End If
        Else
            LimpiarCampos()
            SwalUtils.ShowSwalError(Me, "Sin datos", "No se encontró información para esta persona.")
        End If
    End Sub

    Private Sub LimpiarCampos()
        txtNombre.Text = ""
        txtApellido1.Text = ""
        txtApellido2.Text = ""
        txtNacionalidad.Text = ""
        txtFechaNacimiento.Text = ""
        txtTelefono.Text = ""
        txtPlaca.Text = ""
        txtMarca.Text = ""
        txtModelo.Text = ""
    End Sub
End Class