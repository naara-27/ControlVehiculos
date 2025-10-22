Imports System.Data.SqlClient

Public Class dbPersona
    Private ReadOnly ConectionString As String = ConfigurationManager.ConnectionStrings("II46_P3ConnectionString").ConnectionString
    Public Function create(Persona As Persona) As Boolean
        Try
            Dim sql As String = "INSERT INTO Persona (Nombre, Apellido, Edad) VALUES (@Nombre, @Apellido, @Edad)"
            Dim Parametros As New List(Of SqlParameter) From {
            New SqlParameter("@Nombre", Persona.Nombre),
            New SqlParameter("@Apellido", Persona.Apellido2),
            New SqlParameter("@FechaNacimiento", Persona.FechaNacimiento)
        }

            Using connetion As New SqlConnection(ConectionString)
                Using command As New SqlCommand(sql, connetion)
                    command.Parameters.AddRange(Parametros.ToArray())
                    connetion.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            Return False
        End Try

        Return True
    End Function

    Public Function delete(ByRef id As Integer) As String
        Try
            Dim sql As String = "DELETE FROM Persona WHERE ID = @Id"
            Dim Parametros As New List(Of SqlParameter) From {
            New SqlParameter("@Id", id)
        }
            Using connetion As New SqlConnection(ConectionString)
                Using command As New SqlCommand(sql, connetion)
                    command.Parameters.AddRange(Parametros.ToArray())
                    connetion.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
        End Try
        Return "Persona eliminada"
    End Function

    Public Function update(ByRef Persona As Persona) As String
        Try
            Dim sql As String = "UPDATE Persona SET Nombre = @Nombre, Apellido = @Apellido, Edad = @Edad WHERE ID = @Id"
            Dim Parametros As New List(Of SqlParameter) From {
            New SqlParameter("@Id", Persona.IdPersona),
            New SqlParameter("@Nombre", Persona.Nombre),
            New SqlParameter("@Apellido", Persona.Apellido1),
            New SqlParameter("@FechaNacimiento", Persona.FechaNacimiento)
        }
            Using connetion As New SqlConnection(ConectionString)
                Using command As New SqlCommand(sql, connetion)
                    command.Parameters.AddRange(Parametros.ToArray())
                    connetion.Open()
                    command.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
        End Try
        Return "Persona actualizada"
    End Function

End Class
