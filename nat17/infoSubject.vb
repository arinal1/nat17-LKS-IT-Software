Public Class infoSubject
    Dim drag As Boolean
    Dim mouseX As Integer
    Dim mouseY As Integer

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

    Private Sub infoSubject_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim dt As New jaduwalDataSet.subjectDataTable
        dt = SubjectTableAdapter.GetInfo(mainMenu.sch_subj)
        Title.Text = dt.Rows(0).Item(1) + " Subject Information"
        subTitle.Text = "#" + dt.Rows(0).Item(0)
        teacher.Text = "Teacher : " + mainMenu.sch_teacher
        description.Text = "Description : " + dt.Rows(0).Item(2)
        assign.Text = dt.Rows(0).Item(4).ToString + "%"
        mid.Text = dt.Rows(0).Item(5).ToString + "%"
        final.Text = dt.Rows(0).Item(6).ToString + "%"
    End Sub
End Class