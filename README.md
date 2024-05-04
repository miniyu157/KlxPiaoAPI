# KlxPiaoAPI
提供常用函数、界面美化和一些控件

![此处输入图片的描述][1]

> 修改继承以使用KlxPiaoAPI

    Partial Class Form1
    Inherits KlxPiaoForm
    
> 加载/导出主题方法

    导出主题文件(文件路径 As String)
    加载主题文件(文件路径或资源文件 As String)

>一些常用函数

    应用本地字体(文件路径 As String, 控件 As Control)
    
    Dim 设置样式 As New 设置WindowState With {
        .应用于 = Me,
        .样式 = FormWindowState.Maximized,
        .启用动画 = True
    }

    复制主题(从 As KlxPiaoForm, 到 As KlxPiaoForm)
    
![此处输入图片的描述][2]

> 拖进工具箱即可使用KlxPiao的控件

![此处输入图片的描述][3]


![此处输入图片的描述][4]


![此处输入图片的描述][5]


  [1]: https://github.com/miniyu157/KlxPiaoAPI/blob/main/%E6%88%AA%E5%9B%BE/1.png
  [2]: https://github.com/miniyu157/KlxPiaoAPI/blob/main/%E6%88%AA%E5%9B%BE/2.png
  [3]: https://github.com/miniyu157/KlxPiaoAPI/blob/main/%E6%88%AA%E5%9B%BE/3.png
  [4]: https://github.com/miniyu157/KlxPiaoAPI/blob/main/%E6%88%AA%E5%9B%BE/4.png
  [5]: https://github.com/miniyu157/KlxPiaoAPI/blob/main/%E6%88%AA%E5%9B%BE/5.png
