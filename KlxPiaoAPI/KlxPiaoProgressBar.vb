Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Public Class KlxPiaoProgressBar
    Inherits ProgressBar

    Private _边框颜色 As Color
    Private _边框大小 As Integer

    Public Sub New()
        MyBase.New()
        SetStyle(ControlStyles.UserPaint, True)
        DoubleBuffered = True ' 启用双缓冲

        ' 初始化默认值
        BackColor = Color.White
        ForeColor = Color.PowderBlue

        _边框颜色 = Color.LightGray
        _边框大小 = 1
    End Sub

    <Category("外观"), Description("进度条边框的颜色")>
    Public Property 边框颜色 As Color
        Get
            Return _边框颜色
        End Get
        Set(value As Color)
            _边框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("外观"), Description("进度条边框的大小")>
    Public Property 边框大小 As Integer
        Get
            Return _边框大小
        End Get
        Set(value As Integer)
            _边框大小 = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        ' 创建双缓冲绘图对象
        Using bitmap As New Bitmap(Width, Height)
            Using g As Graphics = Graphics.FromImage(bitmap)
                Dim rect As New Rectangle(0, 0, Width, Height)

                ' 绘制背景
                Using backBrush As New SolidBrush(BackColor)
                    g.FillRectangle(backBrush, rect)
                End Using

                ' 绘制前景
                Dim progressWidth As Integer = Width / (Maximum - Minimum) * Value
                Dim progressRect As New Rectangle(0, 0, progressWidth, Height)

                Using foreBrush As New SolidBrush(ForeColor)
                    g.FillRectangle(foreBrush, progressRect)
                End Using

                ' 绘制边框
                If 边框大小 <> 0 Then
                    Using borderPen As New Pen(边框颜色, 边框大小)
                        g.DrawRectangle(borderPen, New Rectangle(0, 0, Width - 1, Height - 1))
                    End Using
                End If
            End Using

            ' 将绘制的内容一次性绘制到控件上
            e.Graphics.DrawImage(bitmap, 0, 0)
        End Using
    End Sub
End Class
