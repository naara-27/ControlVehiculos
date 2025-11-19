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
                SELECT P.IdPropietario, P.IdPersona,
                       CONCAT(Per.Nombre, ' ', Per.Apellido1, ' ', Per.Apellido2) AS NombreCompleto
                FROM Propietarios P
                INNER JOIN Personas Per ON P.IdPersona = Per.IdPersona"
            Return dbHelper.ExecuteQuery(sql, New List(Of SqlParameter)())
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
End Class