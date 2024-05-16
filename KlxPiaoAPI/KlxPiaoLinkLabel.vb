Imports System.Drawing
Imports System.Windows.Forms
Public Class KlxPiaoLinkLabel
    Inherits LinkLabel

    Public Sub New()
        MyBase.New()

        LinkBehavior = LinkBehavior.HoverUnderline
        BackColor = Color.White
        LinkColor = Color.Black
        ForeColor = Color.Black
        ActiveLinkColor = Color.Black
        DisabledLinkColor = Color.FromArgb(210, 210, 210)
    End Sub

End Class