Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class KlxPiaoTabControl
    Inherits Control

    Private Shared _绑定 As TabControl
    Private _选项卡大小 As Size

    Public Sub New()
        MyBase.New()

        SetStyle(ControlStyles.ContainerControl, True)
        DoubleBuffered = True

        _选项卡大小 = New Size(88, 33)
        Size = New Size(245, 176)
    End Sub

    <Category("KlxPiaoTabControl属性"), Description("选择绑定的选项卡后，会强制设置绑定选项卡的大小、位置等属性")>
    Public Property 绑定 As TabControl
        Get
            Return _绑定
        End Get
        Set(value As TabControl)
            _绑定 = value
            Invalidate()
        End Set
    End Property
    <Category("KlxPiaoTabControl属性"), Description("选项卡的大小")>
    Public Property 选项卡大小 As Size
        Get
            Return _选项卡大小
        End Get
        Set(value As Size)
            _选项卡大小 = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics

        '选项卡边缘
        Using 选项卡Pen As New Pen(Color.Gainsboro, 1)
            g.DrawLine(选项卡Pen, 选项卡大小.Width + 1, 0, 选项卡大小.Width + 1, Height)
        End Using

        '边框
        Using BorderPen As New Pen(Color.Gainsboro, 1)
            g.DrawRectangle(BorderPen, 0, 0, Width - 1, Height - 1)
        End Using

        '更新选项卡菜单
        For Each 清除选项卡 In Controls
            If TypeOf 清除选项卡 Is 无法获得焦点的按钮 Then
                Controls.Remove(清除选项卡)
            End If
        Next

        If 绑定 IsNot Nothing Then
            For i = 0 To 绑定.TabCount - 1
                Dim 标题 As String = 绑定.TabPages(i).Text

                Dim 选项卡按钮 As New 无法获得焦点的按钮 With {
                    .Size = 选项卡大小,
                    .Location = New Point(1, 1 + i * 33),
                    .Text = 标题,
                    .Tag = i
                }

                ''选项卡菜单反馈
                'If 绑定.SelectedIndex = i Then
                '    For Each 选项卡反馈 In Controls
                '        If TypeOf 选项卡反馈 Is 无法获得焦点的按钮 Then
                '            DirectCast(选项卡反馈, 无法获得焦点的按钮).BackColor = Color.White
                '        End If
                '    Next

                '    选项卡按钮.BackColor = Color.FromArgb(240, 240, 240)
                '    选项卡按钮.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240)
                'End If

                Controls.Add(选项卡按钮)
            Next

            If Not Controls.Contains(绑定) Then
                Controls.Add(绑定)
            End If

            绑定.Alignment = TabAlignment.Top
            绑定.Location = New Point(选项卡大小.Width + 1, -(绑定.ItemSize.Height))
            绑定.Size = New Size(Width - 选项卡大小.Width, Height + (绑定.ItemSize.Height))
        End If
    End Sub

    Private Class 无法获得焦点的按钮
        Inherits Button

        Public Sub New()
            MyBase.New()

            SetStyle(ControlStyles.Selectable, False)

            FlatStyle = FlatStyle.Flat
            FlatAppearance.BorderSize = 0
            FlatAppearance.BorderColor = Color.Gainsboro
            FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 230, 230)
            FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240)

            Font = New Font("微软雅黑 Light", 9)

            AddHandler Click, AddressOf Button_Click
        End Sub

        Private Sub Button_Click(sender As Object, e As EventArgs)
            _绑定.SelectedIndex = CInt(Tag)
        End Sub
    End Class

End Class