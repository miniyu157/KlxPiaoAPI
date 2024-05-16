<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits KlxPiaoAPI.KlxPiaoForm

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。  
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.KlxPiaoLabel1 = New KlxPiaoAPI.KlxPiaoLabel()
        Me.KlxPiaoLinkLabel1 = New KlxPiaoAPI.KlxPiaoLinkLabel()
        Me.SuspendLayout()
        '
        'KlxPiaoLabel1
        '
        Me.KlxPiaoLabel1.AutoSize = False
        Me.KlxPiaoLabel1.BackColor = System.Drawing.Color.White
        Me.KlxPiaoLabel1.Font = New System.Drawing.Font("微软雅黑 Light", 9.0!)
        Me.KlxPiaoLabel1.ForeColor = System.Drawing.Color.Black
        Me.KlxPiaoLabel1.Location = New System.Drawing.Point(12, 41)
        Me.KlxPiaoLabel1.Name = "KlxPiaoLabel1"
        Me.KlxPiaoLabel1.Size = New System.Drawing.Size(81, 17)
        Me.KlxPiaoLabel1.TabIndex = 4
        Me.KlxPiaoLabel1.Text = "KlxPiaoLabel1"
        Me.KlxPiaoLabel1.偏移方式 = System.Drawing.Drawing2D.PixelOffsetMode.[Default]
        Me.KlxPiaoLabel1.偏移量 = New System.Drawing.Point(2, 2)
        Me.KlxPiaoLabel1.启用投影 = False
        Me.KlxPiaoLabel1.启用边框 = False
        Me.KlxPiaoLabel1.圆角百分比 = 0!
        Me.KlxPiaoLabel1.投影连线 = True
        Me.KlxPiaoLabel1.投影颜色 = System.Drawing.Color.DarkGray
        Me.KlxPiaoLabel1.抗锯齿 = System.Drawing.Drawing2D.SmoothingMode.[Default]
        Me.KlxPiaoLabel1.文本呈现质量 = System.Drawing.Text.TextRenderingHint.SystemDefault
        Me.KlxPiaoLabel1.算法 = System.Drawing.Drawing2D.InterpolationMode.[Default]
        Me.KlxPiaoLabel1.边框外部颜色 = System.Drawing.Color.White
        Me.KlxPiaoLabel1.边框大小 = 5
        Me.KlxPiaoLabel1.边框颜色 = System.Drawing.Color.LightGray
        Me.KlxPiaoLabel1.颜色减淡 = False
        '
        'KlxPiaoLinkLabel1
        '
        Me.KlxPiaoLinkLabel1.ActiveLinkColor = System.Drawing.Color.Black
        Me.KlxPiaoLinkLabel1.AutoSize = True
        Me.KlxPiaoLinkLabel1.BackColor = System.Drawing.Color.White
        Me.KlxPiaoLinkLabel1.DisabledLinkColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(210, Byte), Integer))
        Me.KlxPiaoLinkLabel1.ForeColor = System.Drawing.Color.Black
        Me.KlxPiaoLinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline
        Me.KlxPiaoLinkLabel1.LinkColor = System.Drawing.Color.Black
        Me.KlxPiaoLinkLabel1.Location = New System.Drawing.Point(416, 474)
        Me.KlxPiaoLinkLabel1.Name = "KlxPiaoLinkLabel1"
        Me.KlxPiaoLinkLabel1.Size = New System.Drawing.Size(103, 17)
        Me.KlxPiaoLinkLabel1.TabIndex = 5
        Me.KlxPiaoLinkLabel1.TabStop = True
        Me.KlxPiaoLinkLabel1.Text = "KlxPiaoLinkLabel1"
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(531, 500)
        Me.Controls.Add(Me.KlxPiaoLinkLabel1)
        Me.Controls.Add(Me.KlxPiaoLabel1)
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents KlxPiaoLabel1 As KlxPiaoAPI.KlxPiaoLabel
    Friend WithEvents KlxPiaoLinkLabel1 As KlxPiaoAPI.KlxPiaoLinkLabel
End Class
