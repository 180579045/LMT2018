using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager.FileHandler
{
	public class BaseFileHandler : IFileHandler
	{
		public BaseFileHandler(string ip)
		{
			boardAddr = ip;

			WorkingForUpgrade = false;
			WorkingForFileTrans = false;
			TFO = null;
			UFO = null;
		}

		#region 虚函数区

		/// <summary>
		/// 上传文件到基站侧
		/// </summary>
		/// <param name="srcFileFullName">源文件名，包括绝对路径</param>
		/// <param name="dstFilePath">目的路径</param>
		/// <returns>执行结果。成功，失败，用户取消</returns>
		public virtual ExecuteResult DoPutFile(string srcFileFullName, string dstFilePath)
		{
			Transfiletype5216 type = GetTransFileType();

			if (Transfiletype5216.TRANSFILE_equipSoftwarePack != type)
			{
				if (FileExistInBoard(dstFilePath))
				{
					var ret = MessageBox.Show("是否覆盖已有文件？", "覆盖文件确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
					if (DialogResult.Cancel == ret)
					{
						return ExecuteResult.UserCancel;
					}
				}
			}

			long reqId = 0;
			long taskId = 0;
			var transFileObj = FileTransTaskMgr.FormatTransInfo(dstFilePath, srcFileFullName, type, TRANSDIRECTION.TRANS_DOWNLOAD);
			var result = FileTransTaskMgr.SendTransFileTask(boardAddr, transFileObj, ref taskId, ref reqId);
			if (SENDFILETASKRES.TRANSFILE_TASK_FAILED == result)
			{
				return ExecuteResult.UpgradeFailed;
			}

			TFO = transFileObj;
			WorkingForFileTrans = true;
			WorkingTaskId = taskId;

			return ExecuteResult.UpgradeFinish;
		}

		protected virtual Transfiletype5216 GetTransFileType()
		{
			return Transfiletype5216.TRANSFILE_generalFile;
		}

		/// <summary>
		/// 上传文件到本地
		/// </summary>
		/// <param name="localPath">本地路径</param>
		/// <param name="remoteFullPath">板卡上文件路径</param>
		/// <returns>操作结果</returns>
		public virtual ExecuteResult DoGetFile(string localPath, string remoteFullPath)
		{
			long reqId = 0;
			long taskId = 0;

			var type = GetTransFileType();
			var tfo = FileTransTaskMgr.FormatTransInfo(localPath, remoteFullPath, type, TRANSDIRECTION.TRANS_UPLOAD);
			var result = FileTransTaskMgr.SendTransFileTask(boardAddr, tfo, ref taskId, ref reqId);
			if (SENDFILETASKRES.TRANSFILE_TASK_FAILED == result)
			{
				return ExecuteResult.UpgradeFailed;
			}

			TFO = tfo;
			WorkingForFileTrans = true;
			WorkingTaskId = taskId;

			return ExecuteResult.UpgradeFinish;
		}

		public CDTCommonFileTrans TFO { get; protected set; }		// trans file object

		public CSWPackPlanProcInfoMgr UFO { get; protected set; }	// upgrade file object

		public bool WorkingForUpgrade { get; protected set; }

		public bool WorkingForFileTrans { get; protected set; }

		public long WorkingTaskId { get; protected set; }
		#endregion

		#region 基类函数区

		// 判断基站中是否已经存在指定的文件
		protected bool FileExistInBoard(string fileFullPath)
		{
			throw new NotImplementedException("数据库模块需要实现CMData相关数据的存取");
		}

		#endregion

		protected string boardAddr;
	}
}
