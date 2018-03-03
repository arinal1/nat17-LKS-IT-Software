Public Class mainMenu
    Dim drag As Boolean
    Dim mouseX As Integer
    Dim mouseY As Integer
    Public logOut As Boolean
    Public answer As Boolean
    'Student
    Public s_edit As Boolean = False
    Public nis As Integer = 0
    Public s_name As String
    Public s_class As String
    Public s_gender As String
    Public s_address As String
    Public s_date As Date
    Public s_email As String
    Public s_phone As String
    'Teacher
    Public t_edit As Boolean = False
    Public nip As Integer = 0
    Public t_name As String
    Public t_gender As String
    Public t_address As String
    Public t_date As Date
    Public t_email As String
    Public t_phone As String
    'Class
    Dim sc_nis As Integer = 0
    Dim sc_name As String
    Dim uc_nis As Integer = 0
    Dim uc_name As String
    Dim uc_class As String
    Public c_edit As Boolean = False
    'Schedule
    Public sch_edit As Boolean = False
    Public sch_id As Integer = 0
    Public sch_subj As String
    Public sch_class As Integer
    Public sch_day As String
    Public sch_time As String
    Public sch_teacher As String
    'Profile
    Dim p_edit As Boolean = False
    Dim foto As Byte()

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
        logOut = False
        Me.Close()
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    'MainMenu
    Private Sub mainMenu_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadProfile()
        leveling()
        hidePanel()
        loadData()
        btnHome_Click(sender, e)
    End Sub

    Sub loadProfile()
        If login.level = 1 Then
            Dim sdt As New jaduwalDataSet.view_studentDataTable
            sdt = View_studentTableAdapter.GetProfile(login.username)
            txNis.Text = sdt.Rows(0).Item(0)
            txName.Text = sdt.Rows(0).Item(1)
            txClass.Text = sdt.Rows(0).Item(2)
            cbGender.Text = sdt.Rows(0).Item(3)
            txAddress.Text = sdt.Rows(0).Item(4)
            dateBirth.Text = sdt.Rows(0).Item(5)
            txEmail.Text = sdt.Rows(0).Item(6)
            txPhone.Text = sdt.Rows(0).Item(7)
            txUsername.Text = login.username
            txPassword.Text = login.password

            If IsDBNull(sdt.Rows(0).Item(9)) Then
                foto = Nothing
                fotoBox.BackgroundImage = Nothing
            Else
                foto = sdt.Rows(0).Item(9)
                Dim ms As New IO.MemoryStream(foto)
                fotoBox.BackgroundImage = Image.FromStream(ms)
            End If
        ElseIf login.level = 2 Then
            Dim tdt As New jaduwalDataSet.teacherDataTable
            tdt = TeacherTableAdapter.GetProfile(login.username)
            txNis.Text = tdt.Rows(0).Item(0)
            txName.Text = tdt.Rows(0).Item(1)
            txUsername.Text = login.username
            cbGender.Text = tdt.Rows(0).Item(3)
            txAddress.Text = tdt.Rows(0).Item(4)
            dateBirth.Text = tdt.Rows(0).Item(5)
            txEmail.Text = tdt.Rows(0).Item(6)
            txPhone.Text = tdt.Rows(0).Item(7)
            txPassword.Text = login.password

            Dim subjects As String = ""
            Dim dt As New jaduwalDataSet.subjectDataTable
            dt = SubjectTableAdapter.GetByTeacher(txNis.Text)
            For i = 0 To dt.Rows.Count - 1
                If i = 0 Then
                    subjects = dt.Rows(0).Item(1)
                Else
                    subjects = subjects + ", " + dt.Rows(i).Item(1)
                End If
            Next
            txClass.Text = subjects
            If IsDBNull(tdt.Rows(0).Item(8)) Then
                foto = Nothing
                fotoBox.BackgroundImage = Nothing
            Else
                foto = tdt.Rows(0).Item(8)
                Dim ms As New IO.MemoryStream(foto)
                fotoBox.BackgroundImage = Image.FromStream(ms)
            End If
        End If
    End Sub

    Sub loadData()
        Me.FindUnsignedTableAdapter.Fill(Me.JaduwalDataSet.findUnsigned)
        Me.SubjectTableAdapter.Fill(Me.JaduwalDataSet1.subject)
        Me.DayTableAdapter.Fill(Me.JaduwalDataSet1.day)
        Me.ScheduleTableAdapter.Fill(Me.JaduwalDataSet1.schedule)
        Me.View_scheduleTableAdapter.Fill(Me.JaduwalDataSet.view_schedule)
        Me.View_studentTableAdapter.Fill(Me.JaduwalDataSet.view_student)
        Me.TeacherTableAdapter.Fill(Me.JaduwalDataSet.teacher)
        Me.ClassTableAdapter.Fill(Me.JaduwalDataSet._class)
    End Sub

    Sub leveling()
        btnTeacher.Visible = False
        btnStudent.Visible = False
        btnClass.Visible = False
        separator3.Visible = False
        separator4.Visible = False
        separator5.Visible = False
        btnProfile.Visible = True
        btnSchedule.Text = "My Schedule"
        btnStudent.Text = "My Student"
        If login.level = 1 Then
            lblUsername.Text = login.username + " - Student"
        ElseIf login.level = 2 Then
            lblUsername.Text = login.username + " - Teacher"
            btnStudent.Visible = True
            separator4.Visible = True
        Else
            lblUsername.Text = login.username + " - Admin"
            btnSchedule.Text = "Manage Schedule"
            btnTeacher.Visible = True
            btnStudent.Visible = True
            btnStudent.Text = "Manage Student"
            btnClass.Visible = True
            separator3.Visible = True
            separator4.Visible = True
            separator5.Visible = False
            btnProfile.Visible = False
        End If
    End Sub

    Sub hidePanel()
        PanelKontenHome.Visible = False
        PanelKontenSchedule.Visible = False
        PanelKontenStudent.Visible = False
        PanelKontenTeacher.Visible = False
        PanelKontenClass.Visible = False
        PanelKontenProfile.Visible = False
    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        hidePanel()
        PanelKontenHome.Visible = True
        Title.Text = "Welcome To Main Menu"
    End Sub

    Private Sub btnSchedule_Click(sender As Object, e As EventArgs) Handles btnSchedule.Click
        hidePanel()
        PanelKontenSchedule.Visible = True
        sch_id = 0
        If login.level = 3 Then
            Title.Text = "Control The Schedule"
            PanelAdminSchedule.Visible = True
        Else
            Title.Text = "View Schedule"
            PanelAdminSchedule.Visible = False
            If login.level = 2 Then
                TeacherDataGridViewTextBoxColumn.Visible = False
            Else
                TeacherDataGridViewTextBoxColumn.Visible = True
            End If
        End If
        loadData()
    End Sub

    Private Sub btnTeacher_Click(sender As Object, e As EventArgs) Handles btnTeacher.Click
        hidePanel()
        PanelKontenTeacher.Visible = True
        Title.Text = "Control Teacher List"
        loadData()
        tabelTeacher.DataSource = TeacherBindingSource
        nip = 0
    End Sub

    Private Sub btnStudent_Click(sender As Object, e As EventArgs) Handles btnStudent.Click
        hidePanel()
        PanelKontenStudent.Visible = True
        txStudentSearch.Text = ""
        If login.level = 3 Then
            Title.Text = "Control Student List"
            PanelAdminStudent.Visible = True
        Else
            Title.Text = "View My Student List"
            PanelAdminStudent.Visible = False
        End If
        loadData()
        tabelStudent.DataSource = ViewstudentBindingSource
        nis = 0
    End Sub

    Private Sub btnClass_Click(sender As Object, e As EventArgs) Handles btnClass.Click
        hidePanel()
        PanelKontenClass.Visible = True
        Title.Text = "Control Class List"
        cbClass.SelectedIndex = 0
        FindClassTableAdapter.Fill(JaduwalDataSet.findClass, cbClass.SelectedValue)
        loadData()
    End Sub

    Private Sub btnProfile_Click(sender As Object, e As EventArgs) Handles btnProfile.Click
        hidePanel()
        PanelKontenProfile.Visible = True
        Title.Text = "View Profile"
        If login.level = 1 Then
            lblClass.Text = "Class"
        ElseIf login.level = 2 Then
            lblClass.Text = "Subject(s)"
        End If
        loadProfile()
        p_edit = True
        btnProfileEdit_Click(sender, e)
        txPassword.PasswordChar = "*"c
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        logOut = True
        Me.Close()
        login.Show()
    End Sub

    'Student
    Private Sub tabelStudent_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles tabelStudent.CellClick
        If e.RowIndex > -1 Then
            nis = tabelStudent.Rows(e.RowIndex).Cells(0).Value
            s_name = tabelStudent.Rows(e.RowIndex).Cells(1).Value
            s_class = tabelStudent.Rows(e.RowIndex).Cells(2).Value
            s_gender = tabelStudent.Rows(e.RowIndex).Cells(3).Value
            s_address = tabelStudent.Rows(e.RowIndex).Cells(4).Value
            s_date = tabelStudent.Rows(e.RowIndex).Cells(5).Value
            s_email = tabelStudent.Rows(e.RowIndex).Cells(6).Value
            s_phone = tabelStudent.Rows(e.RowIndex).Cells(7).Value
        End If
    End Sub

    Private Sub btnStudentAdd_Click(sender As Object, e As EventArgs) Handles btnStudentAdd.Click
        s_edit = False
        formStudent.ShowDialog()
        btnStudent_Click(sender, e)
    End Sub

    Private Sub btnStudentEdit_Click(sender As Object, e As EventArgs) Handles btnStudentEdit.Click
        If nis <> 0 Then
            s_edit = True
            formStudent.ShowDialog()
        Else
            msgeBox.pesan = "Please Select Data"
            msgeBox.ShowDialog()
        End If
        btnStudent_Click(sender, e)
    End Sub

    Private Sub btnStudentDelete_Click(sender As Object, e As EventArgs) Handles btnStudentDelete.Click
        If nis <> 0 Then
            qstnBox.tanya = "Are you sure to delete?"
            qstnBox.ShowDialog()
            If answer = True Then
                StudentTableAdapter._throw(nis)
                btnStudent_Click(sender, e)
                answer = False
            End If
        Else
            msgeBox.pesan = "Please Select Data"
            msgeBox.ShowDialog()
        End If
    End Sub

    Private Sub txStudentSearch_TextChanged(sender As Object, e As EventArgs) Handles txStudentSearch.TextChanged
        Dim qry As String = txStudentSearch.Text
        If qry = "" Then
            tabelStudent.DataSource = ViewstudentBindingSource
        Else
            Me.FindStudentTableAdapter.Fill(Me.JaduwalDataSet.findStudent, qry)
            tabelStudent.DataSource = FindStudentBindingSource
        End If
    End Sub

    'Teacher
    Private Sub tabelTeacher_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles tabelTeacher.CellClick
        If e.RowIndex >= 0 Then
            nip = tabelTeacher.Rows(e.RowIndex).Cells(0).Value
            t_name = tabelTeacher.Rows(e.RowIndex).Cells(1).Value
            t_gender = tabelTeacher.Rows(e.RowIndex).Cells(2).Value
            t_address = tabelTeacher.Rows(e.RowIndex).Cells(3).Value
            t_date = tabelTeacher.Rows(e.RowIndex).Cells(4).Value
            t_email = tabelTeacher.Rows(e.RowIndex).Cells(5).Value
            t_phone = tabelTeacher.Rows(e.RowIndex).Cells(6).Value
        End If
    End Sub

    Private Sub btnTeacherAdd_Click(sender As Object, e As EventArgs) Handles btnTeacherAdd.Click
        t_edit = False
        formTeacher.ShowDialog()
        btnTeacher_Click(sender, e)
    End Sub

    Private Sub btnTeacherEdit_Click(sender As Object, e As EventArgs) Handles btnTeacherEdit.Click
        If nip <> 0 Then
            t_edit = True
            formTeacher.ShowDialog()
        Else
            msgeBox.pesan = "Please Select Data"
            msgeBox.ShowDialog()
        End If
        btnTeacher_Click(sender, e)
    End Sub

    Private Sub btnTeacherDelete_Click(sender As Object, e As EventArgs) Handles btnTeacherDelete.Click
        If nip <> 0 Then
            qstnBox.tanya = "Are you sure to delete?"
            qstnBox.ShowDialog()
            If answer = True Then
                TeacherTableAdapter.del(nip)
                btnTeacher_Click(sender, e)
                answer = False
            End If
        Else
            msgeBox.pesan = "Please Select Data"
            msgeBox.ShowDialog()
        End If
    End Sub

    Private Sub txTeacherSearch_TextChanged(sender As Object, e As EventArgs) Handles txTeacherSearch.TextChanged
        Dim qry As String = txTeacherSearch.Text
        If qry = "" Then
            tabelTeacher.DataSource = TeacherBindingSource
        Else
            qry = "%" + qry + "%"
            Me.FindTeacherTableAdapter.Fill(Me.JaduwalDataSet.findTeacher, qry)
            tabelTeacher.DataSource = FindTeacherBindingSource
        End If
    End Sub

    'Class
    Sub loadUnsigned()
        Dim dt As New jaduwalDataSet.studentDataTable
        StudentTableAdapter.FillByUnsigned(dt)
        tabelClass2.DataSource = dt
        cbTabelClass.SelectedIndex = 1
        ClassDataGridViewTextBoxColumn1.Visible = False
    End Sub

    Private Sub cbClass_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbClass.SelectedValueChanged
        FindClassTableAdapter.Fill(JaduwalDataSet.findClass, cbClass.SelectedValue)
    End Sub

    Private Sub cbTabelClass_SelectedValueChanged(sender As Object, e As EventArgs) Handles cbTabelClass.SelectedValueChanged
        If cbTabelClass.SelectedIndex = 0 Then
            loadData()
            tabelClass2.DataSource = FindUnsignedBindingSource
            ClassDataGridViewTextBoxColumn1.Visible = True
        Else
            loadUnsigned()
        End If
    End Sub

    Private Sub tabelClass1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles tabelClass1.CellClick
        If e.RowIndex >= 0 Then
            sc_nis = tabelClass1.Rows(e.RowIndex).Cells(0).Value
            sc_name = tabelClass1.Rows(e.RowIndex).Cells(1).Value
        End If
    End Sub

    Private Sub tabelClass2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles tabelClass2.CellClick
        If e.RowIndex >= 0 Then
            uc_nis = tabelClass2.Rows(e.RowIndex).Cells(0).Value
            uc_name = tabelClass2.Rows(e.RowIndex).Cells(1).Value
            If IsDBNull(tabelClass2.Rows(e.RowIndex).Cells(2).Value) Or tabelClass2.Rows(e.RowIndex).Cells(2).Value Is Nothing Then
                uc_class = 0
            Else
                uc_class = tabelClass2.Rows(e.RowIndex).Cells(2).Value
            End If
        End If
    End Sub

    Private Sub btnClassAddStudent_Click(sender As Object, e As EventArgs) Handles btnClassAddStudent.Click
        If uc_nis = 0 Or uc_class <> "0" Then
            msgeBox.pesan = "Please Choose an unsigned student!"
            msgeBox.ShowDialog()
        ElseIf cbClass.SelectedValue = 10 Then
            msgeBox.pesan = "Please Choose Class!"
            msgeBox.ShowDialog()
        Else
            qstnBox.tanya = "Are you sure to add " + uc_name + " to " + cbClass.Text + " class?"
            qstnBox.ShowDialog()
            If answer Then
                StudentTableAdapter.pindah(cbClass.SelectedValue, uc_nis)
                FindClassTableAdapter.Fill(JaduwalDataSet.findClass, cbClass.SelectedValue)
                loadUnsigned()
                uc_nis = 0
                answer = False
            End If
        End If
    End Sub

    Private Sub btnClassDeleteStudent_Click(sender As Object, e As EventArgs) Handles btnClassDeleteStudent.Click
        If sc_nis = 0 Then
            msgeBox.pesan = "Please Choose a signed student!"
            msgeBox.ShowDialog()
        Else
            qstnBox.tanya = "Are you sure to remove " + sc_name + " from " + cbClass.Text + " class?"
            qstnBox.ShowDialog()
            If answer Then
                StudentTableAdapter.pindah(Nothing, sc_nis)
                FindClassTableAdapter.Fill(JaduwalDataSet.findClass, cbClass.SelectedValue)
                loadUnsigned()
                sc_nis = 0
                answer = False
            End If
        End If
    End Sub

    Private Sub btnClassAdd_Click(sender As Object, e As EventArgs) Handles btnClassAdd.Click
        c_edit = False
        formClass.ShowDialog()
        btnClass_Click(sender, e)
    End Sub

    Private Sub btnClassEdit_Click(sender As Object, e As EventArgs) Handles btnClassEdit.Click
        If cbClass.SelectedValue <> 10 Then
            c_edit = True
            formClass.ShowDialog()
            btnClass_Click(sender, e)
        End If
    End Sub

    Private Sub btnClassDelete_Click(sender As Object, e As EventArgs) Handles btnClassDelete.Click
        If cbClass.SelectedValue <> 10 Then
            qstnBox.tanya = "Are you sure to delete " + cbClass.Text + " class?"
            qstnBox.ShowDialog()
            If answer = True Then
                StudentTableAdapter.removeClass(10, cbClass.SelectedValue)
                ClassTableAdapter.del(cbClass.SelectedValue)
                btnClass_Click(sender, e)
                loadUnsigned()
                answer = False
            End If
        End If
    End Sub

    'Schedule
    Private Sub tabelSchedule_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles tabelSchedule.CellClick
        If e.RowIndex >= 0 Then
            sch_id = tabelSchedule.Rows(e.RowIndex).Cells("schId").Value
            sch_subj = tabelSchedule.Rows(e.RowIndex).Cells(0).Value
            sch_class = tabelSchedule.Rows(e.RowIndex).Cells("cId").Value
            sch_day = tabelSchedule.Rows(e.RowIndex).Cells("day").Value
            sch_time = tabelSchedule.Rows(e.RowIndex).Cells("time").Value
            sch_teacher = tabelSchedule.Rows(e.RowIndex).Cells(5).Value
        End If
    End Sub

    Private Sub btnScheduleAdd_Click(sender As Object, e As EventArgs) Handles btnScheduleAdd.Click
        sch_edit = False
        formSchedule.ShowDialog()
        btnSchedule_Click(sender, e)
    End Sub

    Private Sub btnScheduleEdit_Click(sender As Object, e As EventArgs) Handles btnScheduleEdit.Click
        If sch_id <> 0 Then
            sch_edit = True
            formSchedule.ShowDialog()
            btnSchedule_Click(sender, e)
        Else
            msgeBox.pesan = "Please Select Data"
            msgeBox.ShowDialog()
        End If
    End Sub

    Private Sub btnScheduleDelete_Click(sender As Object, e As EventArgs) Handles btnScheduleDelete.Click
        If sch_id <> 0 Then
            qstnBox.tanya = "Are you sure to delete?"
            qstnBox.ShowDialog()
            If answer = True Then
                ScheduleTableAdapter.del(sch_id)
                btnSchedule_Click(sender, e)
                answer = False
            End If
        Else
            msgeBox.pesan = "Please Select Data"
            msgeBox.ShowDialog()
        End If
    End Sub

    Private Sub btnSubjectInfo_Click(sender As Object, e As EventArgs) Handles btnSubjectInfo.Click
        If sch_id <> 0 Then
            sch_edit = True
            infoSubject.ShowDialog()
        Else
            msgeBox.pesan = "Please Select Data"
            msgeBox.ShowDialog()
        End If
    End Sub

    'Profile
    Private Sub btnProfileEdit_Click(sender As Object, e As EventArgs) Handles btnProfileEdit.Click
        If p_edit Then
            txName.Enabled = False
            cbGender.Enabled = False
            dateBirth.Enabled = False
            txAddress.Enabled = False
            txEmail.Enabled = False
            txPhone.Enabled = False
            txUsername.Enabled = False
            txPassword.Enabled = False
            btnShowPass.Enabled = False
            btnProfileSave.Enabled = False
            btnProfilePict.Enabled = False
        Else
            txName.Enabled = True
            cbGender.Enabled = True
            dateBirth.Enabled = True
            txAddress.Enabled = True
            txEmail.Enabled = True
            txPhone.Enabled = True
            txUsername.Enabled = True
            txPassword.Enabled = True
            btnShowPass.Enabled = True
            btnProfileSave.Enabled = True
            btnProfilePict.Enabled = True
            txNis.Select()
        End If
        p_edit = Not p_edit
    End Sub

    Private Sub btnProfilePict_Click(sender As Object, e As EventArgs) Handles btnProfilePict.Click
        OpenFileDialog1.Filter = "Image Files (*.jpg , *.png) | *.jpg; *.png"
        If OpenFileDialog1.ShowDialog(Me) = DialogResult.OK Then
            Dim ms As New IO.MemoryStream()
            fotoBox.BackgroundImage = Image.FromFile(OpenFileDialog1.FileName)
            fotoBox.BackgroundImage.Save(ms, fotoBox.BackgroundImage.RawFormat)
            foto = ms.GetBuffer
        End If
    End Sub

    Private Sub btnProfileSave_Click(sender As Object, e As EventArgs) Handles btnProfileSave.Click
        If txNis.Text = "" Or txName.Text = "" Or txAddress.Text = "" Or txEmail.Text = "" Or
            txPhone.Text = "" Or txUsername.Text = "" Or txPassword.Text = "" Or cbGender.Text = "" Then
            msgeBox.pesan = "All field must be filled!"
            msgeBox.ShowDialog()
        ElseIf foto Is Nothing Then
            msgeBox.pesan = "Please choose picture!"
            msgeBox.ShowDialog()
        ElseIf txUsername.Text <> login.username And UserTableAdapter.GetUsername(txUsername.Text).Rows.Count > 0 Then
            msgeBox.pesan = "Username already existed!"
            msgeBox.ShowDialog()
        Else
            qstnBox.tanya = "Are you sure to save your profile?"
            qstnBox.ShowDialog()
            If answer Then
                If login.level = 1 Then
                    StudentTableAdapter.editProfile(txName.Text, cbGender.Text, txAddress.Text, dateBirth.Value.ToShortDateString, txEmail.Text, txPhone.Text, foto, txNis.Text)
                ElseIf login.level = 2 Then
                    TeacherTableAdapter.edit(txName.Text, cbGender.Text, txAddress.Text, dateBirth.Value.ToShortDateString, txEmail.Text, txPhone.Text, txNis.Text)
                    TeacherTableAdapter.editPhoto(foto, txNis.Text)
                End If
                If txUsername.Text <> login.username Then
                    UserTableAdapter.editUsername(txUsername.Text, login.username)
                    login.username = txUsername.Text
                End If
                UserTableAdapter.editPassword(txPassword.Text, login.username)
                login.password = txPassword.Text
            End If
                btnProfile_Click(sender, e)
                answer = False
            End If
    End Sub

    Private Sub btnShowPass_Click(sender As Object, e As EventArgs) Handles btnShowPass.Click
        If txPassword.PasswordChar.ToString = vbNullChar Then
            txPassword.PasswordChar = "*"c
        Else
            txPassword.PasswordChar = vbNullChar
        End If
    End Sub

    Sub banInt(e As KeyPressEventArgs)
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub
End Class