﻿'for read/writes
Imports System.IO
'for configuration reading and writing
Imports System.Configuration
Imports System.Collections.Specialized
Public Class Query
    'My Defines go here, most will be overwritten when a configuration file is implimented.
    Dim CB As String
    Public SDirectory As String = ""
    Dim tbt As String = "clear"
    Dim path2 As String = ConfigurationSettings.AppSettings("Path2")
    Dim Editor As String = ConfigurationSettings.AppSettings("Editor")
    Dim Settings As String = "C:\test.ini"
    Dim InputType As String = "String"
    Dim CSVFile As String = ""
    Dim Delimiter As String = ""

    Public Property Editor1 As String
        Get
            Return Editor
        End Get
        Set(value As String)
            Editor = value
        End Set
    End Property

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Empty any previous results
        ListBox1.Hide()
        ListBox1.Items.Clear()

        'Need to show a splash screen here so the user doesn't think the application is hung up.

        'Change the button so you don't think the program has hung
        Button1.Text = "Searching..."
        'Disable the button to prevent restarting the search by mistake
        Button1.Enabled = False

        'If the input type is string then we search for the string, looping through all files
        If InputType = "String" Then
            Dim a As System.Collections.ObjectModel.ReadOnlyCollection(Of String)
            a = My.Computer.FileSystem.FindInFiles(SDirectory,
            TextBox1.Text, True, FileIO.SearchOption.SearchAllSubDirectories)
            For Each b In a
                ListBox1.Items.Add(b)
            Next
            Button1.Enabled = True
            Button1.Text = "Search"
        End If
        ' CSV
        If InputType = "CSV" Then
            Using MyReader As New FileIO.TextFieldParser(CSVFile)
                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(Delimiter)
                Dim currentRow As String()
                While Not MyReader.EndOfData
                    Try
                        currentRow = MyReader.ReadFields()
                        Dim currentField As String
                        For Each currentField In currentRow
                            'recycle the code from the textbox search
                            Dim a As ObjectModel.ReadOnlyCollection(Of String)
                            a = My.Computer.FileSystem.FindInFiles(SDirectory,
                            currentField, True, FileIO.SearchOption.SearchAllSubDirectories)
                            Dim counter As Int16 = 0
                            For Each b In a
                                If counter = 0 Then
                                    ListBox1.Items.Add(currentField + " in")
                                End If
                                ListBox1.Items.Add(b)
                                counter = counter + 1

                            Next
                            ListBox1.Items.Add(vbNewLine)
                            'end of recycling
                        Next
                    Catch ex As Microsoft.VisualBasic.
                                FileIO.MalformedLineException
                        MsgBox("Line " & ex.Message &
                        "is not valid and will be skipped.")
                    End Try
                End While
            End Using
        End If

        ' LoadScreen1.Hide()
        ListBox1.Show()

    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Select directory and store it to variable SDirectory
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            SDirectory = FolderBrowserDialog1.SelectedPath
            'If SDirectory isn't empty then change the default enter button to the search button (pretty nifty trick if I say so)
            If SDirectory <> "" Then
                AcceptButton = Button1
                'Show the working directory on the directory select button
                Button2.Text = SDirectory + Environment.NewLine + "Click to Change"
            Else
                'If no directory has been set then the default enter button is select directory
                AcceptButton = Button2
                Button2.Text = "Select Directory"
            End If
        End If
    End Sub
    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        'set the variable fullpath to be the fully qualified path of the double clicked file
        Dim fullPath = Path.Combine(ListBox1.SelectedItem.ToString())
        'launch the fully qualified path to the editor and pass it the fully qualified path we set for the file
        Try
            System.Diagnostics.Process.Start(Editor1, fullPath)
        Catch ex As Exception
            MessageBox.Show("Editor (" & Editor1 & ") not set or file not available.")
        End Try

    End Sub
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        'open the default browser to the URL:
        System.Diagnostics.Process.Start("http://www.sterlingpublishing.com/catalogs.html")
    End Sub
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        'open the default browser to the URL:
        System.Diagnostics.Process.Start("http://www.coenterprise.com")
    End Sub
    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        'open the default browser to the URL:
        System.Diagnostics.Process.Start("http://www.syncrofy.com/")
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'clear the clipboard
        Clipboard.Clear()
        'set the clipboard to the contents of Listbox1
        Clipboard.SetText(ListBox1.Text)
    End Sub
    Private Sub TextBox1_MouseClick(sender As Object, e As MouseEventArgs) Handles TextBox1.MouseClick
        'on clicking the textbox, clear the contents
        If tbt = "clear" Then
            TextBox1.Text = ""
            tbt = ""
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'this is the current iteration of the sidetrack process, Need to add support for multiple files and move out every file first
        'then move the selected files back
        'then we need a button to restore the files back to the original directory afterwards.

        'I need to set up the same file searching with a NOT to get a list of files to move out for the sidetrack
        'Then I need a button to move everything back
        CB = ListBox1.Text
        Dim filename As String
        filename = Path.GetFileName(CB)
        File.Move(CB, path2 + filename)
        ListBox1.Items.Remove(CB)
    End Sub

    Private Sub Query_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'this entire section is commented out, it's development for using a configuration file

        'Dim objStreamReader As StreamReader
        'Dim strLine As String

        'Pass the file path and the file name to the StreamReader constructor.
        'objStreamReader = New StreamReader(Settings)

        'Read the first line of text.
        'strLine = objStreamReader.ReadLine

        'Continue to read until you reach the end of the file.
        'Do While Not strLine Is Nothing

        'Write the line to the Console window.
        'Console.WriteLine(strLine)

        'Read the next line.
        'strLine = objStreamReader.ReadLine
        'Loop 'uncomment this, it's not a comment, dumbass...

        'Close the file.
        'objStreamReader.Close()

        'Console.ReadLine()

    End Sub

    Private Sub CSVToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CSVToolStripMenuItem.Click
        InputType = "CSV"
        Delimiter = ","
        OpenFileDialog1.Filter = "CSV Files |*.csv;*.CSV"
        OpenFileDialog1.ShowDialog()
        CSVFile = OpenFileDialog1.FileName
    End Sub

    Private Sub SearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SearchToolStripMenuItem.Click

    End Sub

    Private Sub ListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ListToolStripMenuItem.Click
        InputType = "newline"
        Delimiter = vbNewLine
        OpenFileDialog1.Filter = "All Files (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
        CSVFile = OpenFileDialog1.FileName
    End Sub

    Private Sub SettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingsToolStripMenuItem.Click

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'This will be where everything is moved FROM the 850 directory to a temp directory
        'and everything will be moved from the sidetrack directory, back to inbound
        'we will need a popup indicating that the BP should be run until we can intigrate it with 
        'a command line option for SI.

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'This will be where we move everything from the temporary directory back to the inbound folder
    End Sub
End Class