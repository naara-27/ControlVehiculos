Imports System.Data.SqlClient

Public Class dbPropietario
    Private ReadOnly dbHelper = New DbHelper()

    ' Crear propietario a partir de una persona
    Public Function create(idPersona As Integer) As String
        Try
            Dim sql As String = "INSERT INTO Propietarios (IdPersona) VALUES (@IdPersona)"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdPersona", idPersona)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return "Propietario creado correctamente"
        Catch ex As Exception
            Return "Error al crear propietario: " & ex.Message
        End Try
    End Function

    ' Obtener el último IdPropietario insertado para una persona
    Public Function GetLastInsertedId(idPersona As Integer) As Integer
        Try
            Dim sql As String = "SELECT TOP 1 IdPropietario FROM Propietarios WHERE IdPersona = @IdPersona ORDER BY IdPropietario DESC"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdPersona", idPersona)
            }
            Dim dt As DataTable = dbHelper.ExecuteQuery(sql, parametros)
            If dt.Rows.Count > 0 Then
                Return Convert.ToInt32(dt.Rows(0)("IdPropietario"))
            End If
            Return 0
        Catch
            Return 0
        End Try
    End Function

    ' Consultar propietarios activos
    Public Function ConsultaActivas() As DataTable
        Try
            Dim sql As String = "
                SELECT p.IdPersona, CONCAT(p.Nombre, ' ', p.Apellido1, ' ', p.Apellido2) AS NombreCompleto
                FROM Personas p
                WHERE p.Activo = 1"
            Return dbHelper.ExecuteQuery(sql)
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    ' Consultar vehículos por persona
    Public Function ConsultaPorPersona(idPersona As Integer) As DataTable
        Try
            Dim sql As String = "
                SELECT v.IdVehiculo, v.Placa, v.Marca, v.Modelo,
                       CONCAT(pe.Nombre, ' ', pe.Apellido1, ' ', pe.Apellido2) AS NombrePropietario
                FROM Vehiculos v
                INNER JOIN Propietarios pr ON v.IdPropietario = pr.IdPropietario
                INNER JOIN Personas pe ON pr.IdPersona = pe.IdPersona
                WHERE pr.IdPersona = @IdPersona"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdPersona", idPersona)
            }
            Return dbHelper.ExecuteQuery(sql, parametros)
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function
End Class