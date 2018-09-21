using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtility;

namespace NetPlan
{
	public class NPECmdHelper : Singleton<NPECmdHelper>
	{
		#region 公共接口

		// 根据传入的类型获取所有的命令
		public ObjOperCmd GetAllCmdByType(string type)
		{
			if (string.IsNullOrEmpty(type) || string.IsNullOrWhiteSpace(type))
			{
				throw new CustomException("传入的设备类型有误");
			}

			ObjOperCmd retCmd = null;
			switch (type)
			{
				case "board":
					retCmd = _npeCmd.board;
					break;
				case "rru":
					retCmd = _npeCmd.rru;
					break;
				case "rhub":
					retCmd = _npeCmd.rhub;
					break;
				case "ant":
					retCmd = _npeCmd.ant;
					break;
				case "rru_ant":
					retCmd = _npeCmd.rru_ant;
					break;
				case "board_rru":
					retCmd = _npeCmd.board_rru;
					break;
				default:
					break;
			}

			return retCmd;
		}

		#endregion

		#region 私有方法

		private NPECmdHelper()
		{
			_npeCmd = null;

			try
			{
				var path = FilePathHelper.GetAppPath() + ConfigFileHelper.NetPlanCmdJson;
				var jsonContent = FileRdWrHelper.GetFileContent(path);
				_npeCmd = JsonHelper.SerializeJsonToObject<NPECmd>(jsonContent);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}

		#endregion


		#region 私有属性

		private NPECmd _npeCmd;

		#endregion

	}

	public class NPECmd
	{
		public ObjOperCmd board;
		public ObjOperCmd rru;
		public ObjOperCmd rhub;
		public ObjOperCmd ant;
		public ObjOperCmd rru_ant;
		public ObjOperCmd board_rru;
	}

	public class ObjOperCmd
	{
		public List<string> get;
		public List<string> set;
		public List<string> add;
		public List<string> del;
	}
}
