Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Windows.Forms.Design

<Designer(GetType(设计时交互支持))>
Public Class KlxPiaoTabControl
    Inherits Control

    Private _绑定 As KlxPiaoTabPage
    Private _选项卡大小 As Size
    Private _文字位置 As ContentAlignment
    Private _图片位置 As ContentAlignment
    Private _边框颜色 As Color
    Private _投影颜色 As Color
    Private _投影长度 As Integer

    Public Sub New()
        MyBase.New()

        SetStyle(ControlStyles.ContainerControl, True)
        DoubleBuffered = True

        _选项卡大小 = New Size(88, 33)
        _文字位置 = ContentAlignment.MiddleCenter
        _图片位置 = ContentAlignment.MiddleLeft
        _边框颜色 = Color.Gainsboro
        _投影颜色 = Color.Gainsboro
        _投影长度 = 5
        _绑定 = Nothing

        Size = New Size(249, 135)
    End Sub

    <Category("KlxPiaoTabControl属性"), Description("选择绑定的KlxPiaoTabPage后，会强制设置绑定选项卡的大小、位置等属性")>
    Public Property 绑定 As KlxPiaoTabPage
        Get
            Return _绑定
        End Get
        Set(value As KlxPiaoTabPage)
            _绑定 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTabControl属性"), Description("选项卡的大小")>
    Public Property 选项卡大小 As Size
        Get
            Return _选项卡大小
        End Get
        Set(value As Size)
            _选项卡大小 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTabControl属性"), Description("文字的位置")>
    Public Property 文字位置 As ContentAlignment
        Get
            Return _文字位置
        End Get
        Set(value As ContentAlignment)
            _文字位置 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTabControl属性"), Description("图片的位置")>
    Public Property 图片位置 As ContentAlignment
        Get
            Return _图片位置
        End Get
        Set(value As ContentAlignment)
            _图片位置 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTabControl属性"), Description("边框的颜色")>
    Public Property 边框颜色 As Color
        Get
            Return _边框颜色
        End Get
        Set(value As Color)
            _边框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTabControl属性"), Description("选项卡菜单边缘的投影颜色")>
    Public Property 投影颜色 As Color
        Get
            Return _投影颜色
        End Get
        Set(value As Color)
            _投影颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTabControl属性"), Description("选项卡菜单边缘的投影长度，为1时隐藏投影，为0时隐藏边框")>
    Public Property 投影长度 As Integer
        Get
            Return _投影长度
        End Get
        Set(value As Integer)
            _投影长度 = value
            Invalidate()
        End Set
    End Property
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics

        g.Clear(Color.White)

        '选项卡边缘
        Using 选项卡Pen As New Pen(边框颜色, 1)
            g.DrawLine(选项卡Pen, 选项卡大小.Width + 1, 0, 选项卡大小.Width + 1, Height)
        End Using

        '更新选项卡菜单
        For Each 清除选项卡 In Controls
            If TypeOf 清除选项卡 Is 无法获得焦点的按钮 Then
                Controls.Remove(清除选项卡)
            End If
        Next

        If 绑定 IsNot Nothing Then

            For i = 0 To 绑定.TabCount - 1
                Dim 标题 As String = 绑定.TabPages(i).Text

                Dim 选项卡按钮 As New 无法获得焦点的按钮(绑定) With {
                    .Size = 选项卡大小,
                    .Location = New Point(1, 1 + i * 选项卡大小.Height),
                    .Text = 标题,
                    .Tag = i,
                    .Font = Font,
                    .Padding = Padding,
                    .TextAlign = 文字位置,
                    .ImageAlign = 图片位置
                }
                选项卡按钮.FlatAppearance.BorderColor = 边框颜色

                If 绑定.ImageList IsNot Nothing Then
                    选项卡按钮.Image = 绑定.ImageList.Images(绑定.TabPages(i).ImageIndex)
                End If

                Controls.Add(选项卡按钮)
            Next

            If Not Controls.Contains(绑定) Then
                Controls.Add(绑定)
            End If

            绑定.Alignment = TabAlignment.Left
            绑定.ItemSize = New Size(0, 1)
            绑定.Location = New Point(选项卡大小.Width + 1 + 投影长度, 0)
            绑定.Size = New Size(Width - 选项卡大小.Width - 1 - 投影长度, Height)
            绑定.边框颜色 = 边框颜色
        Else
            g.DrawString($"绑定{vbCrLf}KlxPiaoTabPage{vbCrLf}以使用{vbCrLf}{vbCrLf}请勿重复绑定", New Font("微软雅黑", 9), New SolidBrush(Color.Red), New Point(6, 6))
        End If

        '绘制投影
        Dim 递减值 As Integer = If(投影长度 = 0, 0, 255 \ 投影长度)
        For i = 选项卡大小.Width + 1 To 选项卡大小.Width + 1 + 投影长度
            Dim 透明度 As Integer = 255 - (i - (选项卡大小.Width + 1)) * 递减值
            Dim 数字0_n As Integer = i - (选项卡大小.Width + 1)
            g.DrawLine(New Pen(Color.FromArgb(透明度, 投影颜色), 1), New Point(i, 0), New Point(i, Height))
        Next

        '边框
        Using BorderPen As New Pen(边框颜色, 1)
            g.DrawRectangle(BorderPen, 0, 0, Width - 1, Height - 1)
        End Using
    End Sub

    Public Sub 设置选项卡索引(索引 As Integer)
        绑定.SelectedIndex = 索引
    End Sub

    Public Function 获取选项卡索引() As Integer
        Return 绑定.SelectedIndex
    End Function

    Public Function 获取绑定的KlxPiaoTabPage() As KlxPiaoTabPage
        Return 绑定
    End Function

    Public Function 获取选中选项卡文字() As String
        Return 绑定.SelectedTab.ToString
    End Function

    Private Class 无法获得焦点的按钮
        Inherits Button

        Private WithEvents 监听Timer As New Timer
        Private ReadOnly _传递 As KlxPiaoTabPage

        Public Sub New(传递变量 As KlxPiaoTabPage)
            MyBase.New()

            SetStyle(ControlStyles.Selectable, False)

            FlatStyle = FlatStyle.Flat
            FlatAppearance.BorderSize = 0
            FlatAppearance.MouseDownBackColor = Color.FromArgb(245, 245, 245)
            FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 245, 245)

            Font = New Font("微软雅黑 Light", 9)

            监听Timer.Interval = 100
            监听Timer.Start()

            _传递 = 传递变量
        End Sub

        Private Sub Button_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
            If e.Button = MouseButtons.Left Then
                _传递.SelectedIndex = CInt(Tag)
            End If
        End Sub

        '启用视觉反馈（目前想不出更好的方法）
        Private Sub 监听状态(sender As Object, e As EventArgs) Handles 监听Timer.Tick
            If _传递.SelectedIndex = CInt(Tag) Then
                BackColor = Color.FromArgb(245, 245, 245)
            Else
                BackColor = Color.White
            End If
        End Sub
    End Class

End Class

Public Class 设计时交互支持
    Inherits ControlDesigner

    Private Const WM_LBUTTONUP As Integer = &H202
    Protected Overrides Sub WndProc(ByRef m As Message)
        Dim 绑定项 As KlxPiaoTabControl = DirectCast(Control, KlxPiaoTabControl)

        Select Case m.Msg
            Case WM_LBUTTONUP
                If 绑定项.绑定 IsNot Nothing AndAlso 绑定项.绑定.TabCount > 0 Then
                    Dim xPosition As Integer = LowWord(m.LParam.ToInt32())
                    Dim yPosition As Integer = HighWord(m.LParam.ToInt32())

                    If 绑定项.获取选项卡索引 > -1 AndAlso xPosition >= 0 AndAlso yPosition >= 0 AndAlso xPosition < 绑定项.选项卡大小.Width AndAlso yPosition < 绑定项.Height Then
                        绑定项.设置选项卡索引(yPosition \ 绑定项.选项卡大小.Height)
                    End If
                End If
        End Select

        MyBase.WndProc(m)
    End Sub
    Private Function LowWord(value As Integer) As Integer
        Return value And &HFFFF
    End Function

    Private Function HighWord(value As Integer) As Integer
        Return (value >> 16) And &HFFFF
    End Function
End Class