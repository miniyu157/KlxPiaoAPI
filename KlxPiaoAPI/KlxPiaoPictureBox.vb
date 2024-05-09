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

    Public Sub New()
        _启用边框 = False
        _边框外部颜色 = Color.White
        _圆角百分比 = 0
        _边框大小 = 10
        _边框颜色 = Color.LightGray

        SizeMode = PictureBoxSizeMode.Zoom
        Size = New Size(155, 155)
    End Sub

    <DefaultValue(GetType(Size), "155,155")>
    Public Overloads Property Size As Size
        Get
            Return MyBase.Size
        End Get
        Set(value As Size)
            MyBase.Size = value
            Invalidate()
        End Set
    End Property
    <DefaultValue(GetType(PictureBoxSizeMode), "Zoom")>
    Public Overloads Property SizeMode As PictureBoxSizeMode
        Get
            Return MyBase.SizeMode
        End Get
        Set(value As PictureBoxSizeMode)
            MyBase.SizeMode = value
            Invalidate()
        End Set
    End Property

    <Category("KlxPiaoPictureBox外观")>
    <Description("是否启用边框")>
    <DefaultValue(False)>
    Public Property 启用边框 As Boolean
        Get
            Return _启用边框
        End Get
        Set(value As Boolean)
            _启用边框 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPictureBox外观")>
    <Description("边框外部的颜色")>
    <DefaultValue(GetType(Color), "White")>
    Public Property 边框外部颜色 As Color
        Get
            Return _边框外部颜色
        End Get
        Set(value As Color)
            _边框外部颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPictureBox外观")>
    <Description("范围：0.00-1.00，等于0时取消圆角")>
    <DefaultValue(0)>
    Public Property 圆角百分比 As Single
        Get
            Return _圆角百分比
        End Get
        Set(value As Single)
            _圆角百分比 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPictureBox外观")>
    <Description("边框的大小，为0时隐藏边框")>
    <DefaultValue(10)>
    Public Property 边框大小 As Integer
        Get
            Return _边框大小
        End Get
        Set(value As Integer)
            _边框大小 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPictureBox外观")>
    <Description("边框的颜色")>
    <DefaultValue(GetType(Color), "LightGray")>
    Public Property 边框颜色 As Color
        Get
            Return _边框颜色
        End Get
        Set(value As Color)
            _边框颜色 = value
            Invalidate()
        End Set
    End Property
    Protected Overrides Sub OnPaint(pe As PaintEventArgs)
        MyBase.OnPaint(pe)

        Dim g As Graphics = pe.Graphics

        g.SmoothingMode = SmoothingMode.AntiAlias
        g.PixelOffsetMode = PixelOffsetMode.HighQuality

        Dim 区域 As New Rectangle(0, 0, Width, Height)
        Dim 圆角区域 As New Size
        Select Case 区域.Width - 区域.Height
            Case 0
                圆角区域 = New Size(区域.Width * 圆角百分比, 区域.Height * 圆角百分比)
            Case < 0
                圆角区域 = New Size(区域.Width * 圆角百分比, 区域.Width * 圆角百分比)
            Case > 0
                圆角区域 = New Size(区域.Height * 圆角百分比, 区域.Height * 圆角百分比)
        End Select
        Dim 圆角区域1 As New Point(区域.X, 区域.Y)
        Dim 圆角区域2 As New Point(区域.Width - 圆角区域.Width, 区域.Y)
        Dim 圆角区域3 As New Point(区域.Width - 圆角区域.Width, 区域.Height - 圆角区域.Height)
        Dim 圆角区域4 As New Point(区域.X, 区域.Height - 圆角区域.Height)

        If 边框大小 <> 0 AndAlso 启用边框 Then
            Dim 边框 As New GraphicsPath
            If 圆角百分比 <> 0 Then
                边框.AddArc(New Rectangle(圆角区域1, 圆角区域), 180, 90)
                边框.AddArc(New Rectangle(圆角区域2, 圆角区域), 270, 90)
                边框.AddArc(New Rectangle(圆角区域3, 圆角区域), 0, 90)
                边框.AddArc(New Rectangle(圆角区域4, 圆角区域), 90, 90)
            Else
                边框.AddRectangle(区域)
            End If
            边框.CloseFigure()

            g.DrawPath(New Pen(边框颜色, 边框大小), 边框)
        End If

        If 圆角百分比 <> 0 Then
            Dim 圆角 As New GraphicsPath
            '左上角
            圆角.AddArc(New Rectangle(圆角区域1, 圆角区域), 180, 90)
            圆角.AddLine(圆角区域1, New Point(圆角区域1.X, 圆角区域1.Y + 圆角区域.Height / 2))
            圆角.CloseFigure()
            '右上角
            圆角.AddArc(New Rectangle(圆角区域2, 圆角区域), 270, 90)
            圆角.AddLine(New Point(圆角区域2.X + 圆角区域.Width, 圆角区域2.Y), 圆角区域2)
            圆角.CloseFigure()
            '右下角
            圆角.AddArc(New Rectangle(圆角区域3, 圆角区域), 0, 90)
            圆角.AddLine(New Point(圆角区域3.X + 圆角区域.Width, 圆角区域3.Y + 圆角区域.Height), New Point(圆角区域3.X + 圆角区域.Width, 圆角区域3.Y))
            圆角.CloseFigure()
            '左下角
            圆角.AddArc(New Rectangle(圆角区域4, 圆角区域), 90, 90)
            圆角.AddLine(New Point(圆角区域4.X, 圆角区域4.Y + 圆角区域.Height), New Point(圆角区域4.X + 圆角区域.Width, 圆角区域4.Y + 圆角区域.Height))
            圆角.CloseFigure()

            g.FillPath(New SolidBrush(边框外部颜色), 圆角)
        End If
    End Sub

    Public Function 返回图像() As Bitmap
        Dim bmp As New Bitmap(Width, Height)

        Using g As Graphics = Graphics.FromImage(bmp)
            Dim e As New PaintEventArgs(g, New Rectangle(0, 0, Width, Height))
            OnPaint(e)
        End Using

        Return bmp
    End Function
End Class