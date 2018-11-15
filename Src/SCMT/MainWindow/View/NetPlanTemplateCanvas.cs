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
using Microsoft.Win32;

namespace SCMTMainWindow.View
{
    public partial class NetPlanTemplateCanvas : DesignerCanvas
    {
        private int nrruNo = 0;
        private int nrHUBNo = 0;
        private int nAntNo = 0;

        private List<TemBoardInfo> ListTemBoardInfo = new List<TemBoardInfo>();
        private List<TemRRUInfo> ListTemRRUInfo = new List<TemRRUInfo>();
        private List<TemrHUBInfo> ListTemrHUBInfo = new List<TemrHUBInfo>();
        private List<TemAntInfo> ListTemAntInfo = new List<TemAntInfo>();

        //全局变量，将网元名称和网元信息结构体对应
        private Dictionary<string, List<MibLeafNodeInfo>> globalDic = new Dictionary<string, List<MibLeafNodeInfo>>();
        //全局变量，将网元名称和网元的属性表格对应
        public Dictionary<string, Grid> g_GridForTem = new Dictionary<string, Grid>();

        //全局字典，保存设备名称和 index 索引，删除的时候需要根据 index 获取设备信息进行删除
        public Dictionary<EnumDevType, Dictionary<string, string>> g_TemAllDevInfo = new Dictionary<EnumDevType, Dictionary<string, string>>();
        //保存界面上的属性表格
        public Grid gridTemProperty;

        /// <summary>
        /// 拖拽事件，处理各种被拖拽过来的器件信息
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrop(DragEventArgs e)
        {
            //base.OnDrop(e);

            DragObject dragObject = e.Data.GetData(typeof(DragObject)) as DragObject;

            if (dragObject != null && !String.IsNullOrEmpty(dragObject.Xaml))
            {
                Point position = e.GetPosition(this);
                if (dragObject.Xaml.Contains("Text=\"RRU\""))
                {
                    if (!CreateRRU(dragObject, position))
                    {
                        return;
                    }
                }
                else if (dragObject.Xaml.Contains("Text=\"rHUB\""))
                {
                    CreaterHUB(dragObject, position);
                }
                else if (dragObject.Xaml.Contains("Text=\"Antenna\""))
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
            int nMaxRRUPath = dlgChooseRRU.nMaxRRUPath; //RRU的最大通道数
            int nRRUNumber = dlgChooseRRU.nRRUNumber;   //需要添加的RRU的数量
            string strXAML = string.Empty;              //解析xml文件
            Size newSize;                               //根据不同的通道数，确定不同的RRU的大小
            string strRRUName = dlgChooseRRU.strRRUName;
            strXAML = GetElementFromXAML(nMaxRRUPath, strXAML, out newSize);

            dragObject.DesiredSize = newSize;
            

            //根据输入的个数，添加多个网元
            for (int i = 0; i < nRRUNumber; i++)
            {
                DesignerItem newItem = new DesignerItem();

                string strXAML1 = strXAML;
                string strRRUFullName = string.Empty;             

                strRRUFullName = string.Format("{0}-{1}", strRRUName, ++nrruNo);
                string strInstedName = string.Format("Text=\"{0}\"", strRRUFullName);
                strXAML1 = strXAML1.Replace("Text=\"RRU\"", strInstedName);
                Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML1)));
                newItem.Content = testContent;
                newItem.ItemName = strRRUFullName;
                newItem.NPathNumber = nMaxRRUPath;

                TemRRUInfo rruTem = new TemRRUInfo();
                rruTem.rruId = nrruNo;
                rruTem.rruName = dlgChooseRRU.strRRUName;
                rruTem.rruPathNum = dlgChooseRRU.nMaxRRUPath;
                rruTem.rruWorkMode = dlgChooseRRU.strWorkModel;
                rruTem.rruIndex = dlgChooseRRU.nRRUTypeIndex;
                ListTemRRUInfo.Add(rruTem);

                //添加 RRU              
                var devInfo = new DevAttributeInfo(EnumDevType.rru, nrruNo);

                if (devInfo == null)
                    return false;

                if (g_TemAllDevInfo.ContainsKey(EnumDevType.rru))
                {
                    g_TemAllDevInfo[EnumDevType.rru].Add(strRRUFullName, devInfo.m_strOidIndex);
                }
                else
                {
                    g_TemAllDevInfo.Add(EnumDevType.rru, new Dictionary<string, string>());
                    g_TemAllDevInfo[EnumDevType.rru].Add(strRRUFullName, devInfo.m_strOidIndex);
                }
                newItem.DevIndex = devInfo.m_strOidIndex;
                newItem.DevType = EnumDevType.rru;

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

                Grid ucTest = GetRootElement<Grid>(newItem, "MainGrid");

                if (ucTest != null)
                {
                    gridTemProperty = GetChildrenElement<Grid>(ucTest, "gridProperty");
                    CreateTemGirdForNetInfo(strRRUFullName, devInfo);
                    gridTemProperty.Children.Clear();
                    gridTemProperty.Children.Add(g_GridForTem[strRRUFullName]);
                }
            }

            return true;
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

            int nMaxrHUBPath = dlg.nRHUBType;        //rHUB 的最大通道数
            int nrHUBNumber = dlg.nRHUBNo;           //需要添加的 rHUB 的数量
            string strXAML = string.Empty;           //解析xml文件
            Size newSize;                            //根据不同的通道数，确定不同的 rHUB 的大小
            string strRRUName = "rHUB";
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

                TemrHUBInfo hubInfo = new TemrHUBInfo();
                hubInfo.rHUBId = nrHUBNo;
                hubInfo.rHUBName = dlg.strrHUBType; ;
                hubInfo.rHUBPathNum = dlg.nRHUBType;
                hubInfo.rHUBWorkMode = dlg.strWorkModel;
                ListTemrHUBInfo.Add(hubInfo);

                //添加 rHUB 
                var devrHuB = new RHubDevAttri(nrHUBNo, dlg.strrHUBType);

                if (devrHuB == null)
                    return false;

                if (g_TemAllDevInfo.ContainsKey(EnumDevType.rhub))
                {
                    g_TemAllDevInfo[EnumDevType.rhub].Add(strrHUBFullName, devrHuB.m_strOidIndex);
                }
                else
                {
                    g_TemAllDevInfo.Add(EnumDevType.rhub, new Dictionary<string, string>());
                    g_TemAllDevInfo[EnumDevType.rhub].Add(strrHUBFullName, devrHuB.m_strOidIndex);
                }
                newItem.DevType = EnumDevType.rhub;
                newItem.DevIndex = devrHuB.m_strOidIndex;

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

                Grid ucTest = GetRootElement<Grid>(newItem, "MainGrid");

                if (ucTest != null)
                {
                    gridTemProperty = GetChildrenElement<Grid>(ucTest, "gridProperty");
                    CreateTemGirdForNetInfo(strrHUBFullName, devrHuB);
                    gridTemProperty.Children.Clear();
                    gridTemProperty.Children.Add(g_GridForTem[strrHUBFullName]);
                }
            }

            return true;
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

            int nAntNumber = 1;                    //需要添加的Ant的数量
            string strXAML = string.Empty;         //解析xml文件
            Size newSize;                          //根据不同的通道数，确定不同的RRU的大小
            string strAntName = "No:";
            strXAML = GetAntennaromXML(dlg.nAntennaType, strXAML, out newSize);

            dragObject.DesiredSize = newSize;      

            //根据输入的个数，添加多个网元
            for (int i = 0; i < nAntNumber; i++)
            {
                DesignerItem newItem = new DesignerItem();

                string strXAML1 = strXAML;
                string strAntFullName = string.Empty;

                strAntFullName = string.Format("{0}-{1}", strAntName, ++nAntNo);
                string strInstedName = string.Format("Text=\"{0}\"", strAntFullName);
                strXAML1 = strXAML1.Replace("Text=\"Antenna\"", strInstedName);
                Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML1)));
                newItem.Content = testContent;
                newItem.ItemName = strAntFullName;

                var temAntInfo = new TemAntInfo();
                temAntInfo.antId = nAntNo;
                temAntInfo.antName = strAntFullName;
                temAntInfo.antArrayNum = dlg.nAntennaType;
                temAntInfo.antWorkMode = "正常模式";
                ListTemAntInfo.Add(temAntInfo);

                //添加天线
                var devAntInfo = new DevAttributeInfo(EnumDevType.ant, nAntNo);

                if (devAntInfo == null)
                    return false;

                if (g_TemAllDevInfo.ContainsKey(EnumDevType.ant))
                {
                    g_TemAllDevInfo[EnumDevType.ant].Add(strAntFullName, devAntInfo.m_strOidIndex);
                }
                else
                {
                    g_TemAllDevInfo.Add(EnumDevType.ant, new Dictionary<string, string>());
                    g_TemAllDevInfo[EnumDevType.ant].Add(strAntFullName, devAntInfo.m_strOidIndex);
                }
                newItem.DevIndex = devAntInfo.m_strOidIndex;
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

                Grid ucTest = GetRootElement<Grid>(newItem, "MainGrid");

                if (ucTest != null)
                {
                    gridTemProperty = GetChildrenElement<Grid>(ucTest, "gridProperty");
                    CreateTemGirdForNetInfo(strAntFullName, devAntInfo);
                    gridTemProperty.Children.Clear();
                    gridTemProperty.Children.Add(g_GridForTem[strAntFullName]);
                }
            }

            return true;
        }
        #endregion

        public void CreateTemGirdForNetInfo(string strName, DevAttributeInfo mibInfo)
        {
            if (g_GridForTem.Keys.Contains(strName))
            {
                return;
            }

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
                g_GridForTem.Add(strName, grid);
                return;
            }

            //根据传过来的值，添加行，每一行代表一个属性，并根据不同的类型添加不同的控件
            int nRow = 0;

            foreach (var item in mibInfo.m_mapAttributes)
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

                        var valueInfo = MibStringHelper.SplitManageValue(item.Value.mibAttri.managerValueRange);

                        for (int j = 0; j < valueInfo.Count; j++)
                        {
                            cbValue.Items.Add(valueInfo.ElementAt(j).Value);
                        }

                        //string[] strValue = mibInfo[i].mibAttri.defaultValue.Split(':');
                        //string strDefaultInfo = valueInfo[int.Parse(strValue[0])];
                        //cbValue.SelectedItem = strDefaultInfo;

                        grid.Children.Add(cbValue);
                        Grid.SetColumn(cbValue, 2);
                        Grid.SetRow(cbValue, nRow + 1);

                        if (cbValue.Items.Contains(item.Value.m_strOriginValue))
                        {
                            cbValue.SelectedIndex = cbValue.Items.IndexOf(item.Value.m_strOriginValue);
                        }
                        if (item.Value.m_bReadOnly)
                        {
                            cbValue.IsReadOnly = true;
                            cbValue.IsEnabled = false;
                        }
                        else
                        {
                            //属性是可以修改的，则创建选择改变事件
                            //cbValue.SelectionChanged += CbValue_SelectionChanged;
                        }
                        break;
                    default:
                        TextBox txtValue = new TextBox();
                        txtName.Margin = new Thickness(1);
                        txtName.Height = 25;
                        txtValue.Text = item.Value.m_strOriginValue;
                        grid.Children.Add(txtValue);
                        Grid.SetColumn(txtValue, 2);
                        Grid.SetRow(txtValue, nRow + 1);
                        if (item.Value.m_bReadOnly)
                        {
                            txtValue.IsReadOnly = true;
                        }
                        break;
                }
                nRow++;
            }

            if (mibInfo.m_mapAttributes.Count != 0)
            {
                Grid.SetRowSpan(gridSplit, mibInfo.m_mapAttributes.Count);
            }

            if (!g_GridForTem.ContainsKey(strName))
                g_GridForTem.Add(strName, grid);
        }

        /// <summary>
        /// 点击导出保存模板页面信息到文件
        /// </summary>
        public void CreateTempalteToFile()
        {
            NPTemplate npTem = new NPTemplate();
            if(ListTemBoardInfo.Count > 0)
            {
                foreach (TemBoardInfo info in ListTemBoardInfo)
                {
                    npTem.temBoardInfo.Add(info);
                }
            }

            if (ListTemRRUInfo.Count > 0)
            {
                foreach (TemRRUInfo info in ListTemRRUInfo)
                {
                    npTem.temRruInfo.Add(info);
                }
            }

            if (ListTemrHUBInfo.Count > 0)
            {
                foreach (TemrHUBInfo info in ListTemrHUBInfo)
                {
                    npTem.temrHubInfo.Add(info);
                }
            }

            if (ListTemAntInfo.Count > 0)
            {
                foreach (TemAntInfo info in ListTemAntInfo)
                {
                    npTem.temAntInfo.Add(info);
                }
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            sfd.RestoreDirectory = true;
            sfd.Filter = "Files (*.json)|*.json|All Files (*.*)|*.*";
            bool? result = sfd.ShowDialog();
            
            if (result == true)
            {
                try
                {                
                    var filepath = sfd.FileName.ToString();
                    var filename = filepath.Substring(filepath.LastIndexOf("\\") + 1);
                    npTem.templateName = filename;
                    NetPlanTemplateInfo.GetInstance().SaveNetPlanTemplate(npTem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace, ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// 清除模板页面内容
        /// </summary>
        public void ClearTemInfo()
        {
            g_TemAllDevInfo.Clear();
            g_GridForTem.Clear();
            this.Children.Clear();

            ListTemBoardInfo.Clear();
            ListTemRRUInfo.Clear();
            ListTemrHUBInfo.Clear();
            ListTemAntInfo.Clear();

            nrruNo = 0;
            nrHUBNo = 0;
            nAntNo = 0;
    }

        //双击模板，根据模板文件内容绘制模板
        public void DrawTemplate(NPTemplate npTempalte)
        {
            double PosX, PosY;
            int i = 0;
            //绘制板卡
            if (npTempalte.temBoardInfo.Count > 0)
            {

            }

            //绘制RRU
            if (npTempalte.temRruInfo.Count > 0)
            {
                string strXAML = string.Empty; //解析xml文件
                Size newSize;                  //根据不同的通道数，确定不同的RRU的大小               

                foreach (TemRRUInfo rruInfo in npTempalte.temRruInfo)
                {
                    strXAML = GetElementFromXAML(rruInfo.rruPathNum, strXAML, out newSize);

                    DesignerItem newItem = new DesignerItem();

                    string strXAML1 = strXAML;
                    string strRRUFullName = string.Empty;

                    strRRUFullName = string.Format("{0}-{1}", rruInfo.rruName, rruInfo.rruId);
                    string strInstedName = string.Format("Text=\"{0}\"", strRRUFullName);
                    strXAML1 = strXAML1.Replace("Text=\"RRU\"", strInstedName);
                    Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML1)));
                    newItem.Content = testContent;
                    newItem.ItemName = strRRUFullName;
                    newItem.NPathNumber = rruInfo.rruPathNum;

                    //添加 RRU              
                    var devInfo = new DevAttributeInfo(EnumDevType.rru, rruInfo.rruId);

                    if (devInfo == null)
                        continue;

                    if (g_TemAllDevInfo.ContainsKey(EnumDevType.rru))
                    {
                        g_TemAllDevInfo[EnumDevType.rru].Add(strRRUFullName, devInfo.m_strOidIndex);
                    }
                    else
                    {
                        g_TemAllDevInfo.Add(EnumDevType.rru, new Dictionary<string, string>());
                        g_TemAllDevInfo[EnumDevType.rru].Add(strRRUFullName, devInfo.m_strOidIndex);
                    }
                    newItem.DevIndex = devInfo.m_strOidIndex;
                    newItem.DevType = EnumDevType.rru;
       
                    newItem.Width = newSize.Width;
                    newItem.Height = newSize.Height;

                    DesignerCanvas.SetLeft(newItem, Math.Max(0, this.ActualWidth/3 + i * 20));
                    DesignerCanvas.SetTop(newItem, Math.Max(0, this.ActualHeight/3 + i * 30));

                    Canvas.SetZIndex(newItem, this.Children.Count);
                    this.Children.Add(newItem);
                    SetConnectorDecoratorTemplate(newItem);

                    this.SelectionService.SelectItem(newItem);
                    newItem.Focus();

                    Grid ucTest = GetRootElement<Grid>(newItem, "MainGrid");

                    if (ucTest != null)
                    {
                        gridTemProperty = GetChildrenElement<Grid>(ucTest, "gridProperty");
                        CreateTemGirdForNetInfo(strRRUFullName, devInfo);
                        gridTemProperty.Children.Clear();
                        gridTemProperty.Children.Add(g_GridForTem[strRRUFullName]);
                    }
                    i++;
                }
            }

            PosX = this.ActualWidth / 3 + i * 20;
            PosY = this.ActualHeight / 3 + i * 30;
            i = 0;

            //rHUB
            if (npTempalte.temrHubInfo.Count > 0)
            {
                string strXAML = string.Empty; //解析xml文件
                Size newSize;                  //根据不同的通道数，确定不同的 rHUB 的大小
                string strRRUName = "rHUB";

                foreach (TemrHUBInfo rHub in npTempalte.temrHubInfo)
                {
                    strXAML = GetrHUBFromXML(rHub.rHUBPathNum, strXAML, out newSize);

                    DesignerItem newItem = new DesignerItem();

                    string strXAML1 = strXAML;
                    string strrHUBFullName = string.Empty;

                    strrHUBFullName = string.Format("{0}-{1}", strRRUName, rHub.rHUBId);
                    string strInstedName = string.Format("Text=\"{0}\"", strrHUBFullName);
                    strXAML1 = strXAML1.Replace("Text=\"rHUB\"", strInstedName);
                    Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML1)));
                    newItem.Content = testContent;
                    newItem.ItemName = strrHUBFullName;

                    //添加 rHUB
                    var devrHuB = new RHubDevAttri(rHub.rHUBId, rHub.rHUBName);

                    if (devrHuB == null)
                        continue;

                    if (g_TemAllDevInfo.ContainsKey(EnumDevType.rhub))
                    {
                        g_TemAllDevInfo[EnumDevType.rhub].Add(strrHUBFullName, devrHuB.m_strOidIndex);
                    }
                    else
                    {
                        g_TemAllDevInfo.Add(EnumDevType.rhub, new Dictionary<string, string>());
                        g_TemAllDevInfo[EnumDevType.rhub].Add(strrHUBFullName, devrHuB.m_strOidIndex);
                    }

                    newItem.DevType = EnumDevType.rhub;
                    newItem.DevIndex = devrHuB.m_strOidIndex;
                    newItem.Width = newSize.Width;
                    newItem.Height = newSize.Height;

                    DesignerCanvas.SetLeft(newItem, PosX + i * 20);
                    DesignerCanvas.SetTop(newItem, PosY+ i * 20);

                    Canvas.SetZIndex(newItem, this.Children.Count);
                    this.Children.Add(newItem);
                    SetConnectorDecoratorTemplate(newItem);

                    this.SelectionService.SelectItem(newItem);
                    newItem.Focus();

                    Grid ucTest = GetRootElement<Grid>(newItem, "MainGrid");

                    if (ucTest != null)
                    {
                        gridTemProperty = GetChildrenElement<Grid>(ucTest, "gridProperty");
                        CreateTemGirdForNetInfo(strrHUBFullName, devrHuB);
                        gridTemProperty.Children.Clear();
                        gridTemProperty.Children.Add(g_GridForTem[strrHUBFullName]);
                    }
                    i++;
                }
            }
            PosX = PosX + i * 20;
            PosY = PosY + i * 20;
            i = 0;
            if (npTempalte.temAntInfo.Count > 0)
            {
                string strXAML = string.Empty; //解析xml文件
                Size newSize;                  //根据不同的通道数，确定不同的Ant的大小
                string strAntName = "No:";

                foreach (TemAntInfo ant in npTempalte.temAntInfo)
                {
                    strXAML = GetAntennaromXML(ant.antArrayNum, strXAML, out newSize);
                    DesignerItem newItem = new DesignerItem();

                    string strXAML1 = strXAML;
                    string strAntFullName = string.Empty;

                    strAntFullName = string.Format("{0}-{1}", strAntName, ant.antId);
                    string strInstedName = string.Format("Text=\"{0}\"", strAntFullName);
                    strXAML1 = strXAML1.Replace("Text=\"Antenna\"", strInstedName);
                    Object testContent = XamlReader.Load(XmlReader.Create(new StringReader(strXAML1)));
                    newItem.Content = testContent;
                    newItem.ItemName = strAntFullName;

                    //添加天线
                    var devAntInfo = new DevAttributeInfo(EnumDevType.ant, ant.antId);

                    if (devAntInfo == null)
                        continue;

                    if (g_TemAllDevInfo.ContainsKey(EnumDevType.ant))
                    {
                        g_TemAllDevInfo[EnumDevType.ant].Add(strAntFullName, devAntInfo.m_strOidIndex);
                    }
                    else
                    {
                        g_TemAllDevInfo.Add(EnumDevType.ant, new Dictionary<string, string>());
                        g_TemAllDevInfo[EnumDevType.ant].Add(strAntFullName, devAntInfo.m_strOidIndex);
                    }
                    newItem.DevIndex = devAntInfo.m_strOidIndex;
                    newItem.DevType = EnumDevType.ant;
                    newItem.Width = newSize.Width;
                    newItem.Height = newSize.Height;

                    DesignerCanvas.SetLeft(newItem, PosX + i * 20);
                    DesignerCanvas.SetTop(newItem, PosY + i * 20);
                    Canvas.SetZIndex(newItem, this.Children.Count);
                    this.Children.Add(newItem);
                    SetConnectorDecoratorTemplate(newItem);

                    this.SelectionService.SelectItem(newItem);
                    newItem.Focus();

                    Grid ucTest = GetRootElement<Grid>(newItem, "MainGrid");

                    if (ucTest != null)
                    {
                        gridTemProperty = GetChildrenElement<Grid>(ucTest, "gridProperty");
                        CreateTemGirdForNetInfo(strAntFullName, devAntInfo);
                        gridTemProperty.Children.Clear();
                        gridTemProperty.Children.Add(g_GridForTem[strAntFullName]);
                    }
                    i++;
                }
            }
        }
    }
}
