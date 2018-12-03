namespace CommonUtility
{
	/// <summary>
	/// 定义自定义结构体
	/// </summary>
	public class CommStructs
	{
		public const int MAXTABNAMELEN = 50;
		public const int ALLSLOT = 10;
		public const int CMD_LIST_LEN = 400;
		public const int IPADDR_LEN = 128;   //在离线配置情况下，该长度标示的是配置文件长度
		public const int FILE_POSTFIX_LEN = 10; //文件后缀长度
		public const int MAX_DIR_LEN = 256;

		//public const int  MAX_VALUE_LEN = 256;
		public const int MAX_VALUE_LEN = 5120;

		public const int MAX_OID_SIZE = 128;
		public const int MAX_MIBVAL_ALLLIST_SIZE = 300;
		public const int MAX_EQUIPOID_LEN = 10;
		public const int MAX_ELEMTYPE_LEN = 50;
		public const int MAX_INDEXCOUNT = 6;  //MIB的最大维数
		public const int MAX_MIBDESC_LEN = 100; //Mib节点的中文描述
		public const int MAX_MIBTOTALDESC_LEN = 400; //Mib节点的整个中文描述
		public const int MOI_LEN = 8;       //MOI的长度，固定为8个字节

											//const int MENU_COMMAND_BASE = 32900;     //动态菜单Command ID下限
											//const int MENU_COMMAND_CREATEWND_MAX = MENU_COMMAND_BASE + 50; //创建窗口的菜单ID的上限
											//const int MENU_COMMAND_MAX = 33000;   //动态菜单Command ID上限
											//const int MENU_CLOSE_CHILDTABFLAG=MENU_COMMAND_MAX+1;//ChildFrm里面的Tab的关闭菜单弹出标志
		public const int MAX_MIBVERSION_LEN = 256;

		public const string EPC_KEEPALIVE_SNMPFUNCNAME = "GetEquipmentLoginInfo";

		public const int LTE_KEEPALIVE_TIMEOUT = 15000;
		public const int RNC_KEEPALIVE_TIMEOUT = 5000;
		public const int ERROR_DETAIL_LEN = 50;
		public const int USERINFO_DESC_LEN = 50;

		public const int MAX_LMTORNAME_SIZE = 256;
		public const int MAX_SNMPCOMNAME_SIZE = 50;
		public const int MAXID_SIZE = 255;
		public const int MAX_DBNAME_SIZE = 100;
		public const int MAX_MIBVER_LEN = 255;
		public const int MAX_MIBPREFIX_LEN = 128;
		public const int MAX_IVTCFG_LEN = 100;
	}
}