Imports System.Web.Optimization
Imports Microsoft.ApplicationInsights

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Se desencadena al iniciar la aplicación
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub
    Protected Sub Application_Error(sender As Object, e As EventArgs)
        Dim ex = Server.GetLastError()
        Dim tc As New TelemetryClient()
        tc.TrackException(ex)
        tc.Flush()
    End Sub
End Class