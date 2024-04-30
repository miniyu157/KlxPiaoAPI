Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class KlxPiaoPictureBox
    Inherits PictureBox

    Private _启用边框 As Boolean
    Private _边框外部颜色 As Color
    Private _圆角百分比 As Single
    Private _边框大小 As Integer
    Private _边框颜色 As Color
    Private _图片缩放 As Boolean

    Public Sub New()
        _启用边框 = False
        _边框外部颜色 = Color.White
        _圆角百分比 = 0
        _边框大小 = 0
        _边框颜色 = Color.LightGray
        _图片缩放 = False

        SizeMode = PictureBoxSizeMode.Zoom
    End Sub

    <Category("边框"), Description("是否启用边框")>
    Public Property 启用边框 As Boolean
        Get
            Return _启用边框
        End Get
        Set(value As Boolean)
            _启用边框 = value
            Invalidate()
        End Set
    End Property
    <Category("边框"), Description("边框的颜色")>
    Public Property 边框外部颜色 As Color
        Get
            Return _边框外部颜色
        End Get
        Set(value As Color)
            _边框外部颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("边框"), Description("范围：0.00-1.00，为1时为圆形，为0时取消圆角")>
    Public Property 圆角百分比 As Single
        Get
            Return _圆角百分比
        End Get
        Set(value As Single)
            _圆角百分比 = value
            Invalidate()
        End Set
    End Property
    <Category("边框"), Description("边框的大小，为0时隐藏边框")>
    Public Property 边框大小 As Integer
        Get
            Return _边框大小
        End Get
        Set(value As Integer)
            _边框大小 = value
            Invalidate()
        End Set
    End Property
    <Category("边框"), Description("边框的颜色")>
    Public Property 边框颜色 As Color
        Get
            Return _边框颜色
        End Get
        Set(value As Color)
            _边框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("边框"), Description("不启用缩放时，边框将覆盖图片")>
    Public Property 图片缩放 As Boolean
        Get
            Return _图片缩放
        End Get
        Set(value As Boolean)
            _图片缩放 = value
            Invalidate()
        End Set
    End Property
    Protected Overrides Sub OnPaint(pe As PaintEventArgs)
        MyBase.OnPaint(pe)

        Dim g As Graphics = pe.Graphics

        '边框
        If 启用边框 Then

            Dim 圆角大小 As Double = 圆角百分比
            Dim 外部区域 As New Rectangle(0, 0, Width, Height)
            Dim 内部区域 As New Rectangle(外部区域.X + 边框大小, 外部区域.Y + 边框大小, 外部区域.Width - 边框大小 * 2, 外部区域.Height - 边框大小 * 2)

            Dim pathOuter As New GraphicsPath()
            Dim pathInner As New GraphicsPath()
            Dim 外部圆角 As Single = 圆角大小 * (外部区域.Width / 2)
            Dim 内部圆角 As Single = 外部圆角 * ((外部区域.Width - 边框大小 * 2) / 外部区域.Width)

            If 图片缩放 Then
                g.Clear(Color.White)
                g.DrawImage(New Bitmap(Image, New Size(Width - 边框大小 * 2, Height - 边框大小 * 2)), 边框大小, 边框大小)
            End If

            If 圆角大小 = 0 Then
                pathOuter.AddRectangle(外部区域)
                pathInner.AddRectangle(内部区域)
            ElseIf 圆角大小 = 1 Then
                pathOuter.AddEllipse(外部区域)
                pathInner.AddEllipse(内部区域)
            Else
                pathOuter.AddArc(外部区域.Left, 外部区域.Top, 外部圆角 * 2, 外部圆角 * 2, 180, 90)
                pathOuter.AddArc(外部区域.Right - 外部圆角 * 2, 外部区域.Top, 外部圆角 * 2, 外部圆角 * 2, 270, 90)
                pathOuter.AddArc(外部区域.Right - 外部圆角 * 2, 外部区域.Bottom - 外部圆角 * 2, 外部圆角 * 2, 外部圆角 * 2, 0, 90)
                pathOuter.AddArc(外部区域.Left, 外部区域.Bottom - 外部圆角 * 2, 外部圆角 * 2, 外部圆角 * 2, 90, 90)

                pathInner.AddArc(内部区域.Left, 内部区域.Top, 内部圆角 * 2, 内部圆角 * 2, 180, 90)
                pathInner.AddArc(内部区域.Right - 内部圆角 * 2, 内部区域.Top, 内部圆角 * 2, 内部圆角 * 2, 270, 90)
                pathInner.AddArc(内部区域.Right - 内部圆角 * 2, 内部区域.Bottom - 内部圆角 * 2, 内部圆角 * 2, 内部圆角 * 2, 0, 90)
                pathInner.AddArc(内部区域.Left, 内部区域.Bottom - 内部圆角 * 2, 内部圆角 * 2, 内部圆角 * 2, 90, 90)
            End If

            pathOuter.CloseFigure()
            pathInner.CloseFigure()

            ' 组合外部和内部圆角矩形路径
            Dim combinePath As New GraphicsPath()
            combinePath.AddPath(pathOuter, False) ' 添加外部路径
            combinePath.AddPath(pathInner, True) ' 添加内部路径

            ' 填充内部圆角矩形区域外部
            g.FillPath(New SolidBrush(边框颜色), combinePath)

            '创建一个Region对象， 表示圆角矩形区域
            Dim circleRegion As New Region(pathOuter)

            ' 将圆形区域排除在外
            g.ExcludeClip(circleRegion)

            ' 填充被排除的区域
            g.FillRectangle(New SolidBrush(边框外部颜色), 外部区域)

        End If
    End Sub
End Class
