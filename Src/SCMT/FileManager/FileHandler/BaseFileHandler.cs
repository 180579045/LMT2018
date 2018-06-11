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
		}

		#region 虚函数区

		public virtual ExecuteResult DoHandle(string srcFileFullName, string dstFilePath)
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

			// TODO AddFileTransProcess();

			return ExecuteResult.UpgradeFinish;
		}

		protected virtual Transfiletype5216 GetTransFileType()
		{
			return Transfiletype5216.TRANSFILE_generalFile;
		}

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
