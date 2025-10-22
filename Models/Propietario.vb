Public Class Propietario
    Inherits Persona
    Private _idPropietario As Integer
    Private _numVehiculos As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(idPropietario As Integer, numVehiculos As String)
        Me.IdPropietario = idPropietario
        Me.NumVehiculos = numVehiculos
    End Sub

    Public Sub New(idPropietario As Integer, numVehiculos As String, persona As Persona)
        MyBase.New(persona.IdPersona, persona.Nombre, persona.Apellido1, persona.Apellido2, persona.Nacionalidad, persona.FechaNacimiento, persona.Telefono)
        Me.IdPropietario = idPropietario
        Me.NumVehiculos = numVehiculos
    End Sub

    Public Property IdPropietario As Integer
        Get
            Return _idPropietario
        End Get
        Set(value As Integer)
            _idPropietario = value
        End Set
    End Property

    Public Property NumVehiculos As String
        Get
            Return _numVehiculos
        End Get
        Set(value As String)
            _numVehiculos = value
        End Set
    End Property
End Class
