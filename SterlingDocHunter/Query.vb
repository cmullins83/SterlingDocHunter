Imports System.IO
Public Class Query
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim list As System.Collections.ObjectModel.
  ReadOnlyCollection(Of String)
        list = My.Computer.FileSystem.FindInFiles("C:\Test",
        TextBox1.Text, True, FileIO.SearchOption.SearchTopLevelOnly)
        For Each File In list
            ListBox1.Items.Add(File)
        Next
        ListBox1.Items.Add("Search Complete")
    End Sub
End Class