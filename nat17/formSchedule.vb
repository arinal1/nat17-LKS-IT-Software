Public Class formSchedule
    Dim drag As Boolean
    Dim mouseX As Integer
    Dim mouseY As Integer
    Dim s_id As Integer

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
    Private Sub formSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'JaduwalDataSet.header_schedule' table. You can move, or remove it, as needed.
        Me.Header_scheduleTableAdapter.Fill(Me.JaduwalDataSet.header_schedule)
        'TODO: This line of code loads data into the 'JaduwalDataSet1.schedule' table. You can move, or remove it, as needed.
        Me.ScheduleTableAdapter.Fill(Me.JaduwalDataSet1.schedule)
        'TODO: This line of code loads data into the 'JaduwalDataSet.subject' table. You can move, or remove it, as needed.
        Me.SubjectTableAdapter.Fill(Me.JaduwalDataSet.subject)
        'TODO: This line of code loads data into the 'JaduwalDataSet._class' table. You can move, or remove it, as needed.
        Me.ClassTableAdapter.Fill(Me.JaduwalDataSet._class)
        'TODO: This line of code loads data into the 'JaduwalDataSet.day' table. You can move, or remove it, as needed.
        Me.DayTableAdapter.Fill(Me.JaduwalDataSet.day)
        resetForm()
    End Sub

    Sub resetForm()
        If mainMenu.sch_edit Then
            Dim time1, time2 As String
            cbDay.Text = mainMenu.sch_day
            cbClass.SelectedValue = mainMenu.sch_class
            cbSubject.SelectedValue = mainMenu.sch_subj
            time1 = Mid(mainMenu.sch_time, 1, 5)
            time2 = Mid(mainMenu.sch_time, 9)
            tmPick1.Value = DateTime.ParseExact(time1, "HH.mm", Nothing)
            tmPick2.Value = DateTime.ParseExact(time2, "HH.mm", Nothing)
        Else
            cbDay.SelectedIndex = 0
            cbClass.SelectedIndex = 0
            cbSubject.SelectedIndex = 0
            tmPick1.Value = Now()
            tmPick2.Value = Now()
        End If
    End Sub

    Private Sub tmPick2_ValueChanged(sender As Object, e As EventArgs) Handles tmPick2.ValueChanged
        If tmPick2.Value < tmPick1.Value Then
            tmPick2.Value = tmPick1.Value
        End If
    End Sub

    Private Sub tmPick1_ValueChanged(sender As Object, e As EventArgs) Handles tmPick1.ValueChanged
        If tmPick2.Value < tmPick1.Value Then
            tmPick2.Value = tmPick1.Value
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim h_id As String = Header_scheduleTableAdapter.GetId(cbDay.SelectedValue, cbClass.SelectedValue).Rows(0).Item(0).ToString
        Dim time As String = tmPick1.Text + " - " + tmPick2.Text
        If mainMenu.sch_edit Then
            ScheduleTableAdapter.edit(h_id, cbSubject.SelectedValue, time, mainMenu.sch_id)
        Else
            ScheduleTableAdapter.Insert(h_id, cbSubject.SelectedValue, time)
        End If
        Me.Close()
    End Sub

End Class