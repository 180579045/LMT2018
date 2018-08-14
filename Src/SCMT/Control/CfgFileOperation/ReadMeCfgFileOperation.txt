//打开本地小区布配控制开关,xiejing
BOOL CDTNetPlanInfoMgr::OpenNetPlanSwitch(int iLcCellNo, int/*WORKMODE*/ eWorkMode)

// 真正操作
CDtCfgOp 

class ICFE_DLL_API CDtCfgOp {
public:
	CDtCfgOp();
	~CDtCfgOp();
	
//
CDtCfgOp.CreateCfgFile(...)
  ##原始数据从哪来？
  # 数据库 : 
	CString strConSql = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
			strConSql +=  pPath;//ENodeB70MibVersion.mdb...
			strConSql += ";Jet OLEDB:Database Password=(/1ac2";
	# sql 语句 :
  m_tableNum = 0;
	//遍历整个MIBTree生成配置文件
	strSQL.Format("select * from MibTree where DefaultValue='/' and ICFWriteAble = '√' order by ExcelLine");
	# 获取数据
	TableRecordSet 
	
	
	## dStep dPos 什么用途？
	# double dStep = 95.0/iTableCount;
	# double dPos = 0.0;
	暂时，消息循环用？具体用途未知
	dPos += dStep;
	CDtMsgDispCenter::Initstance().ProcessWindowMessage(NULL,WM_PROGRESSDLG_DISPLAY,0,(LONGLONG)dPos,lt);
	Class: CDtMsgDispCenter //消息分派中心 //	暂不考虑无窗口的消息派送,等进一步需求完成后,与底层socket和msgQ一同考虑 
  ProcessWindowMessage // //消息派送
  
  ## 数据的用法
  while(!TableRecordSet.IsEOF())   //在表之间循环
	  CString NodeOid = TableRecordSet.GetValueByField("OID");
		CString strTableName = TableRecordSet.GetValueByField("MIBName");
		CString strTableContent = TableRecordSet.GetValueByField("TableContent");
		CString strChFriendName = TableRecordSet.GetValueByField("ChFriendName");
		...
	  CreatCfgFile_tabInfo(pAdoCon,strTableName,TableOffset,NodeOid,isDyTable,strTableContent,bSpecialTable,strTalbename,strChFriendName)
	  #
	  TableRecordSet.MoveNext();
	
	
     
// 外面的调用	
void CDTTreeCtrl::OnExportdatatocfg()
	CDtCfgOp * 应用