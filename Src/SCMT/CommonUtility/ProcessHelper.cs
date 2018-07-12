using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

// 进程查询、程序启动等助手

namespace CommonUtility
{
	public static class ProcessHelper
	{
		/// <summary>
		/// 判断给定名字的进程是否在运行
		/// </summary>
		/// <param name="processName"></param>
		/// <returns>true:程序已启动；false:程序未启动</returns>
		public static bool ProcessIsRunning(string processName)
		{
			if (string.IsNullOrEmpty(processName))
			{
				return false;
			}

			var process = GetProcessByName(processName);
			return (null != process);
		}

		/// <summary>
		/// 杀掉指定的进程
		/// </summary>
		/// <param name="processName"></param>
		/// <returns>true:进程杀掉成功；其他情况返回false</returns>
		public static bool KillProcess(string processName)
		{
			if (string.IsNullOrEmpty(processName))
			{
				return true;
			}

			var process = GetProcessByName(processName);
			try
			{
				process?.Kill();

				return (null != GetProcessByName(processName));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			return false;
		}

		/// <summary>
		/// 启动进程
		/// </summary>
		/// <param name="execPath"></param>
		/// <returns></returns>
		public static bool StartProcess(string execPath)
		{
			if (string.IsNullOrEmpty(execPath))
			{
				return false;
			}

			if (!File.Exists(execPath))
			{
				return false;
			}

			var fi = new FileInfo(execPath);
			var execFullName = fi.Name;
			var extension = fi.Extension;
			var execName = execFullName.Replace(extension, "");
			if (string.IsNullOrEmpty(execName))
			{
				return false;
			}

			var process = GetProcessByName(execName);
			if (null != process)
			{
				return true;
			}

			try
			{
				Process.Start(execPath);
				return (null != GetProcessByName(execName));
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			return false;
		}

		/// <summary>
		/// 根据进程名获取进程对象
		/// </summary>
		/// <param name="processName"></param>
		/// <returns></returns>
		public static Process GetProcessByName(string processName)
		{
			if (string.IsNullOrEmpty(processName))
			{
				return null;
			}

			var vProcesses = Process.GetProcesses();
			foreach (var process in vProcesses)
			{
				// ProcessName是不带后缀的
				if (process.ProcessName.Equals(processName, StringComparison.InvariantCultureIgnoreCase))
				{
					return process;
				}
			}

			return null;
		}

		//根据端口号，查找该端口所在的进程pid.-1表示查找失败
		private static int GetPidByPort(int port)
		{
			var pro = new Process
			{
				StartInfo =
				{
					FileName = "cmd.exe",
					UseShellExecute = false,
					RedirectStandardInput = true,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true
				}
			};

			// 启动CMD
			pro.Start();

			// 运行端口检查命令
			pro.StandardInput.WriteLine("netstat -ano");
			pro.StandardInput.WriteLine("exit");

			// 获取结果
			Regex reg = new Regex("\\s+", RegexOptions.Compiled);
			string line = null;
			string endStr = ":" + Convert.ToString(port);
			while ((line = pro.StandardOutput.ReadLine()) != null)
			{
				line = line.Trim();
				line = reg.Replace(line, ",");
				var arr = line.Split(',');

				if (line.StartsWith("TCP", StringComparison.OrdinalIgnoreCase))
				{
					if (arr[1].EndsWith(endStr))
					{
						var pid = int.Parse(arr[4]);
						return pid;
					}
				}
				else if (line.StartsWith("UDP", StringComparison.OrdinalIgnoreCase))
				{
					if (arr[1].EndsWith(endStr))
					{
						var pid = int.Parse(arr[3]);
						return pid;
					}
				}
			}
			pro.Close();

			return -1;
		}
	}
}



