Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Windows.Forms

Public Class KlxPiaoForm
    Inherits Form

    Public Enum 位置
        左
        居中
        右
    End Enum

    Public Enum 窗体按钮样式
        仅显示关闭
        显示关闭和最小化
        全部显示
        不显示
    End Enum

    Public Enum 拖动位置
        仅标题框
        整个窗体
        不启用拖动
    End Enum

    Public Enum 双击位置
        双击标题框时
        双击窗体任意位置时
        不启用
    End Enum

    Private _标题框颜色 As Color
    Private _标题框高度 As Integer
    Private _标题位置 As 位置
    Private _标题字体 As Font
    Private _标题字体颜色 As Color
    Private _标题左右边距 As Integer


    Private _窗体按钮 As 窗体按钮样式
    Private _窗体边框颜色 As Color
    Private _全屏时隐藏窗体边框 As Boolean
    Private _窗体可拖动位置 As 拖动位置
    Private _窗体可调整大小 As Boolean
    Private _快捷缩放方式 As 双击位置
    Private _启用缩放动画 As Boolean
    Private _启用启动动画 As Boolean
    Private _启用关闭动画 As Boolean


    Private _窗体按钮宽度 As Integer
    '窗体按钮，不知道怎么在窗体设计器中展开或收起
    Private _关闭按钮前景色 As Color
    Private _关闭按钮背景色 As Color
    Private _关闭按钮鼠标移入背景色 As Color
    Private _关闭按钮鼠标按下背景色 As Color
    Private _最小化按钮前景色 As Color
    Private _最小化按钮背景色 As Color
    Private _最小化按钮鼠标移入背景色 As Color
    Private _最小化按钮鼠标按下背景色 As Color
    Private _缩放按钮前景色 As Color
    Private _缩放按钮背景色 As Color
    Private _缩放按钮鼠标移入背景色 As Color
    Private _缩放按钮鼠标按下背景色 As Color

    Public Sub New()
        MyBase.New()

        _标题框颜色 = Color.Linen
        _标题框高度 = 31
        _标题位置 = 位置.左
        _标题字体 = New Font("微软雅黑 Light", 9)
        _标题字体颜色 = Color.Black
        _标题左右边距 = 11

        _窗体按钮 = 窗体按钮样式.全部显示
        _窗体边框颜色 = Color.FromArgb(100, 100, 100)
        _全屏时隐藏窗体边框 = True
        _窗体可拖动位置 = 拖动位置.仅标题框
        _窗体可调整大小 = True
        _快捷缩放方式 = 双击位置.双击标题框时
        _启用缩放动画 = True
        _启用启动动画 = True
        _启用关闭动画 = True

        _窗体按钮宽度 = 40

        _关闭按钮前景色 = Color.Black
        _关闭按钮背景色 = Color.Linen
        _关闭按钮鼠标移入背景色 = Color.BlanchedAlmond
        _关闭按钮鼠标按下背景色 = Color.NavajoWhite
        _最小化按钮前景色 = Color.Black
        _最小化按钮背景色 = Color.Linen
        _最小化按钮鼠标移入背景色 = Color.BlanchedAlmond
        _最小化按钮鼠标按下背景色 = Color.NavajoWhite
        _缩放按钮前景色 = Color.Black
        _缩放按钮背景色 = Color.Linen
        _缩放按钮鼠标移入背景色 = Color.BlanchedAlmond
        _缩放按钮鼠标按下背景色 = Color.NavajoWhite

        BackColor = Color.White
        Font = New Font("微软雅黑 Light", 9)
        Text = "KlxPiaoForm"
        StartPosition = FormStartPosition.CenterScreen
        Size = New Size(700, 450)
        FormBorderStyle = FormBorderStyle.None
        DoubleBuffered = True
    End Sub

    <Category("标题外观"), Description("标题框的背景色")>
    Public Property 标题框颜色 As Color
        Get
            Return _标题框颜色
        End Get
        Set(value As Color)
            _标题框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("标题外观"), Description("标题框的高度")>
    Public Property 标题框高度 As Integer
        Get
            Return _标题框高度
        End Get
        Set(value As Integer)
            _标题框高度 = value
            Invalidate()
        End Set
    End Property
    <Category("标题外观"), Description("标题位于标题框的位置")>
    Public Property 标题位置 As 位置
        Get
            Return _标题位置
        End Get
        Set(value As 位置)
            _标题位置 = value
            Invalidate()
        End Set
    End Property
    <Category("标题外观"), Description("标题文字的字体")>
    Public Property 标题字体 As Font
        Get
            Return _标题字体
        End Get
        Set(value As Font)
            _标题字体 = value
            Invalidate()
        End Set
    End Property
    <Category("标题外观"), Description("标题文字的前景色")>
    Public Property 标题字体颜色 As Color
        Get
            Return _标题字体颜色
        End Get
        Set(value As Color)
            _标题字体颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("标题外观"), Description("标题文字位于标题框左侧或右侧的最小距离")>
    Public Property 标题左右边距 As Integer
        Get
            Return _标题左右边距
        End Get
        Set(value As Integer)
            _标题左右边距 = value
            Invalidate()
        End Set
    End Property


    <Category("窗体特性"), Description("显示在窗体上的按钮（包含最小化、缩放、和关闭按钮）")>
    Public Property 窗体按钮 As 窗体按钮样式
        Get
            Return _窗体按钮
        End Get
        Set(value As 窗体按钮样式)
            _窗体按钮 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体特性")， Description("窗体边框的颜色")>
    Public Property 窗体边框颜色 As Color
        Get
            Return _窗体边框颜色
        End Get
        Set(value As Color)
            _窗体边框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体特性")， Description("设置为True，全屏时隐藏窗体边框")>
    Public Property 全屏时隐藏窗体边框 As Boolean
        Get
            Return _全屏时隐藏窗体边框
        End Get
        Set(value As Boolean)
            _全屏时隐藏窗体边框 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体特性"), Description("窗体可以拖动的位置，当窗体最大化时不允许拖动")>
    Public Property 窗体可拖动位置 As 拖动位置
        Get
            Return _窗体可拖动位置
        End Get
        Set(value As 拖动位置)
            _窗体可拖动位置 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体特性"), Description("设置为True后，窗体可调整大小")>
    Public Property 窗体可调整大小 As Boolean
        Get
            Return _窗体可调整大小
        End Get
        Set(value As Boolean)
            _窗体可调整大小 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体特性"), Description("执行最大化或还原的快捷方式，不显示缩放按钮时，此快捷方式失效")>
    Public Property 快捷缩放方式 As 双击位置
        Get
            Return _快捷缩放方式
        End Get
        Set(value As 双击位置)
            _快捷缩放方式 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体特性"), Description("最大化或还原的动画，可以使用""New 设置WindowState""手动执行动画")>
    Public Property 启用缩放动画 As Boolean
        Get
            Return _启用缩放动画
        End Get
        Set(value As Boolean)
            _启用缩放动画 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体特性"), Description("是否启用启动动画")>
    Public Property 启用启动动画 As Boolean
        Get
            Return _启用启动动画
        End Get
        Set(value As Boolean)
            _启用启动动画 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体特性"), Description("是否启用关闭动画")>
    Public Property 启用关闭动画 As Boolean
        Get
            Return _启用关闭动画
        End Get
        Set(value As Boolean)
            _启用关闭动画 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("设置每个窗体按钮的宽度")>
    Public Property 窗体按钮宽度 As Integer
        Get
            Return _窗体按钮宽度
        End Get
        Set(value As Integer)
            _窗体按钮宽度 = value
            Invalidate()
        End Set
    End Property


    <Category("窗体按钮"), Description("窗体按钮的前景色")>
    Public Property 关闭按钮前景色 As Color
        Get
            Return _关闭按钮前景色
        End Get
        Set(value As Color)
            _关闭按钮前景色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("窗体按钮的按钮背景色")>
    Public Property 关闭按钮背景色 As Color
        Get
            Return _关闭按钮背景色
        End Get
        Set(value As Color)
            _关闭按钮背景色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("鼠标移入时按钮背景色")>
    Public Property 关闭按钮鼠标移入背景色 As Color
        Get
            Return _关闭按钮鼠标移入背景色
        End Get
        Set(value As Color)
            _关闭按钮鼠标移入背景色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("鼠标按下时按钮背景色")>
    Public Property 关闭按钮鼠标按下背景色 As Color
        Get
            Return _关闭按钮鼠标按下背景色
        End Get
        Set(value As Color)
            _关闭按钮鼠标按下背景色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("窗体按钮的前景色")>
    Public Property 最小化按钮前景色 As Color
        Get
            Return _最小化按钮前景色
        End Get
        Set(value As Color)
            _最小化按钮前景色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("窗体按钮的按钮背景色")>
    Public Property 最小化按钮背景色 As Color
        Get
            Return _最小化按钮背景色
        End Get
        Set(value As Color)
            _最小化按钮背景色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("鼠标移入时按钮背景色")>
    Public Property 最小化按钮鼠标移入背景色 As Color
        Get
            Return _最小化按钮鼠标移入背景色
        End Get
        Set(value As Color)
            _最小化按钮鼠标移入背景色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("鼠标按下时按钮背景色")>
    Public Property 最小化按钮鼠标按下背景色 As Color
        Get
            Return _最小化按钮鼠标按下背景色
        End Get
        Set(value As Color)
            _最小化按钮鼠标按下背景色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("窗体按钮的前景色")>
    Public Property 缩放按钮前景色 As Color
        Get
            Return _缩放按钮前景色
        End Get
        Set(value As Color)
            _缩放按钮前景色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("窗体按钮的按钮背景色")>
    Public Property 缩放按钮背景色 As Color
        Get
            Return _缩放按钮背景色
        End Get
        Set(value As Color)
            _缩放按钮背景色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("鼠标移入时按钮背景色")>
    Public Property 缩放按钮鼠标移入背景色 As Color
        Get
            Return _缩放按钮鼠标移入背景色
        End Get
        Set(value As Color)
            _缩放按钮鼠标移入背景色 = value
            Invalidate()
        End Set
    End Property
    <Category("窗体按钮"), Description("鼠标按下时按钮背景色")>
    Public Property 缩放按钮鼠标按下背景色 As Color
        Get
            Return _缩放按钮鼠标按下背景色
        End Get
        Set(value As Color)
            _缩放按钮鼠标按下背景色 = value
            Invalidate()
        End Set
    End Property

    <Browsable(False)>
    Public Overloads Property FormBorderStyle As FormBorderStyle
        Get
            Return MyBase.FormBorderStyle
        End Get
        Set(value As FormBorderStyle)
            MyBase.FormBorderStyle = value
        End Set
    End Property

    Private WithEvents CloseButton As New Button
    Private WithEvents MinButton As New Button
    Private WithEvents ResizingButton As New Button
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics

        '绘制标题框
        Using brush As New SolidBrush(标题框颜色)
            g.FillRectangle(brush, New Rectangle(0, 0, Width, 标题框高度))
        End Using

        '绘制边框
        Using BorderPen As New Pen(窗体边框颜色, 1)
            If Not (WindowState = FormWindowState.Maximized AndAlso 全屏时隐藏窗体边框) Then
                g.DrawRectangle(BorderPen, 0, 0, Width - 1, Height - 1)
            End If
        End Using

        '绘制标题
        Using brush As New SolidBrush(标题字体颜色)
            Dim 文字大小 As SizeF = g.MeasureString(Text, 标题字体)
            Dim 文字位置x As Integer
            Dim 文字位置y As Integer = (标题框高度 - 文字大小.Height) \ 2

            Select Case 标题位置
                Case 位置.左
                    文字位置x = 标题左右边距
                Case 位置.居中
                    文字位置x = (Width - 文字大小.Width) \ 2
                Case 位置.右 '防止窗体按钮和标题文字重叠
                    Select Case 窗体按钮
                        Case 窗体按钮样式.不显示
                            文字位置x = Width - 文字大小.Width - 标题左右边距
                        Case 窗体按钮样式.显示关闭和最小化
                            文字位置x = Width - 文字大小.Width - 标题左右边距 - 窗体按钮宽度 * 2
                        Case 窗体按钮样式.仅显示关闭
                            文字位置x = Width - 文字大小.Width - 标题左右边距 - 窗体按钮宽度
                        Case 窗体按钮样式.全部显示
                            文字位置x = Width - 文字大小.Width - 标题左右边距 - 窗体按钮宽度 * 3
                    End Select
            End Select

            g.DrawString(Text, 标题字体, brush, 文字位置x, 文字位置y)

        End Using

        '窗体按钮
        CloseButton.Size = New Size(窗体按钮宽度, 标题框高度 - 1)
        CloseButton.Location = New Point(Width - 窗体按钮宽度 - 1, 1)
        CloseButton.FlatStyle = FlatStyle.Flat
        CloseButton.BackColor = 关闭按钮背景色
        CloseButton.ForeColor = 关闭按钮前景色
        CloseButton.Padding = New Padding(3, 0, 0, 0)
        CloseButton.Text = "⨉"
        CloseButton.Font = New Font("微软雅黑", 10.5)
        CloseButton.FlatAppearance.BorderSize = 0
        CloseButton.FlatAppearance.MouseDownBackColor = 关闭按钮鼠标按下背景色
        CloseButton.FlatAppearance.MouseOverBackColor = 关闭按钮鼠标移入背景色

        MinButton.Size = New Size(窗体按钮宽度, 标题框高度 - 1)
        MinButton.FlatStyle = FlatStyle.Flat
        MinButton.BackColor = 最小化按钮背景色
        MinButton.ForeColor = 最小化按钮前景色
        MinButton.Padding = New Padding(3, 0, 0, 0)
        MinButton.Text = "–"
        MinButton.Font = New Font("微软雅黑", 10.5)
        MinButton.FlatAppearance.BorderSize = 0
        MinButton.FlatAppearance.MouseDownBackColor = 最小化按钮鼠标按下背景色
        MinButton.FlatAppearance.MouseOverBackColor = 最小化按钮鼠标移入背景色

        ResizingButton.Size = New Size(窗体按钮宽度, 标题框高度 - 1)
        ResizingButton.Location = New Point(Width - 窗体按钮宽度 * 2 - 1, 1)
        ResizingButton.FlatStyle = FlatStyle.Flat
        ResizingButton.BackColor = 缩放按钮背景色
        ResizingButton.ForeColor = 缩放按钮前景色
        ResizingButton.FlatAppearance.BorderSize = 0
        ResizingButton.FlatAppearance.MouseDownBackColor = 缩放按钮鼠标按下背景色
        ResizingButton.FlatAppearance.MouseOverBackColor = 缩放按钮鼠标移入背景色

        '适应最大化、还原图标
        Select Case WindowState
            Case FormWindowState.Maximized
                ResizingButton.Image = 图片处理.图片颜色替换(My.Resources.还原按钮图标, Color.Black, 缩放按钮前景色)
            Case FormWindowState.Normal, FormWindowState.Minimized
                ResizingButton.Image = 图片处理.图片颜色替换(My.Resources.最大化按钮图标, Color.Black, 缩放按钮前景色)
        End Select

        Select Case 窗体按钮
            Case 窗体按钮样式.显示关闭和最小化
                MinButton.Location = New Point(Width - 窗体按钮宽度 * 2 - 1, 1) '倒数第二是最小化按钮

                Controls.Remove(ResizingButton)
                Controls.Add(CloseButton)
                Controls.Add(MinButton)
            Case 窗体按钮样式.仅显示关闭
                Controls.Remove(MinButton)
                Controls.Remove(ResizingButton)
                Controls.Add(CloseButton)

            Case 窗体按钮样式.全部显示
                MinButton.Location = New Point(Width - 窗体按钮宽度 * 3 - 1, 1) '倒数第三是最小化按钮
                Controls.Add(CloseButton)
                Controls.Add(MinButton)
                Controls.Add(ResizingButton)

            Case 窗体按钮样式.不显示
                Controls.Remove(CloseButton)
                Controls.Remove(MinButton)
                Controls.Remove(ResizingButton)
        End Select
    End Sub


#Region "最大化/还原、缩放、最小化按钮"
    Private Sub MinButton_Click(sender As Object, e As EventArgs) Handles MinButton.Click
        WindowState = FormWindowState.Minimized
    End Sub

    '关闭动画（模拟Windows关闭动画）
    Private isClosing As Boolean = False
    Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
        CloseForm()
    End Sub
    Protected Overrides Sub OnClosing(e As CancelEventArgs)
        MyBase.OnClosing(e)
        CloseForm()
    End Sub
    Protected Overrides Sub OnClosed(e As EventArgs)
        MyBase.OnClosed(e)
        CloseForm()
    End Sub
    Private ReadOnly 动画长度 As Integer = 6
    Private Async Sub CloseForm()
        If Not isClosing Then
            isClosing = True

            If 启用关闭动画 Then
                Dim newbit As New Bitmap(Width, Height)
                Using g As Graphics = Graphics.FromImage(newbit)
                    g.CopyFromScreen(Left, Top, 0, 0, Size)
                End Using
                BackgroundImageLayout = ImageLayout.Zoom
                BackgroundImage = newbit
                Controls.Clear()
                For i = 0 To 动画长度
                    Size -= New Size(i, i)
                    Location += New Size(i \ 2, i \ 2)

                    Opacity = (100 - i * (100 \ 动画长度)) / 100
                    Await Task.Delay(10)
                Next
                Opacity = 0
            End If

            Close()
        End If
    End Sub

    '启动动画
    Private Sub KlxPiaoForm_Load(sender As Object, e As EventArgs) Handles Me.Load
        If 启用启动动画 Then
            FormBorderStyle = FormBorderStyle.FixedDialog
            FormBorderStyle = FormBorderStyle.None
        End If
    End Sub

    '最大化、还原按钮
    Private Sub ResizingButton_Click(sender As Object, e As EventArgs) Handles ResizingButton.Click
        '在边缘最大化时恢复图标
        Cursor = Cursors.Default

        Select Case WindowState
            Case FormWindowState.Normal
                Dim 设置样式 As New 设置WindowState With {
                    .应用于 = Me,
                    .样式 = FormWindowState.Maximized,
                    .启用动画 = 启用缩放动画
                }

                设置样式.设置()
            Case FormWindowState.Maximized
                Dim 设置样式 As New 设置WindowState With {
                    .应用于 = Me,
                    .样式 = FormWindowState.Normal,
                    .启用动画 = 启用缩放动画
                }

                设置样式.设置()
        End Select
    End Sub

#End Region

    '标题文字改变时重绘
    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e)

        Refresh()
    End Sub

#Region "窗体拖动和调整大小"
    Private Enum 调整位置
        左边
        上边
        右边
        下边

        调完了

        左上
        右上
        左下
        右下
    End Enum

    Private MouseDownLocation As Point

    Private 原来的窗体位置 As Point
    Private 原来的窗体大小 As Size

    Private 正在调整的位置 As 调整位置 = 调整位置.调完了

    '提取了KlxPiaoForm的事件，只有调整大小没有拖动（只响应上边和右边）
    Private Sub 窗体按钮_MouseDown(sender As Object, e As MouseEventArgs) Handles MinButton.MouseDown, CloseButton.MouseDown, ResizingButton.MouseDown
        If 窗体可调整大小 = True AndAlso (Not WindowState = FormWindowState.Maximized) Then
            Dim 上边 As Boolean = Cursor.Position.Y < Top + 7
            Dim 右边 As Boolean = Cursor.Position.X > Left + Width - 7

            原来的窗体位置 = Location
            原来的窗体大小 = Size

            If 右边 Then
                If 上边 Then
                    正在调整的位置 = 调整位置.右上
                Else
                    正在调整的位置 = 调整位置.右边
                End If
            ElseIf 上边 Then
                正在调整的位置 = 调整位置.上边
            End If
        End If
    End Sub
    Private Sub 窗体按钮_MouseMove(sender As Object, e As MouseEventArgs) Handles MinButton.MouseMove, CloseButton.MouseMove, ResizingButton.MouseMove
        If 窗体可调整大小 = True AndAlso (Not WindowState = FormWindowState.Maximized) Then
            Dim 上边 As Boolean = Cursor.Position.Y < Top + 7
            Dim 右边 As Boolean = Cursor.Position.X > Left + Width - 7

            Dim 在边缘 As Boolean = 上边 Or 右边

            '调整光标图标
            If 在边缘 Then
                If 右边 Then
                    Cursor = Cursors.SizeWE
                ElseIf 上边 Then
                    Cursor = Cursors.SizeNS
                End If

                If 右边 And 上边 Then
                    Cursor = Cursors.SizeNESW
                End If
            Else
                Cursor = Cursors.Default
            End If

            '响应传递的参数
            Select Case 正在调整的位置

                Case 调整位置.上边
                    Top = Cursor.Position.Y
                    Height = 原来的窗体大小.Height + 原来的窗体位置.Y - Cursor.Position.Y

                Case 调整位置.右边
                    Width = Cursor.Position.X - Left

                Case 调整位置.右上
                    Top = Cursor.Position.Y
                    Height = 原来的窗体大小.Height + 原来的窗体位置.Y - Cursor.Position.Y
                    Width = Cursor.Position.X - Left

                Case 调整位置.调完了
                    Exit Select
            End Select

            '还没调完，重绘
            If Not 正在调整的位置 = 调整位置.调完了 Then
                Refresh()
            End If
        End If
    End Sub
    Private Sub 窗体按钮_MouseUp(sender As Object, e As MouseEventArgs) Handles MinButton.MouseUp, CloseButton.MouseUp, ResizingButton.MouseUp
        If 窗体可调整大小 = True AndAlso (Not WindowState = FormWindowState.Maximized) Then
            正在调整的位置 = 调整位置.调完了
        End If
    End Sub

    '响应窗体拖动，和调整大小
    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)

        '窗体拖动
        If 窗体可拖动位置 <> 拖动位置.不启用拖动 AndAlso WindowState <> FormWindowState.Maximized AndAlso e.Button = MouseButtons.Left Then
            If 窗体可拖动位置 = 拖动位置.整个窗体 Then
                MouseDownLocation = e.Location
            ElseIf 窗体可拖动位置 = 拖动位置.仅标题框 AndAlso e.Y < 标题框高度 Then
                MouseDownLocation = e.Location
            End If
        End If

        '调整大小
        If 窗体可调整大小 AndAlso (Not WindowState = FormWindowState.Maximized) Then
            Dim 左边 As Boolean = Cursor.Position.X < Left + 7
            Dim 上边 As Boolean = Cursor.Position.Y < Top + 7
            Dim 右边 As Boolean = Cursor.Position.X > Left + Width - 7
            Dim 下边 As Boolean = Cursor.Position.Y > Top + Height - 7

            原来的窗体位置 = Location
            原来的窗体大小 = Size

            '传递参数
            If 左边 Then

                If 上边 Then
                    正在调整的位置 = 调整位置.左上
                ElseIf 下边 Then
                    正在调整的位置 = 调整位置.左下
                Else
                    正在调整的位置 = 调整位置.左边
                End If

            ElseIf 右边 Then

                If 上边 Then
                    正在调整的位置 = 调整位置.右上
                ElseIf 下边 Then
                    正在调整的位置 = 调整位置.右下
                Else
                    正在调整的位置 = 调整位置.右边
                End If

            ElseIf 上边 Then
                正在调整的位置 = 调整位置.上边

            ElseIf 下边 Then
                正在调整的位置 = 调整位置.下边
            End If
        End If
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        '正在调整窗体大小时不允许拖动窗体
        If 正在调整的位置 = 调整位置.调完了 AndAlso e.Button = MouseButtons.Left AndAlso MouseDownLocation <> Point.Empty Then
            Location += (e.Location - MouseDownLocation)
        End If

        '调整大小
        If 窗体可调整大小 = True AndAlso (Not WindowState = FormWindowState.Maximized) Then
            Dim 左边 As Boolean = Cursor.Position.X < Left + 7
            Dim 上边 As Boolean = Cursor.Position.Y < Top + 7
            Dim 右边 As Boolean = Cursor.Position.X > Left + Width - 7
            Dim 下边 As Boolean = Cursor.Position.Y > Top + Height - 7

            Dim 在边缘 As Boolean = 左边 Or 上边 Or 右边 Or 下边

            '调整光标图标
            If 在边缘 Then
                If 左边 Or 右边 Then
                    Cursor = Cursors.SizeWE
                ElseIf 上边 Or 下边 Then
                    Cursor = Cursors.SizeNS
                End If

                If 左边 And 上边 Then
                    Cursor = Cursors.SizeNWSE
                ElseIf 右边 And 上边 Then
                    Cursor = Cursors.SizeNESW
                ElseIf 左边 And 下边 Then
                    Cursor = Cursors.SizeNESW
                ElseIf 右边 And 下边 Then
                    Cursor = Cursors.SizeNWSE
                End If
            Else
                Cursor = Cursors.Default
            End If

            '响应传递的参数（应该还可以简化）
            Select Case 正在调整的位置
                Case 调整位置.左边
                    Left = Cursor.Position.X
                    Width = 原来的窗体大小.Width + 原来的窗体位置.X - Cursor.Position.X

                Case 调整位置.上边
                    Top = Cursor.Position.Y
                    Height = 原来的窗体大小.Height + 原来的窗体位置.Y - Cursor.Position.Y

                Case 调整位置.右边
                    Width = Cursor.Position.X - Left

                Case 调整位置.下边
                    Height = Cursor.Position.Y - Top

                Case 调整位置.左上
                    Location = Cursor.Position
                    Width = 原来的窗体大小.Width + 原来的窗体位置.X - Cursor.Position.X
                    Height = 原来的窗体大小.Height + 原来的窗体位置.Y - Cursor.Position.Y

                Case 调整位置.左下
                    Left = Cursor.Position.X
                    Width = 原来的窗体大小.Width + 原来的窗体位置.X - Cursor.Position.X
                    Height = Cursor.Position.Y - Top

                Case 调整位置.右上
                    Top = Cursor.Position.Y
                    Height = 原来的窗体大小.Height + 原来的窗体位置.Y - Cursor.Position.Y
                    Width = Cursor.Position.X - Left

                Case 调整位置.右下
                    Size = Cursor.Position - Location

                Case 调整位置.调完了
                    Exit Select
            End Select

            '还没调完，重绘
            If Not 正在调整的位置 = 调整位置.调完了 Then
                '如果这里报错了，很有可能因为加载了本地字体
                Refresh()
            End If
        End If
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)

        MouseDownLocation = Point.Empty

        If 窗体可调整大小 = True AndAlso (Not WindowState = FormWindowState.Maximized) Then
            正在调整的位置 = 调整位置.调完了
        End If
    End Sub

    '缩放快捷方式（双击标题栏）
    Protected Overrides Sub OnDoubleClick(e As EventArgs)
        MyBase.OnDoubleClick(e)

        '仅只有显示缩放按钮时才允许最大化、还原

        If 窗体按钮 = 窗体按钮样式.全部显示 Then

            Select Case 快捷缩放方式
                Case 双击位置.双击标题框时

                    If Cursor.Position.Y - Top < 标题框高度 Then
                        ResizingButton.PerformClick()
                    End If

                Case 双击位置.双击窗体任意位置时
                    ResizingButton.PerformClick()

                Case 双击位置.不启用
                    Exit Select

            End Select

        End If
    End Sub
#End Region

    '-----KlxPiaoForm函数

#Region "加载主题文件"
    ''' <summary>
    ''' 加载现有主题文件或资源文件
    ''' </summary>
    ''' <param name="文件路径或资源文件"></param>
    Public Sub 加载主题文件(文件路径或资源文件 As String)
        Dim 未解析数据 As String()

        If 文件路径或资源文件.IndexOf("\") > -1 Then
            未解析数据 = File.ReadAllLines(文件路径或资源文件)
        Else
            未解析数据 = 文件路径或资源文件.Split(vbCrLf)
        End If

        Dim 读取属性并设置 As Action(Of String) = Sub(属性名)

                                               Dim 值 As String = 读取值(未解析数据, 属性名)

                                               If 值 <> "未找到" Then

                                                   Try
                                                       Select Case 值.IndexOf(",")
                                                           Case Is > -1
                                                               '格式RGB
                                                               Dim R As Integer = 值.Split(",")(0)
                                                               Dim G As Integer = 值.Split(",")(1)
                                                               Dim B As Integer = 值.Split(",")(2)

                                                               控件.设置属性(Me, 属性名, Color.FromArgb(R, G, B))
                                                           Case Else
                                                               '格式Hex
                                                               控件.设置属性(Me, 属性名, ColorTranslator.FromHtml(值))
                                                       End Select

                                                   Catch ex As Exception
                                                       Throw New Exception($"格式不正确：{属性名}={值}")
                                                   End Try

                                               Else
                                                   Throw New Exception($"{属性名} 未设置值")
                                               End If
                                           End Sub

        Dim 属性 As New List(Of String) From {"BackColor", "窗体边框颜色", "标题框颜色", "标题字体颜色", "关闭按钮背景色", "关闭按钮前景色", "关闭按钮鼠标按下背景色", "关闭按钮鼠标移入背景色", "缩放按钮背景色", "缩放按钮前景色", "缩放按钮鼠标按下背景色", "缩放按钮鼠标移入背景色", "最小化按钮背景色", "最小化按钮前景色", "最小化按钮鼠标按下背景色", "最小化按钮鼠标移入背景色"}

        属性.ForEach(Sub(n) 读取属性并设置(n))
    End Sub
    Private Function 读取值(全部文本 As String(), 名称 As String)
        Dim 值 As String = ""

        For Each a In 全部文本
            If a.Contains(名称) Then
                值 = a.Substring(a.IndexOf("=") + 1)
            End If
        Next

        Return If(值 = "", "未找到", 值)
    End Function
#End Region

#Region "导出主题文件"
    ''' <summary>
    ''' 导出主题文件
    ''' </summary>
    ''' <param name="文件路径"></param>
    Public Sub 导出主题文件(文件路径 As String)
        Using 写入文件 As New StreamWriter(文件路径)
            写入文件.WriteLine($"[KlxPiaoForm窗体属性]")
            Dim KlxPiaoForm窗体属性 As New List(Of String) From {"BackColor", "窗体边框颜色"}
            KlxPiaoForm窗体属性.ForEach(Sub(n) 写入文件.WriteLine($"{n}={控件.读取属性(Me, n).R},{控件.读取属性(Me, n).G},{控件.读取属性(Me, n).B}"))

            写入文件.WriteLine($"{vbCrLf}[KlxPiaoForm标题外观]")
            Dim KlxPiaoForm标题外观 As New List(Of String) From {"标题框颜色", "标题字体颜色"}
            KlxPiaoForm标题外观.ForEach(Sub(n) 写入文件.WriteLine($"{n}={控件.读取属性(Me, n).R},{控件.读取属性(Me, n).G},{控件.读取属性(Me, n).B}"))

            写入文件.WriteLine($"{vbCrLf}[KlxPiaoForm窗体按钮]")
            Dim KlxPiaoForm窗体按钮 As New List(Of String) From {"关闭按钮背景色", "关闭按钮前景色", "关闭按钮鼠标按下背景色", "关闭按钮鼠标移入背景色", "缩放按钮背景色", "缩放按钮前景色", "缩放按钮鼠标按下背景色", "缩放按钮鼠标移入背景色", "最小化按钮背景色", "最小化按钮前景色", "最小化按钮鼠标按下背景色", "最小化按钮鼠标移入背景色"}
            KlxPiaoForm窗体按钮.ForEach(Sub(n) 写入文件.WriteLine($"{n}={控件.读取属性(Me, n).R},{控件.读取属性(Me, n).G},{控件.读取属性(Me, n).B}"))
        End Using
    End Sub
#End Region

    ''' <summary>
    ''' 最大化/还原时的动画
    ''' </summary>
    Public Class 设置WindowState
        ''' <summary>
        ''' 需要设置窗体样式的Form
        ''' </summary>
        Public 应用于 As Form
        Public 样式 As FormWindowState
        ''' <summary>
        ''' 是否启用动画，默认为True
        ''' </summary>
        Public 启用动画 As Boolean = True
        ''' <summary>
        ''' 动画持续的时间(毫秒)，默认为50
        ''' </summary>
        Public 持续时间 As Integer = 50
        ''' <summary>
        ''' 为0-100之间，默认为50
        ''' </summary>
        Public 最低透明度 As Integer = 50

        Public Sub 设置()
            If 启用动画 Then
                For i = 100 To 最低透明度 Step -((100 - 最低透明度) / (持续时间 / 10))
                    应用于.Opacity = i / 100
                    Threading.Thread.Sleep(10)
                Next

                应用于.WindowState = 样式
                应用于.Refresh()

                For i = 最低透明度 To 100 Step ((100 - 最低透明度) / (持续时间 / 10))
                    应用于.Opacity = i / 100
                    Threading.Thread.Sleep(10)
                Next
            Else
                应用于.WindowState = 样式
                应用于.Refresh()
            End If

        End Sub
    End Class

    ''' <summary>
    ''' 很容易卡死闪退
    ''' </summary>
    ''' <param name="文件路径"></param>
    ''' <param name="控件"></param>
    ''' 
    Public Sub 应用本地字体(文件路径 As String, 控件 As Control)
        If Not (TypeOf 控件 Is Form OrElse TypeOf 控件 Is ContainerControl) Then
            控件.Font = 文件处理.加载字体(文件路径, 控件.Font.Size, 控件.Font.Style)
        End If

        For Each ctrl As Control In 控件.Controls
            ctrl.Font = 文件处理.加载字体(文件路径, ctrl.Font.Size, ctrl.Font.Style)

            If ctrl.HasChildren Then
                应用本地字体(文件路径, ctrl)
            End If
        Next
    End Sub

    ''' <summary>
    ''' 把一个窗体的主题复制到另一个窗体
    ''' </summary>
    ''' <param name="从"></param>
    ''' <param name="到"></param>
    Public Sub 复制主题(从 As KlxPiaoForm, 到 As KlxPiaoForm)
        到.关闭按钮背景色 = 从.关闭按钮背景色
        到.关闭按钮前景色 = 从.关闭按钮前景色
        到.关闭按钮鼠标移入背景色 = 从.关闭按钮鼠标移入背景色
        到.关闭按钮鼠标按下背景色 = 从.关闭按钮鼠标按下背景色
        到.最小化按钮背景色 = 从.最小化按钮背景色
        到.最小化按钮前景色 = 从.最小化按钮前景色
        到.最小化按钮鼠标移入背景色 = 从.最小化按钮鼠标移入背景色
        到.最小化按钮鼠标按下背景色 = 从.最小化按钮鼠标按下背景色
        到.缩放按钮背景色 = 从.缩放按钮背景色
        到.缩放按钮前景色 = 从.缩放按钮前景色
        到.缩放按钮鼠标移入背景色 = 从.缩放按钮鼠标移入背景色
        到.缩放按钮鼠标按下背景色 = 从.缩放按钮鼠标按下背景色
        到.标题框颜色 = 从.标题框颜色
        到.标题字体颜色 = 从.标题字体颜色
    End Sub

    ''' <summary>
    ''' 根据一个颜色自动生成主题并应用于窗体上
    ''' </summary>
    ''' <param name="主题色"></param>
    Public Sub 自动生成主题(主题色 As Color)
        Dim 浅色主题 As Boolean = 颜色.获取亮度(主题色) > 125
        Dim 前景色 = If(浅色主题, Color.Black, Color.White)
        Dim 鼠标移入背景色 = 颜色.调整亮度(主题色, If(浅色主题, -0.03, 0.03))
        Dim 鼠标按下背景色 = 颜色.调整亮度(鼠标移入背景色, If(浅色主题, -0.03, 0.03))

        标题框颜色 = 主题色
        标题字体颜色 = 前景色

        缩放按钮背景色 = 主题色
        缩放按钮前景色 = 前景色
        缩放按钮鼠标移入背景色 = 鼠标移入背景色
        缩放按钮鼠标按下背景色 = 鼠标按下背景色

        最小化按钮背景色 = 主题色
        最小化按钮前景色 = 前景色
        最小化按钮鼠标移入背景色 = 鼠标移入背景色
        最小化按钮鼠标按下背景色 = 鼠标按下背景色

        关闭按钮背景色 = 主题色
        关闭按钮前景色 = 前景色
        关闭按钮鼠标移入背景色 = 鼠标移入背景色
        关闭按钮鼠标按下背景色 = 鼠标按下背景色

        Refresh()
    End Sub

End Class