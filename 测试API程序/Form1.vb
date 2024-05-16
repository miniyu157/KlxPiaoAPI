Imports System.IO
Imports System.Threading
Imports KlxPiaoAPI
Imports KlxPiaoAPI.字符串操作
Public Class Form1

    Private 窗体已加载 As Boolean = False
    Private 动画已完成 As Boolean = False
    Private rand As New Random

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Text = $"{关于KlxPiao.产品名称} {关于KlxPiao.产品版本} Demo"
        LinkLabel1.Text = Path.Combine(Path.GetDirectoryName(Path.GetDirectoryName(Application.StartupPath)), $"Form1.Designer.vb")

        颜色亮度TrackBar.值 = 0.0606

        CheckBox1.Checked = 启用缩放动画
        CheckBox2.Checked = 窗体可调整大小
        CheckBox3.Checked = KlxPiaoLabel1.投影连线
        CheckBox4.Checked = KlxPiaoLabel1.颜色减淡

        KlxPiaoTrackBar1.值 = KlxPiaoPictureBox1.边框大小
        KlxPiaoTrackBar2.值 = KlxPiaoPictureBox1.圆角百分比 * 100

        KlxPiaoTrackBar8.值 = 标题框高度
        KlxPiaoTrackBar9.值 = 窗体按钮宽度
        KlxPiaoTrackBar10.值 = 标题左右边距
        窗体已加载 = True

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

        '给KlxPiaoAPI函数.控件添加点击事件
        For Each c In Panel1.Controls
            If TypeOf c Is KlxPiaoPanel Then
                AddHandler DirectCast(c, KlxPiaoPanel).Click, AddressOf KlxPiaoPanel_Click
            End If
        Next
    End Sub
    '大小适应
    Private Sub Form1_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        TabControl1.Width = Width - 24
        TabControl1.Height = Height - 标题框高度 - 23
        If 动画已完成 Then KlxPiaoLabel19.Left = (TabPage2.Width - KlxPiaoLabel19.Width) \ 2
        GroupBox1.Height = TabPage2.Height - GroupBox1.Top - 28
        GroupBox3.Height = TabPage2.Height - GroupBox3.Top - 28
        GroupBox2.Left = Width \ 2
        GroupBox3.Left = Width \ 2
        GroupBox2.Width = Width - GroupBox2.Left - 68
        GroupBox3.Width = Width - GroupBox3.Left - 68
        GroupBox1.Width = GroupBox2.Left - GroupBox1.Left - 43
    End Sub

#Region "属性代码生成器"
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
#End Region

    '应用本地字体
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        应用本地字体(Application.StartupPath & "\义启星空之翼.ttf", Me)
    End Sub

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

#Region "调整窗体属性"
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
    '切换皮肤编辑器
    Private Sub KlxPiaoButton6_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton6.Click
        TabControl1.SelectedIndex = 4
    End Sub
    '3拖动条
    Private Sub KlxPiaoTrackBar8_值Changed(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles KlxPiaoTrackBar8.值Changed
        If 窗体已加载 Then
            标题框高度 = KlxPiaoTrackBar8.值
            TabControl1.Top = 标题框高度 + 10
            TabControl1.Height = Height - 标题框高度 - 23
            Refresh()
            MyBase.OnSizeChanged(e)
        End If
    End Sub
    Private Sub KlxPiaoTrackBar9_值Changed(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles KlxPiaoTrackBar9.值Changed
        If 窗体已加载 Then
            窗体按钮宽度 = KlxPiaoTrackBar9.值
            Refresh()
        End If
    End Sub
    Private Sub KlxPiaoTrackBar10_值Changed(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles KlxPiaoTrackBar10.值Changed
        If 窗体已加载 Then
            标题左右边距 = KlxPiaoTrackBar10.值
            Refresh()
        End If
    End Sub
#End Region

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

#Region "皮肤编辑器"
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
    '随机生成主题
    Private Sub KlxPiaoButton1_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton1.Click
        Dim 随机颜色 As Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))

        自动生成主题(随机颜色)

        刷新配色()
    End Sub
#End Region

#Region "Designer代码生成器"
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
    '应用到.Designer
    Private Sub KlxPiaoButton7_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton7.Click
        If TextBox5.Text = "" Then

            显示提示框("先生成，再应用", "提示")

        Else
            Dim 目标路径 As String = LinkLabel1.Text
            Dim 全部内容 As String = File.ReadAllText(目标路径)

#Region "以前的代码"
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
#End Region

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
#End Region

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

#Region "主页的动画"
    Private WithEvents 颜色计时器 As New Windows.Forms.Timer
    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        '调整选项卡顺序
        TabControl1.TabPages.Remove(TabPage1)
        TabControl1.TabPages.Add(TabPage1)
        TabControl1.TabPages.Remove(TabPage18)
        TabControl1.TabPages.Insert(2, TabPage18)
        颜色计时器.Interval = 800
        颜色计时器.Start()
        Dim a As New Thread(Async Sub()
                                控件.过渡动画(KlxPiaoLabel19, "Left", KlxPiaoLabel19.Left, (TabPage2.Width - KlxPiaoLabel19.Width) \ 2, 2000, 控件.贝塞尔曲线控制点.EaseOutCubic)
                                Await Task.Delay(2000)
                                动画已完成 = True
                            End Sub)
        a.Start()
        动画演示Panel.BackColor = 标题框颜色
    End Sub
    Private Sub 颜色计时器_Tick(sender As Object, e As EventArgs) Handles 颜色计时器.Tick
        Dim 随机颜色 As Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
        控件.过渡动画(KlxPiaoLabel19, "ForeColor", KlxPiaoLabel19.ForeColor, 随机颜色, 300, 控件.贝塞尔曲线控制点.Linear)
    End Sub
#End Region

    'KlxPiaoAPI函数

#Region "控件"
    Private Sub 控制点拖动(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles PointBar3.值Changed, PointBar4.值Changed
        Dim 控制点1 As New PointF(PointBar3.值.X / 100, PointBar3.值.Y / 100)
        Dim 控制点2 As New PointF(PointBar4.值.X / 100, PointBar4.值.Y / 100)
        TextBox8.Text = $"{控制点1.X}, {控制点1.Y}, {控制点2.X}, {控制点2.Y}"
        TextBox9.Text = $"{{New PointF({控制点1.X},{控制点1.Y}), New PointF({控制点2.X},{控制点2.Y})}}"
    End Sub
    '开始动画
    Private Async Sub KlxPiaoButton9_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton9.Click
        KlxPiaoButton9.Enabled = False
        KlxPiaoButton9.Text = "正在播放"

        Dim 坐标数据 As String() = TextBox8.Text.Split(",")
        Dim x1 As Double
        Dim x2 As Double
        Dim y1 As Double
        Dim y2 As Double
        Try
            x1 = 坐标数据(0)
            x2 = 坐标数据(1)
            y1 = 坐标数据(2)
            y2 = 坐标数据(3)
        Catch ex As Exception
        End Try
        Dim 动画持续时间 As Integer = 2000
        Dim 控制点 As PointF() = {New PointF(x1, x2), New PointF(y1, y2)}
        Dim 随机颜色 As Color = Color.FromArgb(rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255))
        控件.过渡动画(动画演示Panel, "Left", If(动画演示Panel.Left = 1, 1, 471), If(动画演示Panel.Left = 1, 471, 1), 动画持续时间, 控制点)
        控件.过渡动画(动画演示Panel, "BackColor", 动画演示Panel.BackColor, 随机颜色, 200, 控制点)

        Dim 参数 As Tuple(Of Color, Integer) = Tuple.Create(随机颜色, If(动画演示Panel.Left = 1, 0, 1))
        Dim 灯条 As New Thread(Sub()
                                 绘制灯条(参数.Item1, 参数.Item2)
                             End Sub)
        灯条.Start()

        Dim 启动时间 As Date = Date.Now
        Dim 运行状态 As Boolean = False
        Dim grap As Graphics = 动画曲线准确性测试.CreateGraphics
        动画曲线准确性测试.Refresh()
        Dim 绘制矩形 As New Rectangle(0, 动画曲线准确性测试.标题框高度, 动画曲线准确性测试.Width, 动画曲线准确性测试.Height - 动画曲线准确性测试.标题框高度)
        Dim 开始动画 As New Thread(Async Sub()
                                   Do While 运行状态 = False
                                       Dim 当前时间 As TimeSpan = Date.Now - 启动时间
                                       Dim 时间进度 As Double = 当前时间.TotalMilliseconds / TimeSpan.FromSeconds(动画持续时间 / 1000).TotalMilliseconds
                                       grap.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                                       If 时间进度 >= 1.0 Then
                                           运行状态 = True
                                           Invoke(Sub() 动画曲线准确性测试.KlxPiaoLabel1.Text = 当前时间.ToString)
                                       Else
                                           Dim p As PointF = 控件.根据时间进度返回坐标(时间进度, 控制点)
                                           grap.FillRectangle(New SolidBrush(Color.FromArgb(50, Color.Red)), New Rectangle(p.X * 绘制矩形.Width, (1 - p.Y) * 绘制矩形.Height + 绘制矩形.Y, 4, 4))
                                           Invoke(Sub()
                                                      动画曲线准确性测试.KlxPiaoLabel1.Text = 当前时间.ToString
                                                      动画曲线准确性测试.KlxPiaoPanel1.Left = 时间进度 * 绘制矩形.Width + 8
                                                  End Sub)
                                       End If
                                       Await Task.Delay(1000 / 100)
                                   Loop
                               End Sub)
        开始动画.Start()
        If CheckBox8.Checked Then
            Await Task.Delay(动画持续时间 + 15)
            KlxPiaoButton9.Enabled = True
            KlxPiaoButton9.PerformClick()
        Else
            Await Task.Delay(动画持续时间 + 15)
            KlxPiaoButton9.Enabled = True
            KlxPiaoButton9.Text = "开始动画"
        End If
    End Sub
    '绘制贝塞尔曲线
    Private Sub TextBox8_TextChanged(sender As Object, e As EventArgs) Handles TextBox8.TextChanged
        Dim 坐标数据 As String() = TextBox8.Text.Split(",")
        Dim x1 As Double
        Dim x2 As Double
        Dim y1 As Double
        Dim y2 As Double
        Try
            x1 = 坐标数据(0)
            x2 = 坐标数据(1)
            y1 = 坐标数据(2)
            y2 = 坐标数据(3)
        Catch ex As Exception
        End Try

        KlxPiaoPanel7.Refresh()

        Dim Panel大小 = KlxPiaoPanel7.获取工作区大小
        Dim 中心矩形 = New Rectangle(0, ((PointBar3.最大值.Y - 100) / (PointBar3.最大值.Y - PointBar3.最小值.Y)) * Panel大小.Height, Panel大小.Width, (100 / (PointBar3.最大值.Y - PointBar3.最小值.Y)) * Panel大小.Height)
        Dim 控制点Brush As Brush = New SolidBrush(Color.Red)
        Dim 控制点Pen As New Pen(Color.Red, 2)
        Dim 曲线Pen As New Pen(Color.Black, 2)
        Dim 中心矩形Pen As New Pen(KlxPiaoPanel7.边框颜色, 2)
        Dim 控制点大小 As New Size(10, 10)
        Dim 控制点1 As New PointF(x1, x2)
        Dim 控制点2 As New PointF(y1, y2)

        Using grap As Graphics = KlxPiaoPanel7.CreateGraphics
            grap.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            grap.PixelOffsetMode = Drawing2D.PixelOffsetMode.HighQuality

            grap.DrawRectangle(中心矩形Pen, 中心矩形)
            Dim p0 As New Point(中心矩形.X, 中心矩形.Bottom)
            Dim p1 As New Point(中心矩形.X + 中心矩形.Width * 控制点1.X, 中心矩形.Y + 中心矩形.Height * (1 - 控制点1.Y))
            Dim p2 As New Point(中心矩形.X + 中心矩形.Width * 控制点2.X, 中心矩形.Y + 中心矩形.Height * (1 - 控制点2.Y))
            Dim p3 As New Point(中心矩形.Right, 中心矩形.Y)

            grap.DrawBezier(曲线Pen, p0, p1, p2, p3)

            grap.FillEllipse(控制点Brush, New Rectangle(p1 - New Size(控制点大小.Width / 2, 控制点大小.Height / 2), 控制点大小))
            grap.FillEllipse(控制点Brush, New Rectangle(p2 - New Size(控制点大小.Width / 2, 控制点大小.Height / 2), 控制点大小))
            grap.DrawLine(控制点Pen, p0, p1)
            grap.DrawLine(控制点Pen, p3, p2)
        End Using
    End Sub
    '限制0-1
    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
        Select Case CheckBox7.Checked
            Case True
                PointBar3.最小值 = New Point(0, 0)
                PointBar3.最大值 = New Point(100, 100)
                PointBar4.最小值 = New Point(0, 0)
                PointBar4.最大值 = New Point(100, 100)

                If PointBar3.值.X < PointBar3.最小值.X Then PointBar3.值 = New Point(PointBar3.最小值.X, PointBar3.值.Y)
                If PointBar3.值.X > PointBar3.最大值.X Then PointBar3.值 = New Point(PointBar3.最大值.X, PointBar3.值.Y)
                If PointBar3.值.Y < PointBar3.最小值.Y Then PointBar3.值 = New Point(PointBar3.值.X, PointBar3.最小值.Y)
                If PointBar3.值.Y > PointBar3.最大值.Y Then PointBar3.值 = New Point(PointBar3.值.X, PointBar3.最大值.Y)
                If PointBar4.值.X < PointBar4.最小值.X Then PointBar4.值 = New Point(PointBar4.最小值.X, PointBar4.值.Y)
                If PointBar4.值.X > PointBar4.最大值.X Then PointBar4.值 = New Point(PointBar4.最大值.X, PointBar4.值.Y)
                If PointBar4.值.Y < PointBar4.最小值.Y Then PointBar4.值 = New Point(PointBar4.值.X, PointBar4.最小值.Y)
                If PointBar4.值.Y > PointBar4.最大值.Y Then PointBar4.值 = New Point(PointBar4.值.X, PointBar4.最大值.Y)
            Case False
                PointBar3.最小值 = New Point(0, -100)
                PointBar3.最大值 = New Point(100, 200)
                PointBar4.最小值 = New Point(0, -100)
                PointBar4.最大值 = New Point(100, 200)
        End Select
        TextBox8_TextChanged(sender, e)
    End Sub
    Private Sub 绘制灯条(颜色 As Color, 方向 As Integer)
        Dim grap As Graphics = Panel2.CreateGraphics()
        Dim 启动时间 As Date = Date.Now
        Dim 总时间 As TimeSpan = TimeSpan.FromSeconds(2)
        Dim 运行状态 As Boolean = False
        Dim 灯条宽度 As Integer = 10
        Dim 控制点 As PointF() = {New PointF(0.75, 0), New PointF(0.25, 1)}
        Dim 开始动画 As New Thread(Async Sub()
                                   Do While 运行状态 = False
                                       Dim 当前时间 As TimeSpan = Date.Now - 启动时间
                                       Dim 时间进度 As Double = 当前时间.TotalMilliseconds / 总时间.TotalMilliseconds
                                       grap.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                                       If 时间进度 >= 1.0 Then
                                           运行状态 = True
                                           grap.FillRectangle(New SolidBrush(颜色), New Rectangle(0, 0, Panel2.Width, 灯条宽度))
                                       Else
                                           Dim p As PointF = 控件.根据时间进度返回坐标(时间进度, 控制点)
                                           If 方向 = 0 Then
                                               grap.FillRectangle(New SolidBrush(颜色), New Rectangle(0, 0, Panel2.Width * p.Y, 灯条宽度))
                                           Else
                                               grap.FillRectangle(New SolidBrush(颜色), New Rectangle(Panel2.Width * (1 - p.Y), 0, 50, 灯条宽度))
                                           End If

                                       End If
                                       Await Task.Delay(1000 / 100)
                                   Loop
                               End Sub)
        开始动画.Start()
    End Sub
    '刷新曲线
    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 2 Then
            Dim a As New Thread(Async Sub()
                                    Await Task.Delay(50)
                                    Invoke(Sub() TextBox8_TextChanged(sender, e))
                                End Sub)
            a.Start()
        End If
        Form1_SizeChanged(sender, e)
    End Sub
    '准确性测试窗体
    Private Sub KlxPiaoButton10_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton10.Click
        动画曲线准确性测试.Show()
        动画曲线准确性测试.Location = New Point(Left - 动画曲线准确性测试.Width - 20, Top)
        动画曲线准确性测试.Focus()
    End Sub
    '刷新曲线
    Private Sub KlxPiaoTabPage4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles KlxPiaoTabPage4.SelectedIndexChanged
        If KlxPiaoTabPage4.SelectedIndex = 0 Then
            Dim a As New Thread(Async Sub()
                                    Await Task.Delay(50)
                                    Invoke(Sub() TextBox8_TextChanged(sender, e))
                                End Sub)
            a.Start()
        End If
    End Sub
    '复制坐标
    Private Sub KlxPiaoButton12_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton12.Click
        My.Computer.Clipboard.SetText(TextBox8.Text)
    End Sub
    Private Sub KlxPiaoButton11_Click(sender As Object, e As EventArgs) Handles KlxPiaoButton11.Click
        My.Computer.Clipboard.SetText(TextBox9.Text)
    End Sub
    '循环播放
    Private Sub CheckBox8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox8.CheckedChanged
        Select Case CheckBox8.Checked
            Case True

        End Select
    End Sub
#End Region

#Region "颜色"
    Private Sub 颜色亮度TrackBar_值Changed(sender As Object, e As System.ComponentModel.PropertyChangedEventArgs) Handles 颜色亮度TrackBar.值Changed
        Dim kp1 As New List(Of KlxPiaoPanel) From {KlxPiaoPanel8, KlxPiaoPanel9, KlxPiaoPanel10, KlxPiaoPanel11, KlxPiaoPanel12, KlxPiaoPanel13}
        Dim kp2 As New List(Of KlxPiaoPanel) From {KlxPiaoPanel20, KlxPiaoPanel21, KlxPiaoPanel22, KlxPiaoPanel23, KlxPiaoPanel24, KlxPiaoPanel25}
        Dim kp3 As New List(Of KlxPiaoPanel) From {KlxPiaoPanel30, KlxPiaoPanel31, KlxPiaoPanel32, KlxPiaoPanel33, KlxPiaoPanel34, KlxPiaoPanel35}

        For i = 1 To kp1.Count - 1
            kp1(i).BackColor = 颜色.调整亮度(kp1(i - 1).BackColor, 颜色亮度TrackBar.值)
            kp2(i).BackColor = 颜色.调整亮度(kp2(i - 1).BackColor, 颜色亮度TrackBar.值)
            kp3(i).BackColor = 颜色.调整亮度(kp3(i - 1).BackColor, 颜色亮度TrackBar.值)
        Next
    End Sub
    Private Sub KlxPiaoPanel_Click(sender As Object, e As EventArgs)
        Dim kp As KlxPiaoPanel = DirectCast(sender, KlxPiaoPanel)

        显示提示框($"RGB：{kp.BackColor.R}, {kp.BackColor.G}, {kp.BackColor.B}", $"亮度：{颜色.获取亮度(kp.BackColor)}/255({Math.Round(颜色.获取亮度(kp.BackColor) / 255, 2) * 100}%)")
    End Sub


#End Region

    '点击关闭按钮时强制退出程序
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        End
    End Sub

End Class