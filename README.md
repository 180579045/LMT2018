# SCMT多站连接分支修改说明
撤退之前，最后一个负责修改的特性，以下是所有涉及到的修改内容和一些详细的说明，请负责归并的同学关注

## 新增依赖：
- ChromeTabs<br>
多站连接的核心，以标签页的形式呈现在程序的最上边，与Chrome浏览器的呈现形式一致，故起名ChromeTabs，该依赖通过工程方式放在了解决方案之中，**在路径：UI/ChrmoeTabs 中**
- MvvmLight<br>
一个轻量级MVVM框架，解耦和View层即XAML与后端处理逻辑，方便团队成员内部的协作，具体使用到的版本为：<br>
    - CommonServiceLocator.2.0.2
    - MvvmLight.5.4.1.1
    - MvvmLightLibs.5.4.1.1

用到的dll分别为
- packages\MvvmLightLibs.5.4.1.1\lib\net45\GalaSoft.MvvmLight.dll
- packages\MvvmLightLibs.5.4.1.1\lib\net45\GalaSoft.MvvmLight.Extras.dll
- packages\MvvmLightLibs.5.4.1.1\lib\net45\GalaSoft.MvvmLight.Platform.dll
- packages\MvvmLightLibs.5.4.1.1\lib\net45\System.Windows.Interactivity.dll
- packages\CommonServiceLocator.2.0.2\lib\net45\CommonServiceLocator.dll

## 主要修改内容

### 主窗口界面
1. 将主窗口界面抽象成了一个UserControl，并在App.xaml中应用为了DataTemplate，以便后续所有页签的使用<br>










