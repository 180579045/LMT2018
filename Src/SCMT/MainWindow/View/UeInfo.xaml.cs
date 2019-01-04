using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogManager;
using UEData;
using System.Data;

namespace SCMTMainWindow.View
{
    using ExcelOperator = Microsoft.Office.Interop.Excel;
    /// <summary>
    /// UeInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UeInfo : UserControl
    {
        public UeInfo()
        {
            InitializeComponent();
        }
        public DataSet Dt;
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditColumns edclw = new EditColumns(this.dataGrid.Columns);
            edclw.ShowDialog();
            if (!edclw.IsOK)
            {
                return;
            }
            for (int k = 0; k < this.dataGrid.Columns.Count; k++)
            {
                this.dataGrid.Columns[k].Visibility = Visibility.Visible;
            }
            for (int i = 0; i < edclw.HiddenHeader1.Count; i++)
            {
                for (int j = 0; j < this.dataGrid.Columns.Count; j++)
                {
                    if (edclw.HiddenHeader1[i].DisplayIndex == this.dataGrid.Columns[j].DisplayIndex)
                    {
                        this.dataGrid.Columns[j].Visibility = Visibility.Hidden;
                        continue;
                    }
                }
            }
        }
        /// <summary>
        /// 导出日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Export_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog diag = new System.Windows.Forms.SaveFileDialog();
            diag.Filter = "Excel文件(*.xls)|*.xls|所有文件(*.*)|*.*";
            if (diag.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!PutExcelfile(Dt, diag.FileName))
                {
                    System.Windows.Forms.MessageBox.Show("UE信息导出失败！", "UE信息查询");
                }
            }
        }
        /// <summary>
        /// 查询UE信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Query_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.CellUeInfo.Clear();
            GlobalData.strUeInfo.Clear();
            try
            {
                UEOperation Operation = new UEOperation();
                Operation.QueryUeinfo();
                //UETest();
            }
            catch (Exception err)
            {
                MessageBox.Show("UE信息查询失败!", "UE查询");
            }

            if (!CellUeInfo(ref Dt))
            {
                MessageBox.Show("UE信息装载表数据失败!", "UE查询");
            }
            dataGrid0.DataContext = Dt.Tables[0];
            dataGrid1.DataContext = Dt.Tables[1];
        }
        /// <summary>
        /// 装载表数据
        /// </summary>
        /// <returns></returns>
        private bool CellUeInfo(ref DataSet Dt)
        {
            try
            {
                if (Dt != null)
                {
                    Dt.Dispose();
                }
                Dt = new DataSet("UE信息库");
                Dt.Tables.Add(new DataTable("表1"));
                Dt.Tables.Add(new DataTable("表2"));
                Dt.Tables.Add(new DataTable("表3"));
                Dt.Tables[0].Columns.Add("小区", Type.GetType("System.Byte"));
                Dt.Tables[0].Columns.Add("所在BBU板卡槽位", Type.GetType("System.UInt32"));
                Dt.Tables[0].Columns.Add("BUU内小区索引", Type.GetType("System.Int32"));
                Dt.Tables[0].Columns.Add("物理小区ID", Type.GetType("System.UInt16"));
                Dt.Tables[1].Columns.Add("小区", Type.GetType("System.Byte"));
                Dt.Tables[1].Columns.Add("本小区的UE个数", Type.GetType("System.UInt16"));
                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    if (i == 10 || (i > 11 && i < 15))
                    {
                        continue;
                    }
                    Dt.Tables[2].Columns.Add(dataGrid.Columns[i].Header.ToString());
                }
                DataRow Dr;
                DataRow Dr1;
                for (int i = 0; i < GlobalData.CellUeInfo.Count; i++)
                {
                    Dr = Dt.Tables[0].NewRow();
                    Dr1 = Dt.Tables[1].NewRow();
                    Dr[0] = GlobalData.CellUeInfo[i].PCellLocalCellId;
                    Dr[1] = GlobalData.CellUeInfo[i].CellSlotNo;
                    Dr[2] = GlobalData.CellUeInfo[i].CellIndexBBU;
                    Dr[3] = GlobalData.CellUeInfo[i].PhyId;
                    Dt.Tables[0].Rows.Add(Dr);
                    Dr1[0] = GlobalData.CellUeInfo[i].PCellLocalCellId;
                    Dr1[1] = GlobalData.CellUeInfo[i].UeNum;
                    Dt.Tables[1].Rows.Add(Dr1);
                }
                for (int i = 0; i < GlobalData.strUeInfo.Count; i++)
                {
                    Dr = Dt.Tables[2].NewRow();
                    Dr["小区"] = GlobalData.strUeInfo[i].SpcellLocalCellId;
                    Dr["辅小区ID"] = GlobalData.strUeInfo[i].ScellLocalCellId;
                    Dr["小区UE索引"] = GlobalData.strUeInfo[i].u16UeIndexCell;
                    Dr["站内UE索引"] = GlobalData.strUeInfo[i].u32UeIndexGnb;
                    Dr["Crint"] = GlobalData.strUeInfo[i].u16Crint;
                    Dr["Amf侧NgapID"] = GlobalData.strUeInfo[i].u32AmfNgapId;
                    Dr["Ran侧NgapID"] = GlobalData.strUeInfo[i].u32RanNgapId;
                    Dr["AMBR下行"] = GlobalData.strUeInfo[i].u64AmbrDownlink;
                    Dr["AMBR上行"] = GlobalData.strUeInfo[i].u64AmbrUplink;
                    Dr["DRB个数"] = GlobalData.strUeInfo[i].u8ValidNofDrb;
                    Dr["Pdusession个数"] = GlobalData.strUeInfo[i].u8ValidNofPdusession;
                    Dr["gumai(AmfRegID)"] = GlobalData.strUeInfo[i].u8AmfRegionId;
                    Dr["gumai(AmfPoniter)"] = GlobalData.strUeInfo[i].u8AmfPoniter;
                    Dr["gumai(AmfSetId)"] = GlobalData.strUeInfo[i].u16AmfSetId;
                    Dt.Tables[2].Rows.Add(Dr);
                }
                Dt.Tables[0].AcceptChanges();
                Dt.Tables[1].AcceptChanges();
                Dt.Tables[2].AcceptChanges();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return false;
            }
            return true;
        }
        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <param name="dataset"></param>
        /// <param name="Path"></param>
        public bool PutExcelfile(DataSet DS, string Path)
        {
            //int rows = DS.Rows.Count;
            if (DS == null)
            {
                Console.Write("表为空！");
                return false;
            }
            ExcelOperator.Application xsl = new ExcelOperator.Application();
            if (xsl == null)
            {
                Console.Write("无法创建Excel文件！");
                return false;
            }
            int FormatNum;
            string Version;
            Version = xsl.Version;
            if (Convert.ToDouble(Version) < 12)
            {
                FormatNum = -4143;
            }
            else
            {
                FormatNum = 56;
            }
            Object obj = System.Reflection.Missing.Value;
            ExcelOperator.Workbooks workbooks = xsl.Workbooks;
            ExcelOperator.Workbook workbook = workbooks.Add(ExcelOperator.XlWBATemplate.xlWBATWorksheet);
            ExcelOperator.Worksheet worksheet = (ExcelOperator.Worksheet)workbook.Worksheets[1];
            worksheet.Name = "小区表";
            ExcelOperator.Worksheet worksheetII = (ExcelOperator.Worksheet)workbook.Worksheets.Add(obj, obj, obj, obj);
            worksheetII.Name = "UE数量统计表";
            ExcelOperator.Worksheet worksheetIII = (ExcelOperator.Worksheet)workbook.Worksheets.Add(obj, obj, obj, obj);
            worksheetIII.Name = "UE表";
            ExcelOperator.Range rang = null;
            //long totalCount = DT.Rows.Count;
            string file = "";
            string filename = Path;
            SetColumn(worksheet, DS.Tables[0]);
            SetColumn(worksheet.Range["A1:D1"]);
            SetColumn(worksheetII, DS.Tables[1]);
            SetColumn(worksheetII.Range["A1:B1"]);
            SetColumn(worksheetIII, DS.Tables[2]);
            SetColumn(worksheetIII.Range["A1:N1"]);
            WriteData(worksheet, DS.Tables[0]);
            SetCell(worksheet.Range[string.Format("A2:D{0}", DS.Tables[0].Rows.Count + 1)]);
            WriteData(worksheetII, DS.Tables[1]);
            SetCell(worksheetII.Range[string.Format("A2:B{0}", DS.Tables[1].Rows.Count + 1)]);
            WriteData(worksheetIII, DS.Tables[2]);
            SetCell(worksheetIII.Range[string.Format("A2:N{0}", DS.Tables[2].Rows.Count + 1)]);

            try
            {
                workbook.Saved = true;
                workbook.SaveAs(filename, FormatNum);
                System.Windows.Forms.MessageBox.Show(string.Format("{0}保存成功", file));
                workbook.Close();
                if (xsl != null)
                {
                    xsl.Workbooks.Close();
                    xsl.Quit();
                    int generation = System.GC.GetGeneration(xsl);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xsl);
                    xsl = null;
                    System.GC.Collect(generation);
                }
                GC.Collect();

            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                System.Windows.Forms.MessageBox.Show(string.Format("{0}保存失败", file));
                return false;
            }
            return true;
        }
        public bool PutExcelfile(string Path)
        {
            //判断UE业务查询数据是否为空
            if (GlobalData.strUeIpCellInfo == null || GlobalData.strUeInfo == null)
            {
                Log.Error("业务面查询导出数据失败，未查到数据");
                return false;
            }
            Object obj = System.Reflection.Missing.Value;
            //实例化Excel应用进程
            ExcelOperator.Application xls = new ExcelOperator.Application();
            if (xls == null)
            {
                Console.Write("无法创建Excel文件！");
                return false;
            }
            int FormatNum;
            string Version;
            Version = xls.Version;
            if (Convert.ToDouble(Version) < 12)
            {
                FormatNum = -4143;
            }
            else
            {
                FormatNum = 56;
            }
            ExcelOperator.Workbooks workbooks = xls.Workbooks;
            //实例化一个Excel工作簙
            ExcelOperator.Workbook workbook = workbooks.Add(ExcelOperator.XlWBATemplate.xlWBATWorksheet);
            ExcelOperator.Worksheet workSheetI = workbook.Worksheets[1];
            workSheetI.Name = "小区表";
            ExcelOperator.Worksheet workSheetII = workbook.Worksheets.Add(obj, obj, obj, obj);
            workSheetII.Name = "UE表";
            Microsoft.Office.Interop.Excel.Range rang = null;
            for (int i = 0; i < DataGrid_Cell.Columns.Count; i++)
            {
                workSheetI.Cells[1, i + 1] = DataGrid_Cell.Columns[i].Header;
            }
            SetColumn(workSheetI.Range["A1:I1"]);
            for (int i = 0; i < DataGrid_Ue.Columns.Count; i++)
            {
                workSheetII.Cells[1, i + 1] = DataGrid_Ue.Columns[i].Header;
            }
            SetColumn(workSheetII.Range["A1:S1"]);
            //写入内容
            for (int r = 0; r < GlobalData.strUeIpCellInfo.Count; r++)
            {
                workSheetI.Cells[r + 2, 1] = GlobalData.strUeIpCellInfo[r].BbuCellIndex;
                workSheetI.Cells[r + 2, 2] = GlobalData.strUeIpCellInfo[r].CellIndexEnb;
                workSheetI.Cells[r + 2, 3] = GlobalData.strUeIpCellInfo[r].SlotId;
                workSheetI.Cells[r + 2, 4] = GlobalData.strUeIpCellInfo[r].ProcId;
                workSheetI.Cells[r + 2, 5] = GlobalData.strUeIpCellInfo[r].ValidNofUeInEnb;
                workSheetI.Cells[r + 2, 6] = GlobalData.strUeIpCellInfo[r].UlSpsActiveUeNum;
                workSheetI.Cells[r + 2, 7] = GlobalData.strUeIpCellInfo[r].DlSpsActiveUeNum;
                workSheetI.Cells[r + 2, 8] = GlobalData.strUeIpCellInfo[r].nAmrNBNum;
                workSheetI.Cells[r + 2, 9] = GlobalData.strUeIpCellInfo[r].AmrWBNum;
            }
            SetCell(workSheetI.Range[string.Format("A2:I{0}", GlobalData.strUeIpCellInfo.Count + 1)]);
            for (int r = 0; r < GlobalData.strUeIpInfo.Count; r++)
            {
                workSheetII.Cells[r + 2, 1] = GlobalData.strUeIpInfo[r].UeIndexCell;
                workSheetII.Cells[r + 2, 2] = GlobalData.strUeIpInfo[r].UeIpInfo;
                workSheetII.Cells[r + 2, 3] = GlobalData.strUeIpInfo[r].HlMacUeLocation;
                workSheetII.Cells[r + 2, 4] = GlobalData.strUeIpInfo[r].MacUeLocation;
                workSheetII.Cells[r + 2, 5] = GlobalData.strUeIpInfo[r].MacTA;
                workSheetII.Cells[r + 2, 6] = GlobalData.strUeIpInfo[r].MacTmMode;
                workSheetII.Cells[r + 2, 7] = GlobalData.strUeIpInfo[r].UeCapability;
                workSheetII.Cells[r + 2, 8] = GlobalData.strUeIpInfo[r].FlowType;
                workSheetII.Cells[r + 2, 9] = GlobalData.strUeIpInfo[r].UlSpsActiveFlag;
                workSheetII.Cells[r + 2, 10] = GlobalData.strUeIpInfo[r].DlSpsActiveFlag;
                workSheetII.Cells[r + 2, 11] = GlobalData.strUeIpInfo[r].CaActiveFlag;
                workSheetII.Cells[r + 2, 12] = GlobalData.strUeIpInfo[r].ScellCellIndexEnb;
                workSheetII.Cells[r + 2, 13] = GlobalData.strUeIpInfo[r].ScellUeIndex;
                workSheetII.Cells[r + 2, 14] = GlobalData.strUeIpInfo[r].UIRruInfo;
                workSheetII.Cells[r + 2, 15] = GlobalData.strUeIpInfo[r].DIRruInfo;
                workSheetII.Cells[r + 2, 16] = GlobalData.strUeIpInfo[r].UeUlMcl;
                workSheetII.Cells[r + 2, 17] = GlobalData.strUeIpInfo[r].UeDlMcl;
                workSheetII.Cells[r + 2, 18] = GlobalData.strUeIpInfo[r].UlSinr;
                workSheetII.Cells[r + 2, 19] = GlobalData.strUeIpInfo[r].UlSinr;
            }
            SetCell(workSheetII.Range[string.Format("A2:S{0}", GlobalData.strUeIpInfo.Count + 1)]);
            try
            {
                workbook.Saved = true;
                workbook.SaveAs(Path, FormatNum);
                System.Windows.Forms.MessageBox.Show("UE业务面数据导出成功");
                workbook.Close();
                if (xls != null)
                {
                    xls.Workbooks.Close();
                    xls.Quit();
                    int generation = System.GC.GetGeneration(xls);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xls);
                    xls = null;
                    System.GC.Collect(generation);
                }
                GC.Collect();

            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                System.Windows.Forms.MessageBox.Show("UE业务面数据导出失败");
                return false;
            }
            return true;
        }
        /// <summary>
        ///设置导出的excel表的列名
        /// </summary>
        /// <param name="worksheet">当前工作表</param>
        /// <param name="Dt">数据源</param>
        private void SetColumn(ExcelOperator.Worksheet worksheet, DataTable Dt)
        {
            for (int i = 0; i < Dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = Dt.Columns[i].ColumnName;
            }
        }
        /// <summary>
        /// 将表数据填入excel工作表
        /// </summary>
        /// <param name="worksheet">当前操作的sheet</param>
        /// <param name="DT">数据源</param>
        private void WriteData(ExcelOperator.Worksheet worksheet, DataTable DT)
        {
            for (int r = 0; r < DT.DefaultView.Count; r++)
            {
                for (int i = 0; i < DT.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = DT.DefaultView[r][i];
                }
            }
        }
        /// <summary>
        /// 设置单元格
        /// </summary>
        /// <param name="SheetCells"></param>
        /// <param name="Cellinfo"></param>
        private void SetCell(ExcelOperator.Range range)
        {
            range.Font.Size = 10;
            range.EntireColumn.AutoFit();
            range.BorderAround(ExcelOperator.XlLineStyle.xlContinuous, ExcelOperator.XlBorderWeight.xlThin, ExcelOperator.XlColorIndex.xlColorIndexAutomatic, null);
            range.Borders[ExcelOperator.XlBordersIndex.xlInsideHorizontal].Weight = ExcelOperator.XlBorderWeight.xlThin;
        }
        /// <summary>
        /// 设置待导出excel工作表头的样式
        /// </summary>
        /// <param name="range"></param>
        private void SetColumn(ExcelOperator.Range range)
        {
            range.Font.Bold = true;//粗体
            range.EntireColumn.AutoFit();
            range.BorderAround(ExcelOperator.XlLineStyle.xlContinuous, ExcelOperator.XlBorderWeight.xlThin, ExcelOperator.XlColorIndex.xlColorIndexAutomatic, null);
            range.Borders[ExcelOperator.XlBordersIndex.xlInsideHorizontal].Weight = ExcelOperator.XlBorderWeight.xlThin;
        }
        private void CellQuery_Checked(object sender, RoutedEventArgs e)
        {
            if (this.UeIndex != null)
            {
                this.UeIndex.IsReadOnly = true;
                UeIndexTest.IsEnabled = false;
                UeIndex.Text = string.Empty;
            }
        }

        private void UeQuery_Checked(object sender, RoutedEventArgs e)
        {
            this.UeIndex.IsReadOnly = false;
            UeIndexTest.IsEnabled = true;
        }
        /// <summary>
        /// 当tab切换时触发方法 
        /// 当选择Ue业务查询页面时执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UeTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = sender as TabControl;
            if (a.SelectedIndex == 1)
            {
                cellRadioButton.IsChecked = true;
            }
        }
        /// <summary>
        /// UE测量配置查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mcf_Click(object sender, RoutedEventArgs e)
        {
            GlobalData.strUeMeasInfo.Clear();
            if (eNBIndex.Text.Trim() == string.Empty)
            {
                MessageBox.Show("UE基站索引输入不能为空！", "UE测量配置查询");
                return;
            }
            int nIndex;
            try
            {
                if (65534 < Convert.ToInt32(eNBIndex.Text.Trim()) || Convert.ToInt32(eNBIndex.Text.Trim()) < 0)
                {
                    MessageBox.Show("UE基站索引超出范围！（0~65534）", "UE测量配置查询");
                    if (eNBIndex.Focus())
                    {
                        eNBIndex.SelectAll();
                    }
                    return;
                }
                nIndex = Convert.ToInt32(eNBIndex.Text.Trim());
            }
            catch (Exception)
            {
                MessageBox.Show("UE基站索引格式不正确，请检查输入正确格式！", "UE测量配置查询");
                if (eNBIndex.Focus())
                {
                    eNBIndex.SelectAll();
                }
                return;
            }
            try
            {
                UEOperation Ue = new UEOperation();
                if (!Ue.QueryMcfinfo(nIndex))
                {
                    QueryResultInfo1.Content = string.Format("UE测量配置查询失败！");
                    QueryResultInfo1.Foreground = Brushes.Red;
                    return;
                }
                else
                {
                    QueryResultInfo1.Content = "UE测量配置查询成功！";
                    QueryResultInfo1.Foreground = Brushes.Green;
                }
            }
            catch (Exception err)
            {
                QueryResultInfo1.Content = err.Message;
                QueryResultInfo1.Foreground = Brushes.Red;
            }
        }
        /// <summary>
        /// 业务面查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UeBusiness_Query(object sender, RoutedEventArgs e)
        {
            string nCellIndex = "";
            string nUeIndex = "";
            GlobalData.CellType = 0;

            if (CellIndex.Text.Trim() == "")
            {
                MessageBox.Show("请输入小区索引！", "UE业务面查询");
                return;
            }
            try
            {
                if (Convert.ToInt32(CellIndex.Text.Trim()) < 0 || Convert.ToInt32(CellIndex.Text.Trim()) > 35)
                {
                    MessageBox.Show("小区索引超出范围！（0~35）", "UE业务面查询");
                    if (CellIndex.Focus())
                    {
                        CellIndex.SelectAll();
                    }
                    return;
                }
                nCellIndex = CellIndex.Text.Trim();
            }
            catch (Exception)
            {
                MessageBox.Show("小区索引格式不正确，请检查输入正确格式（0~35）！", "UE业务面查询");
                if (CellIndex.Focus())
                {
                    CellIndex.SelectAll();
                }
                return;
            }
            if (!UeIndex.IsReadOnly)
            {
                if (UeIndex.Text.Trim() == "")
                {
                    MessageBox.Show("请输入UE索引！", "UE业务面查询");
                    return;
                }
                try
                {
                    if (Convert.ToInt32(UeIndex.Text.Trim()) < 0 || Convert.ToInt32(UeIndex.Text.Trim()) > 1200)
                    {
                        MessageBox.Show("UE索引超出范围！（0~1200）", "UE业务面查询");
                        if (UeIndex.Focus())
                        {
                            UeIndex.SelectAll();
                        }
                        return;
                    }
                    nUeIndex = UeIndex.Text.Trim();
                }
                catch (Exception)
                {
                    MessageBox.Show("UE索引格式不正确，请检查输入正确格式（0~1200）！", "UE业务面查询");
                    if (UeIndex.Focus())
                    {
                        UeIndex.SelectAll();
                    }
                    return;
                }
            }
            try
            {
                //初始化列状态
                SetColumns(GlobalData.CellType);
                UEOperation Operation = new UEOperation();
                if (!Operation.QueryIpinfo(nCellIndex, nUeIndex))
                {
                    QueryResultInfo.Content = string.Format("UE业务面查询失败！");
                    QueryResultInfo.Foreground = Brushes.Red;
                    return;
                }
                else
                {
                    QueryResultInfo.Content = string.Format("UE业务面查询成功！");
                    QueryResultInfo.Foreground = Brushes.Green;
                    SetColumns(GlobalData.CellType);
                }
            }
            catch (Exception err)
            {
                QueryResultInfo.Content = err.Message;
                QueryResultInfo.Foreground = Brushes.Red;
            }

        }
        /// <summary>
        /// 业务面导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UeBusiness_Put(object sender, RoutedEventArgs e)
        {
            //Test();
            System.Windows.Forms.SaveFileDialog Savefile = new System.Windows.Forms.SaveFileDialog();
            Savefile.Filter = "Excel文件(*.xls)|*.xls|所有文件(*.*)|*.*";
            if (Savefile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!PutExcelfile(Savefile.FileName))
                {
                    System.Windows.Forms.MessageBox.Show("UE业务面查询结果导出失败");
                }
            }
        }
        /// <summary>
        /// 根据小区类型修改列集合
        /// </summary>
        /// <param name="nType">小区类型</param>
        private void SetColumns(int nType)
        {
            if (nType == 1)
            {
                for (int iLoop = 0; iLoop < DataGrid_Ue.Columns.Count; iLoop++)
                {
                    if (iLoop == 1 || iLoop == 4 || iLoop == 6)
                    {
                        continue;
                    }
                    if (iLoop < 15)
                    {
                        DataGrid_Ue.Columns[iLoop].Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        DataGrid_Ue.Columns[iLoop].Visibility = Visibility.Visible;
                    }
                }
                for (int iLoop = 5; iLoop < DataGrid_Cell.Columns.Count; iLoop++)
                {
                    DataGrid_Cell.Columns[iLoop].Visibility = Visibility.Hidden;
                }

            }
            else if (nType == 0)
            {
                for (int iLoop = 0; iLoop < DataGrid_Ue.Columns.Count; iLoop++)
                {
                    if (iLoop < 15)
                    {
                        DataGrid_Ue.Columns[iLoop].Visibility = Visibility.Visible;
                    }
                    else
                    {
                        DataGrid_Ue.Columns[iLoop].Visibility = Visibility.Hidden;
                    }
                }
                for (int iLoop = 5; iLoop < DataGrid_Cell.Columns.Count; iLoop++)
                {
                    DataGrid_Cell.Columns[iLoop].Visibility = Visibility.Visible;
                }
            }

        }
        private void Test()
        {
            GlobalData.strUeIpInfo.Clear();
            GlobalData.strUeIpCellInfo.Clear();
            for (int i = 0; i < 5; i++)
            {
                UeipInfo info = new UeipInfo();
                info.CaActiveFlag = "aaaaa";
                info.DIRruInfo = "111";
                info.DlSinr = "222";
                info.DlSpsActiveFlag = "333";
                info.FlowType = "444";
                info.HlMacUeLocation = "5555";
                info.MacTA = "6666";
                info.MacTmMode = "777";
                info.MacUeLocation = "888";
                info.ScellCellIndexEnb = "999";
                info.ScellUeIndex = "1111";
                info.UeCapability = "2222";
                info.UeDlMcl = "3333";
                info.UeIndexCell = "4444";
                info.UeIpInfo = "5555";
                info.UeUlMcl = "6666";
                info.UIRruInfo = "7777";
                info.UlSinr = "8888";
                info.UlSpsActiveFlag = "9999";
                GlobalData.strUeIpInfo.Add(info);
            }
            for (int i = 0; i < 5; i++)
            {
                UeipCellInfo cell = new UeipCellInfo();
                cell.AmrWBNum = "1223";
                cell.BbuCellIndex = "aaaa";
                cell.CellIndexEnb = "ssss";
                cell.nAmrNBNum = "ddddd";
                cell.UlSpsActiveUeNum = "fffff";
                cell.DlSpsActiveUeNum = "gggg";
                cell.ValidNofUeInEnb = "gggjj";
                cell.ProcId = "kkkkk";
                cell.SlotId = "lllll";
                GlobalData.strUeIpCellInfo.Add(cell);
            }
        }
        private void UETest()
        {
            GlobalData.strUeInfo.Clear();
            GlobalData.CellUeInfo.Clear();
            CellUeInformation CellInfo;
            UeInformation Ue;
            for (int i = 0; i < 5; i++)
            {
                CellInfo = new CellUeInformation();
                CellInfo.CellIndexBBU = 1;
                CellInfo.CellSlotNo = 10;
                CellInfo.PCellLocalCellId = 123;
                CellInfo.PhyId = 12346;
                CellInfo.UeNum = 10;
                GlobalData.CellUeInfo.Add(CellInfo);
            }
            for (int i = 0; i < 10; i++)
            {
                Ue = new UeInformation();
                Ue.ScellLocalCellId = "aaaa";
                Ue.SpcellLocalCellId = "NNNN";
                Ue.u16AmfSetId = "bbbb";
                Ue.u16Crint = "1";
                Ue.u16UeIndexCell = "ffffff";
                Ue.u32AmfNgapId = "dsadsa";
                Ue.u32RanNgapId = "5655555";
                Ue.u32UeIndexGnb = "dddddd";
                Ue.u64AmbrDownlink = "1111111";
                Ue.u64AmbrUplink = "1222";
                Ue.u8AmfPoniter = "qqqq";
                Ue.u8AmfRegionId = "eeeee";
                Ue.u8ValidNofDrb = "1";
                Ue.u8ValidNofPdusession = "23";
                GlobalData.strUeInfo.Add(Ue);
            }
        }
    }
}
