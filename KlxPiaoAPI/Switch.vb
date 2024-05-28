Imports System.ComponentModel
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

<DefaultEvent("CheckedChanged")>
Public Class Switch
    Inherits Control

    Private _Checked As Boolean

    Private _背景色 As Color
    Private _前景色 As Color
    Private _边框颜色 As Color
    Private _边框大小 As Integer
    Private _文字与开关间距 As Integer

    Private _激活时背景色 As Color
    Private _激活时前景色 As Color
    Private _激活时边框颜色 As Color
    Private _激活时边框大小 As Integer
    Private _激活时文字 As String
    Public Sub New()
        MyBase.New

        _Checked = False

        _背景色 = Color.FromArgb(204, 204, 204)
        _前景色 = Color.White
        _边框颜色 = Color.FromArgb(204, 204, 204)
        _边框大小 = 1
        _文字与开关间距 = 5

        _激活时背景色 = Color.FromArgb(120, 214, 144)
        _激活时前景色 = Color.Transparent
        _激活时边框颜色 = Color.Transparent
        _激活时边框大小 = -1
        _激活时文字 = String.Empty

        Size = New Size(94, 17)
        DoubleBuffered = True
        SetStyle(ControlStyles.Selectable, True)
    End Sub
    Public Event CheckedChanged As PropertyChangedEventHandler
    Protected Overridable Sub OnValueChanged(propertyName As String)
        RaiseEvent CheckedChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    <Category("Switch属性")>
    <Description("获取或设置开关的状态")>
    <DefaultValue(False)>
    Public Property Checked As Boolean
        Get
            Return _Checked
        End Get
        Set(value As Boolean)
            _Checked = value
            Invalidate()
            OnValueChanged("Checked")
        End Set
    End Property

    <Category("Switch外观")>
    <Description("开关背景色，并不是BackColor属性")>
    <DefaultValue(GetType(Color), "204,204,204")>
    Public Property 背景色 As Color
        Get
            Return _背景色
        End Get
        Set(value As Color)
            _背景色 = value
            Invalidate()
        End Set
    End Property
    <Category("Switch外观")>
    <Description("开关的颜色")>
    <DefaultValue(GetType(Color), "White")>
    Public Property 前景色 As Color
        Get
            Return _前景色
        End Get
        Set(value As Color)
            _前景色 = value
            Invalidate()
        End Set
    End Property
    <Category("Switch外观")>
    <Description("边框的颜色")>
    <DefaultValue(GetType(Color), "204,204,204")>
    Public Property 边框颜色 As Color
        Get
            Return _边框颜色
        End Get
        Set(value As Color)
            _边框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("Switch外观")>
    <Description("边框的大小，为0时隐藏边框")>
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
    <Category("Switch外观")>
    <Description("文字与开关的间距")>
    <DefaultValue(5)>
    Public Property 文字与开关间距 As Integer
        Get
            Return _文字与开关间距
        End Get
        Set(value As Integer)
            _文字与开关间距 = value
            Invalidate()
        End Set
    End Property

    <Category("Switch激活时")>
    <Description("Checked=True时呈现的背景色，Transparent：不改变前景色")>
    <DefaultValue(GetType(Color), "120,214,144")>
    Public Property 激活时背景色 As Color
        Get
            Return _激活时背景色
        End Get
        Set(value As Color)
            _激活时背景色 = value
            Invalidate()
        End Set
    End Property
    <Category("Switch激活时")>
    <Description("Checked=True时呈现的前景色，Transparent：不改变前景色")>
    <DefaultValue(GetType(Color), "Transparent")>
    Public Property 激活时前景色 As Color
        Get
            Return _激活时前景色
        End Get
        Set(value As Color)
            _激活时前景色 = value
            Invalidate()
        End Set
    End Property
    <Category("Switch激活时")>
    <Description("Checked=True时呈现的边框颜色，Transparent：不改变边框颜色")>
    <DefaultValue(GetType(Color), "Transparent")>
    Public Property 激活时边框颜色 As Color
        Get
            Return _激活时边框颜色
        End Get
        Set(value As Color)
            _激活时边框颜色 = value
            Invalidate()
        End Set
    End Property
    <Category("Switch激活时")>
    <Description("Checked=True时呈现的边框大小，-1：不改变边框大小")>
    <DefaultValue(-1)>
    Public Property 激活时边框大小 As Integer
        Get
            Return _激活时边框大小
        End Get
        Set(value As Integer)
            _激活时边框大小 = value
            Invalidate()
        End Set
    End Property
    <Category("Switch激活时")>
    <Description("Checked=True时呈现的文字，留空时不改变文字")>
    Public Property 激活时文字 As String
        Get
            Return _激活时文字
        End Get
        Set(value As String)
            _激活时文字 = value
            Invalidate()
        End Set
    End Property
    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.HighQuality 
        g.PixelOffsetMode = PixelOffsetMode.HighQuality

        Dim 背景色修正 As Color = If(激活时背景色 = Color.Transparent, 背景色, 激活时背景色)
        Dim 前景色修正 As Color = If(激活时前景色 = Color.Transparent, 前景色, 激活时前景色)
        Dim 边框颜色修正 As Color = If(激活时边框颜色 = Color.Transparent, 边框颜色, 激活时边框颜色)
        Dim 边框大小修正 As Integer = If(激活时边框大小 = -1, 边框大小, 激活时边框大小)
        Dim 文字修正 As String = If(激活时文字 = String.Empty, Text, 激活时文字)

        Dim 默认文字宽度 As Integer = g.MeasureString(Text, Font).Width
        Dim 激活文字宽度 As Integer = g.MeasureString(激活时文字, Font).Width
        Dim 较长的文字宽度 As Integer = Math.Max(默认文字宽度, 激活文字宽度)
        Dim 当前显示的文字宽度 As Integer = g.MeasureString(If(Checked, 文字修正, Text), Font).Width
        Dim 文字高度 As Integer = g.MeasureString(If(Checked, 文字修正, Text), Font).Height

        Dim 区域 As New Rectangle(较长的文字宽度 + 文字与开关间距, 0, Width, Height)

        If Not Checked Then '未激活时全部保持默认值
            Dim 绘制区域 As New Rectangle(区域.X + CType(边框大小 / 2, Single), 区域.Y + CType(边框大小 / 2, Single), 区域.Height - 边框大小, 区域.Height - 边框大小)
            g.Clear(背景色)
            g.FillEllipse(New SolidBrush(前景色), 绘制区域)
        Else
            Dim 绘制区域 As New Rectangle(区域.Width - CType(边框大小修正 / 2, Single) - (区域.Height - 边框大小修正), 区域.Y + CType(边框大小修正 / 2, Single), 区域.Height - 边框大小修正, 区域.Height - 边框大小修正)
            g.Clear(背景色修正)
            g.FillEllipse(New SolidBrush(前景色修正), 绘制区域)
        End If

#Region "边框&圆角"
        Dim 边框外部颜色 As Color = BackColor
        Dim 圆角百分比 As Double = 1
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
            g.DrawPath(New Pen(If(Checked, 边框颜色修正, 边框颜色), If(Checked, 边框大小修正, 边框大小)), 边框)
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
#End Region

        '覆盖多余的部分
        g.FillRectangle(New SolidBrush(边框外部颜色), New Rectangle(0, 0, 较长的文字宽度 + 文字与开关间距, Height))
        g.DrawString(If(Checked, 文字修正, Text), Font, New SolidBrush(ForeColor), New PointF((较长的文字宽度 + 文字与开关间距 - 当前显示的文字宽度) / 2, (Height - 文字高度) / 2))
    End Sub

    '点击事件
    Protected Overrides Sub OnMouseClick(e As MouseEventArgs)
        MyBase.OnMouseClick(e)

        If e.Button = MouseButtons.Left Then
            Checked = Not Checked
            Focus()
        End If
    End Sub
    '文字改变时及时刷新
    Protected Overrides Sub OnTextChanged(e As EventArgs)
        MyBase.OnTextChanged(e)

        Refresh()
    End Sub
    '键盘响应
    Protected Overrides Sub OnPreviewKeyDown(e As PreviewKeyDownEventArgs)
        MyBase.OnPreviewKeyDown(e)

        Select Case e.KeyCode
            Case Keys.Enter
                Checked = Not Checked
            Case Keys.Space
                Checked = Not Checked
            Case Keys.Left
                If Checked = True Then
                    Checked = False
                End If
                e.IsInputKey = True
            Case Keys.Right
                If Checked = False Then
                    Checked = True
                End If
                e.IsInputKey = True
        End Select
    End Sub


End Class