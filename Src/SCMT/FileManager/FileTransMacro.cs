using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
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

	public enum SOFTACT : byte
	{
		SOFTACT_CLOSE = 0,
		SOFTACT_OPEN = 1
	}

	public enum ExecuteResult : byte
	{
		UserCancel = 0,					// 用户取消
		UpgradeFinish,					// 升级成功
		UpgradeFailed,					// 升级失败
	}
}
