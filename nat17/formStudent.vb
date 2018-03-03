Public Class formStudent
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
        'TODO: This line of code loads data into the 'JaduwalDataSet.student' table. You can move, or remove it, as needed.
        Me.StudentTableAdapter.Fill(Me.JaduwalDataSet.student)
        'TODO: This line of code loads data into the 'JaduwalDataSet._class' table. You can move, or remove it, as needed.
        Me.ClassTableAdapter.Fill(Me.JaduwalDataSet._class)
        resetForm()

    End Sub

    Sub resetForm()
        txName.Clear()
        txAddress.Clear()
        dtPicker.Value = Now()
        txEmail.Clear()
        txPhone.Clear()
        cbClass.SelectedIndex = 0
        cbGender.SelectedIndex = 0
        If mainMenu.s_edit = True Then
            txNis.Text = mainMenu.nis
            txName.Text = mainMenu.s_name
            cbClass.Text = mainMenu.s_class
            cbGender.Text = mainMenu.s_gender
            txAddress.Text = mainMenu.s_address
            dtPicker.Value = mainMenu.s_date
            txEmail.Text = mainMenu.s_email
            txPhone.Text = mainMenu.s_phone
            txNis.Enabled = False
            txName.Focus()
        Else
            txNis.Enabled = True
            txNis.Clear()
            txNis.Focus()
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        resetForm()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txPhone.Text = "" Or txNis.Text = "" Or txAddress.Text = "" Or txEmail.Text = "" Or txName.Text = "" Then
            msgeBox.pesan = "All field must be filled!"
            msgeBox.ShowDialog()
        Else
            If mainMenu.s_edit Then
                StudentTableAdapter.edit(txName.Text, cbClass.SelectedValue, cbGender.SelectedItem, txAddress.Text, dtPicker.Value, txEmail.Text, txPhone.Text, txNis.Text)
            Else
                StudentTableAdapter.add(txNis.Text, txName.Text, cbClass.SelectedValue, cbGender.SelectedItem, txAddress.Text, dtPicker.Value, txEmail.Text, txPhone.Text)
            End If
            Me.Close()
        End If
    End Sub
End Class