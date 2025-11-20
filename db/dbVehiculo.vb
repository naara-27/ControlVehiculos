Imports System.Data.SqlClient

Public Class dbVehiculo
    Public ReadOnly ConectionString As String = ConfigurationManager.ConnectionStrings("II46_P3ConnectionString").ConnectionString
    Private ReadOnly dbHelper = New DbHelper()

    Public Function create(Vehiculo As Vehiculo) As String
        Try
            Dim sql As String = "INSERT INTO Vehiculos (Placa, Marca, Modelo, IdPropietario) 
                                 VALUES (@Placa, @Marca, @Modelo, @IdPropietario)"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Placa", Vehiculo.Placa),
                New SqlParameter("@Marca", Vehiculo.Marca),
                New SqlParameter("@Modelo", Vehiculo.Modelo),
                New SqlParameter("@IdPropietario", Vehiculo.IdPropietario)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return "Vehículo guardado"
        Catch ex As Exception
            Return "Error al guardar el vehículo: " & ex.Message
        End Try
    End Function

    Public Function delete(ByRef id As Integer) As String
        Try
            Dim sql As String = "DELETE FROM Vehiculos WHERE IdVehiculo = @IdVehiculo"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdVehiculo", id)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return "Vehículo eliminado"
        Catch ex As Exception
            Return "Error al eliminar el vehículo: " & ex.Message
        End Try
    End Function

    Public Function update(ByRef Vehiculo As Vehiculo) As String
        Try
            Dim sql As String = "UPDATE Vehiculos 
                                 SET Placa = @Placa, Marca = @Marca, Modelo = @Modelo, IdPropietario = @IdPropietario 
                                 WHERE IdVehiculo = @IdVehiculo"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdVehiculo", Vehiculo.IdVehiculo),
                New SqlParameter("@Placa", Vehiculo.Placa),
                New SqlParameter("@Marca", Vehiculo.Marca),
                New SqlParameter("@Modelo", Vehiculo.Modelo),
                New SqlParameter("@IdPropietario", Vehiculo.IdPropietario)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return "Vehículo actualizado"
        Catch ex As Exception
            Return "Error al actualizar el vehículo: " & ex.Message
        End Try
    End Function

    Public Function Consulta() As DataTable
        Try
            Dim sql As String = "
                SELECT 
                    v.*, 
                    CONCAT(p.Nombre, ' ', p.Apellido1, ' ', p.Apellido2) AS NombrePropietario
                FROM Vehiculos v
                INNER JOIN Propietarios pr ON v.IdPropietario = pr.IdPropietario
                INNER JOIN Personas p ON pr.IdPersona = p.IdPersona"
            Return dbHelper.ExecuteQuery(sql)
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    Public Function ConsultaPorPersona(idPersona As Integer) As DataTable
        Try
            Dim sql As String = "
                SELECT v.IdVehiculo, v.Placa, v.Marca, v.Modelo
                FROM Vehiculos v
                INNER JOIN Propietarios p ON v.IdPropietario = p.IdPropietario
                WHERE p.IdPersona = @IdPersona"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdPersona", idPersona)
            }
            Return dbHelper.ExecuteQuery(sql, parametros)
        Catch ex As Exception
            Return New DataTable()
        End Try
    End Function

    Public Function ActualizarMarcaModeloPorPropietario(idPropietario As Integer, marca As String, modelo As String) As String
        Try
            Dim sql As String = "
                UPDATE Vehiculos 
                SET Marca = @Marca, Modelo = @Modelo 
                WHERE IdPropietario = @IdPropietario"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@Marca", marca),
                New SqlParameter("@Modelo", modelo),
                New SqlParameter("@IdPropietario", idPropietario)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return "Actualización exitosa"
        Catch ex As Exception
            Return "Error al actualizar vehículo: " & ex.Message
        End Try
    End Function

    Public Function DesasignarVehiculo(idPropietario As Integer) As String
        Try
            Dim sql As String = "
                UPDATE Vehiculos 
                SET IdPropietario = NULL 
                WHERE IdPropietario = @IdPropietario"
            Dim parametros As New List(Of SqlParameter) From {
                New SqlParameter("@IdPropietario", idPropietario)
            }
            dbHelper.ExecuteNonQuery(sql, parametros)
            Return "Vehículo desasignado"
        Catch ex As Exception
            Return "Error al desasignar vehículo: " & ex.Message
        End Try
    End Function

End Class