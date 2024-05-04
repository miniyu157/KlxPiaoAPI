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

    Private _背景色 As Color
    Private _前景色 As Color

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

                重置工作矩形()

#Region "绘制前景和背景"
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

                '绘制遮罩
                Using 遮罩Path As New GraphicsPath
                    Dim 遮罩厚度 As Integer = If(Height Mod 2 = 0, Height, Height + 1)

                    重置工作矩形()

                    工作矩形.X -= 遮罩厚度 \ 2
                    工作矩形.Y -= 遮罩厚度 \ 2
                    工作矩形.Width += 遮罩厚度 \ 2
                    工作矩形.Height += 遮罩厚度 - 1

                    遮罩Path.AddArc(New Rectangle(工作矩形.X, 工作矩形.Y, 工作矩形.Height, 工作矩形.Height), 90, 180)
                    遮罩Path.AddArc(New Rectangle(工作矩形.Width - 工作矩形.Height, 工作矩形.Y, 工作矩形.Height, 工作矩形.Height), -90, 180)
                    遮罩Path.CloseFigure()

                    g.DrawPath(New Pen(BackColor, 遮罩厚度), 遮罩Path)
                End Using

                e.Graphics.DrawImage(bit, 0, 0)

            End Using
        End Using

    End Sub

    Private 绘制百分比 As Double = Nothing

#Region "鼠标设置Value"
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

#Region "滚轮设置Value"
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

#Region "键盘设置Value"
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

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)

        Size += New Size(4, 4)
        Location -= New Size(2, 2)

        Invalidate()
    End Sub
    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)

        Size -= New Size(4, 4)
        Location += New Size(2, 2)

        Invalidate()
    End Sub
End Class
