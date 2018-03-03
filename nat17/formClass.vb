Public Class formClass
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
    Private Sub formClass_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'JaduwalDataSet.teacher' table. You can move, or remove it, as needed.
        Me.TeacherTableAdapter.Fill(Me.JaduwalDataSet.teacher)
        resetForm()
    End Sub

    Sub resetForm()
        If mainMenu.c_edit Then
            txName.Text = mainMenu.cbClass.Text
            cbHomeroom.SelectedValue = ClassTableAdapter.GetHomeroom(mainMenu.cbClass.SelectedValue).Rows(0).Item(2)
        Else
            txName.Clear()
            cbHomeroom.SelectedIndex = 0
        End If
        txName.Select()
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        resetForm()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txName.Text = "" Then
            msgeBox.pesan = "All field must be filled!"
            msgeBox.ShowDialog()
        Else
            If mainMenu.c_edit Then
                ClassTableAdapter.edit(txName.Text, cbHomeroom.SelectedValue, mainMenu.cbClass.SelectedValue)
            Else
                ClassTableAdapter.Insert(txName.Text, cbHomeroom.SelectedValue)
            End If
            Me.Close()
        End If
    End Sub
End Class