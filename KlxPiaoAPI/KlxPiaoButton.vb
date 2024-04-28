Imports System.Drawing
Imports System.Windows.Forms
Public Class KlxPiaoButton
    Inherits Button

    Public Sub New()
        MyBase.New()
        FlatStyle = FlatStyle.Flat
        FlatAppearance.BorderSize = 1
        FlatAppearance.BorderColor = Color.Gainsboro
        FlatAppearance.MouseDownBackColor = Color.FromArgb(230, 230, 230)
        FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240)

        Size = New Size(110, 40)
        Font = New Font("微软雅黑 Light", 9)
    End Sub
End Class