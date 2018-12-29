using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using NetPlan;
using SCMTMainWindow.Property;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.PropertyGrid;
using System.Reflection;
using System.Reflection.Emit;

using CommonUtility;

namespace SCMTMainWindow.View
{
    public partial class DesignerCanvas : Canvas
    {
        private Point? rubberbandSelectionStartPoint = null;

        //规划中的小区
        //public List<int> g_cellPlaning = new List<int>();

        //private Dictionary<string, int> dicRRU = new Dictionary<string, int>();
        public int nRRUNo = -1;
        public int nrHUBNo = 199;
        public int nAntennaNo = -1;

        //全局变量，将网元名称和网元的属性表格对应
        //public Dictionary<string, Grid> g_GridForNet = new Dictionary<string, Grid>();
        //全局变量，保存当前选中设备的信息
        public DevAttributeInfo g_nowDevAttr = null;

        //属性修改的时候，保存之前的值，进行判断
        private string strOldAttr;

        //全局字典，保存设备名称和 index 索引，删除的时候需要根据 index 获取设备信息进行删除
        public Dictionary<EnumDevType, Dictionary<string, string>> g_AllDevInfo = new Dictionary<EnumDevType, Dictionary<string, string>>();
        //保存界面上的属性表格
        public Grid gridProperty;
		public TextBlock noteTB;

        private SelectionService selectionService;
        internal SelectionService SelectionService
        {
            get
            {
                if (selectionService == null)
                    selectionService = new SelectionService(this);

                return selectionService;
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Source == this)
            {
                // in case that this click is the start of a 
                // drag operation we cache the start point
                this.rubberbandSelectionStartPoint = new Point?(e.GetPosition(this));

                // if you click directly on the canvas all 
                // selected items are 'de-selected'
                SelectionService.ClearSelection();
                Focus();
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                this.rubberbandSelectionStartPoint = null;

            // ... but if mouse button is pressed and start
            // point value is set we do have one
            if (this.rubberbandSelectionStartPoint.HasValue)
            {
                // create rubberband adorner
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    RubberbandAdorner adorner = new RubberbandAdorner(this, rubberbandSelectionStartPoint);
                    if (adorner != null)
                    {
                        adornerLayer.Add(adorner);
                    }
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// 拖拽事件，处理各种被拖拽过来的器件信息
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);
            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;

            if (dragObject != null && !String.IsNullOrEmpty(dragObject.Xaml))
            {
                Point position = e.GetPosition(this);
                if (dragObject.Xaml.Contains("Text=\"RRU\""))
                {
                    if(!CreateRRU(dragObject, position))
                    {
                        return;
                    }
                }else if(dragObject.Xaml.Contains("Text=\"rHUB\""))
                {
                    CreaterHUB(dragObject, position);
                }
                else if(dragObject.Xaml.Contains("Text=\"Antenna\""))
                {
                    CreateAntenna(dragObject, position);
                }
                else
                {
                    MessageBox.Show("Nothing");
                    return;
                }

                e.Handled = true;
            }
        }

        #region 创建RRU

        /// <summary>
        /// 构造 RRU 网元
        /// </summary>
        /// <param name="dragObject"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool CreateRRU(DragObject dragObject, Point position)
        {
            //弹出RRU属性对话框，选择RRU的相关类型以及要添加的数量
            ChooseRRUType dlgChooseRRU = new ChooseRRUType();
            dlgChooseRRU.ShowDialog();

            if (!dlgChooseRRU.bOK)
            {
                return false;    //选择取消之后，不进行拖拽
            }
            int nMaxRRUPath = dlgChooseRRU.nMaxRRUPath;         //RRU的最大通道数
            int nRRUNumber = dlgChooseRRU.nRRUNumber;           //需要添加的RRU的数量
            string strXAML = string.Empty;                                        //解析xml文件
            Size newSize;                                                                  //根据不同的通道数，确定不同的RRU的大小
            string strRRUName = dlgChooseRRU.strRRUName;
            strXAML = GetElementFromXAML(nMaxRRUPath, strXAML, out newSize);

            dragObject.DesiredSize = newSize;            //这个是之前代码留下的，实际上可以修改一下，这里并没有太大的意义，以后载重构吧，ByMayi 2018-0927

            //根据输入的个数，添加多个网元
            for (int i = 0; i < nRRUNumber; i++)
            {
                DesignerItem newItem = new DesignerItem();

                string strXAML1 = strXAML;
                string strRRUFullName = string.Empty;

                strRRUFullName = string.Format("{0}-{1}", strRRUName, ++nRRUNo);
                string strInstedName = string.Format("Text=\"{0}\"", strRRUFullName);
                if (nMaxRRUPath > 16)
                {
                    string strPortNo = string.Format("Text=\"1..{0}\"", nMaxRRUPath);
                    strXAML1 = strXAML1.Replace("Text=\"1\"", strPortNo);
                }
                strXAML1 = strXAML1.Replace("Text=\"RRU\"", strInstedName);
                Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML1)));
                newItem.Content = testContent;
                newItem.ItemName = strRRUFullName;
                newItem.NPathNumber = nMaxRRUPath;

                //添加 RRU 的时候需要给基站下发，然后获取设备信息
                var devRRUInfo = MibInfoMgr.GetInstance().AddNewRru(nRRUNo, dlgChooseRRU.nRRUTypeIndex, dlgChooseRRU.strWorkModel);

                if(devRRUInfo == null)
                {
                    MessageBox.Show("MibInfoMgr.GetInstance().AddNewRru 返回为空");
                    return false;
                }

                if(g_AllDevInfo.ContainsKey(EnumDevType.rru))
                {
                    g_AllDevInfo[EnumDevType.rru].Add(strRRUFullName, devRRUInfo.m_strOidIndex);
                }
                else
                {
                    g_AllDevInfo.Add(EnumDevType.rru, new Dictionary<string, string>());
                    g_AllDevInfo[EnumDevType.rru].Add(strRRUFullName, devRRUInfo.m_strOidIndex);
                }

                //双击 RRU 绑定小区
                newItem.MouseDoubleClick += NewItem_MouseDoubleClick;
                newItem.DevIndex = devRRUInfo.m_strOidIndex;
                newItem.DevType = EnumDevType.rru;

                //var test = NPECmdHelper.GetInstance().GetDevAttributesFromMib("rru");
                //globalDic.Add(strRRUFullName, test);
                //Type typeTest = DynamicObject.BuildTypeWithCustomAttributesOnMethod("rru", test);

                if (dragObject.DesiredSize.HasValue)
                {
                    Size desiredSize = dragObject.DesiredSize.Value;
                    newItem.Width = desiredSize.Width;
                    newItem.Height = desiredSize.Height;

                    DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2) + i * 20);
                    DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2) + i * 20);
                }
                Canvas.SetZIndex(newItem, this.Children.Count);
                this.Children.Add(newItem);
                SetConnectorDecoratorTemplate(newItem);

                this.SelectionService.SelectItem(newItem);
                newItem.Focus();

                CreateGirdForNetInfo(strRRUFullName, devRRUInfo);

                //查看 NetPlan中，是否点击连接线

                Grid ucTest = GetRootElement<Grid>(newItem, "MainGrid");
                SCMTMainWindow.View.NetPlan targetNP = GetRootElement<SCMTMainWindow.View.NetPlan>(ucTest, "");
                if (targetNP.bHiddenLineConnector)
                {
                    targetNP.HiddenConnectorDecoratorTemplate(newItem);
                }
                else
                {
                    targetNP.VisibilityConnectorDecoratorTemplate(newItem);
                }
            }

            return true;
        }

        /// <summary>
        /// 从xaml文件中获取对应的 RRU 的xaml信息，名称和大小
        /// </summary>
        /// <param name="nMaxRRUPath"></param>
        /// <param name="strXAML"></param>
        /// <param name="RRUSize"></param>
        /// <returns></returns>
        public string GetElementFromXAML(int nMaxRRUPath, string strXAML, out Size RRUSize)
        {
            Uri strUri = new Uri("pack://application:,,,/View/Resources/Stencils/NetElement.xml");
            Stream stream = Application.GetResourceStream(strUri).Stream;

            FrameworkElement el = XamlReader.Load(stream) as FrameworkElement;

            string strName = string.Empty;
            if (nMaxRRUPath == 1)
            {
                strName = "g_OnePathRRU";
                RRUSize = new Size(80, 70);
            }
            else if (nMaxRRUPath == 2)
            {
                strName = "g_TwoPathRRU";
                RRUSize = new Size(130, 70);
            }
            else if (nMaxRRUPath == 4)
            {
                strName = "g_FourPathRRU";
                RRUSize = new Size(160, 70);
            }
            else if (nMaxRRUPath == 6)
            {
                strName = "g_SixPathRRU";
                RRUSize = new Size(210, 70);
            }
            else if (nMaxRRUPath == 8)
            {
                strName = "g_EightPathRRU";
                RRUSize = new Size(260, 70);
            }
            else if (nMaxRRUPath == 16)
            {
                strName = "g_SixteenPathRRU";
                RRUSize = new Size(480, 70);
            }
            else
            {
                strName = "g_OtherPathRRU";
                RRUSize = new Size(80, 70);
            }

            Object content = el.FindName(strName) as Grid;

            strXAML = XamlWriter.Save(content);

            return strXAML;
        }
        
        /// <summary>
        /// 双击 RRU 进行小区的设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void NewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DesignerItem targetItem = sender as DesignerItem;
            //获取当前基站的IP地址
            string strIP = CSEnbHelper.GetCurEnbAddr();

            if (strIP == null || strIP == "")
            {
                MessageBox.Show("未选择基站  InitCellStatus");
                return;
            }

            List<int> cellInPlaning = new List<int>();
            for (int i = 0; i < 36; i++)
            {
                var cellStatus = NPCellOperator.GetLcStatus(i, strIP);
                if (cellStatus == LcStatus.Planning)
                {
                    if (!cellInPlaning.Contains(i))
                    {
                        cellInPlaning.Add(i);
                    }
                }
            }
            RRUpoint2Cell dlg = new RRUpoint2Cell(targetItem.NPathNumber, cellInPlaning, targetItem.DevIndex);
            dlg.ShowDialog();
        }

        #endregion

        #region 构建rHUB
        
        /// <summary>
        /// 构造 rHUB
        /// </summary>
        /// <param name="dragObject"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool CreaterHUB(DragObject dragObject, Point position)
        {
            ChooserHUBType dlg = new ChooserHUBType();
            dlg.ShowDialog();

            int nMaxrHUBPath = dlg.nRHUBType;         //rHUB 的最大通道数
            int nrHUBNumber = dlg.nRHUBNo;           //需要添加的 rHUB 的数量
            string strXAML = string.Empty;                                        //解析xml文件
            Size newSize;                                                                  //根据不同的通道数，确定不同的 rHUB 的大小
            string strRRUName = dlg.strrHUBType;
            strXAML = GetrHUBFromXML(nMaxrHUBPath, strXAML, out newSize);

            dragObject.DesiredSize = newSize;          

            //根据输入的个数，添加多个网元
            for (int i = 0; i < nrHUBNumber; i++)
            {
                DesignerItem newItem = new DesignerItem();

                string strXAML1 = strXAML;
                string strrHUBFullName = string.Empty;

                strrHUBFullName = string.Format("{0}-{1}", strRRUName, ++nrHUBNo);
                string strInstedName = string.Format("Text=\"{0}\"", strrHUBFullName);
                strXAML1 = strXAML1.Replace("Text=\"rHUB\"", strInstedName);
                Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML1)));
                newItem.Content = testContent;
                newItem.ItemName = strrHUBFullName;

                //添加 rHUB 的时候需要给基站下发，然后获取设备信息
                var devrHUBInfo = MibInfoMgr.GetInstance().AddNewRhub(nrHUBNo, dlg.strrHUBType, dlg.strWorkModel);

                if(devrHUBInfo == null)
                {
                    MessageBox.Show("MibInfoMgr.GetInstance().AddNewRhub 返回为空");
                    return false;
                }

                if(g_AllDevInfo.ContainsKey(EnumDevType.rhub))
                {
                    g_AllDevInfo[EnumDevType.rhub].Add(strrHUBFullName, devrHUBInfo.m_strOidIndex);
                }
                else
                {
                    g_AllDevInfo.Add(EnumDevType.rhub, new Dictionary<string, string>());
                    g_AllDevInfo[EnumDevType.rhub].Add(strrHUBFullName, devrHUBInfo.m_strOidIndex);
                }
                newItem.DevType = EnumDevType.rhub;
                newItem.DevIndex = devrHUBInfo.m_strOidIndex;

                if (dragObject.DesiredSize.HasValue)
                {
                    Size desiredSize = dragObject.DesiredSize.Value;
                    newItem.Width = desiredSize.Width;
                    newItem.Height = desiredSize.Height;

                    DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2) + i * 20);
                    DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2) + i * 20);
                }
                Canvas.SetZIndex(newItem, this.Children.Count);
                this.Children.Add(newItem);
                SetConnectorDecoratorTemplate(newItem);

                this.SelectionService.SelectItem(newItem);
                newItem.Focus();

                CreateGirdForNetInfo(strrHUBFullName, devrHUBInfo);

                //查看 NetPlan中，是否点击连接线
                Grid ucTest = GetRootElement<Grid>(newItem, "MainGrid");
                SCMTMainWindow.View.NetPlan targetNP = GetRootElement<SCMTMainWindow.View.NetPlan>(ucTest, "");
                if(targetNP.bHiddenLineConnector)
                {
                    targetNP.HiddenConnectorDecoratorTemplate(newItem);
                }
                else
                {
                    targetNP.VisibilityConnectorDecoratorTemplate(newItem);
                }
            }

            return true;
        }

        /// <summary>
        /// 从配置文件获取rHUB
        /// </summary>
        /// <param name="nMaxRRUPath"></param>
        /// <param name="strXAML"></param>
        /// <param name="RRUSize"></param>
        /// <returns></returns>
        public string GetrHUBFromXML(int nMaxRRUPath, string strXAML, out Size RRUSize)
        {
            Uri strUri = new Uri("pack://application:,,,/View/Resources/Stencils/NetElement.xml");
            Stream stream = Application.GetResourceStream(strUri).Stream;

            FrameworkElement el = XamlReader.Load(stream) as FrameworkElement;

            string strName = string.Empty;
            if (nMaxRRUPath == 2)
            {
                strName = "g_SingleRHUB";
                RRUSize = new Size(260, 70);
            }
            else if (nMaxRRUPath == 4)
            {
                strName = "g_TwoPathRHUB";
                RRUSize = new Size(260, 70);
			}
			else if (nMaxRRUPath == 6)
			{
				strName = "g_SixPathRHUB";
				RRUSize = new Size(260, 70);
			}
			else
            {
                strName = "g_SingleRHUB";
                RRUSize = new Size(260, 70);
            }

            Object content = el.FindName(strName) as Grid;

            strXAML = XamlWriter.Save(content);

            return strXAML;
        }

        #endregion

        #region 构建Antenna

        /// <summary>
        /// 创建天线阵
        /// </summary>
        /// <param name="dragObject"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool CreateAntenna(DragObject dragObject, Point position)
        {
            ChooseAntennaType dlg = new ChooseAntennaType();
            dlg.ShowDialog();

            if(!dlg.bOK)
            {
                return false;
            }

            AntType antInfo = dlg.currentSelectedAntType;         //RRU的最大通道数
            int nRRUNumber = dlg.nAntNo;           //需要添加的RRU的数量
            string strXAML = string.Empty;                                        //解析xml文件
            Size newSize;                                                                  //根据不同的通道数，确定不同的RRU的大小
            string strRRUName = "No:";
            strXAML = GetAntennaromXML(antInfo.antArrayNum, strXAML, out newSize);

            dragObject.DesiredSize = newSize;            //这个是之前代码留下的，实际上可以修改一下，这里并没有太大的意义，以后载重构吧，ByMayi 2018-0927

            //根据输入的个数，添加多个网元
            for (int i = 0; i < nRRUNumber; i++)
            {
                DesignerItem newItem = new DesignerItem();

                string strXAML1 = strXAML;
                string strRRUFullName = string.Empty;

                strRRUFullName = string.Format("{0}-{1}", strRRUName, ++nAntennaNo);

                if (antInfo.antArrayNum > 8)
                {
                    string strPortNo = string.Format("Text=\"1..{0}\"", antInfo.antArrayNum);
                    strXAML1 = strXAML1.Replace("Text=\"1\"", strPortNo);
                }
                string strInstedName = string.Format("Text=\"{0}\"", strRRUFullName);
                strXAML1 = strXAML1.Replace("Text=\"Antenna\"", strInstedName);
                Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML1)));
                newItem.Content = testContent;
                newItem.ItemName = strRRUFullName;
                newItem.NPathNumber = antInfo.antArrayNum;

                //添加 ant 的时候需要给基站下发，然后获取设备信息
                var devRRUInfo = MibInfoMgr.GetInstance().AddNewAnt(nAntennaNo, antInfo.antArrayNotMibVendorName, antInfo.antArrayModelName);

                if (devRRUInfo == null)
                {
                    MessageBox.Show("MibInfoMgr.GetInstance().AddNewAnt 返回为空");
                    return false;
                }

                if(g_AllDevInfo.ContainsKey(EnumDevType.ant))
                {
                    g_AllDevInfo[EnumDevType.ant].Add(strRRUFullName, devRRUInfo.m_strOidIndex);
                }
                else
                {
                    g_AllDevInfo.Add(EnumDevType.ant, new Dictionary<string, string>());
                    g_AllDevInfo[EnumDevType.ant].Add(strRRUFullName, devRRUInfo.m_strOidIndex);
                }
                newItem.DevIndex = devRRUInfo.m_strOidIndex;
                newItem.DevType = EnumDevType.ant;

                if (dragObject.DesiredSize.HasValue)
                {
                    Size desiredSize = dragObject.DesiredSize.Value;
                    newItem.Width = desiredSize.Width;
                    newItem.Height = desiredSize.Height;

                    DesignerCanvas.SetLeft(newItem, Math.Max(0, position.X - newItem.Width / 2) + i * 20);
                    DesignerCanvas.SetTop(newItem, Math.Max(0, position.Y - newItem.Height / 2) + i * 20);
                }
                Canvas.SetZIndex(newItem, this.Children.Count);
                this.Children.Add(newItem);
                SetConnectorDecoratorTemplate(newItem);

                this.SelectionService.SelectItem(newItem);
                newItem.Focus();

                CreateGirdForNetInfo(strRRUFullName, devRRUInfo);

                //查看 NetPlan中，是否点击连接线
                Grid ucTest = GetRootElement<Grid>(newItem, "MainGrid");
                SCMTMainWindow.View.NetPlan targetNP = GetRootElement<SCMTMainWindow.View.NetPlan>(ucTest, "");
                if (targetNP.bHiddenLineConnector)
                {
                    targetNP.HiddenConnectorDecoratorTemplate(newItem);
                }
                else
                {
                    targetNP.VisibilityConnectorDecoratorTemplate(newItem);
                }
            }

            return true;
        }
        public string GetAntennaromXML(int nMaxRRUPath, string strXAML, out Size RRUSize)
        {
            Uri strUri = new Uri("pack://application:,,,/View/Resources/Stencils/NetElement.xml");
            Stream stream = Application.GetResourceStream(strUri).Stream;

            FrameworkElement el = XamlReader.Load(stream) as FrameworkElement;

            string strName = string.Empty;
            if (nMaxRRUPath == 1)
            {
                strName = "g_SignalAntenna";
                RRUSize = new Size(30, 40);
            }
            else if (nMaxRRUPath == 2)
            {
                strName = "g_TwoPathAntenna";
                RRUSize = new Size(40, 40);
            }
            else if (nMaxRRUPath == 4)
            {
                strName = "g_FourPathAntenna";
                RRUSize = new Size(70, 40);
            }
            else if (nMaxRRUPath == 6)
            {
                strName = "g_SixPathAntenna";
                RRUSize = new Size(110, 40);
            }
            else if (nMaxRRUPath == 8)
            {
                strName = "g_EightPathAntenna";
                RRUSize = new Size(150, 40);
            }
            else
            {
                strName = "g_OtherAntenna";
                RRUSize = new Size(40, 40);
            }

            Object content = el.FindName(strName) as Grid;

            strXAML = XamlWriter.Save(content);

            return strXAML;
        }

        #endregion

        //尝试获取节点的根元素    根据网上的代码，查询一个元素的父节点的父节点。。。通过递归查找到 Name 为
        //某个值的元素
        public T GetRootElement<T>(DependencyObject item, string strName) where T : FrameworkElement
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);

            while(parent != null)
            {
                if(parent is T && ((T)parent).Name == strName)
                {
                    return (T)parent;
                }

                parent = VisualTreeHelper.GetParent(parent);
            }

            return null;
        }

        //尝试获取元素的子节点
        public T GetChildrenElement<T>(DependencyObject item, string strName) where T : FrameworkElement
        {
            DependencyObject child = null;
            T grandChild = null;

            int n = VisualTreeHelper.GetChildrenCount(item);

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(item); i++)
            {
                child = VisualTreeHelper.GetChild(item, i);

                if(child is T && ((T)child).Name == strName)
                {
                    return (T)child;
                }
                else
                {
                    grandChild = GetChildrenElement<T>(child, strName);
                    if(grandChild != null)
                    {
                        return grandChild;
                    }
                }
            }

            return null;
        }


        /// <summary>
        /// 为网元创建属性表格
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="mibInfo"></param>
        public void CreateGirdForNetInfo(string strName, DevAttributeInfo mibInfo)
        {
            Grid grid = new Grid();

            //创建两列，第一列显示属性名称Name，第二列显示属性值Value
            ColumnDefinition columnName = new ColumnDefinition();
            ColumnDefinition columnValue = new ColumnDefinition();
            ColumnDefinition columnSplit = new ColumnDefinition();
            columnSplit.Width = GridLength.Auto;
            columnName.Width = new GridLength(80);
            grid.ColumnDefinitions.Add(columnName);
            grid.ColumnDefinitions.Add(columnSplit);                     //分隔条
            grid.ColumnDefinitions.Add(columnValue);

            GridSplitter gridSplit = new GridSplitter();
            gridSplit.Width = 5;
            gridSplit.VerticalAlignment = VerticalAlignment.Stretch;
            gridSplit.ResizeBehavior = GridResizeBehavior.PreviousAndNext;
            gridSplit.Background = new SolidColorBrush(Colors.LightGray);
            grid.Children.Add(gridSplit);
            Grid.SetColumn(gridSplit, 1);
            Grid.SetRow(gridSplit, 1);

            //创建一个标题行
            RowDefinition rowTitle = new RowDefinition();
            rowTitle.Height = new GridLength(27);
            grid.RowDefinitions.Add(rowTitle);
            TextBlock txtTitile = new TextBlock();
            txtTitile.Text = strName;
            grid.Children.Add(txtTitile);
            Grid.SetRow(txtTitile, 0);
            Grid.SetColumnSpan(txtTitile, 3);

            if (mibInfo == null)
            {
                return;
            }

            //根据传过来的值，添加行，每一行代表一个属性，并根据不同的类型添加不同的控件
            int nRow = 0;
            
            foreach(var item in mibInfo.m_mapAttributes)
            {
                //不可见的属性
                if (!item.Value.m_bVisible)
                {
                    continue;
                }
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(27);
                grid.RowDefinitions.Add(row);


                //添加属性名称的控件
                TextBlock txtName = new TextBlock();
                txtName.Margin = new Thickness(1);
                txtName.Text = item.Value.mibAttri.childNameCh;
				txtName.MouseLeftButtonDown += TxtName_MouseLeftButtonDown;
                grid.Children.Add(txtName);
                Grid.SetColumn(txtName, 0);
                Grid.SetRow(txtName, nRow + 1);

                //根据不同的类型添加属性值控件
                switch (item.Value.mibAttri.OMType)
                {
                    case "enum":
                        ComboBox cbValue = new ComboBox();
                        cbValue.Margin = new Thickness(1);
                        cbValue.Height = 25;

                        var valueInfo = MibStringHelper.SplitManageValue(item.Value.mibAttri.mibValAllList);

                        for (int j = 0; j < valueInfo.Count; j++)
                        {
                            cbValue.Items.Add(valueInfo.ElementAt(j).Value);
                        }

                        grid.Children.Add(cbValue);
                        Grid.SetColumn(cbValue, 2);
                        Grid.SetRow(cbValue, nRow + 1);

                        var strItem = mibInfo.GetNeedUpdateValue(item.Key, false);
                        if(strItem != null)
                        {
	                        if (strItem.Contains('|'))
	                        {
		                        strItem = strItem.Substring(strItem.IndexOf('|') + 1);
	                        }

                            if (cbValue.Items.Contains(strItem))
                            {
                                cbValue.SelectedIndex = cbValue.Items.IndexOf(strItem);
                            }
                        }
                        if (item.Value.m_bReadOnly)
                        {
                            cbValue.IsReadOnly = true;
                            cbValue.IsEnabled = false;
                        }
                        else
                        {
                            //属性是可以修改的，则创建选择改变事件
                            cbValue.SelectionChanged += CbValue_SelectionChanged;
                        }
                        break;
                    default:
                        TextBox txtValue = new TextBox();
                        txtName.Margin = new Thickness(1);
                        txtName.Height = 25;

                        var strItem2Text = mibInfo.GetNeedUpdateValue(item.Key, false);
                        if (strItem2Text != null)
                        {
                            txtValue.Text = strItem2Text;
                        }
                        grid.Children.Add(txtValue);
                        Grid.SetColumn(txtValue, 2);
                        Grid.SetRow(txtValue, nRow + 1);
                        if (item.Value.m_bReadOnly)
                        {
                            txtValue.IsReadOnly = true;
                        }
                        else
                        {
                            txtValue.GotFocus += TxtValue_GotFocus;
                            txtValue.LostFocus += TxtValue_LostFocus;
                        }
                        break;
                }
                nRow++;
            }

            if (mibInfo.m_mapAttributes.Count != 0)
            {
                Grid.SetRowSpan(gridSplit, mibInfo.m_mapAttributes.Count);
            }
			
            Grid ucTest = GetRootElement<Grid>(this, "MainGrid");

            if (ucTest != null)
            {
                gridProperty = GetChildrenElement<Grid>(ucTest, "gridProperty");
                if(gridProperty.Children.Count != 0)
                {
                    gridProperty.Children.Clear();
                }
                gridProperty.Children.Add(grid);
            }
        }

		private void TxtName_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			TextBlock targetTB = sender as TextBlock;

			Grid ucTest = GetRootElement<Grid>(this, "MainGrid");

			if (ucTest != null)
			{
				noteTB = GetChildrenElement<TextBlock>(ucTest, "noteText");
				if (noteTB != null)
				{
					noteTB.Text = "";
					foreach (var item in g_nowDevAttr.m_mapAttributes)
					{
						if (item.Value.mibAttri.childNameCh == targetTB.Text)
						{
							noteTB.Text = item.Value.mibAttri.childNameCh + "\n" + item.Value.mibAttri.detailDesc;
						}
					}
				}
			}
		}

		/// <summary>
		/// 文本框得到焦点的时候，保存当前的属性值
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TxtValue_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox targetText = sender as TextBox;
            strOldAttr = targetText.Text;
        }

        /// <summary>
        /// 文本框失去焦点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtValue_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox targetItem = sender as TextBox;
            Grid grid = targetItem.Parent as Grid;
            TextBlock targetText;

            for (int i = 0; i < grid.Children.Count; i++)
            {
                if (grid.Children[i] == targetItem)
                {
                    targetText = grid.Children[i - 1] as TextBlock;
                    if (targetText != null)
                    {
                        foreach(var item in g_nowDevAttr.m_mapAttributes)
                        {
                            if(item.Value.mibAttri.childNameCh == targetText.Text)
                            {
                                if(strOldAttr != targetItem.Text)
                                {
                                    if(!g_nowDevAttr.SetDevAttributeValue(item.Key, targetItem.Text))
                                    {
                                        MessageBox.Show("修改失败");
                                    }
                                }
                            }
                        }

                        return;
                    }
                }
            }

        }

        /// <summary>
        /// 下拉框选择改变事件，改变之后需要修改相关属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbValue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox targetItem = sender as ComboBox;
            Grid grid = targetItem.Parent as Grid;
            TextBlock targetText;

            for(int i = 0; i < grid.Children.Count; i++)
            {
                if(grid.Children[i] == targetItem)
                {
                    targetText = grid.Children[i - 1] as TextBlock;
                    if (targetText != null)
                    {
                        foreach (var item in g_nowDevAttr.m_mapAttributes)
                        {
                            if (item.Value.mibAttri.childNameCh == targetText.Text)
                            {
                                if (!g_nowDevAttr.SetDevAttributeValue(item.Key, targetItem.SelectedItem.ToString()))
                                {
                                    MessageBox.Show("修改失败");
                                }
                            }
                        }

                        return;
                    }
                }
            }

        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size size = new Size();

            foreach (UIElement element in this.InternalChildren)
            {
                double left = Canvas.GetLeft(element);
                double top = Canvas.GetTop(element);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : top;

                //measure desired size for each child
                element.Measure(constraint);

                Size desiredSize = element.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    size.Width = Math.Max(size.Width, left + desiredSize.Width);
                    size.Height = Math.Max(size.Height, top + desiredSize.Height);
                }
            }
            // add margin 
            size.Width += 10;
            size.Height += 10;
            return size;
        }

        public void SetConnectorDecoratorTemplate(DesignerItem item)
        {
            if (item.ApplyTemplate() && item.Content is UIElement)
            {
                ControlTemplate template = DesignerItem.GetConnectorDecoratorTemplate(item.Content as UIElement);
                Control decorator = item.Template.FindName("PART_ConnectorDecorator", item) as Control;
                if (decorator != null && template != null)
                    decorator.Template = template;
            }
        }
    }
}
