Imports System.Data.SqlClient
Imports System.Security.Cryptography

Public Class dbVehiculo
    Public ReadOnly ConectionString As String = ConfigurationManager.ConnectionStrings("II46_P3ConnectionString").ConnectionString
    Private ReadOnly dbHelper = New DbHelper() ' Clase para manejar conexiones y consultas

    Public Function create(Vehiculo As Vehiculo) As String
        Try
            Dim sql As String = "INSERT INTO Vehiculos (Placa, Marca, Modelo) 
                                 VALUES (@Placa, @Marca, @Modelo)"
            Dim Parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Placa", Vehiculo.Placa),
                New SqlParameter("@Marca", Vehiculo.Marca),
                New SqlParameter("@Modelo", Vehiculo.Modelo)
            }
            dbHelper.ExecuteNonQuery(sql, Parametros)
        Catch ex As Exception
            Return "Error al guardar el vehículo: " & ex.Message
        End Try
        Return "Vehículo guardado"
    End Function

    Public Function delete(ByRef id As Integer) As String
        Try
            Dim sql As String = "DELETE FROM Vehiculos WHERE IdVehiculo = @IdVehiculo"
            Dim Parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdVehiculo", id)
            }
            dbHelper.ExecuteNonQuery(sql, Parametros)
        Catch ex As Exception
            Return "Error al eliminar el vehículo: " & ex.Message
        End Try
        Return "Vehículo eliminado"
    End Function

    Public Function update(ByRef Vehiculo As Vehiculo) As String
        Try
            Dim sql As String = "UPDATE Vehiculos 
                                 SET Placa = @Placa, Marca = @Marca, Modelo = @Modelo 
                                 WHERE IdVehiculo = @IdVehiculo"
            Dim Parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdVehiculo", Vehiculo.IdVehiculo),
                New SqlParameter("@Placa", Vehiculo.Placa),
                New SqlParameter("@Marca", Vehiculo.Marca),
                New SqlParameter("@Modelo", Vehiculo.Modelo)
            }
            dbHelper.ExecuteNonQuery(sql, Parametros)
        Catch ex As Exception
            Return "Error al actualizar el vehículo: " & ex.Message
        End Try
        Return "Vehículo actualizado"
    End Function

    Public Function Consulta() As DataTable
        Try
            Dim sql As String = "SELECT *, CONCAT(Placa, ' - ', Marca, ' ', Modelo) AS DescripcionVehiculo FROM Vehiculos"
            Return dbHelper.ExecuteQuery(sql, New List(Of SqlParameter)())
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function
End Class