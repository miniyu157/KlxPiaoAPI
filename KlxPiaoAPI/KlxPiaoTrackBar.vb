Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DefaultEvent("值Changed")>
Public Class KlxPiaoTrackBar
    Inherits Control

    Public Enum 鼠标滚轮响应
        ''' <summary>
        ''' 向上增加，向下减少
        ''' </summary>
        正向 = 1
        ''' <summary>
        ''' 向上减少，向下增加
        ''' </summary>
        逆向 = -1
        ''' <summary>
        ''' 不启用鼠标滚轮响应
        ''' </summary>
        不启用 = 0
    End Enum

    Public Enum 键盘响应
        ''' <summary>
        ''' 仅响应Up和Down键
        ''' </summary>
        仅上下键
        ''' <summary>
        ''' 仅响应Left和Right键
        ''' </summary>
        仅左右键
        ''' <summary>
        ''' 响应Up,Down,Left,Right键
        ''' </summary>
        全方向键
        ''' <summary>
        ''' 不启用键盘响应
        ''' </summary>
        不启用
    End Enum

    Public Enum 文字位置
        不显示
        左
        居中
        右
    End Enum

    Private _背景色 As Color
    Private _前景色 As Color
    Private _边框大小 As Integer
    Private _边框颜色 As Color
    Private _值显示方式 As 文字位置
    Private _值显示边距 As Integer

    Private _焦点边框颜色 As Color
    Private _焦点边框大小 As Integer

    Private _值 As Integer
    Private _最大值 As Integer
    Private _最小值 As Integer
    Private _鼠标滚轮响应方式 As 鼠标滚轮响应
    Private _键盘响应方式 As 键盘响应
    Private _增减大小 As Integer
    Public Sub New()
        MyBase.New()

        _背景色 = Color.Gainsboro
        _前景色 = Color.Gray
        _边框大小 = 0
        _边框颜色 = Color.FromArgb(0, 210, 212)
        _值显示方式 = 文字位置.不显示
        _值显示边距 = 0

        _焦点边框颜色 = Color.Red
        _焦点边框大小 = -1

        _值 = 0
        _最大值 = 100
        _最小值 = 0
        _鼠标滚轮响应方式 = 鼠标滚轮响应.正向
        _键盘响应方式 = 键盘响应.全方向键
        _增减大小 = 1

        Size = New Size(286, 10)
        BackColor = Color.White

        '防止闪烁
        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.Selectable, True)
    End Sub

    <Category("KlxPiaoTrackBar外观"), Description("组件的背景色")>
    Public Property 背景色 As Color
        Get
            Return _背景色
        End Get
        Set(value As Color)
            _背景色 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTrackBar外观"), Description("组件的前景色")>
    Public Property 前景色 As Color
        Get
            Return _前景色
        End Get
        Set(value As Color)
            _前景色 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTrackBar外观"), Description("边框的大小，为0时隐藏边框")>
    Public Property 边框大小 As Integer
        Get
            Return _边框大小
        End Get
        Set(value As Integer)
            _边框大小 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTrackBar外观"), Description("边框的颜色")>
    Public Property 边框颜色 As Color
        Get
            Return _边框颜色
        End Get
        Set(value As Color)
            _边框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTrackBar外观"), Description("显示值到拖动条上，字体由Font属性决定，字体颜色由ForeColor属性决定")>
    Public Property 值显示方式 As 文字位置
        Get
            Return _值显示方式
        End Get
        Set(value As 文字位置)
            _值显示方式 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTrackBar外观"), Description("显示值的左右边距")>
    Public Property 值显示边距 As Integer
        Get
            Return _值显示边距
        End Get
        Set(value As Integer)
            _值显示边距 = value
            Invalidate()
        End Set
    End Property

    <Category("KlxPiaoTrackBar焦点"), Description("控件激活时的边框颜色，Transparent：控件激活时不会改变边框颜色")>
    Public Property 焦点边框颜色 As Color
        Get
            Return _焦点边框颜色
        End Get
        Set(value As Color)
            _焦点边框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTrackBar焦点"), Description("控件激活时的边框大小，-1：控件激活时不会改变边框大小")>
    Public Property 焦点边框大小 As Integer
        Get
            Return _焦点边框大小
        End Get
        Set(value As Integer)
            _焦点边框大小 = value
            Invalidate()
        End Set
    End Property

    Public Event 值Changed As PropertyChangedEventHandler
    Protected Overridable Sub OnValueChanged(propertyName As String)
        RaiseEvent 值Changed(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
    <Category("KlxPiaoTrackBar属性"), Description("当前的值")>
    Public Property 值 As Integer
        Get
            Return _值
        End Get
        Set(value As Integer)
            _值 = value
            Invalidate()

            OnValueChanged("值")
        End Set
    End Property
    <Category("KlxPiaoTrackBar属性"), Description("最大的值")>
    Public Property 最大值 As Integer
        Get
            Return _最大值
        End Get
        Set(value As Integer)
            _最大值 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTrackBar属性"), Description("最小的值")>
    Public Property 最小值 As Integer
        Get
            Return _最小值
        End Get
        Set(value As Integer)
            _最小值 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTrackBar属性"), Description("鼠标滚轮响应的方式，正向：向上增加，向下减少；逆向：向上减少，向下增加")>
    Public Property 鼠标滚轮响应方式 As 鼠标滚轮响应
        Get
            Return _鼠标滚轮响应方式
        End Get
        Set(value As 鼠标滚轮响应)
            _鼠标滚轮响应方式 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTrackBar属性"), Description("键盘响应的方式")>
    Public Property 键盘响应方式 As 键盘响应
        Get
            Return _键盘响应方式
        End Get
        Set(value As 键盘响应)
            _键盘响应方式 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTrackBar属性"), Description("鼠标滚轮或键盘按下一次增减的大小")>
    Public Property 增减大小 As Integer
        Get
            Return _增减大小
        End Get
        Set(value As Integer)
            _增减大小 = value
            Invalidate()
        End Set
    End Property

    Private 工作矩形 As Rectangle

    Private Sub 重置工作矩形()
        工作矩形 = New Rectangle(0, 0, Width - 1, Height + 1)
    End Sub
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        e.Graphics.Clear(BackColor)

        Using bit As New Bitmap(Width, Height)
            Using g As Graphics = Graphics.FromImage(bit)

                g.SmoothingMode = SmoothingMode.HighQuality
                g.PixelOffsetMode = PixelOffsetMode.HighQuality

#Region "绘制前景和背景"
                重置工作矩形()

                Dim 百分比位置 As Single
                If Not (正在拖动 OrElse 正在滚动 OrElse 正在按键) Then
                    绘制百分比 = (值 - 最小值) / (最大值 - 最小值)
                End If

                百分比位置 = 工作矩形.Width * CType(绘制百分比, Single)

                Dim 左半边 As New GraphicsPath
                左半边.AddArc(工作矩形.X, 工作矩形.Y, 工作矩形.Height, 工作矩形.Height, 90, 180)
                左半边.AddLine(百分比位置, 工作矩形.Y, 百分比位置, 工作矩形.Height)
                Dim 右半边 As New GraphicsPath
                右半边.AddArc(工作矩形.Width - 工作矩形.Height, 工作矩形.Y, 工作矩形.Height, 工作矩形.Height, -90, 180)
                右半边.AddLine(百分比位置, 工作矩形.Height, 百分比位置, 工作矩形.Y)
                g.FillPath(New SolidBrush(背景色), 右半边)
                g.FillPath(New SolidBrush(前景色), 左半边)
#End Region

                '绘制边框
                If 边框大小 <> 0 Then
                    Using 边框Path As New GraphicsPath
                        Dim 遮罩厚度 As Integer = If(Height Mod 2 = 0, Height, Height + 1)
                        重置工作矩形()

                        工作矩形.X -= 遮罩厚度 \ 2
                        工作矩形.Y -= 遮罩厚度 \ 2
                        工作矩形.Width += 遮罩厚度 \ 2
                        工作矩形.Height += 遮罩厚度 - 1

                        边框Path.AddArc(New Rectangle(工作矩形.X, 工作矩形.Y, 工作矩形.Height, 工作矩形.Height), 90, 180)
                        边框Path.AddArc(New Rectangle(工作矩形.Width - 工作矩形.Height, 工作矩形.Y, 工作矩形.Height, 工作矩形.Height), -90, 180)
                        边框Path.CloseFigure()

                        g.DrawPath(New Pen(边框颜色, 遮罩厚度 + 边框大小), 边框Path)
                    End Using
                End If

                '绘制边框二层（遮住第一层出格部分）
                Using 边框Path As New GraphicsPath
                    Dim 遮罩厚度 As Integer = If(Height Mod 2 = 0, Height, Height + 1)
                    重置工作矩形()

                    工作矩形.X -= 遮罩厚度 \ 2
                    工作矩形.Y -= 遮罩厚度 \ 2
                    工作矩形.Width += 遮罩厚度 \ 2
                    工作矩形.Height += 遮罩厚度 - 1

                    边框Path.AddArc(New Rectangle(工作矩形.X, 工作矩形.Y, 工作矩形.Height, 工作矩形.Height), 90, 180)
                    边框Path.AddArc(New Rectangle(工作矩形.Width - 工作矩形.Height, 工作矩形.Y, 工作矩形.Height, 工作矩形.Height), -90, 180)
                    边框Path.CloseFigure()

                    g.DrawPath(New Pen(BackColor, 遮罩厚度), 边框Path)
                End Using

                '绘制值
                If 值显示方式 <> 文字位置.不显示 Then
                    Dim 文字大小 As SizeF = g.MeasureString(值, MyBase.Font)
                    Dim 绘制位置 As PointF

                    Select Case 值显示方式
                        Case 文字位置.左
                            绘制位置 = New PointF(边框大小 + 值显示边距, (Height - 文字大小.Height) / 2)
                        Case 文字位置.右
                            绘制位置 = New PointF(Width - 边框大小 - 文字大小.Width - 值显示边距, (Height - 文字大小.Height) / 2)
                        Case 文字位置.居中
                            绘制位置 = New PointF((Width - 文字大小.Width) / 2, (Height - 文字大小.Height) / 2)
                    End Select

                    g.DrawString(值, Font, New SolidBrush(ForeColor), 绘制位置)
                End If

                e.Graphics.DrawImage(bit, 0, 0)

            End Using
        End Using

    End Sub

    Private 绘制百分比 As Double = Nothing

#Region "鼠标调整值"
    Private 正在拖动 As Boolean = False

    Protected Overrides Sub OnMouseDown(e As MouseEventArgs)
        MyBase.OnMouseDown(e)

        If e.Button = MouseButtons.Left Then
            正在拖动 = True

            绘制百分比 = e.X / Width
            值 = Math.Round(绘制百分比 * (最大值 - 最小值))
        End If

        Focus()
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)

        If 正在拖动 Then

            绘制百分比 = e.X / Width
            If e.X / Width > 1 Then 绘制百分比 = 1
            If e.X / Width < 0 Then 绘制百分比 = 0
            值 = Math.Round(最小值 + 绘制百分比 * (最大值 - 最小值))
        End If
    End Sub
    Protected Overrides Sub OnMouseUp(e As MouseEventArgs)
        MyBase.OnMouseUp(e)

        正在拖动 = False
    End Sub
#End Region

#Region "滚轮调整值"
    Private 正在滚动 As Boolean = False

    Protected Overrides Sub OnMouseWheel(e As MouseEventArgs)
        MyBase.OnMouseWheel(e)

        正在滚动 = True

        Dim 一次移动大小 As Double = (1 / (最大值 - 最小值)) * 增减大小

        Select Case e.Delta
            Case Is > 0
                绘制百分比 += 一次移动大小 * 鼠标滚轮响应方式
            Case Else
                绘制百分比 -= 一次移动大小 * 鼠标滚轮响应方式
        End Select

        If 绘制百分比 > 1 Then 绘制百分比 = 1

        If 绘制百分比 < 0 Then 绘制百分比 = 0

        值 = Math.Round(最小值 + 绘制百分比 * (最大值 - 最小值))

        Focus()
    End Sub
#End Region

#Region "键盘调整值"
    Private 正在按键 As Boolean = False

    Protected Overrides Sub OnPreviewKeyDown(e As PreviewKeyDownEventArgs)
        MyBase.OnPreviewKeyDown(e)

        正在按键 = True
        e.IsInputKey = True

        Dim 一次移动大小 As Double = (1 / (最大值 - 最小值)) * 增减大小

        Select Case 键盘响应方式
            Case 键盘响应.全方向键
                Select Case e.KeyCode
                    Case Keys.Left, Keys.Up
                        绘制百分比 -= 一次移动大小
                    Case Keys.Right, Keys.Down
                        绘制百分比 += 一次移动大小
                    Case Else
                        e.IsInputKey = False
                End Select
            Case 键盘响应.仅上下键
                Select Case e.KeyCode
                    Case Keys.Up
                        绘制百分比 -= 一次移动大小
                    Case Keys.Down
                        绘制百分比 += 一次移动大小
                    Case Else
                        e.IsInputKey = False
                End Select
            Case 键盘响应.仅左右键
                Select Case e.KeyCode
                    Case Keys.Left
                        绘制百分比 -= 一次移动大小
                    Case Keys.Right
                        绘制百分比 += 一次移动大小
                    Case Else
                        e.IsInputKey = False
                End Select
        End Select

        If 绘制百分比 > 1 Then 绘制百分比 = 1

        If 绘制百分比 < 0 Then 绘制百分比 = 0

        值 = Math.Round(最小值 + 绘制百分比 * (最大值 - 最小值))
    End Sub
#End Region

    'Protected Overrides Sub OnMouseEnter(e As EventArgs)
    '    MyBase.OnMouseEnter(e)

    '    Size += New Size(4, 4)
    '    Location -= New Size(2, 2)

    '    Invalidate()
    'End Sub
    'Protected Overrides Sub OnMouseLeave(e As EventArgs)
    '    MyBase.OnMouseLeave(e)

    '    Size -= New Size(4, 4)
    '    Location += New Size(2, 2)

    '    Invalidate()
    'End Sub

    Private 原来的边框颜色
    Private 原来的边框大小
    Protected Overrides Sub OnGotFocus(e As EventArgs)
        MyBase.OnGotFocus(e)

        原来的边框颜色 = 边框颜色
        原来的边框大小 = 边框大小

        If 焦点边框颜色 <> Color.Transparent Then
            边框颜色 = 焦点边框颜色
        End If

        If 焦点边框大小 <> -1 Then
            边框大小 = 焦点边框大小
        End If
    End Sub

    Protected Overrides Sub OnLostFocus(e As EventArgs)
        MyBase.OnLostFocus(e)

        边框颜色 = 原来的边框颜色
        边框大小 = 原来的边框大小
    End Sub
End Class