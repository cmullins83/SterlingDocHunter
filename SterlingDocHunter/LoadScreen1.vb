Public Class LoadScreen1
    Private Sub LoadScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim A As Short = 0
        Do While Me.Visible = True
            If A = 0 Then
                Me.Text = "Searching"
                A = A + 1
                ProgressBar1.Value = 0
                Threading.Thread.Sleep(500)
            End If
            If A = 1 Then
                Me.Text = "Searching."
                A = A + 1
                ProgressBar1.Value = 20
                Threading.Thread.Sleep(500)
            End If
            If A = 2 Then
                Me.Text = "Searching.."
                A = A + 1
                ProgressBar1.Value = 40
                Threading.Thread.Sleep(500)
            End If
            If A = 3 Then
                Me.Text = "Searching..."
                A = A + 1
                ProgressBar1.Value = 60
                Threading.Thread.Sleep(500)
            End If
            If A = 4 Then
                Me.Text = "Searching...."
                A = A + 1
                ProgressBar1.Value = 80
                Threading.Thread.Sleep(500)
            End If
            If A = 5 Then
                Me.Text = "Searching....."
                A = 0
                ProgressBar1.Value = 100
                Threading.Thread.Sleep(500)
            End If
        Loop


    End Sub
End Class