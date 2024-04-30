Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

<DefaultEvent("值Changed")>
Public Class PointBar
    Inherits Control

    Private _边框颜色 As Color
    Private _准星颜色 As Color
    Private _坐标轴颜色 As Color
    Private _边框大小 As Integer
    Private _准星大小 As Integer
    Private _显示坐标轴 As Boolean
    Private _显示坐标 As Boolean
    Private _坐标显示位置 As ContentAlignment

    Private _最小值 As Point
    Private _最大值 As Point
    Private _值 As Point
    Public Sub New()
        MyBase.New()

        _边框颜色 = Color.Gray
        _边框大小 = 1
        _坐标轴颜色 = Color.LightPink
        _显示坐标轴 = True
        _显示坐标 = True
        _坐标显示位置 = ContentAlignment.TopLeft
        _准星颜色 = Color.Red
        _准星大小 = 5

        _最小值 = New Point(-100, -100)
        _最大值 = New Point(100, 100)
        _值 = New Point(0, 0)

        Width = 100
        Height = 100
        BackColor = Color.White

        DoubleBuffered = True
    End Sub

    <Category("PointBar外观"), Description("边框的颜色")>
    Public Property 边框颜色 As Color
        Get
            Return _边框颜色
        End Get
        Set(value As Color)
            _边框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("PointBar外观"), Description("准星的颜色")>
    Public Property 准星颜色 As Color
        Get
            Return _准星颜色
        End Get
        Set(value As Color)
            _准星颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("PointBar外观"), Description("坐标轴的颜色")>
    Public Property 坐标轴颜色 As Color
        Get
            Return _坐标轴颜色
        End Get
        Set(value As Color)
            _坐标轴颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("PointBar外观"), Description("边框的大小")>
    Public Property 边框大小 As Integer
        Get
            Return _边框大小
        End Get
        Set(value As Integer)
            _边框大小 = value
            Invalidate()
        End Set
    End Property
    <Category("PointBar外观"), Description("准星的大小，为0时隐藏准星")>
    Public Property 准星大小 As Integer
        Get
            Return _准星大小
        End Get
        Set(value As Integer)
            _准星大小 = value
            Invalidate()
        End Set
    End Property
    <Category("PointBar外观"), Description("显示以0,0为中心的坐标轴")>
    Public Property 显示坐标轴 As Boolean
        Get
            Return _显示坐标轴
        End Get
        Set(value As Boolean)
            _显示坐标轴 = value
            Invalidate()
        End Set
    End Property
    <Category("PointBar外观"), Description("文字颜色由ForeColor属性决定，字体由Font属性决定")>
    Public Property 显示坐标 As Boolean
        Get
            Return _显示坐标
        End Get
        Set(value As Boolean)
            _显示坐标 = value
            Invalidate()
        End Set
    End Property
    <Category("PointBar外观"), Description("坐标显示的位置")>
    Public Property 坐标显示位置 As ContentAlignment
        Get
            Return _坐标显示位置
        End Get
        Set(value As ContentAlignment)
            _坐标显示位置 = value
            Invalidate()
        End Set
    End Property

    <Category("PointBar数值"), Description("坐标的最小值")>
    Public Property 最小值 As Point
        Get
            Return _最小值
        End Get
        Set(value As Point)
            _最小值 = value
            Invalidate()
        End Set
    End Property
    <Category("PointBar数值"), Description("坐标的最大值")>
    Public Property 最大值 As Point
        Get
            Return _最大值
        End Get
        Set(value As Point)
            _最大值 = value
            Invalidate()
        End Set
    End Property

    Public Event 值Changed As PropertyChangedEventHandler
    Protected Overridable Sub OnValueChanged(propertyName As String)
        RaiseEvent 值Changed(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
    <Category("PointBar数值"), Description("坐标的值")>
    Public Property 值 As Point
        Get
            Return _值
        End Get
        Set(value As Point)
            _值 = value
            Invalidate()

            OnValueChanged("值")
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics

        Dim x轴总长度 As Integer = 最大值.X - 最小值.X
        Dim y轴总长度 As Integer = 最大值.Y - 最小值.Y

        '绘制坐标轴
        If 显示坐标轴 Then
            Using 坐标系Pen As New Pen(坐标轴颜色, 1)
                Dim y轴 As Single = (0 - 最小值.X) / x轴总长度
                Dim x轴 As Single = (0 - 最小值.Y) / y轴总长度

                g.DrawLine(坐标系Pen, Width * y轴, 0, Width * y轴, Height)
                g.DrawLine(坐标系Pen, 0, Height * x轴, Width, Height * x轴)
            End Using
        End If

        '绘制边框
        If 边框大小 <> 0 Then
            Using 边框Pen As New Pen(边框颜色, 边框大小)
                g.DrawRectangle(边框Pen, New Rectangle(0, 0, Width - 1, Height - 1))
            End Using
        End If

        '绘制准星
        Using PointPen As New Pen(准星颜色, 1)
            Dim 画出十字 As Action(Of Point, Integer) = Sub(坐标, 长度)
                                                        Dim x比例 As Double = (坐标.X - 最小值.X) / x轴总长度
                                                        Dim y比例 As Double = (坐标.Y - 最小值.Y) / y轴总长度

                                                        Dim newx As Integer = Width * x比例
                                                        Dim newy As Integer = Height * y比例

                                                        g.DrawLine(PointPen, newx - 长度, newy, newx + 长度, newy)
                                                        g.DrawLine(PointPen, newx, newy - 长度, newx, newy + 长度)
                                                    End Sub
            画出十字(值, 准星大小)
        End Using

        '绘制值
        If 显示坐标 Then
            Using brush As New SolidBrush(ForeColor)
                Dim 绘制位置 As Point
                Dim 文字Width As Single = g.MeasureString($"X:{值.X},Y:{值.Y}", Font).Width
                Dim 文字height As Single = g.MeasureString($"X:{值.X},Y:{值.Y}", Font).Height

                Select Case 坐标显示位置
                    Case ContentAlignment.TopLeft
                        绘制位置 = New Point(0, 0)
                    Case ContentAlignment.TopCenter
                        绘制位置 = New Point((Width - 文字Width) \ 2, 0)
                    Case ContentAlignment.TopRight
                        绘制位置 = New Point(Width - 文字Width, 0)

                    Case ContentAlignment.MiddleLeft
                        绘制位置 = New Point(0, (Height - 文字height) \ 2)
                    Case ContentAlignment.MiddleCenter
                        绘制位置 = New Point((Width - 文字Width) \ 2, (Height - 文字height) \ 2)
                    Case ContentAlignment.MiddleRight
                        绘制位置 = New Point(Width - 文字Width, (Height - 文字height) \ 2)

                    Case ContentAlignment.BottomLeft
                        绘制位置 = New Point(0, Height - 文字height)
                    Case ContentAlignment.BottomCenter
                        绘制位置 = New Point((Width - 文字Width) \ 2, Height - 文字height)
                    Case ContentAlignment.BottomRight
                        绘制位置 = New Point(Width - 文字Width, Height - 文字height)
                End Select

                g.DrawString($"X:{值.X},Y:{值.Y}", Font, brush, 绘制位置)
            End Using
        End If
    End Sub

#Region "鼠标调整大小"
    Private 正在调整 As Boolean = False
    Private Sub PointBar_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If e.Button = MouseButtons.Left Then
            Dim x轴总长度 As Integer = 最大值.X - 最小值.X
            Dim y轴总长度 As Integer = 最大值.Y - 最小值.Y

            值 = New Point(最小值.X + x轴总长度 * (e.Location.X / Width), 最小值.Y + y轴总长度 * (e.Location.Y / Height))
            正在调整 = True
        End If
    End Sub
    Private Sub PointBar_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If 正在调整 Then
            Dim x轴总长度 As Integer = 最大值.X - 最小值.X
            Dim y轴总长度 As Integer = 最大值.Y - 最小值.Y

            值 = New Point(最小值.X + x轴总长度 * (e.Location.X / Width), 最小值.Y + y轴总长度 * (e.Location.Y / Height))

            If 值.X < 最小值.X Then 值 = New Point(最小值.X, 值.Y)

            If 值.X > 最大值.X Then 值 = New Point(最大值.X, 值.Y)

            If 值.Y < 最小值.Y Then 值 = New Point(值.X, 最小值.Y)

            If 值.Y > 最大值.Y Then 值 = New Point(值.X, 最大值.Y)

            Refresh()
        End If
    End Sub
    Private Sub PointBar_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        正在调整 = False
    End Sub
#End Region

End Class