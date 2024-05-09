Imports System.IO
Imports KlxPiaoAPI
Imports KlxPiaoAPI.字符串操作
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = $"{关于KlxPiao.产品名称} {关于KlxPiao.产品版本} Demo"
        LinkLabel1.Text = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath)), $"Form1.Designer.vb")

        CheckBox1.Checked = 启用缩放动画
        CheckBox2.Checked = 窗体可调整大小
        CheckBox3.Checked = KlxPiaoLabel1.投影连线
        CheckBox4.Checked = KlxPiaoLabel1.颜色减淡

        KlxPiaoTrackBar1.值 = KlxPiaoPictureBox1.边框大小
        KlxPiaoTrackBar2.值 = KlxPiaoPictureBox1.圆角百分比 * 100

        KlxPiaoLabel30.Text = KlxPiaoTrackBar1.值
        KlxPiaoLabel31.Text = KlxPiaoTrackBar2.值 & "%"

        Select Case CheckBox4.Checked
            Case True
                KlxPiaoLabel1.投影颜色 = Color.Black
            Case False
                KlxPiaoLabel1.投影颜色 = Color.DarkGray
        End Select

        PointBar1.值 = KlxPiaoLabel1.偏移量

        Select Case 窗体按钮
            Case 窗体按钮样式.全部显示
                ComboBox1.SelectedIndex = 0
            Case 窗体按钮样式.显示关闭和最小化
                ComboBox1.SelectedIndex = 1
            Case 窗体按钮样式.仅显示关闭
                ComboBox1.SelectedIndex = 2
            Case 窗体按钮样式.不显示
                ComboBox1.SelectedIndex = 3
        End Select

        Select Case 标题位置
            Case 位置.左
                ComboBox2.SelectedIndex = 0
            Case 位置.居中
                ComboBox2.SelectedIndex = 1
            Case 位置.右
                ComboBox2.SelectedIndex = 2
        End Select

        Select Case 窗体可拖动位置
            Case 拖动位置.仅标题框
                ComboBox3.SelectedIndex = 0
            Case 拖动位置.整个窗体
                ComboBox3.SelectedIndex = 1
            Case 拖动位置.不启用拖动
                ComboBox3.SelectedIndex = 2
        End Select

        Select Case 快捷缩放方式
            Case 双击位置.双击标题框时
                ComboBox4.SelectedIndex = 0
            Case 双击位置.双击窗体任意位置时
                ComboBox4.SelectedIndex = 1
            Case 双击位置.不启用
                ComboBox4.SelectedIndex = 2
        End Select

        刷新配色()
    End Sub

#Region "杂项，不做修改"
    '适应TabControl大小
    Private Sub Form1_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        TabControl1.Size = Size - New Size(24, 54)
    End Sub

    '代码生成器
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox3.Clear()

        Dim 自定义属性名 As String = TextBox1.Text
        Dim 类型 As String = TextBox2.Text
        Dim 默认值 As String = TextBox4.Text

        TextBox3.Text = $"Private _{自定义属性名} As {类型}"
        TextBox6.Text = $"_{自定义属性名} = {默认值}"
        TextBox7.Text = $"
Public Property {自定义属性名} As {类型}
    Get
        Return _{自定义属性名}
    End Get
    Set(value As {类型})
        _{自定义属性名} = value
        Invalidate()
    End Set
End Property"
    End Sub
    '复制代码
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click, Button4.Click, Button8.Click
        Dim button As Button = TryCast(sender, Button)

        Select Case button.Name
            Case "Button3"
                My.Computer.Clipboard.SetText(TextBox3.Text)
            Case "Button4"
                My.Computer.Clipboard.SetText(TextBox6.Text)
            Case "Button8"
                My.Computer.Clipboard.SetText(TextBox7.Text)
        End Select
    End Sub
    '应用本地字体
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        应用本地字体(Application.StartupPath & "\义启星空之翼.ttf", Me)
    End Sub
    '启用缩放动画
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        启用缩放动画 = CheckBox1.Checked
    End Sub
    '窗体可调整大小
    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        窗体可调整大小 = CheckBox2.Checked
    End Sub
    '设置窗体按钮
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 0
                窗体按钮 = 窗体按钮样式.全部显示
            Case 1
                窗体按钮 = 窗体按钮样式.显示关闭和最小化
            Case 2
                窗体按钮 = 窗体按钮样式.仅显示关闭
            Case 3
                窗体按钮 = 窗体按钮样式.不显示
        End Select
    End Sub

    '设置标题位置
    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        Select Case ComboBox2.SelectedIndex
            Case 0
                标题位置 = 位置.左
            Case 1
                标题位置 = 位置.居中
            Case 2
                标题位置 = 位置.右
        End Select
    End Sub
    '设置窗体拖动
    Private Sub ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox3.SelectedIndexChanged
        Select Case ComboBox3.SelectedIndex
            Case 0
                窗体可拖动位置 = 拖动位置.仅标题框
            Case 1
                窗体可拖动位置 = 拖动位置.整个窗体
            Case 2
                窗体可拖动位置 = 拖动位置.不启用拖动
        End Select
    End Sub
    '快捷缩放方式
    Private Sub ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox4.SelectedIndexChanged
        Select Case ComboBox4.SelectedIndex
            Case 0
                快捷缩放方式 = 双击位置.双击标题框时
            Case 1
                快捷缩放方式 = 双击位置.双击窗体任意位置时
            Case 2
                快捷缩放方式 = 双击位置.不启用
        End Select
    End Sub

#Region "设置展示投影文字的属性"
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        KlxPiaoLabel1.投影连线 = CheckBox3.Checked
    End Sub
    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        KlxPiaoLabel1.颜色减淡 = CheckBox4.Checked

        Select Case CheckBox4.Checked
            Case True
                KlxPiaoLabel1.投影颜色 = Color.Black
            Case False
                KlxPiaoLabel1.投影颜色 = Color.DarkGray
        End Select
    End Sub

    Private Sub PointBar1_值Changed(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles PointBar1.值Changed
        KlxPiaoLabel1.偏移量 = PointBar1.值
    End Sub

    Private Sub PointBar2_值Changed(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles PointBar2.值Changed
        KlxPiaoLabel26.偏移量 = PointBar2.值
    End Sub
#End Region

    'WindowState动画按钮
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Select Case WindowState
            Case FormWindowState.Normal
                Dim 设置样式 As New 设置WindowState With {
                    .应用于 = Me,
                    .样式 = FormWindowState.Maximized,
                    .启用动画 = True
                }

                设置样式.设置()
            Case FormWindowState.Maximized
                Dim 设置样式 As New 设置WindowState With {
                    .应用于 = Me,
                    .样式 = FormWindowState.Normal,
                    .启用动画 = True
                }

                设置样式.设置()
        End Select
    End Sub

    '加载主题按钮
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click, KlxPiaoButton4.Click
        Dim 打开文件 As New OpenFileDialog With {
            .Filter = "主题文件|*.ini",
            .InitialDirectory = Application.StartupPath
        }

        If 打开文件.ShowDialog = DialogResult.OK Then
            加载主题文件(打开文件.FileName)
            刷新配色()
        End If

    End Sub
    '保存主题按钮
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click, KlxPiaoButton3.Click
        Dim 保存文件 As New SaveFileDialog With {
            .Filter = "主题文件|*.ini",
            .FileName = "新配色方案.ini",
            .InitialDirectory = Application.StartupPath
        }

        If 保存文件.ShowDialog = DialogResult.OK Then
            导出主题文件(保存文件.FileName)
        End If

    End Sub
#End Region

    '更新皮肤编辑器颜色
    Private Sub 刷新配色()
        主题Panel.BackColor = 标题框颜色
        背景Panel.BackColor = BackColor

        '皮肤编辑器代码
        Dim 统一配色 As Boolean = False

        If 判断三个值是否相同(关闭按钮背景色, 缩放按钮背景色, 最小化按钮背景色) AndAlso 判断三个值是否相同(关闭按钮前景色, 缩放按钮前景色, 最小化按钮前景色) AndAlso 判断三个值是否相同(关闭按钮鼠标移入背景色, 缩放按钮鼠标移入背景色, 最小化按钮鼠标移入背景色) AndAlso 判断三个值是否相同(关闭按钮鼠标按下背景色, 缩放按钮鼠标按下背景色, 最小化按钮鼠标按下背景色) Then
            统一配色 = True
        End If


        If 统一配色 Then
            KlxPiaoPanel17.Visible = False
            KlxPiaoLabel7.Text = "按钮颜色"
            CheckBox5.Checked = True
        Else
            KlxPiaoPanel17.Visible = True
            CheckBox5.Checked = False
        End If

        '应用到Panel
        缩放按钮背景Panel.BackColor = 缩放按钮背景色
        缩放按钮前景Panel.BackColor = 缩放按钮前景色
        缩放按钮移入Panel.BackColor = 缩放按钮鼠标移入背景色
        缩放按钮按下Panel.BackColor = 缩放按钮鼠标按下背景色

        关闭按钮背景Panel.BackColor = 关闭按钮背景色
        关闭按钮前景Panel.BackColor = 关闭按钮前景色
        关闭按钮移入Panel.BackColor = 关闭按钮鼠标移入背景色
        关闭按钮按下Panel.BackColor = 关闭按钮鼠标按下背景色

        最小化按钮背景Panel.BackColor = 最小化按钮背景色
        最小化按钮前景Panel.BackColor = 最小化按钮前景色
        最小化按钮移入Panel.BackColor = 最小化按钮鼠标移入背景色
        最小化按钮按下Panel.BackColor = 最小化按钮鼠标按下背景色
    End Sub

    '统一按钮配色
    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            KlxPiaoPanel17.Visible = False
            KlxPiaoLabel7.Text = "按钮颜色"
            CheckBox5.Checked = True

            '应用到panel
            关闭按钮背景Panel.BackColor = 缩放按钮背景色
            关闭按钮前景Panel.BackColor = 缩放按钮前景色
            关闭按钮移入Panel.BackColor = 缩放按钮鼠标移入背景色
            关闭按钮按下Panel.BackColor = 缩放按钮鼠标按下背景色

            最小化按钮背景Panel.BackColor = 缩放按钮背景色
            最小化按钮前景Panel.BackColor = 缩放按钮前景色
            最小化按钮移入Panel.BackColor = 缩放按钮鼠标移入背景色
            最小化按钮按下Panel.BackColor = 缩放按钮鼠标按下背景色

            '应用到窗体
            关闭按钮背景色 = 关闭按钮背景Panel.BackColor
            关闭按钮前景色 = 关闭按钮前景Panel.BackColor
            关闭按钮鼠标移入背景色 = 关闭按钮移入Panel.BackColor
            关闭按钮鼠标按下背景色 = 关闭按钮按下Panel.BackColor

            最小化按钮背景色 = 最小化按钮背景Panel.BackColor
            最小化按钮前景色 = 最小化按钮前景Panel.BackColor
            最小化按钮鼠标移入背景色 = 最小化按钮移入Panel.BackColor
            最小化按钮鼠标按下背景色 = 最小化按钮按下Panel.BackColor

            标题字体颜色 = 缩放按钮前景色
            CheckBox6.Checked = False
        Else
            KlxPiaoPanel17.Visible = True
            KlxPiaoLabel7.Text = "缩放按钮"
        End If
    End Sub

    Private Sub 设置颜色_背景(sender As Object, e As EventArgs) Handles 缩放按钮背景Panel.Click, 关闭按钮背景Panel.Click, 最小化按钮背景Panel.Click
        Dim panel As KlxPiaoPanel = TryCast(sender, KlxPiaoPanel)
        Dim 设置颜色 As New ColorDialog
        Dim 触发 As KlxPiaoPanel = Nothing

        设置颜色.FullOpen = True

        Select Case panel.Name
            Case "缩放按钮背景Panel"
                设置颜色.Color = 缩放按钮背景色
                触发 = 缩放按钮背景Panel
            Case "关闭按钮背景Panel"
                设置颜色.Color = 关闭按钮背景色
                触发 = 关闭按钮背景Panel
            Case "最小化按钮背景Panel"
                设置颜色.Color = 最小化按钮背景色
                触发 = 最小化按钮背景Panel
        End Select

        If 设置颜色.ShowDialog = DialogResult.OK Then

            If CheckBox5.Checked Then
                缩放按钮背景色 = 设置颜色.Color
                关闭按钮背景色 = 设置颜色.Color
                最小化按钮背景色 = 设置颜色.Color

                缩放按钮背景Panel.BackColor = 设置颜色.Color
                关闭按钮背景Panel.BackColor = 设置颜色.Color
                最小化按钮背景Panel.BackColor = 设置颜色.Color
            Else

                If 触发 Is 缩放按钮背景Panel Then
                    缩放按钮背景色 = 设置颜色.Color
                ElseIf 触发 Is 关闭按钮背景Panel Then
                    关闭按钮背景色 = 设置颜色.Color
                ElseIf 触发 Is 最小化按钮背景Panel Then
                    最小化按钮背景色 = 设置颜色.Color
                End If

                触发.BackColor = 设置颜色.Color
            End If

        End If
    End Sub
    Private Sub 设置颜色_前景(sender As Object, e As EventArgs) Handles 缩放按钮前景Panel.Click, 关闭按钮前景Panel.Click, 最小化按钮前景Panel.Click
        Dim panel As KlxPiaoPanel = TryCast(sender, KlxPiaoPanel)
        Dim 设置颜色 As New ColorDialog
        Dim 触发 As KlxPiaoPanel = Nothing

        设置颜色.FullOpen = True

        Select Case panel.Name
            Case "缩放按钮前景Panel"
                设置颜色.Color = 缩放按钮前景色
                触发 = 缩放按钮前景Panel
            Case "关闭按钮前景Panel"
                设置颜色.Color = 关闭按钮前景色
                触发 = 关闭按钮前景Panel
            Case "最小化按钮前景Panel"
                设置颜色.Color = 最小化按钮前景色
                触发 = 最小化按钮前景Panel
        End Select

        If 设置颜色.ShowDialog = DialogResult.OK Then

            If CheckBox5.Checked Then
                缩放按钮前景色 = 设置颜色.Color
                关闭按钮前景色 = 设置颜色.Color
                最小化按钮前景色 = 设置颜色.Color

                缩放按钮前景Panel.BackColor = 设置颜色.Color
                关闭按钮前景Panel.BackColor = 设置颜色.Color
                最小化按钮前景Panel.BackColor = 设置颜色.Color

                标题字体颜色 = 设置颜色.Color
            Else

                If 触发 Is 缩放按钮前景Panel Then
                    缩放按钮前景色 = 设置颜色.Color
                ElseIf 触发 Is 关闭按钮前景Panel Then
                    关闭按钮前景色 = 设置颜色.Color
                ElseIf 触发 Is 最小化按钮前景Panel Then
                    最小化按钮前景色 = 设置颜色.Color
                End If

                触发.BackColor = 设置颜色.Color
            End If

        End If
    End Sub
    Private Sub 设置颜色_移入(sender As Object, e As EventArgs) Handles 缩放按钮移入Panel.Click, 关闭按钮移入Panel.Click, 最小化按钮移入Panel.Click
        Dim panel As KlxPiaoPanel = TryCast(sender, KlxPiaoPanel)
        Dim 设置颜色 As New ColorDialog
        Dim 触发 As KlxPiaoPanel = Nothing

        设置颜色.FullOpen = True

        Select Case panel.Name
            Case "缩放按钮移入Panel"
                设置颜色.Color = 缩放按钮鼠标移入背景色
                触发 = 缩放按钮移入Panel
            Case "关闭按钮移入Panel"
                设置颜色.Color = 关闭按钮鼠标移入背景色
                触发 = 关闭按钮移入Panel
            Case "最小化按钮移入Panel"
                设置颜色.Color = 最小化按钮鼠标移入背景色
                触发 = 最小化按钮移入Panel
        End Select

        If 设置颜色.ShowDialog = DialogResult.OK Then

            If CheckBox5.Checked Then
                缩放按钮鼠标移入背景色 = 设置颜色.Color
                关闭按钮鼠标移入背景色 = 设置颜色.Color
                最小化按钮鼠标移入背景色 = 设置颜色.Color

                缩放按钮移入Panel.BackColor = 设置颜色.Color
                关闭按钮移入Panel.BackColor = 设置颜色.Color
                最小化按钮移入Panel.BackColor = 设置颜色.Color
            Else

                If 触发 Is 缩放按钮移入Panel Then
                    缩放按钮鼠标移入背景色 = 设置颜色.Color
                ElseIf 触发 Is 关闭按钮移入Panel Then
                    关闭按钮鼠标移入背景色 = 设置颜色.Color
                ElseIf 触发 Is 最小化按钮移入Panel Then
                    最小化按钮鼠标移入背景色 = 设置颜色.Color
                End If

                触发.BackColor = 设置颜色.Color
            End If

        End If
    End Sub
    Private Sub 设置颜色_按下(sender As Object, e As EventArgs) Handles 缩放按钮按下Panel.Click, 关闭按钮按下Panel.Click, 最小化按钮按下Panel.Click
        Dim panel As KlxPiaoPanel = TryCast(sender, KlxPiaoPanel)
        Dim 设置颜色 As New ColorDialog
        Dim 触发 As KlxPiaoPanel = Nothing

        设置颜色.FullOpen = True

        Select Case panel.Name
            Case "缩放按钮按下Panel"
                设置颜色.Color = 缩放按钮鼠标按下背景色
                触发 = 缩放按钮按下Panel
            Case "关闭按钮按下Panel"
                设置颜色.Color = 关闭按钮鼠标按下背景色
                触发 = 关闭按钮按下Panel
            Case "最小化按钮按下Panel"
                设置颜色.Color = 最小化按钮鼠标按下背景色
                触发 = 最小化按钮按下Panel
        End Select

        If 设置颜色.ShowDialog = DialogResult.OK Then

            If CheckBox5.Checked Then
                缩放按钮鼠标按下背景色 = 设置颜色.Color
                关闭按钮鼠标按下背景色 = 设置颜色.Color
                最小化按钮鼠标按下背景色 = 设置颜色.Color

                缩放按钮按下Panel.BackColor = 设置颜色.Color
                关闭按钮按下Panel.BackColor = 设置颜色.Color
                最小化按钮按下Panel.BackColor = 设置颜色.Color
            Else

                If 触发 Is 缩放按钮按下Panel Then
                    缩放按钮鼠标按下背景色 = 设置颜色.Color
                ElseIf 触发 Is 关闭按钮按下Panel Then
                    关闭按钮鼠标按下背景色 = 设置颜色.Color
                ElseIf 触发 Is 最小化按钮按下Panel Then
                    最小化按钮鼠标按下背景色 = 设置颜色.Color
                End If

                触发.BackColor = 设置颜色.Color
            End If

        End If
    End Sub
    Private Sub 主题Panel_Click(sender As Object, e As EventArgs) Handles 主题Panel.Click
        Dim 设置颜色 As New ColorDialog With {
            .Color = 标题框颜色,
            .FullOpen = True
        }

        If 设置颜色.ShowDialog = DialogResult.OK Then
            主题Panel.BackColor = 设置颜色.Color
            标题框颜色 = 设置颜色.Color

            缩放按钮背景色 = 设置颜色.Color
            关闭按钮背景色 = 设置颜色.Color
            最小化按钮背景色 = 设置颜色.Color

            刷新配色()
        End If
    End Sub
    Private Sub 背景Panel_Click(sender As Object, e As EventArgs) Handles 背景Panel.Click
        Dim 设置颜色 As New ColorDialog With {
            .Color = BackColor,
            .FullOpen = True
        }

        If 设置颜色.ShowDialog = DialogResult.OK Then
            背景Panel.BackColor = 设置颜色.Color
            BackColor = 设置颜色.Color
        End If
    End Sub

    '一键生成
    Private Sub KlxPiaoButton2_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton2.Click
        自动生成主题(主题Panel.BackColor)

        刷新配色()

        CheckBox5.Checked = True
    End Sub
    '关闭按钮高亮
    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked Then
            CheckBox5.Checked = False

            关闭按钮移入Panel.BackColor = Color.FromArgb(255, 0, 0)
            关闭按钮按下Panel.BackColor = Color.FromArgb(255, 85, 85)

            关闭按钮鼠标移入背景色 = Color.FromArgb(255, 0, 0)
            关闭按钮鼠标按下背景色 = Color.FromArgb(255, 85, 85)
        Else
            CheckBox5.Checked = True
        End If
    End Sub
    '代码生成器
    Private Sub KlxPiaoButton5_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton5.Click
        TextBox5.Text = $"        Me.关闭按钮前景色 = System.Drawing.Color.FromArgb(CType(CType({关闭按钮前景色.R}, Byte), Integer), CType(CType({关闭按钮前景色.G}, Byte), Integer), CType(CType({关闭按钮前景色.B}, Byte), Integer))
        Me.关闭按钮背景色 = System.Drawing.Color.FromArgb(CType(CType({关闭按钮背景色.R}, Byte), Integer), CType(CType({关闭按钮背景色.G}, Byte), Integer), CType(CType({关闭按钮背景色.B}, Byte), Integer))
        Me.关闭按钮鼠标按下背景色 = System.Drawing.Color.FromArgb(CType(CType({关闭按钮鼠标按下背景色.R}, Byte), Integer), CType(CType({关闭按钮鼠标按下背景色.G}, Byte), Integer), CType(CType({关闭按钮鼠标按下背景色.B}, Byte), Integer))
        Me.关闭按钮鼠标移入背景色 = System.Drawing.Color.FromArgb(CType(CType({关闭按钮鼠标移入背景色.R}, Byte), Integer), CType(CType({关闭按钮鼠标移入背景色.G}, Byte), Integer), CType(CType({关闭按钮鼠标移入背景色.B}, Byte), Integer))
        Me.最小化按钮前景色 = System.Drawing.Color.FromArgb(CType(CType({最小化按钮前景色.R}, Byte), Integer), CType(CType({最小化按钮前景色.G}, Byte), Integer), CType(CType({最小化按钮前景色.B}, Byte), Integer))
        Me.最小化按钮背景色 = System.Drawing.Color.FromArgb(CType(CType({最小化按钮背景色.R}, Byte), Integer), CType(CType({最小化按钮背景色.G}, Byte), Integer), CType(CType({最小化按钮背景色.B}, Byte), Integer))
        Me.最小化按钮鼠标按下背景色 = System.Drawing.Color.FromArgb(CType(CType({最小化按钮鼠标按下背景色.R}, Byte), Integer), CType(CType({最小化按钮鼠标按下背景色.G}, Byte), Integer), CType(CType({最小化按钮鼠标按下背景色.B}, Byte), Integer))
        Me.最小化按钮鼠标移入背景色 = System.Drawing.Color.FromArgb(CType(CType({最小化按钮鼠标移入背景色.R}, Byte), Integer), CType(CType({最小化按钮鼠标移入背景色.G}, Byte), Integer), CType(CType({最小化按钮鼠标移入背景色.B}, Byte), Integer))
        Me.缩放按钮前景色 = System.Drawing.Color.FromArgb(CType(CType({缩放按钮前景色.R}, Byte), Integer), CType(CType({缩放按钮前景色.G}, Byte), Integer), CType(CType({缩放按钮前景色.B}, Byte), Integer))
        Me.缩放按钮背景色 = System.Drawing.Color.FromArgb(CType(CType({缩放按钮背景色.R}, Byte), Integer), CType(CType({缩放按钮背景色.G}, Byte), Integer), CType(CType({缩放按钮背景色.B}, Byte), Integer))
        Me.缩放按钮鼠标按下背景色 = System.Drawing.Color.FromArgb(CType(CType({缩放按钮鼠标按下背景色.R}, Byte), Integer), CType(CType({缩放按钮鼠标按下背景色.G}, Byte), Integer), CType(CType({缩放按钮鼠标按下背景色.B}, Byte), Integer))
        Me.缩放按钮鼠标移入背景色 = System.Drawing.Color.FromArgb(CType(CType({缩放按钮鼠标移入背景色.R}, Byte), Integer), CType(CType({缩放按钮鼠标移入背景色.G}, Byte), Integer), CType(CType({缩放按钮鼠标移入背景色.B}, Byte), Integer))
        Me.标题框颜色 = System.Drawing.Color.FromArgb(CType(CType({标题框颜色.R}, Byte), Integer), CType(CType({标题框颜色.G}, Byte), Integer), CType(CType({标题框颜色.B}, Byte), Integer))
        Me.标题字体颜色 = System.Drawing.Color.FromArgb(CType(CType({标题字体颜色.R}, Byte), Integer), CType(CType({标题字体颜色.G}, Byte), Integer), CType(CType({标题字体颜色.B}, Byte), Integer))"

        My.Computer.Clipboard.SetText(TextBox5.Text)
    End Sub
    '切换皮肤编辑器
    Private Sub KlxPiaoButton6_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton6.Click
        TabControl1.SelectedIndex = 4
    End Sub
    '应用到.Designer
    Private Sub KlxPiaoButton7_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton7.Click
        If TextBox5.Text = "" Then

            显示提示框("先生成，再应用", "提示")

        Else
            Dim 目标路径 As String = LinkLabel1.Text
            Dim 全部内容 As String = File.ReadAllText(目标路径)

            'Dim 读行 As String() = File.ReadAllLines(目标路径)
            'Dim 查找属性 As String() = {"Me.标题框颜色", "Me.标题字体颜色", "Me.关闭按钮前景色", "Me.关闭按钮背景色", "Me.关闭按钮鼠标按下背景色", "Me.关闭按钮鼠标移入背景色", "Me.最小化按钮前景色", "Me.最小化按钮背景色", "Me.最小化按钮鼠标按下背景色", "Me.最小化按钮鼠标移入背景色", "Me.缩放按钮前景色", "Me.缩放按钮背景色", "Me.缩放按钮鼠标按下背景色", "Me.缩放按钮鼠标移入背景色"}

            'For Each 原行 In 读行
            '    For Each 属性名 In 查找属性
            '        If 原行.Contains(属性名) Then
            '            For Each 替换行 In TextBox5.Lines
            '                If 替换行.Contains(属性名) Then
            '                    全部内容 = 全部内容.Replace(原行, 替换行)
            '                End If
            '            Next
            '        End If
            '    Next
            'Next

            Dim 读行 As List(Of String) = File.ReadAllLines(目标路径).ToList
            Dim 查找属性 As New List(Of String) From {"Me.标题框颜色", "Me.标题字体颜色", "Me.关闭按钮前景色", "Me.关闭按钮背景色", "Me.关闭按钮鼠标按下背景色", "Me.关闭按钮鼠标移入背景色", "Me.最小化按钮前景色", "Me.最小化按钮背景色", "Me.最小化按钮鼠标按下背景色", "Me.最小化按钮鼠标移入背景色", "Me.缩放按钮前景色", "Me.缩放按钮背景色", "Me.缩放按钮鼠标按下背景色", "Me.缩放按钮鼠标移入背景色"}

            读行.ForEach(Sub(原行 As String) 查找属性.ForEach(Sub(属性名 As String) If 原行.Contains(属性名) Then TextBox5.Lines.ToList.ForEach(Sub(替换行 As String) If 替换行.Contains(属性名) Then 全部内容 = 全部内容.Replace(原行, 替换行))))

            Using 更新文件 As New StreamWriter(目标路径)
                更新文件.Write(全部内容)
            End Using

            显示提示框("应用成功", "提示")

        End If
    End Sub
    '修改窗体设计文件路径
    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim 选择文件 As New OpenFileDialog With {
            .InitialDirectory = Path.GetDirectoryName(LinkLabel1.Text),
            .Filter = "窗体设计文件|*.Designer.vb"
        }

        If 选择文件.ShowDialog = DialogResult.OK Then
            LinkLabel1.Text = 选择文件.FileName
        End If
    End Sub
    Private Sub 显示提示框(内容 As String, 标题 As String)
        Dim 提示框 As New KlxPiaoForm With {
            .Text = 标题,
            .窗体按钮 = 窗体按钮样式.仅显示关闭,
            .窗体可调整大小 = False,
            .ShowInTaskbar = False,
            .StartPosition = FormStartPosition.CenterParent,
            .Size = New Size(250, 150),
            .窗体边框颜色 = Color.FromArgb(113, 96, 232)
        }

        复制主题(Me, 提示框)

        Dim 提示文本 As New KlxPiaoLabel With {
            .Text = 内容
        }

        Dim 文字大小 As SizeF = 提示框.CreateGraphics.MeasureString(提示文本.Text, New Font("微软雅黑", 9))
        提示文本.Location = New Point((提示框.Width - 文字大小.Width) / 2, (提示框.Height - 文字大小.Height) / 2)

        提示框.Controls.Add(提示文本)
        提示框.ShowDialog()
    End Sub

#Region "PictureBox展示"
    Private Sub KlxPiaoTrackBar1_值Changed(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles KlxPiaoTrackBar1.值Changed
        KlxPiaoPictureBox1.边框大小 = KlxPiaoTrackBar1.值
        KlxPiaoLabel30.Text = KlxPiaoTrackBar1.值
    End Sub

    Private Sub KlxPiaoTrackBar2_值Changed(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles KlxPiaoTrackBar2.值Changed
        KlxPiaoPictureBox1.圆角百分比 = KlxPiaoTrackBar2.值 / 100
        KlxPiaoLabel31.Text = KlxPiaoTrackBar2.值 & "%"
    End Sub
#End Region

    Private Sub KlxPiaoTrackBar3_值Changed(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles KlxPiaoTrackBar3.值Changed
        Console.WriteLine(KlxPiaoTrackBar3.值)
    End Sub

    '随机生成主题
    Dim rand As New Random
    Private Sub KlxPiaoButton1_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton1.Click
        Dim 随机颜色 As Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))

        自动生成主题(随机颜色)

        刷新配色()
    End Sub

    Private Sub KlxPiaoButton8_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton8.Click
        Dim 随机颜色 As Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
        Form2.自动生成主题(随机颜色)
        Form2.Show()
    End Sub

End Class