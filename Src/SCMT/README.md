# 概览
## 项目综述
SCMT主界面以及核心功能

## 项目划分以及职责
| 项目名称 | 综述 | 职能 | 依赖项目
| --- | --- | --- | --- |
| SCMTOperationCore | LMT基础业务入口 | 1、实现Tcp与Udp连接<br> 2、定义基站操作网元 <br>3、定义基站所有消息类型 | 无
| UICore | 界面与控件 | 1、界面窗体风格<br>2、基础控件（例如按钮、对象树、页签等） | 无
| LineChart | 笛卡尔坐标系线图 |  1、Y轴坐标可定制的实时滚动坐标系<br>2、可查看详细信息的坐标系 | 无

## 开发环境说明
首先需要准备以下软件，才能够正常查看库中所有的文件
- Visual Studio 2015
    - .NetFrameWork4.5
    - WindowsSDK
- XMind
- EnterpriseArchitect
- Mockplus
