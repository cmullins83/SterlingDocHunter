Imports System.IO

Public Class Query
    Public SDirectory As String = "C:\EDIDATA\ArchiveIn"
    Dim tbt As String = "clear"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Button1.Text = "Searching..."
        Button1.Enabled = False
        ListBox1.Items.Clear()
        Dim a As System.Collections.ObjectModel.
  ReadOnlyCollection(Of String)
        a = My.Computer.FileSystem.FindInFiles(SDirectory,
        TextBox1.Text, True, FileIO.SearchOption.SearchAllSubDirectories)
        For Each b In a
            ListBox1.Items.Add(b)
        Next
        Button1.Enabled = True
        Button1.Text = "Search"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            SDirectory = FolderBrowserDialog1.SelectedPath
            If SDirectory <> "" Then
                AcceptButton = Button1
            Else
                AcceptButton = Button2
            End If
        End If
    End Sub

    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        Dim fullPath = Path.Combine(ListBox1.SelectedItem.ToString())
        System.Diagnostics.Process.Start("C:\Program Files (x86)\Notepad++\notepad++.exe", fullPath)
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        System.Diagnostics.Process.Start("http://www.sterlingpublishing.com/catalogs.html")
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        System.Diagnostics.Process.Start("http://www.coenterprise.com")
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        System.Diagnostics.Process.Start("http://www.syncrofy.com/")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim CB As String
        Clipboard.Clear()
        CB = ListBox1.Text
        Clipboard.SetText(CB)
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseClick
        If tbt = "clear" Then
            TextBox1.Text = ""
            tbt = ""
        End If
    End Sub


End Class