Public Class Propietario
    Private _idPropietario As Integer
    Private _idPersona As Integer

    ' Constructor vacío
    Public Sub New()
    End Sub

    ' Constructor completo
    Public Sub New(idPropietario As Integer, idPersona As Integer)
        Me.IdPropietario = idPropietario
        Me.IdPersona = idPersona
    End Sub

    Public Property IdPropietario As Integer
        Get
            Return _idPropietario
        End Get
        Set(value As Integer)
            _idPropietario = value
        End Set
    End Property

    Public Property IdPersona As Integer
        Get
            Return _idPersona
        End Get
        Set(value As Integer)
            _idPersona = value
        End Set
    End Property
End Class