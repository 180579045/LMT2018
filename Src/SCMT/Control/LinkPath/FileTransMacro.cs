using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LmtbSnmp;

namespace LinkPath
{
	public static class FileTransMacro
	{
		public const string STR_CREATANDGO = ("4");
		public const string STR_DESTROY = ("6");
		public const string STR_EMPTYPATH = ("null");
		public const string STR_FILEPATH = ("/ata2/VER/CFG");		//一般文件下载路径固定
		public const string STR_FILEPATH_RNC = ("/ata2/OM/CFG");    //RNC容灾文件下载路径固定

		public const byte SWPACK_ENB_TYPE = 0;              //基站软件包
		public const byte SWPACK_ENB_PERIPHERAL_TYPE = 1;   //基站外设软件包

		public static string INVALID_RelayVersion = "NULL";

		public const byte EQUIP_SWPACK = 1;                 //主设备软件
		public const byte EQUIP_SWPACK_BBU_COLDPATCH = 0;   //BBU冷补丁
		public const byte EQUIP_SWPACK_HOTPATCH = 1;        //热补丁
		public const byte EQUIP_SWPACK_RRU_COLDPATCH = 2;   //RRU冷补丁

		// 外设软件类型
		public const byte PERIP_SWPACK_RRU = 1;         //RRU软件
		public const byte PERIP_SWPACK_RETANT = 2;      //电调天线
		public const byte PERIP_SWPACK_EM = 3;          //环境监控单元
		public const byte PERIP_SWPACK_GPS = 4;         //GPS软件
		public const byte PERIP_SWPACK_1588 = 5;        //1588
		public const byte PERIP_SWPACK_CNSS = 6;        //北斗
		public const byte PERIP_SWPACK_OCU = 7;         //时钟拉远单元

		/*压缩文件类型的宏定义*/
		public const byte SI_ZIPFILETYPE_SOFT = 1;          /* 软件压缩包*/
		public const byte SI_ZIPFILETYPE_FIRM = 2;          /* 固件压缩包*/
		public const byte SI_ZIPFILETYPE_GPS = 3;           /* GPS压缩包*/
		public const byte SI_ZIPFILETYPE_ERU = 4;           /* ERU压缩包 */
		public const byte SI_ZIPFILETYPE_EM = 5;            /* 环境监控单元压缩包*/
		public const byte SI_ZIPFILETYPE_RRU = 7;           /* RRU压缩包*/
		public const byte SI_ZIPFILETYPE_RETANT = 8;        /* 电调天线压缩包*/
		public const byte SI_ZIPFILETYPE_1588 = 9;          /* 1588压缩包*/
		public const byte SI_ZIPFILETYPE_CNSS = 10;         /* 北斗压缩包*/
		public const byte SI_ZIPFILETYPE_OCU = 11;          /* 时钟拉远单元压缩包*/

		/*补丁包类型的宏定义*/
		public const byte SI_ZIPPATCHTYPE_COMMON = 0;       /* 普通补丁包*/
		public const byte SI_ZIPPATCHTYPE_HOT = 1;          /* 热补丁包*/
		public const byte SI_ZIPPATCHTYPE_RRUCOLD = 2;      /* rru冷补丁包*/

		public const string STR_DOTHING = "0";		//无进度
		public const string STR_DOWNLOADING = "1";	//正在下载
		public const string STR_UNZIPPING = "2";	//正在解压缩
		public const string STR_ACTIVATING = "3";	//正在激活
		public const string STR_SYNING = "4";       //正在同步

		public const string STATE_VALUE_TRANSFERING = "1";
		public const string STATE_VALUE_UNZIPING = "2";
	}

	public enum Transfiletype5216 : byte
	{
		TRANSFILE_unknowtype = 0,
		TRANSFILE_operationLog = 1,     //|操作日志
		TRANSFILE_alterLog,             //|变更日志
		TRANSFILE_omSecurityLog,        //|安全日志
		TRANSFILE_alarmLog,             //|告警日志文件
		TRANSFILE_omKeyLog,             //|重要过程日志
		TRANSFILE_updateLog,            //|升级日志
		TRANSFILE_debugLog,             //|黑匣子日志
		TRANSFILE_statelessAlarmLog,    //|异常日志
		TRANSFILE_eventLog,             //|事件日志
		TRANSFILE_userLog,              //|用户日志
		TRANSFILE_cfgDataConsistency,   //|配置数据一致性文件
		TRANSFILE_stateDataConsistency, //|状态数据一致性文件
		TRANSFILE_dataConsistency,      //|数据一致性文件
		TRANSFILE_curConfig,            //|当前运行配置文件
		TRANSFILE_planConfig,           //|期望配置文件
		TRANSFILE_equipSoftwarePack,    //|主设备软件包
		TRANSFILE_coldPatchPack,        //|主设备冷补丁包
		TRANSFILE_hotPatchPack,         //|主设备热补丁包
		TRANSFILE_rruEquipSoftwarePack, //|RRU软件包
		TRANSFILE_relantEquipSoftwarePack,              //|电调天线软件包
		TRANSFILE_enviromentEquipSoftwarePackPack,      //|环境监控软件包
		TRANSFILE_gpsEquipSoftwarePack,                 //|GPS软件包
		TRANSFILE_1588EquipSoftwarePack,                //|1588软件包
		TRANSFILE_cnssEquipSoftwarePackPack,            //|北斗软件包
		TRANSFILE_generalFile,                          //|普通文件/
		TRANSFILE_lmtMDBFile,                           //|数据库文件 lm.dtz
		TRANSFILE_activeAlarmFile,                      //|活跃告警文件
		TRANSFILE_performanceFile,                      //|性能文件
		TRANSFILE_cfgPackFile,                          //|性能文件
		TRANSFILE_snapshotFile = 30,                    //快照配置文件
		TRANSFILE_rncDisa = 49,                         //RNC容灾文件
		TRANSFILE_rarFile = 42,                         //rar压缩文件
	}

	public enum SENDFILETASKRES : byte
	{
		TRANSFILE_TASK_QUERYOIDFAIL,    /*获取OID失败*/
		TRANSFILE_TASK_SUCCEED,         /*传输任务下发成功*/
		TRANSFILE_TASK_FAILED,          /*传输任务下发失败*/
		TRANSFILE_TASK_NOIDLE           /*没有空闲的任务*/
	}

	public enum TRANSDIRECTION : byte
	{
		TRANS_UPLOAD = 1,       /*eNB上传到管理站*/
		TRANS_DOWNLOAD          /*管理站下载到eNB*/
	}

	public enum ExecuteResult : byte
	{
		UserCancel = 0,					// 用户取消
		UpgradeFinish,					// 升级成功
		UpgradeFailed,					// 升级失败
	}

	public enum SOFTACT : byte
	{
		SOFTACT_CLOSE = 0,
		SOFTACT_OPEN = 1
	}

	public enum FILETRANSOPER : byte
	{
		UnKnown,
		DownLoading,
		UpLoading,
		UnZipping,
		Syncing,
		Activing
	}

	public enum FILETRANSTYPE : byte
	{
		COMMONFILE,    //一般文件下载
		SWPACKPPLAN,   //主设备软件配置
		EXSWPACKPLAN   //外设软件配置
	}

	// TODO 这TM定义这么多结构体，作死啊

	public struct TswPackInfo
	{
		public int nSWEqpType;                  //软件包大类型，是外设还是基站软件包
		public int nSWPackType;                 //软件包类型
		public string csSWPackName;             //软件包名称
		public string csSWPackVersion;          //软件包版本号
		public string csSWPackRelayVersion;     //软件包依赖版本
		public string csSWPackTypeName;         //RRU软件
	}

	public class CDTAbstractFileTrans
	{
		public CDTAbstractFileTrans(bool bIsReqIdValid)
		{
			m_bIsReqIdValid = bIsReqIdValid;
		}

		public virtual bool StartFileTrans(long unFileTransId, ref long lreqId)
		{
			return true;
		}

		public void SetReqId(long lReqId)
		{
			m_bIsReqIdValid = true;
			m_lReqId = lReqId;
		}
		public long GetReqId()
		{
			return m_lReqId;
		}

		public bool m_bIsReqIdValid { get; set; }

		public long m_lReqId { get; set; }
	};

	public class CDTCommonFileTrans : CDTAbstractFileTrans
	{
		public CDTCommonFileTrans()
		: base(false)
		{

		}

		// 启动文件下发任务
		public override bool StartFileTrans(long unFileTransId, ref long lReqId)
		{
			Dictionary<string, string> mapName2Value = new Dictionary<string, string>
			{
				{"fileTransRowStatus", FileTransRowStatus},
				{"fileTransType", FileTransFileType},
				{"fileTransIndicator", FileTransDirection},
				{"fileTransFTPDirectory", FileTransFtpDir},
				{"fileTransFileName", FileTransFileName},
				{"fileTransNEDirectory", FileTransNeDir}
			};

			CDTLmtbPdu inOutPdu = new CDTLmtbPdu();

			var ret = CDTCmdExecuteMgr.CmdSetSync("AddFileTransTask", out lReqId, mapName2Value, $".{unFileTransId}",
				IpAddr, ref inOutPdu);

			return (0 == ret);
		}

		public string FileTransRowStatus { get; set; }

		public string FileTransFileType { get; set; }

		public string FileTransFtpDir { get; set; }

		public string FileTransFileName { get; set; }

		public string FileTransNeDir { get; set; }

		public string IpAddr { get; set; }

		public string FileTransDirection { get; set; }
	};

	public class StruFileTransDes
	{
		public long nFileTransTaskId;
		public long nFileDownSize;
		public FILETRANSOPER enumFileTransOp;
		public FILETRANSTYPE emFileTransType;       //触发文件下载的原因
		public string csSWPackPlanTypeIndex;        //软件包索引
		public string csEXSWPackVerIndex;           //外设软件包厂商索引
		public string csFilePathAndName;            //下载文件的路径和名称
	}

	public class TswPackDlProcInfo
	{
		public string FileTypeName;             //软件包类型名
		public string Index;                    //命令下发的索引号
		public long SetReqId;                   //流水号，对外提供，在弹出进度条时使用
		public string GetProcCmdName;           //进度获取命令名
		public ulong FileSize;                  //下载的文件大小
		public bool IsSwPack;                   //是否是基站软件包
		public string ActiveIndValue;           //激活标志
		public string FileName;                 //文件名称
	}

	//软件包规划进度显示信息管理类
	public class CSWPackPlanProcInfoMgr
	{
		public long m_lReqId;
		public bool m_bIsValid;
		public string m_csIndex;
		public string m_csFileTypeName;
		public ulong m_u64FileSize;
		public FILETRANSOPER m_emTransStatus;
		public bool m_bIsSwPack;
		public string m_csActiveIndValue;
		public string m_csFileName;

		public int iResCount;			//连接失败计数, 达到一定次数认为基站复位

		public CSWPackPlanProcInfoMgr()
		{
			m_bIsValid = false;
			m_lReqId = 0;
		}

		public void SetInfo(TswPackDlProcInfo Info)
		{
			m_lReqId = Info.SetReqId;
			m_csIndex = Info.Index;
			m_csFileTypeName = Info.FileTypeName;
			m_u64FileSize = Info.FileSize;
			m_emTransStatus = FILETRANSOPER.UnKnown;//开始是未知，如果是未知就要启动进度条
			m_bIsValid = true;
			m_bIsSwPack = Info.IsSwPack;
			m_csActiveIndValue = Info.ActiveIndValue;
			m_csFileName = Info.FileName;
		}

		public string GetActiveIndValue()
		{
			return m_csActiveIndValue;
		}

		public void SetFileTransStatus(FILETRANSOPER emTransStatus)
		{
			m_emTransStatus = emTransStatus;
		}

		public FILETRANSOPER GetFileTransStatus()
		{
			return m_emTransStatus;
		}


		public bool GetInfo(ref long lReqId, ref string csIndex )
		{
			lReqId = m_lReqId;
			csIndex = m_csIndex;
			return m_bIsValid;
		}

		public string GetFileName()
		{
			return m_csFileName;
		}

		public bool GetFileTypeName(ref string csFileTypeName)
		{
			csFileTypeName = m_csFileTypeName;
			return m_bIsValid;
		}

		public ulong GetFileSize()
		{
			return m_u64FileSize;
		}

		public void MakeInvalid()
		{
			m_bIsValid = false;
		}

		public bool IsValid()
		{
			return m_bIsValid;
		}

		public bool IsMainEqpSw()
		{
			return m_bIsSwPack;
		}
	};

	public enum PROGRESSLISTHEAD : byte
	{
		//进度条表头
		//PROGRESSLISTHEAD_NENAME = 0,			//网元信息
		PROGRESSLISTHEAD_FILENAME = 0,			//文件信息
		PROGRESSLISTHEAD_FINISHEDPERCENT,		//操作完成百分比
		PROGRESSLISTHEAD_STATE,					//状态
		PROGRESSLISTHEAD_OPERATIONTYPE			//操作类型
	};

	public enum OPERTYPE : byte
	{
		//操作类型
		OPERTYPE_UNKNOWN = PROGRESSLISTHEAD.PROGRESSLISTHEAD_OPERATIONTYPE + 1,
		OPERTYPE_DOWNLOAD,
		OPERTYPE_UPLOAD,
		OPERTYPE_UNZIP,
		OPERTYPE_ACTIVE,
		OPERTYPE_SYN,
		OPERTYPE_FINISHED
	};

	public enum FILETRANSSTATE : byte
	{
		TRANSSTATE_UNKNOWN = OPERTYPE.OPERTYPE_FINISHED + 1,
		//上传
		TRANSSTATE_UPLOADWAITING,       //文件上传等待中
		TRANSSTATE_UPLOADING,           //上传正在进行
		TRANSSTATE_UPLOADFINISHED,      //上传已完成
		TRANSSTATE_UPLOADFAILED,        //上传失败
		//下载
		TRANSSTATE_DOWNLOADWAITING,     //文件下载等待中
		TRANSSTATE_DOWNLOADING,         //下载正在进行
		TRANSSTATE_DOWNLOADFINISHED,    //下载已完成
		TRANSSTATE_DOWNLOADFAILED,      //下载失败
		//解压
		TRANSSTATE_UPZIPING,            //解压正在进行
		TRANSSTATE_UPZIPFINISHED,       //解压已完成
		TRANSSTATE_UPZIPFAILED,         //解压失败
		//激活
		TRANSSTATE_ACTIVEING,           //激活正在进行
		TRANSSTATE_ACTIVEFINISHED,      //激活已完成
		TRANSSTATE_ACTIVEFAILED,        //激活失败
		//同步
		OPERTYPE_SYNING,
		OPERTYPE_SYNFINISHED,
		OPERTYPE_SYNFAILED,

		//任务状态
		TRANSSTATE_TASKSENDFAILED,      //任务下发失败
		TRANSSTATE_TASKDELETESUCCESSED, //任务删除成功
		TRANSSTATE_TASKDELETEFAILED,    //任务删除失败
		//异常
		TRANSSTATE_DISCONNECT           //网元断开连接
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct CompressFileHead
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] u8FileCheckHead;                              /*必须是固定的数字*/

		public byte u8FileVersion;                                  /*文件版本，只能压缩，解压缩比自己版本小的*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public byte[] u8Reserve;                                    /*保留字节*/

		public byte u8SuitableType;                                 /*适用网元类型*/
		public ushort u16SuitableVersion;                           /*适用网元型号*/
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
		public char[] u8ZipFileBigVersion;                          /*文件大版本30字节*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
		public char[] u8ZipFileRelayVersion;                        /*文件大版本依赖关系30字节*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 30)]
		public char[] u8FileDateTime;                               /*文件生成日期30字节*/

		public byte u8ZipFileType;                                  /*压缩包类型，新增加，占用了一个保留字节*/
		public byte u8ZipPatchType;                                 /*补丁包类型，占用了一个保留字节*/
		public byte u8SubDtzNum;                                    /*拆分后小包的个数，0和1为未差分，大于1为小包个数，占用了一个保留字节*/
		public byte u8SubFileNameType;                              /*小文件名长度类型，0为40字节，1为240字节。占用了一个保留字节*/

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
		public byte[] u8Reserve2;                                   /*保留字节，剩下36个字节*/

		public ushort u16FileNum;                                   /*子文件个数*/
	};
}
