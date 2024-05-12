Imports System.Net
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Text
Imports System.Windows.Forms
Imports System.Threading

Public Class 控件
    Public Shared Sub 设置属性(Of T As Control)(控件 As T, 属性名称 As String, 值 As Object)
        Dim propInfo As PropertyInfo = GetType(T).GetProperty(属性名称)
        If propInfo IsNot Nothing AndAlso propInfo.CanWrite Then
            Dim convertedValue As Object = Convert.ChangeType(值, propInfo.PropertyType)
            propInfo.SetValue(控件, convertedValue)
        Else
            Throw New Exception("找不到属性 " & 属性名称 & " 或者不可写。")
        End If
    End Sub
    Public Shared Function 读取属性(Of T As Control)(控件 As T, 属性名称 As String) As Object
        Dim propInfo As PropertyInfo = GetType(T).GetProperty(属性名称)
        If propInfo IsNot Nothing Then
            Return propInfo.GetValue(控件)
        Else
            Throw New ArgumentException("属性不存在：" & 属性名称)
        End If
    End Function
    ''' <summary>
    ''' 修改控件属性的过渡动画
    ''' </summary>
    ''' <param name="控件名称">要修改属性的控件</param>
    ''' <param name="属性名">要修改的属性</param>
    ''' <param name="开始值">初始值</param>
    ''' <param name="结束值">最终值</param>
    ''' <param name="持续时间">动画持续时间</param>
    ''' <param name="控制点">贝塞尔曲线控制点</param>
    ''' <param name="帧率">动画的帧率</param>
    Public Shared Sub 过渡动画(控件名称 As Control, 属性名 As String, 开始值 As Object, 结束值 As Object, 持续时间 As Integer, 控制点 As PointF(), Optional 帧率 As Integer = 100)
        Dim 启动时间 As Date = Date.Now
        Dim 总时间 As TimeSpan = TimeSpan.FromSeconds(持续时间 / 1000)
        Dim 运行状态 As Boolean = False
        Dim 开始动画 As New Thread(Async Sub()
                                   If TypeOf 开始值 Is Color OrElse TypeOf 结束值 Is Color Then
                                       Dim 执行次数 As Integer = 持续时间 \ (1000 / 帧率)

                                       Dim 原颜色 As Color = 开始值
                                       Dim 目标颜色 As Color = 结束值

                                       Dim R差值 As Integer = Math.Abs(CInt(目标颜色.R) - CInt(原颜色.R))
                                       Dim G差值 As Integer = Math.Abs(CInt(目标颜色.G） - CInt(原颜色.G))
                                       Dim B差值 As Integer = Math.Abs(CInt(目标颜色.B） - CInt(原颜色.B))

                                       For i = 1 To 执行次数
                                           Dim R = 原颜色.R + (R差值 * i) \ 执行次数 * If(CInt(目标颜色.R) - CInt(原颜色.R) > 0, 1, -1)
                                           Dim G = 原颜色.G + (G差值 * i) \ 执行次数 * If(CInt(目标颜色.G) - CInt(原颜色.G) > 0, 1, -1)
                                           Dim B = 原颜色.B + (B差值 * i) \ 执行次数 * If(CInt(目标颜色.B) - CInt(原颜色.B) > 0, 1, -1)
                                           控件名称.Invoke(Sub() 设置属性(控件名称, 属性名, Color.FromArgb(R, G, B)))
                                           Await Task.Delay(1000 / 帧率)
                                       Next
                                   Else
                                       Do While 运行状态 = False
                                           Dim 当前时间 As TimeSpan = Date.Now - 启动时间
                                           Dim 时间百分比 As Double = 当前时间.TotalMilliseconds / 总时间.TotalMilliseconds
                                           If 时间百分比 >= 1.0 Then
                                               运行状态 = True
                                               控件名称.Invoke(Sub() 设置属性(控件名称, 属性名, 结束值.ToString))
                                           Else
                                               Dim p As PointF = BezierCurve(返回曲线百分比(时间百分比, 控制点(0), 控制点(1)), 控制点(0), 控制点(1))
                                               控件名称.Invoke(Sub() 设置属性(控件名称, 属性名, (结束值 + p.Y * (开始值 - 结束值))))
                                           End If
                                           Await Task.Delay(1000 / 帧率)
                                       Loop
                                   End If
                               End Sub)
        开始动画.Start()
    End Sub
    ''' <summary>
    ''' 根据时间百分比返回曲线百分比
    ''' </summary>
    ''' <param name="时间百分比"></param>
    ''' <param name="控制点1">平面直角坐标系上的点</param>
    ''' <param name="控制点2">平面直角坐标系上的点</param>
    ''' <returns></returns>
    Public Shared Function 返回曲线百分比(时间百分比 As Double, 控制点1 As PointF, 控制点2 As PointF) As Double
        Dim epsilon As Double = 0.001 ' 容差
        Dim low As Double = 0.0
        Dim high As Double = 1.0
        Dim mid As Double

        Do
            mid = (low + high) / 2.0
            Dim p As PointF = BezierCurve(mid, 控制点1, 控制点2)
            If Math.Abs(p.X - 时间百分比) < epsilon Then
                Return mid
            ElseIf p.X < 时间百分比 Then
                low = mid
            Else
                high = mid
            End If
        Loop While Math.Abs(high - low) > epsilon

        Return mid
    End Function

    ''' <summary>
    ''' 根据曲线百分比返回坐标
    ''' </summary>
    ''' <param name="t"></param>
    ''' <param name="控制点1">平面直角坐标系上的点</param>
    ''' <param name="控制点2">平面直角坐标系上的点</param>
    ''' <returns></returns>
    Public Shared Function BezierCurve(t As Double, 控制点1 As PointF, 控制点2 As PointF) As PointF
        Dim p0 As New PointF(0.00, 1.0)
        Dim p3 As New PointF(1.0, 0.00)
        控制点1.Y = 1 - 控制点1.Y
        控制点2.Y = 1 - 控制点2.Y

        Dim p As PointF
        Dim mt As Single = 1.0F - t
        p.X = mt * mt * mt * p0.X + 3.0F * mt * mt * t * 控制点1.X + 3.0F * mt * t * t * 控制点2.X + t * t * t * p3.X
        p.Y = mt * mt * mt * p0.Y + 3.0F * mt * mt * t * 控制点1.Y + 3.0F * mt * t * t * 控制点2.Y + t * t * t * p3.Y
        Return p
    End Function
End Class

Public Class 颜色

    Public Shared Function 调整亮度(color As Color, factor As Double) As Color
        Dim red As Integer = Math.Min(Math.Max(0, color.R * (1 + factor)), 255)
        Dim green As Integer = Math.Min(Math.Max(0, color.G * (1 + factor)), 255)
        Dim blue As Integer = Math.Min(Math.Max(0, color.B * (1 + factor)), 255)
        Return Color.FromArgb(color.A, red, green, blue)
    End Function
    Public Shared Function 获取亮度(color As Color) As Double
        Return 0.299 * color.R + 0.587 * color.G + 0.114 * color.B
    End Function
End Class

Public Class 文件处理
    Public Shared Function 加载字体(本地文件路径 As String, 字体大小 As Single, 字体样式 As FontStyle) As Font
        ' 创建一个私有字体集合
        Dim privateFonts As New PrivateFontCollection()

        ' 加载字体文件到私有字体集合
        privateFonts.AddFontFile(本地文件路径)

        Dim customFont As New Font(privateFonts.Families(0), 字体大小, 字体样式)
        Return customFont

    End Function
End Class

Public Class 图片处理
    Public Shared Function 图片颜色替换(原图 As Bitmap, 要替换的颜色 As Color, 替换为 As Color) As Bitmap
        Dim originalBitmap As New Bitmap(原图)

        ' 修改像素
        Dim modifiedBitmap As New Bitmap(originalBitmap.Width, originalBitmap.Height)
        For y As Integer = 0 To originalBitmap.Height - 1
            For x As Integer = 0 To originalBitmap.Width - 1

                Dim pixelColor As Color = originalBitmap.GetPixel(x, y)

                If pixelColor.A = 0 Then
                    ' 透明像素，保持原始颜色
                    modifiedBitmap.SetPixel(x, y, pixelColor)
                ElseIf pixelColor.R = 要替换的颜色.R AndAlso pixelColor.G = 要替换的颜色.G AndAlso pixelColor.B = 要替换的颜色.B Then
                    ' 非透明且颜色匹配，进行替换
                    modifiedBitmap.SetPixel(x, y, 替换为)
                Else
                    ' 其他情况保持原始颜色
                    modifiedBitmap.SetPixel(x, y, pixelColor)
                End If
            Next
        Next
        Return modifiedBitmap
    End Function
End Class


Namespace 常用功能
    ''' <summary>
    ''' 调用WScript.Shell创建快捷方式
    ''' </summary>
    Public Class 创建快捷方式
        ''' <summary>
        ''' 要带有.lnk
        ''' </summary>
        Public 快捷方式路径 As String
        Public 目标文件路径 As String
        ''' <summary>
        ''' 未设置起始位置时使用目标文件所在文件夹
        ''' </summary>
        Public 起始位置 As String
        ''' <summary>
        ''' 未设置图标时使用默认图标
        ''' </summary>
        Public 图标 As String
        Public 备注 As String
        ''' <summary>
        ''' 例如"Ctrl+Alt+e"
        ''' </summary>
        Public 快捷键 As String
        ''' <summary>
        ''' 在常规窗口（默认），最大化，最小化中选择一项
        ''' </summary>
        Public 运行方式 As Integer
        Public Class 窗体样式
            ''' <summary>
            ''' 创建快捷方式.窗体.常规窗口 = 1
            ''' </summary>
            Public Shared 常规窗口 As Integer = 1
            ''' <summary>
            ''' 创建快捷方式.窗体.最大化 = 3
            ''' </summary>
            Public Shared 最大化 As Integer = 3
            ''' <summary>
            ''' 创建快捷方式.窗体.最小化 = 7
            ''' </summary>
            Public Shared 最小化 As Integer = 7
        End Class

        Public Sub 保存()
            Dim ShortCut1 = CreateObject("WScript.Shell").CreateShortcut(快捷方式路径)
            ShortCut1.TargetPath = 目标文件路径

            If String.IsNullOrEmpty(起始位置) Then
                ShortCut1.WorkingDirectory = Path.GetDirectoryName(目标文件路径)
            Else
                ShortCut1.WorkingDirectory = 起始位置
            End If

            If String.IsNullOrEmpty(图标) Then
                ShortCut1.IconLocation = $"{目标文件路径},0"
            Else
                ShortCut1.IconLocation = 图标
            End If

            ShortCut1.Description = 备注
            ShortCut1.Hotkey = 快捷键
            ShortCut1.WindowStyle = 运行方式
            ShortCut1.Save()
        End Sub
    End Class

    Public Module 默认类
        Public Sub 刷新图标缓存()
            Const SHCNE_ASSOCCHANGED As Integer = &H8000000
            Const HCNF_FLUSH As Integer = &H1000
            SHChangeNotify(SHCNE_ASSOCCHANGED, HCNF_FLUSH, IntPtr.Zero, IntPtr.Zero)
        End Sub
        <DllImport("shell32.dll")>
        Private Sub SHChangeNotify(eventId As Integer, flags As Integer, item1 As IntPtr, item2 As IntPtr)
        End Sub
    End Module
End Namespace

Public Class 关于KlxPiao
    Public Shared Function 产品名称() As String
        Dim currentAssembly As Assembly = Assembly.GetExecutingAssembly()
        Dim assemblyName As String = currentAssembly.GetName().Name
        Return assemblyName
    End Function
    Public Shared Function 产品版本() As String
        Dim currentAssembly As Assembly = Assembly.GetExecutingAssembly()
        Dim assemblyName As String = currentAssembly.GetName().Version.ToString
        Return assemblyName
    End Function
End Class

Public Class 网络功能
    Public Shared Function 获取HTML内容(ByVal url As String) As String
        Dim client As New WebClient With {
            .Encoding = Encoding.UTF8
        }

        Return Encoding.UTF8.GetString(client.DownloadData(url))
    End Function
End Class

Namespace 字符串操作
    Public Class 编码转换
        '将中文转换为Unicode编码
        Public Shared Function ChineseToUnicode(ByVal chineseString As String) As String
            Dim unicodeString As New StringBuilder()
            For Each c As Char In chineseString
                unicodeString.AppendFormat("\u{0:X4}", AscW(c))
            Next
            Return unicodeString.ToString()
        End Function

        ' 将形如\uXXXX的Unicode字符串转换为中文
        Public Shared Function UnicodeToChinese(ByVal unicodeString As String) As String
            ' 匹配所有的Unicode转义序列
            Dim regex As New Regex("\\u(?<Value>[a-fA-F0-9]{4})", RegexOptions.Compiled)
            Return regex.Replace(unicodeString, Function(m) ChrW(Convert.ToInt32(m.Groups("Value").Value, 16)).ToString())
        End Function
    End Class
    Public Module 默认类
        ''' <summary>
        ''' 提供者：ChatGPT
        ''' </summary>
        <Runtime.CompilerServices.Extension()>
        Public Function 提取中间文本1(原文本 As String, 前导文本 As String, 后导文本 As String) As String
            Dim startIndex As Integer = 原文本.IndexOf(前导文本)
            If startIndex = -1 Then
                Return ""
            End If

            startIndex += 前导文本.Length
            Dim endIndex As Integer = 原文本.IndexOf(后导文本, startIndex)
            If endIndex = -1 Then
                Return ""
            End If

            Return 原文本.Substring(startIndex, endIndex - startIndex)
        End Function
        ''' <summary>
        ''' 提供者：百度AI
        ''' </summary>
        <Runtime.CompilerServices.Extension()>
        Public Function 提取中间文本2(原文本 As String, 前导文本 As String, 后导文本 As String) As String
            Dim matches As MatchCollection = Regex.Matches(原文本, 前导文本 + "(.*?)" & 后导文本)
            Dim result As New List(Of String)
            For Each match As Match In matches
                If match.Groups.Count > 1 Then
                    result.Add(match.Groups(1).Value)
                End If
            Next

            If result.Count > 0 Then
                Return result(0)
            Else
                Return ("")
            End If
        End Function
        ''' <summary>
        ''' 提供者：KlxPiao
        ''' </summary>
        <Runtime.CompilerServices.Extension()>
        Public Function 提取中间文本3(ByVal 原文本 As String, ByVal 前导文本 As String, ByVal 后导文本 As String) As String
            Try
                Dim 原文本_ As String = 原文本
                Dim 前导文本_ As String = 前导文本
                Dim 后导文本_ As String = 后导文本
                Dim 前导文本初始位置_ As Integer = 原文本_.IndexOf(前导文本_)
                Dim 前导文本结束位置_ As Integer = 前导文本初始位置_ + Len(前导文本_)
                Dim 后导文本初始位置_ As Integer
                Dim 后导文本结束位置_ As Integer
                Dim 中间文本长度_ As Integer
                If 前导文本_.Contains(后导文本_) Or 后导文本_.Contains(前导文本_) Then '判断前导文本和后导文本是否互相包含
                    后导文本初始位置_ = 原文本_.IndexOf(后导文本_, 前导文本结束位置_)
                ElseIf 原文本_.Substring(0, 前导文本结束位置_).Contains(后导文本_) Then '判断后导文本是否在前导文本的前面
                    后导文本初始位置_ = 原文本_.IndexOf(后导文本_, 前导文本结束位置_)
                Else
                    后导文本初始位置_ = 原文本_.IndexOf(后导文本_)
                End If
                后导文本结束位置_ = 后导文本初始位置_ + Len(后导文本_)
                中间文本长度_ = 后导文本初始位置_ - 前导文本结束位置_
                Dim 是否都包含 As Boolean = True
                If 中间文本长度_ >= 0 Then
                    '判断前导文本和后导文本是否都在原文本之内
                    If 原文本_.Contains(前导文本_) = False Then
                        是否都包含 = False
                    End If
                    If 原文本_.Contains(后导文本_) = False Then
                        是否都包含 = False
                    End If
                    If 是否都包含 = True Then
                        Return (原文本_.Substring(前导文本结束位置_, 中间文本长度_))
                    Else
                        Return ("")
                    End If
                Else
                    Return ("")
                End If
            Catch ex As Exception
                Return ex.Message
            End Try
        End Function
        ''' <summary>
        ''' 提供者：百度AI
        ''' </summary>
        <Runtime.CompilerServices.Extension()>
        Public Function 提取所有中间文本1(原文本 As String, 前导文本 As String, 后导文本 As String) As List(Of String)
            Dim matches As MatchCollection = Regex.Matches(原文本, 前导文本 + "(.*?)" & 后导文本)
            Dim result As New List(Of String)
            For Each match As Match In matches
                If match.Groups.Count > 1 Then
                    result.Add(match.Groups(1).Value)
                End If
            Next
            Return result
        End Function
        ''' <summary>
        ''' 提供者：ChatGPT
        ''' </summary>
        <Runtime.CompilerServices.Extension()>
        Function 提取所有中间文本2(ByVal inputString As String, ByVal leadingText As String, ByVal trailingText As String) As List(Of String)
            Dim resultList As New List(Of String)()
            Dim startPos As Integer = 0
            While True
                ' 查找前导文本的位置
                Dim leadPos As Integer = inputString.IndexOf(leadingText, startPos)
                If leadPos = -1 Then Exit While ' 如果找不到，退出循环
                leadPos += leadingText.Length ' 调整位置到前导文本之后
                ' 查找后导文本的位置
                Dim trailPos As Integer = inputString.IndexOf(trailingText, leadPos)
                If trailPos = -1 Then Exit While ' 如果找不到，退出循环
                ' 提取前导和后导文本之间的字符串
                Dim extractedString As String = inputString.Substring(leadPos, trailPos - leadPos)
                ' 添加到结果列表中
                resultList.Add(extractedString)
                ' 更新开始位置，为下一次查找做准备
                startPos = trailPos + trailingText.Length
            End While
            Return resultList
        End Function

        Public Function 判断三个值是否相同(value1 As Object, value2 As Object, value3 As Object) As Boolean
            Return value1.Equals(value2) AndAlso value2.Equals(value3)
        End Function
    End Module
End Namespace