Public Class formTeacher
    Dim drag As Boolean
    Dim mouseX As Integer
    Dim mouseY As Integer

    'WindowBar
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

    Private Sub formStudent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'JaduwalDataSet.teacher' table. You can move, or remove it, as needed.
        Me.TeacherTableAdapter.Fill(Me.JaduwalDataSet.teacher)
        resetForm()

    End Sub

    Sub resetForm()
        txName.Clear()
        txAddress.Clear()
        dtPicker.Value = Now()
        txEmail.Clear()
        txPhone.Clear()
        cbGender.SelectedIndex = 0
        If mainMenu.t_edit = True Then
            txNip.Text = mainMenu.nip
            txName.Text = mainMenu.t_name
            cbGender.Text = mainMenu.t_gender
            txAddress.Text = mainMenu.t_address
            dtPicker.Value = mainMenu.t_date
            txEmail.Text = mainMenu.t_email
            txPhone.Text = mainMenu.t_phone
            txNip.Enabled = False
            txName.Focus()
        Else
            txNip.Enabled = True
            txNip.Clear()
            txNip.Focus()
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        resetForm()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txPhone.Text = "" Or txNip.Text = "" Or txAddress.Text = "" Or txEmail.Text = "" Or txName.Text = "" Then
            msgeBox.pesan = "All field must be filled!"
            msgeBox.ShowDialog()
        Else
            If mainMenu.t_edit Then
                TeacherTableAdapter.edit(txName.Text, cbGender.SelectedItem, txAddress.Text, dtPicker.Value, txEmail.Text, txPhone.Text, txNip.Text)
            Else
                TeacherTableAdapter.add(txNip.Text, txName.Text, cbGender.SelectedItem, txAddress.Text, dtPicker.Value, txEmail.Text, txPhone.Text)
            End If
            Me.Close()
        End If
    End Sub
End Class