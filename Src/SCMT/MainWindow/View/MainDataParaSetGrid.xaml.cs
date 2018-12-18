using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using CommonUtility;
using DataBaseUtil;
using LinkPath;
using LmtbSnmp;
using LogManager;
using MIBDataParser;
using MIBDataParser.JSONDataMgr;
using SCMTMainWindow.Component.ViewModel;
using SCMTMainWindow.Utils;

namespace SCMTMainWindow.View
{
	/// <summary>
	/// 基本信息右键菜单参数设置
	/// </summary>
	public partial class MainDataParaSetGrid : Window
	{
		/// <summary>
		/// 保存当前命令的属性节点信息
		/// </summary>
		private CmdMibInfo cmdMibInfo = new CmdMibInfo();

		/// <summary>
		/// 保存索引节点信息
		/// </summary>
		private List<MibLeaf> listIndexInfo = new List<MibLeaf>();

		private MibTable m_MibTable;

		/// <summary>
		/// 0为查询指令，1为增加，2为删除，3为修改指令
		/// </summary>
		private int m_operType = 0;
        private int m_ModifyIndexGrade = 0;

        //private bool m_bAllSelect = true;

		public bool bOK = false;

		/// <summary>
		/// 动态表的所有列信息,该属性必须设置，否则无法正常显示;
		/// 设置该属性之后，动态表就会将所有列对应的模板全部生成;
		/// </summary>
		private DyDataGrid_MIBModel m_ParaModel;

		private MainDataGrid m_MainDataGrid;

        /// <summary>
        /// 保存命令类型为4时的信息，不显示在界面需要下发指令
        /// </summary>
        ObservableCollection<DyDataGrid_MIBModel> m_CmdDataList = new ObservableCollection<DyDataGrid_MIBModel>();


        public MainDataParaSetGrid(MainDataGrid dataGrid)
		{
			InitializeComponent();

			m_MainDataGrid = dataGrid;

			WindowStartupLocation = WindowStartupLocation.CenterScreen;
			this.DynamicParaSetGrid.SelectionChanged += DynamicParaSetGrid_SelectionChanged;
			this.DynamicParaSetGrid.BeginningEdit += DynamicParaSetGrid_BeginningEdit;
			this.DynamicParaSetGrid.LostFocus += DynamicParaSetGrid_LostFocus;
		}

		private void DynamicParaSetGrid_LostFocus(object sender, RoutedEventArgs e)
		{
			if (typeof(TextBox) != e.OriginalSource.GetType())
			{
				return;
			}
			string cellValue = "";

			DataGrid dataGrid = (DataGrid)sender;

			if (!(dataGrid.CurrentCell.Item is DyDataGrid_MIBModel))
			{
				return;
			}
			// 行Model
			DyDataGrid_MIBModel mibModel = (DyDataGrid_MIBModel)dataGrid.CurrentCell.Item;

			// TextBox
			if (typeof(TextBox) == e.OriginalSource.GetType())
			{
				cellValue = (e.OriginalSource as TextBox).Text;
				//用于处理参数值列(目前是第2列)单元格内容为字符串时，修改后对列表的显示
				if (mibModel.PropertyList[1].Item3 is DataGrid_Cell_MIB)
				{
					var ff = mibModel.PropertyList[1].Item3 as DataGrid_Cell_MIB;
					if (!string.IsNullOrWhiteSpace(cellValue))
						ff.m_Content = cellValue;
				}
			}
		}

		private void DynamicParaSetGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
		{
			dynamic temp = e.Column.GetCellContent(e.Row).DataContext as DyDataGrid_MIBModel;
			// 根据不同的列（既数据类型）改变不同的处理策略;
			try
			{
				temp.JudgeParaPropertyName_StartEditing(e.Column.Header);
			}
			catch (Exception ex)
			{
			}
		}

		private void DynamicParaSetGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// 如果SelectedIndex是-1，则表明是初始化过程中调用的;
			if (((e.OriginalSource as ComboBox).SelectedIndex == -1))
			{
				return;
			}
			else
			{
				try
				{
					(sender as DataGrid).SelectedCells[0].Item.GetType();
				}
				catch (Exception ex)
				{
					return;
				}
			}
			try
			{
				((sender as DataGrid).SelectedCells[0].Item as DyDataGrid_MIBModel).JudgePropertyName_ChangeSelection(
					(sender as DataGrid).SelectedCells[0].Column.Header.ToString(), (e.OriginalSource as ComboBox).SelectedItem);
			}
			catch
			{
			}
		}

		private void BtnOK_Click(object sender, RoutedEventArgs e)
		{
			string strMsg;
			bOK = true;
			//获取右键菜单列表内容
			ObservableCollection<DyDataGrid_MIBModel> datalist = new ObservableCollection<DyDataGrid_MIBModel>();
			datalist = (ObservableCollection<DyDataGrid_MIBModel>)this.DynamicParaSetGrid.DataContext;

            Dictionary<string, object> lineData = new Dictionary<string, object>();
            if (m_operType != 0)//添加修改
            {
                // Mib值校验
                foreach (DyDataGrid_MIBModel mm in datalist)
				{
					string strMibVal = null;
					var cell = mm.Properties["ParaValue"] as GridCell;
					if (cell.cellDataType == LmtbSnmp.DataGrid_CellDataType.enumType)
					{
						var emnuCell = cell as DataGrid_Cell_MIB_ENUM;
						if (emnuCell != null)
							strMibVal = emnuCell.m_CurrentValue.ToString();
						else
							strMibVal = cell.m_Content;
					}
					else
						strMibVal = cell.m_Content;

					// 获取Mib节点属性
					MibLeaf mibLeaf = SnmpToDatabase.GetMibNodeInfoByName(cell.MibName_EN, CSEnbHelper.GetCurEnbAddr());
					if (null == mibLeaf)
					{
						strMsg = "无法获取Mib节点信息！";
						Log.Error(strMsg);
						MessageBox.Show(strMsg);
						return;
					}

					// 校验是否包含特殊字符
					if (false == SnmpMibUtil.CheckMibValidChar(strMibVal, out strMsg))
					{
						Log.Error(string.Format("{0},节点名称：{1}，值：{2}", strMsg, cell.MibName_EN, strMibVal));
						MessageBox.Show(string.Format("{0}: {1}", mibLeaf.childNameCh, strMsg));
						return;
					}
					// 校验Mib值
					if (false == SnmpMibUtil.CheckMibValueByMibLeaf(mibLeaf, strMibVal))
					{
						strMsg = string.Format("{0}: 您所设置的值格式错误或不在取值范围内", mibLeaf.childNameCh);
						Log.Error(strMsg);
						MessageBox.Show(strMsg);
						return;
					}
				}

                // 索引
                int indexGrade = 0;
                string strIndex = "";
                if(datalist.Count > 0)
                    m_MainDataGrid.ModelConvertToDic(datalist, ref indexGrade, ref strIndex, ref lineData,true);

                if (m_CmdDataList.Count > 0)
                {
                    m_MainDataGrid.ModelConvertToDic(m_CmdDataList, ref indexGrade, ref strIndex, ref lineData, false);
                }

                // 像基站下发添加指令
                CDTLmtbPdu lmtPdu = new CDTLmtbPdu();
                if (!m_MainDataGrid.AddAndModifyCmd(lineData, ref lmtPdu))
                    return;           
                             
                // 判读SNMP响应结果
                if (lmtPdu.m_LastErrorStatus != 0)
                {
                    strMsg = $"参数配置失败，错误信息:{SnmpErrDescHelper.GetErrDescById(lmtPdu.m_LastErrorStatus)}";
                    Log.Error(strMsg);
                    MessageBox.Show(strMsg);
                    return;
                }

                if (m_operType == 3 || m_operType == 4)
                {
                    MessageBox.Show("参数修改成功！");
                }
                else if (m_operType == 1)
                {
                    MessageBox.Show("参数添加成功！");
                }

                //下发指令成功后更新基本信息列表
                if (m_operType == 3 || m_operType == 4)
                    indexGrade = m_ModifyIndexGrade;
                m_MainDataGrid.RefreshDataGrid(lmtPdu, indexGrade);
            }
            else//查询
            {
                string value;
                string strindex = "";
                string indexDes = "";
                Dictionary<string, string> dicMibToValue = new Dictionary<string, string>();
                Dictionary<string, string> dicMibToOid = new Dictionary<string, string>();
                Dictionary<int, string> dicNoToValue = new Dictionary<int, string>();
                Dictionary<int, string> dicNoToDes = new Dictionary<int, string>();

                m_MainDataGrid.GetChildMibInfo(cmdMibInfo, ref dicMibToValue, ref dicMibToOid);

                //获取查询的索引信息，进行组合
                foreach (DyDataGrid_MIBModel mm in datalist)
                {
                    var cell = mm.Properties["ParaValue"] as GridCell;
                    if (cell.cellDataType == LmtbSnmp.DataGrid_CellDataType.enumType)
                    {
                        var emnuCell = cell as DataGrid_Cell_MIB_ENUM;
                        if (emnuCell != null)
                            value = emnuCell.m_CurrentValue.ToString();
                        else
                            value = cell.m_Content;
                    }
                    else
                        value = cell.m_Content;

                    // 获取Mib节点属性
                    MibLeaf mibLeaf = SnmpToDatabase.GetMibNodeInfoByName(cell.MibName_EN, CSEnbHelper.GetCurEnbAddr());
                    if (null == mibLeaf)
                    {
                        return;
                    }
                    dicNoToValue.Add(mibLeaf.childNo, value);
                    dicNoToDes.Add(mibLeaf.childNo, mibLeaf.childNameCh);
                }

                List<int> q = (from d in dicNoToValue orderby d.Key select d.Key).ToList();//根据索引号排序

                foreach (int key in q)
                {
                    strindex += "." + dicNoToValue[key];
                    indexDes += dicNoToDes[key] + dicNoToValue[key];
                }

                //下发查询指令
                if (CommLinkPath.GetMibValueFromCmdExeResult(strindex, cmdMibInfo.m_cmdNameEn, ref dicMibToValue, CSEnbHelper.GetCurEnbAddr()))
                {
                    m_MainDataGrid.QuerySuccessRefreshDataGrid(dicMibToValue, dicMibToOid, q.Count, indexDes);
                }
            }

			this.Close();
		}

		private void BtnCancle_Click(object sender, RoutedEventArgs e)
		{
			bOK = false;
			this.Close();
		}

		private void CheckSelect_Click(object sender, RoutedEventArgs e)
		{
		}

		public void InitAddParaSetGrid(CmdMibInfo mibInfo, MibTable table, int operType)
		{
			cmdMibInfo = mibInfo;
            m_operType = operType;
			m_MibTable = table;

			listIndexInfo.Clear();
            m_CmdDataList.Clear();

            foreach (MibLeaf leaf in table.childList)
			{
				if (leaf.IsIndex.Equals("True"))
					listIndexInfo.Add(leaf);
			}

			this.Title = mibInfo.m_cmdDesc;
			int i = 0;
			ObservableCollection<DyDataGrid_MIBModel> datalist = new ObservableCollection<DyDataGrid_MIBModel>();
			if (cmdMibInfo == null)
				return;

			if (cmdMibInfo.m_cmdDesc.Equals(this.Title))
			{
				if (listIndexInfo.Count > 0)
				{
					//索引节点
					foreach (MibLeaf mibLeaf in listIndexInfo)
					{
						dynamic model = new DyDataGrid_MIBModel();
                        model.AddParaProperty("ParaName", new DataGrid_Cell_MIB()
						{
							m_Content = mibLeaf.childNameCh,
							oid = mibLeaf.childOid,
							MibName_CN = mibLeaf.childNameCh,
							MibName_EN = mibLeaf.childNameMib
						}, "参数名称");

						model.AddParaProperty("ParaValue", new DataGrid_Cell_MIB()
						{
							m_Content = SnmpToDatabase.GetDefaultValue(mibLeaf),
							oid = mibLeaf.childOid,
							MibName_CN = mibLeaf.childNameCh,
							MibName_EN = mibLeaf.childNameMib
						}, "参数值");

						model.AddParaProperty("ParaValueRange", new DataGrid_Cell_MIB()
						{
							m_Content = mibLeaf.managerValueRange,
							oid = mibLeaf.childOid,
							MibName_CN = mibLeaf.childNameCh,
							MibName_EN = mibLeaf.childNameMib
						}, "取值范围");

						model.AddParaProperty("Unit", new DataGrid_Cell_MIB()
						{
							m_Content = mibLeaf.unit,
							oid = mibLeaf.childOid,
							MibName_CN = mibLeaf.childNameCh,
							MibName_EN = mibLeaf.childNameMib
						}, "单位");

						// 将这个整行数据填入List;
						if (model.Properties.Count != 0)
						{
							// 向单元格内添加内容;
							datalist.Add(model);
							i++;
						}
						// 最终全部收集完成后，为控件赋值;
						if (i == datalist.Count)
						{
							this.ParaDataModel = model;
							this.DynamicParaSetGrid.DataContext = datalist;
						}
					}
				}

				if (cmdMibInfo.m_leaflist.Count > 0)
				{
					//属性节点
					foreach (string oid in cmdMibInfo.m_leaflist)
					{
						MibLeaf mibLeaf = Database.GetInstance().GetMibDataByOid(oid, CSEnbHelper.GetCurEnbAddr());
						dynamic model = new DyDataGrid_MIBModel();

						string devalue = SnmpToDatabase.GetDefaultValue(mibLeaf);
                        bool bisDefault = false;
                        //判断是否含有默认值，如包含不显示到界面
                        if(cmdMibInfo.m_leafDefault != null)
                        {
                            if(cmdMibInfo.m_leafDefault.ContainsKey(oid))
                            {
                                devalue = cmdMibInfo.m_leafDefault[oid];
                                bisDefault = true;
                            }                               
                        }

                        model.AddParaProperty("ParaName", new DataGrid_Cell_MIB()
						{
							m_Content = mibLeaf.childNameCh,
							oid = mibLeaf.childOid,
							MibName_CN = mibLeaf.childNameCh,
							MibName_EN = mibLeaf.childNameMib
						}, "参数名称");

						// 在这里要区分DataGrid要显示的数据类型;
						var dgm = DataGridCellFactory.CreateGridCell(mibLeaf.childNameMib, mibLeaf.childNameCh, devalue, mibLeaf.childOid, CSEnbHelper.GetCurEnbAddr());

						model.AddParaProperty("ParaValue", dgm, "参数值");

						model.AddParaProperty("ParaValueRange", new DataGrid_Cell_MIB()
						{
							m_Content = mibLeaf.mibValAllList,
							oid = mibLeaf.childOid,
							MibName_CN = mibLeaf.childNameCh,
							MibName_EN = mibLeaf.childNameMib
						}, "取值范围");

						model.AddParaProperty("ParaUnit", new DataGrid_Cell_MIB()
						{
							m_Content = mibLeaf.unit,
							oid = mibLeaf.childOid,
							MibName_CN = mibLeaf.childNameCh,
							MibName_EN = mibLeaf.childNameMib
						}, "单位");

						// 将这个整行数据填入List;
						if (model.Properties.Count != 0)
						{
                            // 向单元格内添加内容;
                            if (bisDefault)
                                m_CmdDataList.Add(model);
                            else
                            {
                                datalist.Add(model);
                                i++;
                            }							    
						}

						// 最终全部收集完成后，为控件赋值;
						if (i == datalist.Count && !bisDefault)
						{
							this.ParaDataModel = model;
							this.DynamicParaSetGrid.DataContext = datalist;
						}
					}
				}
			}
		}

		/// <summary>
		/// 根据基本信息列表选择的行填充信息，对于填充第一条数据信息(后续添加)
		/// </summary>
		/// <param name="model"></param>
		public bool InitModifyParaSetGrid(CmdMibInfo mibInfo, DyDataGrid_MIBModel mibModel, MibTable table, int operType)
		{
			if (mibModel == null || mibInfo == null)
				return false;

			cmdMibInfo = mibInfo;
			m_MibTable = table;
            m_operType = operType;
            this.Title = mibInfo.m_cmdDesc;
			listIndexInfo.Clear();
            m_ModifyIndexGrade = 0;
            m_CmdDataList.Clear();

            string strIndex = "";

            foreach (MibLeaf leaf in table.childList)
            {
                if (leaf.IsIndex.Equals("True"))
                {
                    listIndexInfo.Add(leaf);
                    m_ModifyIndexGrade++;
                }                    
            }

            int i = 0;
			ObservableCollection<DyDataGrid_MIBModel> datalist = new ObservableCollection<DyDataGrid_MIBModel>();
			Dictionary<string, string> temdicValue = new Dictionary<string, string>();//保存当前选中行的信息，key为nameMib，value为值
			Dictionary<string, string> temdicOid = new Dictionary<string, string>();//保存当前选中行的信息，key为nameMib，value为Oid
			if (cmdMibInfo == null)
				return false;

			foreach (var iter in mibModel.Properties)
			{
				dynamic model = new DyDataGrid_MIBModel();
				if (iter.Key.Equals("indexlist"))
					continue;

                MibLeaf mibLeaf = SnmpToDatabase.GetMibNodeInfoByName(iter.Key, CSEnbHelper.GetCurEnbAddr());

				if (mibLeaf == null)
					continue;

				if (iter.Value is DataGrid_Cell_MIB)
				{
					var cellGrid = iter.Value as DataGrid_Cell_MIB;

					temdicValue.Add(mibLeaf.childNameMib, cellGrid.m_Content);
					temdicOid.Add(mibLeaf.childNameMib, cellGrid.oid);
                    if(m_ModifyIndexGrade > 0)
                        strIndex = MibStringHelper.GetIndexValueByGrade(cellGrid.oid, m_ModifyIndexGrade);
                }
				else if (iter.Value is DataGrid_Cell_MIB_ENUM)
				{
					var cellGrid = iter.Value as DataGrid_Cell_MIB_ENUM;

					temdicValue.Add(mibLeaf.childNameMib, cellGrid.m_CurrentValue.ToString());
					temdicOid.Add(mibLeaf.childNameMib, cellGrid.oid);
                    if (m_ModifyIndexGrade > 0)
                        strIndex = MibStringHelper.GetIndexValueByGrade(cellGrid.oid, m_ModifyIndexGrade);
                }
			}

            if (m_ModifyIndexGrade == 0)
                strIndex = ".0";

			if (cmdMibInfo.m_cmdDesc.Equals(this.Title))
			{
				if (cmdMibInfo.m_leaflist.Count > 0)
				{
					//属性节点
					foreach (string oid in cmdMibInfo.m_leaflist)
					{
						MibLeaf mibLeaf = Database.GetInstance().GetMibDataByOid(oid, CSEnbHelper.GetCurEnbAddr());
						dynamic model = new DyDataGrid_MIBModel();
						string devalue = null;
						string stroid = null;

                        bool bisDefault = false;
                        //判断是否含有默认值，如包含不显示到界面
                        if (cmdMibInfo.m_leafDefault != null)
                        {
                            if (cmdMibInfo.m_leafDefault.ContainsKey(oid))
                            {
                                devalue = cmdMibInfo.m_leafDefault[oid];
                                bisDefault = true;
                            }
                        }

                        if(!bisDefault)
                        {
                            if (temdicValue.ContainsKey(mibLeaf.childNameMib))
                                devalue = temdicValue[mibLeaf.childNameMib];
                            else
                                devalue = SnmpToDatabase.GetDefaultValue(mibLeaf) ;

                            if (temdicOid.ContainsKey(mibLeaf.childNameMib))
                                stroid = temdicOid[mibLeaf.childNameMib];
                            else
                                stroid = SnmpToDatabase.GetMibPrefix() + oid + strIndex;
                        }
                        else
                        {
                            if (temdicOid.ContainsKey(mibLeaf.childNameMib))
                            {
                                if (temdicOid[mibLeaf.childNameMib].Contains(oid))
                                    stroid = temdicOid[mibLeaf.childNameMib];
                                else
                                    stroid = SnmpToDatabase.GetMibPrefix() + oid + strIndex;
                            }                              
                            else
                                stroid = SnmpToDatabase.GetMibPrefix() + oid + strIndex;
                        }

						model.AddParaProperty("ParaName", new DataGrid_Cell_MIB()
						{
							m_Content = mibLeaf.childNameCh,
							oid = stroid,
							MibName_CN = mibLeaf.childNameCh,
							MibName_EN = mibLeaf.childNameMib
						}, "参数名称");

						// 在这里要区分DataGrid要显示的数据类型;
						var dgm = DataGridCellFactory.CreateGridCell(mibLeaf.childNameMib, mibLeaf.childNameCh, devalue, stroid, CSEnbHelper.GetCurEnbAddr());

						model.AddParaProperty("ParaValue", dgm, "参数值");

						model.AddParaProperty("ParaValueRange", new DataGrid_Cell_MIB()
						{
							m_Content = mibLeaf.managerValueRange,
							oid = stroid,
							MibName_CN = mibLeaf.childNameCh,
							MibName_EN = mibLeaf.childNameMib
						}, "取值范围");

						model.AddParaProperty("ParaUnit", new DataGrid_Cell_MIB()
						{
							m_Content = mibLeaf.unit,
							oid = stroid,
							MibName_CN = mibLeaf.childNameCh,
							MibName_EN = mibLeaf.childNameMib
						}, "单位");

						// 将这个整行数据填入List;
						if (model.Properties.Count != 0)
						{
                            // 向单元格内添加内容;
                            if (bisDefault)
                                m_CmdDataList.Add(model);
                            else
                            {
                                datalist.Add(model);
                                i++;
                            }
                        }

						// 最终全部收集完成后，为控件赋值;
						if (i == datalist.Count && !bisDefault)
						{
							this.ParaDataModel = model;
							this.DynamicParaSetGrid.DataContext = datalist;
						}
					}
				}
			}

			return true;
		}

        public void InitQueryParaSetGrid(CmdMibInfo mibInfo, MibTable table, int operType)
        {
            cmdMibInfo = mibInfo;
            m_operType = operType;
            m_MibTable = table;

            listIndexInfo.Clear();
            m_CmdDataList.Clear();
            foreach (MibLeaf leaf in table.childList)
            {
                if (leaf.IsIndex.Equals("True"))
                    listIndexInfo.Add(leaf);
            }

            this.Title = mibInfo.m_cmdDesc;
            int i = 0;
            ObservableCollection<DyDataGrid_MIBModel> datalist = new ObservableCollection<DyDataGrid_MIBModel>();
            if (cmdMibInfo == null)
                return;

            if (cmdMibInfo.m_cmdDesc.Equals(this.Title))
            {
                if (listIndexInfo.Count > 0)
                {
                    //索引节点
                    foreach (MibLeaf mibLeaf in listIndexInfo)
                    {
                        dynamic model = new DyDataGrid_MIBModel();
                        model.AddParaProperty("ParaName", new DataGrid_Cell_MIB()
                        {
                            m_Content = mibLeaf.childNameCh,
                            oid = mibLeaf.childOid,
                            MibName_CN = mibLeaf.childNameCh,
                            MibName_EN = mibLeaf.childNameMib
                        }, "参数名称");

                        model.AddParaProperty("ParaValue", new DataGrid_Cell_MIB()
                        {
                            m_Content = SnmpToDatabase.GetDefaultValue(mibLeaf),
                            oid = mibLeaf.childOid,
                            MibName_CN = mibLeaf.childNameCh,
                            MibName_EN = mibLeaf.childNameMib
                        }, "参数值");

                        model.AddParaProperty("ParaValueRange", new DataGrid_Cell_MIB()
                        {
                            m_Content = mibLeaf.managerValueRange,
                            oid = mibLeaf.childOid,
                            MibName_CN = mibLeaf.childNameCh,
                            MibName_EN = mibLeaf.childNameMib
                        }, "取值范围");

                        model.AddParaProperty("Unit", new DataGrid_Cell_MIB()
                        {
                            m_Content = mibLeaf.unit,
                            oid = mibLeaf.childOid,
                            MibName_CN = mibLeaf.childNameCh,
                            MibName_EN = mibLeaf.childNameMib
                        }, "单位");

                        // 将这个整行数据填入List;
                        if (model.Properties.Count != 0)
                        {
                            // 向单元格内添加内容;
                            datalist.Add(model);
                            i++;
                        }
                        // 最终全部收集完成后，为控件赋值;
                        if (i == datalist.Count)
                        {
                            this.ParaDataModel = model;
                            this.DynamicParaSetGrid.DataContext = datalist;
                        }
                    }
                }
            }
        }

		public DyDataGrid_MIBModel ParaDataModel
		{
			get
			{
				return m_ParaModel;
			}
			set
			{
				m_ParaModel = value;
				this.DynamicParaSetGrid.Columns.Clear();

				// 获取所有列信息，并将列信息填充到DataGrid当中;
				foreach (var iter in m_ParaModel.PropertyList)
				{
					if (iter.Item1.Equals("ParaValue"))
					{
						DataGridTemplateColumn column = new DataGridTemplateColumn();
						DataTemplate TextBlockTemplate = new DataTemplate();
						DataTemplate ComboBoxTemplate = new DataTemplate();

						string textblock_xaml =
						   @"<DataTemplate xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                                            xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                                            xmlns:model='clr-namespace:WPF.Model'>
                                <TextBlock Text='{Binding " + iter.Item1 + @".m_Content}'/>
                            </DataTemplate>";

						string combobox_xaml =
						   @"<DataTemplate xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                                            xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                                            xmlns:model='clr-namespace:WPF.Model'>
                                <ComboBox IsEditable='True' IsReadOnly='False' ItemsSource='{Binding " + iter.Item1 + @".m_AllContent}' SelectedIndex='0'/>
                             </DataTemplate>";

						TextBlockTemplate = XamlReader.Parse(textblock_xaml) as DataTemplate;
						ComboBoxTemplate = XamlReader.Parse(combobox_xaml) as DataTemplate;

						column.Header = iter.Item2;                                      // 填写列名称;
						column.CellTemplate = TextBlockTemplate;                         // 将单元格的显示形式赋值;
						column.CellEditingTemplate = ComboBoxTemplate;                   // 将单元格的编辑形式赋值;
						column.Width = 230;                                              // 设置显示宽度;

						this.DynamicParaSetGrid.Columns.Add(column);
					}
					/*else if(iter.Item1.Equals("ParaSelect"))
                    {
                        DataGridTemplateColumn column = new DataGridTemplateColumn();
                        DataTemplate checkBoxHeaderTemplate = new DataTemplate();
                        DataTemplate checkBoxTemplate = new DataTemplate();

                        string checkBoxHeader_xaml =
                           @"<DataTemplate xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                                            xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                                            xmlns:model='clr-namespace:WPF.Model'>
                                <CheckBox IsChecked='{Binding " + @"m_bAllSelect}'/>
                            </DataTemplate>";

                        string checkBox_xaml =
                           @"<DataTemplate xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
                                            xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                                            xmlns:model='clr-namespace:WPF.Model'>
                                <CheckBox IsChecked='{Binding " + iter.Item1 + @".m_bIsSelected}'/>
                            </DataTemplate>";

                        checkBoxHeaderTemplate = XamlReader.Parse(checkBoxHeader_xaml) as DataTemplate;
                        checkBoxTemplate = XamlReader.Parse(checkBox_xaml) as DataTemplate;

                        //DataGridCheckBoxColumn column = new DataGridCheckBoxColumn();
                        //column.HeaderTemplate = CheckBoxTemplate;
                        column.HeaderTemplate = checkBoxHeaderTemplate;
                        column.CellTemplate = checkBoxTemplate;

                        this.DynamicParaSetGrid.Columns.Add(column);
                    }*/
                    else
					{
						// 当前添加的表格类型只有Text类型，应该使用工厂模式添加对应不同的数据类型;
						var column = new DataGridTextColumn
						{
							Header = iter.Item2,
							IsReadOnly = true,
							Binding = new Binding(iter.Item1 + ".m_Content")
						};

						this.DynamicParaSetGrid.Columns.Add(column);
					}
				}
			}
		}
	}
}