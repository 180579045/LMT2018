//�򿪱���С��������ƿ���,xiejing
BOOL CDTNetPlanInfoMgr::OpenNetPlanSwitch(int iLcCellNo, int/*WORKMODE*/ eWorkMode)

// ��������
CDtCfgOp 

class ICFE_DLL_API CDtCfgOp {
public:
	CDtCfgOp();
	~CDtCfgOp();
	
//
CDtCfgOp.CreateCfgFile(...)
  ##ԭʼ���ݴ�������
  # ���ݿ� : 
	CString strConSql = "provider=Microsoft.Jet.OLEDB.4.0;Data Source=";
			strConSql +=  pPath;//ENodeB70MibVersion.mdb...
			strConSql += ";Jet OLEDB:Database Password=(/1ac2";
	# sql ��� :
  m_tableNum = 0;
	//��������MIBTree���������ļ�
	strSQL.Format("select * from MibTree where DefaultValue='/' and ICFWriteAble = '��' order by ExcelLine");
	# ��ȡ����
	TableRecordSet 
	
	
	## dStep dPos ʲô��;��
	# double dStep = 95.0/iTableCount;
	# double dPos = 0.0;
	��ʱ����Ϣѭ���ã�������;δ֪
	dPos += dStep;
	CDtMsgDispCenter::Initstance().ProcessWindowMessage(NULL,WM_PROGRESSDLG_DISPLAY,0,(LONGLONG)dPos,lt);
	Class: CDtMsgDispCenter //��Ϣ�������� //	�ݲ������޴��ڵ���Ϣ����,�Ƚ�һ��������ɺ�,��ײ�socket��msgQһͬ���� 
  ProcessWindowMessage // //��Ϣ����
  
  ## ���ݵ��÷�
  while(!TableRecordSet.IsEOF())   //�ڱ�֮��ѭ��
	  CString NodeOid = TableRecordSet.GetValueByField("OID");
		CString strTableName = TableRecordSet.GetValueByField("MIBName");
		CString strTableContent = TableRecordSet.GetValueByField("TableContent");
		CString strChFriendName = TableRecordSet.GetValueByField("ChFriendName");
		...
	  CreatCfgFile_tabInfo(pAdoCon,strTableName,TableOffset,NodeOid,isDyTable,strTableContent,bSpecialTable,strTalbename,strChFriendName)
	  #
	  TableRecordSet.MoveNext();
	
	
     
// ����ĵ���	
void CDTTreeCtrl::OnExportdatatocfg()
	CDtCfgOp * Ӧ��