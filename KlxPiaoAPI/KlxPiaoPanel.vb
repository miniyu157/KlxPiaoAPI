Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DefaultEvent("Click")>
Public Class KlxPiaoPanel
    Inherits Panel

    Public Enum 方向
        右下
        左下
        左下右
    End Enum

    Private _边框颜色 As Color
    Private _边框外部颜色 As Color
    Private _边框大小 As Integer
    Private _圆角百分比 As Single
    Private _启用投影 As Boolean
    Private _投影长度 As Integer
    Private _投影颜色 As Color
    Private _投影方向 As 方向

    Public Sub New()
        MyBase.New()

        _边框颜色 = Color.FromArgb(199, 199, 199)
        _边框外部颜色 = Color.White
        _边框大小 = 1
        _启用投影 = True
        _投影长度 = 5
        _投影颜色 = Color.FromArgb(142, 142, 142)
        _投影方向 = 方向.右下
        _圆角百分比 = 0

        BackColor = Color.White
        BorderStyle = BorderStyle.None
        Size = New Size(100, 100)

        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
    End Sub

    <Category("KlxPiaoPanel外观")>
    <Description("边框的颜色")>
    <DefaultValue(GetType(Color), "199,199,199")>
    Public Property 边框颜色 As Color
        Get
            Return _边框颜色
        End Get
        Set(value As Color)
            _边框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPanel外观")>
    <Description("边框外部的颜色，启用阴影时失效")>
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
    <Category("KlxPiaoPanel外观")>
    <Description("边框的大小，启用阴影时失效")>
    <DefaultValue(1)>
    Public Property 边框大小 As Integer
        Get
            Return _边框大小
        End Get
        Set(value As Integer)
            _边框大小 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPanel外观")>
    <Description("范围：0.00-1.00，等于0时取消圆角，启用阴影时失效")>
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
    <Category("KlxPiaoPanel外观")>
    <Description("是否启用投影")>
    <DefaultValue(True)>
    Public Property 启用投影 As Boolean
        Get
            Return _启用投影
        End Get
        Set(value As Boolean)
            _启用投影 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPanel外观")>
    <Description("投影的长度")>
    <DefaultValue(5)>
    Public Property 投影长度 As Integer
        Get
            Return _投影长度
        End Get
        Set(value As Integer)
            _投影长度 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPanel外观")>
    <Description("投影的颜色，减淡到白色")>
    <DefaultValue(GetType(Color), "142,142,142")>
    Public Property 投影颜色 As Color
        Get
            Return _投影颜色
        End Get
        Set(value As Color)
            _投影颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPanel外观")>
    <Description("投影的方向")>
    <DefaultValue(GetType(方向), "右下")>
    Public Property 投影方向 As 方向
        Get
            Return _投影方向
        End Get
        Set(value As 方向)
            _投影方向 = value
            Invalidate()
        End Set
    End Property

    <DefaultValue(GetType(Size), "100,100")>
    Public Overloads Property Size As Size
        Get
            Return MyBase.Size
        End Get
        Set(value As Size)
            MyBase.Size = value
            Invalidate()
        End Set
    End Property
    <Browsable(False)>
    Public Overloads Property BorderStyle As BorderStyle
        Get
            Return MyBase.BorderStyle
        End Get
        Set(value As BorderStyle)
            MyBase.BorderStyle = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        '创建双缓冲绘图对象
        Using bitmap As New Bitmap(Width, Height)

            Dim g As Graphics = Graphics.FromImage(bitmap)
            g.Clear(Color.White)

            Dim 递减值R As Integer = (255 - 投影颜色.R) \ 投影长度
            Dim 递减值G As Integer = (255 - 投影颜色.G) \ 投影长度
            Dim 递减值B As Integer = (255 - 投影颜色.B) \ 投影长度

            Dim 递减值 As Integer = 255 \ 投影长度

            If 启用投影 Then
                Dim 边框Rect As Rectangle
                Dim 背景Rect As Rectangle

                Select Case 投影方向
                    Case 方向.右下
                        边框Rect = New Rectangle(0, 0, Width - 投影长度 - 1, Height - 投影长度 - 1)
                        背景Rect = New Rectangle(1, 1, Width - 投影长度 - 2, Height - 投影长度 - 2)

                        For i = 0 To 投影长度
                            Using brush As New SolidBrush(Color.FromArgb(递减值, 255 - i * 递减值R, 255 - i * 递减值G, 255 - i * 递减值B))
                                g.FillRectangle(brush, New Rectangle(投影长度 - i, 投影长度 - i, Width - 投影长度, Height - 投影长度))
                            End Using
                        Next
                    Case 方向.左下
                        边框Rect = New Rectangle(投影长度, 0, Width - 投影长度 - 1, Height - 投影长度 - 1)
                        背景Rect = New Rectangle(投影长度 + 1, 1, Width - 投影长度 - 2, Height - 投影长度 - 2)

                        For i = 0 To 投影长度
                            Using brush As New SolidBrush(Color.FromArgb(递减值, 255 - i * 递减值R, 255 - i * 递减值G, 255 - i * 递减值B))
                                g.FillRectangle(brush, New Rectangle(i, 投影长度 - i, Width - 投影长度, Height - 投影长度))
                            End Using
                        Next
                    Case 方向.左下右
                        边框Rect = New Rectangle(投影长度, 0, Width - 投影长度 * 2 - 1, Height - 投影长度 - 1)
                        背景Rect = New Rectangle(投影长度 + 1, 1, Width - 投影长度 * 2 - 2, Height - 投影长度 - 2)

                        For i = 0 To 投影长度
                            Using brush As New SolidBrush(Color.FromArgb(递减值, 255 - i * 递减值R, 255 - i * 递减值G, 255 - i * 递减值B))
                                g.FillRectangle(brush, New Rectangle(投影长度 * 2 - i, 投影长度 - i, Width - 投影长度 * 2, Height - 投影长度))
                                g.FillRectangle(brush, New Rectangle(i, 投影长度 - i, Width - 投影长度 * 2, Height - 投影长度))
                            End Using
                        Next
                End Select

                '边框
                Using BorderPen As New Pen(边框颜色, 1)
                    g.DrawRectangle(BorderPen, 边框Rect)
                End Using

                '背景
                Using brush As New SolidBrush(BackColor)
                    g.FillRectangle(brush, 背景Rect)
                End Using
            Else
                g.SmoothingMode = SmoothingMode.AntiAlias
                g.PixelOffsetMode = PixelOffsetMode.HighQuality

                '背景
                Using brush As New SolidBrush(BackColor)
                    g.FillRectangle(brush, New Rectangle(0, 0, Width, Height))
                End Using

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

                If 边框大小 <> 0 Then
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
            End If

            e.Graphics.DrawImage(bitmap, 0, 0)
        End Using
    End Sub

    Public Function 获取工作区矩形() As Rectangle
        Dim rect As New Rectangle With {
            .Size = Size,
            .Location = New Point(0, 0)
        }

        If 投影方向 = 方向.右下 Then
            rect.Size -= New Size(投影长度, 投影长度)
        Else
            rect.X = 投影长度

            If 投影方向 = 方向.左下右 Then
                rect.Width = Width - 投影长度 * 2
            Else
                rect.Width = Width - 投影长度
            End If

            rect.Height = Height - 投影长度
        End If

        Return rect
    End Function
    Public Function 获取工作区大小() As Size
        Dim w As Integer = Width

        Select Case 投影方向
            Case 方向.左下右
                w -= 投影长度 * 2
            Case Else
                w -= 投影长度
        End Select

        Return New Size(w, Height - 投影长度)
    End Function

End Class