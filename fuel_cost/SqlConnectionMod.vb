Imports System.Data.SqlClient

Module SqlConnectionMod
    Public Connect As SqlConnection
    Public Server As String
    Public Base As String
    Public Username As String
    Public Password As String

    Public Function GetConnect()
        Try
            Connect = New SqlConnection("Data Source=" & Server & ";Initial Catalog=" & Base & ";User ID=" & Username & ";password=" & Password)
            Connect.Open()
            Return 1
        Catch ex As Exception
            Return MsgBox("Ошибка подключения к SQL серверу! " & ex.Message)
        End Try
    End Function

    Public Sub LoadGridFromDB(ByVal Grid1 As DataGridView, ByVal cmd As String)
        Dim command As New SqlCommand
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet

        command = Connect.CreateCommand
        command.CommandText = cmd

        da.SelectCommand = command
        da.Fill(ds, "costs")

        Grid1.DataSource = ds
        Grid1.DataMember = "costs"
    End Sub
End Module
