using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUploadManager
{
    public class CommonFunction
    {
        public enum LOGTYPEENUM {
            ENUM_PUBLICLOG,
	        ENUM_BOARDLOG,
	        ENUM_RRULOG,
	        ENUM_CELLLOG,	
	        ENUM_ANTANALOG,	
	        ENUM_TDSCELLLOG,	
	        ENUM_HUBLOG
        }
        public enum FILETRANSSTATE
        {
            TRANSSTATE_UPLOADING = 0,       //上传正在进行
            TRANSSTATE_UPLOADFINISHED,      //上传已完成
            TRANSSTATE_UPLOADWAITING,       //文件上传等待中
            TRANSSTATE_TaskSendFAILED,      //任务下发失败
            TRANSSTATE_UPLOADFAILED,        //上传失败
            TRANSSTATE_TASKDELETESUCCESSED, //任务删除成功
            TRANSSTATE_TASKDELETEFAILED,    //任务删除失败
            TRANSSTATE_UPLOAD,              //文件上传
            TRANSSTATE_FILENAME,            //文件信息
            TRANSSTATE_FINISHEDPERCENT,     //操作完成百分比
            TRANSSTATE_STATE,               //状态
            TRANSSTATE_OPERATIONTYPE,       //操作类型
        }
    }
}
