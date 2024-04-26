Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class KlxPiaoLabel
    Inherits Label

    Private _启用投影 As Boolean
    Private _投影颜色 As Color
    Private _投影连线 As Boolean
    Private _偏移量 As Point
    Private _颜色减淡 As Boolean
    Public Sub New()
        MyBase.New

        _启用投影 = False
        _投影颜色 = Color.DarkGray
        _投影连线 = True
        _偏移量 = New Point(2, 2)
        _颜色减淡 = False


        Font = New Font("微软雅黑 Light", 9)
        ForeColor = Color.Black
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


    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics

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

#Region "以前的超复杂代码"
                    'If 偏移量.X > 0 Then

                    '    '横向绘制
                    '    '第一象限，第四象限，x正半轴
                    '    For x = 0 To 偏移量.X Step 1
                    '        Dim 斜率 As Double = 偏移量.Y / 偏移量.X

                    '        g.DrawString(Text, Font, brush, New Point(x, x * 斜率))
                    '    Next

                    '    '纵向绘制
                    '    If 偏移量.Y > 0 Then
                    '        '第四象限
                    '        For y = 0 To 偏移量.Y Step 1
                    '            Dim 斜率 As Double = 偏移量.X / 偏移量.Y

                    '            g.DrawString(Text, Font, brush, New Point(y * 斜率, y))
                    '        Next
                    '    ElseIf 偏移量.Y < 0 Then
                    '        '第一象限
                    '        For y = 0 To 偏移量.Y Step -1
                    '            Dim 斜率 As Double = 偏移量.X / 偏移量.Y

                    '            g.DrawString(Text, Font, brush, New Point(y * 斜率, y))
                    '        Next
                    '    ElseIf 偏移量.Y = 0 Then
                    '        'x正半轴
                    '        For x = 0 To 偏移量.X Step 1
                    '            g.DrawString(Text, Font, brush, New Point(x, 0))
                    '        Next
                    '    End If

                    'ElseIf 偏移量.X < 0 Then

                    '    '横向绘制
                    '    '第二象限，第三象限，x负半轴
                    '    For x = 0 To 偏移量.X Step -1
                    '        Dim 斜率 As Double = 偏移量.Y / 偏移量.X

                    '        g.DrawString(Text, Font, brush, New Point(x, x * 斜率))
                    '    Next

                    '    '纵向绘制
                    '    If 偏移量.Y > 0 Then
                    '        '第三象限
                    '        For y = 0 To 偏移量.Y Step 1
                    '            Dim 斜率 As Double = 偏移量.X / 偏移量.Y

                    '            g.DrawString(Text, Font, brush, New Point(y * 斜率, y))
                    '        Next
                    '    ElseIf 偏移量.Y < 0 Then
                    '        '第二象限
                    '        For y = 0 To 偏移量.Y Step -1
                    '            Dim 斜率 As Double = 偏移量.X / 偏移量.Y

                    '            g.DrawString(Text, Font, brush, New Point(y * 斜率, y))
                    '        Next
                    '    ElseIf 偏移量.Y = 0 Then
                    '        'x负半轴
                    '        For x = 0 To 偏移量.X Step -1
                    '            g.DrawString(Text, Font, brush, New Point(x, 0))
                    '        Next
                    '    End If
                    'ElseIf 偏移量.X = 0 Then
                    '    If 偏移量.Y > 0 Then
                    '        'y负半轴
                    '        For y = 0 To 偏移量.Y Step 1
                    '            g.DrawString(Text, Font, brush, New Point(0, y))
                    '        Next
                    '    ElseIf 偏移量.Y < 0 Then
                    '        'y正半轴
                    '        For y = 0 To 偏移量.Y Step -1
                    '            g.DrawString(Text, Font, brush, New Point(0, y))
                    '        Next
                    '    End If
                    'End If
#End Region

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
                    g.DrawString(Text, Font, brush, New Point(偏移量.X, 偏移量.Y))
                End If

                '文字本体
                g.DrawString(Text, Font, New SolidBrush(ForeColor), 绘制位置)
            End Using
        Else
            '文字本体
            g.DrawString(Text, Font, New SolidBrush(ForeColor), 绘制位置)
        End If
    End Sub
End Class