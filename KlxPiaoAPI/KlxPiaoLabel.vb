Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Imports System.Windows.Forms

Public Class KlxPiaoLabel
    Inherits Label

    Private _启用投影 As Boolean
    Private _投影颜色 As Color
    Private _投影连线 As Boolean
    Private _偏移量 As Point
    Private _颜色减淡 As Boolean

    Private _启用边框 As Boolean
    Private _边框外部颜色 As Color
    Private _圆角百分比 As Single
    Private _边框大小 As Integer
    Private _边框颜色 As Color

    Private _文本呈现质量 As TextRenderingHint
    Private _抗锯齿 As SmoothingMode
    Private _算法 As InterpolationMode
    Private _偏移方式 As PixelOffsetMode

    Public Sub New()
        MyBase.New

        _启用投影 = False
        _投影颜色 = Color.DarkGray
        _投影连线 = True
        _偏移量 = New Point(2, 2)
        _颜色减淡 = False

        _启用边框 = False
        _边框外部颜色 = Color.White
        _圆角百分比 = 0
        _边框大小 = 5
        _边框颜色 = Color.LightGray

        _文本呈现质量 = TextRenderingHint.SystemDefault
        _抗锯齿 = SmoothingMode.Default
        _算法 = InterpolationMode.Default
        _偏移方式 = PixelOffsetMode.Default

        Font = New Font("微软雅黑 Light", 9)
        ForeColor = Color.Black
        BackColor = Color.White
    End Sub

    <Category("文字投影"), Description("绘制投影")>
    Public Property 启用投影 As Boolean
        Get
            Return _启用投影
        End Get
        Set(value As Boolean)
            _启用投影 = value
            Invalidate()
        End Set
    End Property
    <Category("文字投影"), Description("投影的颜色，通常比文字颜色稍淡")>
    Public Property 投影颜色 As Color
        Get
            Return _投影颜色
        End Get
        Set(value As Color)
            _投影颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("文字投影"), Description("决定了阴影的长度和方向")>
    Public Property 偏移量 As Point
        Get
            Return _偏移量
        End Get
        Set(value As Point)
            _偏移量 = value
            Invalidate()
        End Set
    End Property
    <Category("文字投影"), Description("设置为True时，相当于物体的投影；设置为False时，相当于复制了一份")>
    Public Property 投影连线 As Boolean
        Get
            Return _投影连线
        End Get
        Set(value As Boolean)
            _投影连线 = value
            Invalidate()
        End Set
    End Property
    <Category("文字投影"), Description("设置为True时，建议把投影颜色设置为为更深的颜色，例如：Black")>
    Public Property 颜色减淡 As Boolean
        Get
            Return _颜色减淡
        End Get
        Set(value As Boolean)
            _颜色减淡 = value
            Invalidate()
        End Set
    End Property
    <Category("文字投影"), Description("是否固定大小")>
    <Browsable(True)>
    <DefaultValue(True)>
    Public Overrides Property AutoSize As Boolean
        Get
            Return MyBase.AutoSize
        End Get
        Set(value As Boolean)
            MyBase.AutoSize = value
        End Set
    End Property

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

    <Category("质量"), Description("指定文本呈现的质量")>
    Public Property 文本呈现质量 As TextRenderingHint
        Get
            Return _文本呈现质量
        End Get
        Set(value As TextRenderingHint)
            _文本呈现质量 = value
            Invalidate()
        End Set
    End Property
    <Category("质量"), Description("指定是否将平滑处理（抗锯齿）应用于直线、曲线和已填充区域的边缘")>
    Public Property 抗锯齿 As SmoothingMode
        Get
            Return _抗锯齿
        End Get
        Set(value As SmoothingMode)
            _抗锯齿 = value
            Invalidate()
        End Set
    End Property
    <Category("质量"), Description("InterpolationMode 枚举指定在缩放或旋转图像时使用的算法")>
    Public Property 算法 As InterpolationMode
        Get
            Return _算法
        End Get
        Set(value As InterpolationMode)
            _算法 = value
            Invalidate()
        End Set
    End Property
    <Category("质量"), Description("指定在呈现期间像素偏移的方式")>
    Public Property 偏移方式 As PixelOffsetMode
        Get
            Return _偏移方式
        End Get
        Set(value As PixelOffsetMode)
            _偏移方式 = value
            Invalidate()
        End Set
    End Property
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics

        g.TextRenderingHint = 文本呈现质量
        g.SmoothingMode = 抗锯齿
        g.InterpolationMode = 算法
        g.PixelOffsetMode = 偏移方式

        g.Clear(BackColor)

        Dim 绘制位置 As Point
        Dim 文字Width As Single = g.MeasureString(Text, Font).Width
        Dim 文字height As Single = g.MeasureString(Text, Font).Height

        '适应文字位置
        If Not AutoSize Then
            Select Case TextAlign
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
        End If

        '绘制阴影
        If 启用投影 Then
            Using brush As New SolidBrush(投影颜色)

                If 投影连线 Then

                    Dim 横向递减值 As Integer = If(颜色减淡, If(偏移量.X = 0, 0, 255 \ Math.Abs(偏移量.X)), 255)
                    Dim 纵向递减值 As Integer = If(颜色减淡, If(偏移量.Y = 0, 0, 255 \ Math.Abs(偏移量.Y)), 255)

                    Dim 横向递减颜色R As Integer = If(偏移量.X = 0, 0, (255 - 投影颜色.R) \ Math.Abs(偏移量.X))
                    Dim 横向递减颜色G As Integer = If(偏移量.X = 0, 0, (255 - 投影颜色.G) \ Math.Abs(偏移量.X))
                    Dim 横向递减颜色B As Integer = If(偏移量.X = 0, 0, (255 - 投影颜色.B) \ Math.Abs(偏移量.X))

                    Dim 纵向递减颜色R As Integer = If(偏移量.Y = 0, 0, (255 - 投影颜色.R) \ Math.Abs(偏移量.Y))
                    Dim 纵向递减颜色G As Integer = If(偏移量.Y = 0, 0, (255 - 投影颜色.G) \ Math.Abs(偏移量.Y))
                    Dim 纵向递减颜色B As Integer = If(偏移量.Y = 0, 0, (255 - 投影颜色.B) \ Math.Abs(偏移量.Y))

                    If 偏移量.X = 0 OrElse 偏移量.Y = 0 Then
                        '坐标轴和原点

                        'x轴
                        For x = 0 To 偏移量.X Step If(偏移量.X > 0, 1, -1)
                            '颜色减淡
                            Dim 递减次数 As Integer = Math.Abs(偏移量.X) - Math.Abs(x)
                            Dim 颜色 As Color = If(颜色减淡, Color.FromArgb(255 - 递减次数 * 横向递减颜色R, 255 - 递减次数 * 横向递减颜色G, 255 - 递减次数 * 横向递减颜色B), 投影颜色)

                            g.DrawString(Text, Font, New SolidBrush(Color.FromArgb(横向递减值, 颜色)), New Point(绘制位置.X + x, 绘制位置.Y))
                        Next

                        'y轴
                        For y = 0 To 偏移量.Y Step If(偏移量.Y > 0, 1, -1)
                            '颜色减淡
                            Dim 递减次数 As Integer = Math.Abs(偏移量.Y) - Math.Abs(y)
                            Dim 颜色 As Color = If(颜色减淡, Color.FromArgb(255 - 递减次数 * 纵向递减颜色R, 255 - 递减次数 * 纵向递减颜色G, 255 - 递减次数 * 纵向递减颜色B), 投影颜色)

                            g.DrawString(Text, Font, New SolidBrush(Color.FromArgb(纵向递减值, 颜色)), New Point(绘制位置.X, 绘制位置.Y + y))
                        Next

                    Else
                        '四个象限，不包括坐标轴和原点

                        '防止接近xy轴时会出现问题，同时减少内存占用
                        Dim 接近x轴 As Boolean
                        Dim 接近y轴 As Boolean
                        If Math.Abs(偏移量.X) < Math.Abs(偏移量.Y) \ Math.PI Then
                            接近x轴 = True
                        ElseIf Math.Abs(偏移量.Y) < Math.Abs(偏移量.X) \ Math.PI Then
                            接近y轴 = True
                        End If

                        If Not 接近x轴 Then
                            '横向绘制
                            For x = 0 To 偏移量.X Step If(偏移量.X > 0, 1, -1)
                                Dim 斜率 As Double = 偏移量.Y / 偏移量.X

                                '颜色减淡
                                Dim 递减次数 As Integer = Math.Abs(偏移量.X) - Math.Abs(x)
                                Dim 颜色 As Color = If(颜色减淡, Color.FromArgb(255 - 递减次数 * 横向递减颜色R, 255 - 递减次数 * 横向递减颜色G, 255 - 递减次数 * 横向递减颜色B), 投影颜色)

                                g.DrawString(Text, Font, New SolidBrush(Color.FromArgb(横向递减值, 颜色)), New Point(绘制位置.X + x, 绘制位置.Y + x * 斜率))
                            Next
                        End If

                        If Not 接近y轴 Then
                            '纵向绘制
                            For y = 0 To 偏移量.Y Step If(偏移量.Y > 0, 1, -1)
                                Dim 斜率 As Double = 偏移量.X / 偏移量.Y

                                '颜色减淡
                                Dim 递减次数 As Integer = Math.Abs(偏移量.Y) - Math.Abs(y)
                                Dim 颜色 As Color = If(颜色减淡, Color.FromArgb(255 - 递减次数 * 纵向递减颜色R, 255 - 递减次数 * 纵向递减颜色G, 255 - 递减次数 * 纵向递减颜色B), 投影颜色)

                                g.DrawString(Text, Font, New SolidBrush(Color.FromArgb(纵向递减值, 颜色)), New Point(绘制位置.X + y * 斜率, 绘制位置.Y + y))
                            Next
                        End If

                    End If

                Else
                    g.DrawString(Text, Font, brush, New Point(绘制位置.X + 偏移量.X, 绘制位置.Y + 偏移量.Y))
                End If

                '文字本体
                g.DrawString(Text, Font, New SolidBrush(ForeColor), 绘制位置)
            End Using
        Else
            '文字本体
            g.DrawString(Text, Font, New SolidBrush(ForeColor), 绘制位置)
        End If

        '边框
        If 启用边框 Then

            Dim 圆角大小 As Double = 圆角百分比
            Dim 外部区域 As New Rectangle(0, 0, Width, Height)
            Dim 内部区域 As New Rectangle(外部区域.X + 边框大小, 外部区域.Y + 边框大小, 外部区域.Width - 边框大小 * 2, 外部区域.Height - 边框大小 * 2)

            Dim pathOuter As New GraphicsPath()
            Dim pathInner As New GraphicsPath()
            Dim 外部圆角 As Single = 圆角大小 * (外部区域.Width / 2)
            Dim 内部圆角 As Single = 外部圆角 * ((外部区域.Width - 边框大小 * 2) / 外部区域.Width)

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

            ' 创建一个Region对象， 表示圆角矩形区域
            Dim circleRegion As New Region(pathOuter)

            ' 将圆形区域排除在外
            g.ExcludeClip(circleRegion)

            ' 填充被排除的区域
            g.FillRectangle(New SolidBrush(边框外部颜色), 外部区域)

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