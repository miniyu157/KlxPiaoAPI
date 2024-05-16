Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Public Class KlxPiaoTabPage
    Inherits TabControl

    Public 边框颜色 As Color
    Private 当前页背景色 As Color = Nothing
    Public Sub New()
        SetStyle(ControlStyles.UserPaint, True)

        Size = New Size(283, 127)
    End Sub

    <DefaultValue(GetType(Size), "283,127")>
    Public Overloads Property Size As Size
        Get
            Return MyBase.Size
        End Get
        Set(value As Size)
            MyBase.Size = value
            Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim g As Graphics = e.Graphics

        '适应边缘颜色
        If 当前页背景色 = Nothing Then
            If SelectedIndex > -1 Then
                g.Clear(TabPages(SelectedIndex).BackColor)
            End If
        Else
            g.Clear(当前页背景色)
        End If

        '边框
        Using BorderPen As New Pen(边框颜色, 1)
            '绘制上边
            g.DrawLine(BorderPen, 0, 0, Width - 1, 0)
            '绘制右边
            g.DrawLine(BorderPen, Width - 1, 0, Width - 1, Height - 1)
            '绘制下边
            g.DrawLine(BorderPen, Width - 1, Height - 1, 0, Height - 1)
            '不绘制左边，防止覆盖投影
            'g.DrawLine(BorderPen, 0, Height - 1, 0, 0)
        End Using

        '未绑定时显示提示文本
        If ItemSize.Height <> 1 Then
            g.DrawString($"{Name}:请绑定KlxPiaoTabControl", New Font("微软雅黑", 9), New SolidBrush(Color.Red), New Point(6, 6))
        End If
    End Sub

    Private Sub KlxPiaoTabPage_Selected(sender As Object, e As TabControlEventArgs) Handles Me.Selected
        当前页背景色 = e.TabPage.BackColor
        Refresh()
    End Sub
End Class
