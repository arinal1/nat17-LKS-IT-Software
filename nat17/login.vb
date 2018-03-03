Public Class login
    Dim drag As Boolean
    Dim mouseX As Integer
    Dim mouseY As Integer
    Public username As String
    Public password As String
    Public level As Integer

    Private Sub formTitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles formTitleBar.MouseDown
        drag = True
        mouseX = Cursor.Position.X - Me.Left
        mouseY = Cursor.Position.Y - Me.Top
    End Sub

    Private Sub formTitleBar_MouseMove(sender As Object, e As MouseEventArgs) Handles formTitleBar.MouseMove
        If drag Then
            Me.Left = Cursor.Position.X - mouseX
            Me.Top = Cursor.Position.Y - mouseY
        End If
    End Sub

    Private Sub formTitleBar_MouseUp(sender As Object, e As MouseEventArgs) Handles formTitleBar.MouseUp
        drag = False
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles Me.Load
        txUsername.Focus()
        txUsername.Select()
        lblWarn.Visible = False
    End Sub

    Private Sub txUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles txUsername.KeyDown
        If e.KeyCode = Keys.Enter Then
            If txUsername.Text = "" Then
                lblWarn.Visible = True
            Else
                lblWarn.Visible = False
                txPassword.Select()
            End If
        End If
    End Sub

    Private Sub txPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnLogin_Click(sender, e)
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txUsername.Text = "" Or txPassword.Text = "" Then
            lblWarn.Visible = True
        Else
            lblWarn.Visible = False
            Dim dt As New jaduwalDataSet.LoginDataTable
            dt = LoginTableAdapter.GetData(txUsername.Text, txPassword.Text)
            If dt.Count = 0 Then
                msgeBox.pesan = "Login Gagal"
                msgeBox.Show()
            Else
                username = dt.Rows(0).Item(0)
                password = dt.Rows(0).Item(1)
                level = dt.Rows(0).Item(2)
                Me.Hide()
                mainMenu.ShowDialog()
                If mainMenu.logOut = False Then
                    Me.Close()
                Else
                    txUsername.Clear()
                    txPassword.Clear()
                    lblWarn.Visible = False
                    txUsername.Focus()
                End If
            End If
        End If
    End Sub

End Class
