Imports System.Data.SqlClient

Public Class dbPropietario
    Public ReadOnly ConectionString As String = ConfigurationManager.ConnectionStrings("II46_P3ConnectionString").ConnectionString
    Private ReadOnly dbHelper = New DbHelper() ' Clase para manejar conexiones y consultas

    Public Function create(idPersona As Integer) As String
        Try
            Dim sql As String = "INSERT INTO Propietarios (IdPersona) VALUES (@IdPersona)"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdPersona", idPersona)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
        Catch ex As Exception
            Return "Error al registrar propietario: " & ex.Message
        End Try
        Return "Propietario registrado correctamente"
    End Function

    ' Consultar propietarios con nombre completo
    Public Function Consulta() As DataTable
        Try
            Dim sql As String = "
                SELECT pr.IdPropietario, 
                       CONCAT(p.Nombre, ' ', p.Apellido1, ' ', p.Apellido2) AS NombreCompleto
                FROM Propietarios pr
                INNER JOIN Personas p ON pr.IdPersona = p.IdPersona
                ORDER BY p.Nombre ASC"
            Return dbHelper.ExecuteQuery(sql)
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    ' Consultar datos personales y vehículo (si existe) por persona
    Public Function ConsultaPorPersona(idPersona As Integer) As DataTable
        Try
            Dim sql As String = "
                SELECT 
                    p.Nombre, p.Apellido1, p.Apellido2, p.Nacionalidad, p.FechaNacimiento, p.Telefono,
                    v.Placa, v.Marca, v.Modelo
                FROM Personas p
                INNER JOIN Propietarios pr ON p.IdPersona = pr.IdPersona
                LEFT JOIN Vehiculos v ON pr.IdPropietario = v.IdPropietario
                WHERE p.IdPersona = @IdPersona"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdPersona", idPersona)
            }
            Return dbHelper.ExecuteQuery(sql, parametros)
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    ' Eliminar propietario (si no está asignado a ningún vehículo)
    Public Function delete(idPropietario As Integer) As String
        Try
            Dim sql As String = "DELETE FROM Propietarios WHERE IdPropietario = @IdPropietario"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdPropietario", idPropietario)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
        Catch ex As Exception
            Return "Error al eliminar propietario: " & ex.Message
        End Try
        Return "Propietario eliminado"
    End Function

    ' Actualizar persona asociada a un propietario
    Public Function update(idPropietario As Integer, idPersona As Integer) As String
        Try
            Dim sql As String = "UPDATE Propietarios SET IdPersona = @IdPersona WHERE IdPropietario = @IdPropietario"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdPropietario", idPropietario),
                New SqlParameter("@IdPersona", idPersona)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
        Catch ex As Exception
            Return "Error al actualizar propietario: " & ex.Message
        End Try
        Return "Propietario actualizado"
    End Function

    Public Function GetLastInsertedId(idPersona As Integer) As Integer
        Try
            Dim sql As String = "
                SELECT TOP 1 IdPropietario 
                FROM Propietarios 
                WHERE IdPersona = @IdPersona 
                ORDER BY IdPropietario DESC"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdPersona", idPersona)
            }
            Dim dt As DataTable = dbHelper.ExecuteQuery(sql, parametros)
            If dt.Rows.Count > 0 Then
                Return Convert.ToInt32(dt.Rows(0)("IdPropietario"))
            End If
        Catch ex As Exception
        End Try
        Return 0
    End Function
End Class