Imports System.ComponentModel
Imports System.Drawing
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
    Private _边框大小 As Integer
    Private _启用投影 As Boolean
    Private _投影长度 As Integer
    Private _投影颜色 As Color
    Private _投影方向 As 方向

    Public Sub New()
        MyBase.New()

        _边框颜色 = Color.FromArgb(199, 199, 199)
        _边框大小 = 1
        _启用投影 = True
        _投影长度 = 5
        _投影颜色 = Color.FromArgb(142, 142, 142)
        _投影方向 = 方向.右下

        BackColor = Color.White
        BorderStyle = BorderStyle.None

        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
    End Sub

    <Category("KlxPiaoPanel外观"), Description("边框的颜色")>
    Public Property 边框颜色 As Color
        Get
            Return _边框颜色
        End Get
        Set(value As Color)
            _边框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPanel外观"), Description("边框的大小，启用阴影时失效")>
    Public Property 边框大小 As Integer
        Get
            Return _边框大小
        End Get
        Set(value As Integer)
            _边框大小 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPanel外观"), Description("是否启用投影")>
    Public Property 启用投影 As Boolean
        Get
            Return _启用投影
        End Get
        Set(value As Boolean)
            _启用投影 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPanel外观"), Description("投影的长度")>
    Public Property 投影长度 As Integer
        Get
            Return _投影长度
        End Get
        Set(value As Integer)
            _投影长度 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPanel外观"), Description("投影的颜色，减淡到白色")>
    Public Property 投影颜色 As Color
        Get
            Return _投影颜色
        End Get
        Set(value As Color)
            _投影颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoPanel外观"), Description("投影的方向")>
    Public Property 投影方向 As 方向
        Get
            Return _投影方向
        End Get
        Set(value As 方向)
            _投影方向 = value
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
                '背景
                Using brush As New SolidBrush(BackColor)
                    g.FillRectangle(brush, New Rectangle(0, 0, Width, Height))
                End Using

                '边框
                If 边框大小 <> 0 Then
                    Using pen1 As New Pen(边框颜色, 边框大小)
                        g.DrawRectangle(pen1, New Rectangle(0, 0, Width - 1, Height - 1))
                    End Using
                End If
            End If

            e.Graphics.DrawImage(bitmap, 0, 0)
        End Using
    End Sub
End Class