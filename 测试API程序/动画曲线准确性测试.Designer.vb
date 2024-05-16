Imports KlxPiaoAPI

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class 动画曲线准确性测试
    Inherits KlxPiaoForm

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.KlxPiaoLabel1 = New KlxPiaoAPI.KlxPiaoLabel()
        Me.KlxPiaoPanel1 = New KlxPiaoAPI.KlxPiaoPanel()
        Me.SuspendLayout()
        '
        'KlxPiaoLabel1
        '
        Me.KlxPiaoLabel1.AutoSize = False
        Me.KlxPiaoLabel1.BackColor = System.Drawing.Color.DimGray
        Me.KlxPiaoLabel1.Font = New System.Drawing.Font("微软雅黑 Light", 9.0!)
        Me.KlxPiaoLabel1.ForeColor = System.Drawing.Color.White
        Me.KlxPiaoLabel1.Location = New System.Drawing.Point(162, 287)
        Me.KlxPiaoLabel1.Name = "KlxPiaoLabel1"
        Me.KlxPiaoLabel1.Size = New System.Drawing.Size(114, 23)
        Me.KlxPiaoLabel1.TabIndex = 1
        Me.KlxPiaoLabel1.Text = "00:00:00.0000000"
        Me.KlxPiaoLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
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
        'KlxPiaoPanel1
        '
        Me.KlxPiaoPanel1.BackColor = System.Drawing.Color.White
        Me.KlxPiaoPanel1.Location = New System.Drawing.Point(-1, 31)
        Me.KlxPiaoPanel1.Name = "KlxPiaoPanel1"
        Me.KlxPiaoPanel1.Size = New System.Drawing.Size(1, 319)
        Me.KlxPiaoPanel1.TabIndex = 2
        Me.KlxPiaoPanel1.启用投影 = False
        Me.KlxPiaoPanel1.圆角百分比 = 0!
        Me.KlxPiaoPanel1.边框颜色 = System.Drawing.Color.Red
        '
        '动画曲线准确性测试
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(288, 319)
        Me.Controls.Add(Me.KlxPiaoLabel1)
        Me.Controls.Add(Me.KlxPiaoPanel1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "动画曲线准确性测试"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultLocation
        Me.Text = "动画曲线准确性测试"
        Me.窗体可拖动位置 = KlxPiaoAPI.KlxPiaoForm.拖动位置.整个窗体
        Me.窗体可调整大小 = False
        Me.窗体按钮 = KlxPiaoAPI.KlxPiaoForm.窗体按钮样式.仅显示关闭
        Me.窗体边框颜色 = System.Drawing.Color.FromArgb(CType(CType(113, Byte), Integer), CType(CType(96, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents KlxPiaoLabel1 As KlxPiaoLabel
    Friend WithEvents KlxPiaoPanel1 As KlxPiaoPanel
End Class
