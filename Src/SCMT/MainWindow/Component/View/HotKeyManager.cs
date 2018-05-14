using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Input;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;

namespace SCMTMainWindow.Component.View
{
    public class HotKeyManager
    {
        /// <summary>
        /// 全局消息  热键消息
        /// </summary>
        public const int WM_HOTKEY = 0x312;

        /// <summary>
        ///记录快捷键  在全局原子表中的唯一标志符       
        /// </summary>
        private static Dictionary<eHotKey, int> m_HotKeyDic = new Dictionary<eHotKey, int>();

        /// <summary>
        /// 注册  所有  的全局快捷键
        /// </summary>
        /// <param name="listHotKeyModel">待注册的  快捷键集合</param>
        /// <param name="hWnd">注册快捷键的窗口句柄</param>
        /// <param name="outHotKeyDic">出参 返回注册的快捷键信息，调用者可以用来判断哪个快捷键发生指定事件</param>
        /// <returns>返回失败的快捷键信息</returns>
        public static string RegisterAllHotKey(IEnumerable<HotKeyModel> listHotKeyModel, IntPtr hWnd, out Dictionary<eHotKey, int> outHotKeyDic)
        {
            string strFail = string.Empty;
            foreach (var item in listHotKeyModel)
            {
                if (!RegisterHotKeySingle(item, hWnd))
                {
                    string strSingleFail = string.Empty;

                    if (item.IsSelectCtrl)
                    {
                        strSingleFail += ModifierKeys.Control.ToString();
                        strSingleFail += "+";
                    }
                    if (item.IsSelectShift)
                    {
                        strSingleFail += ModifierKeys.Shift.ToString();
                        strSingleFail += "+";
                    }
                    if (item.IsSelectAlt)
                    {
                        strSingleFail += ModifierKeys.Alt.ToString();
                        strSingleFail += "+";
                    }

                    strSingleFail += item.SelectKey;
                    strSingleFail = string.Format("{0} 【{1}】\n\r", item.Name, strSingleFail);
                    strFail += strSingleFail;
                }
            }

            outHotKeyDic = m_HotKeyDic;
            return strFail;
        }

        /// <summary>
        /// 注册  单个快捷键  
        /// </summary>
        /// <param name="modelValue">需要注册的快捷键  实例</param>
        /// <param name="hEnd">注册快捷键的窗口句柄</param>
        /// <returns>成功返回true 失败返回false</returns>
        private static bool RegisterHotKeySingle(HotKeyModel modelValue, IntPtr hWnd)
        {
            //快捷键的辅助项
            var mModiferKeys = new ModifierKeys();

            //获取待注册的快捷键 在 全局枚举中的  值
            var mHotKey = (eHotKey)Enum.Parse(typeof(eHotKey), modelValue.Name);

            //是否被注册过
            if (!m_HotKeyDic.ContainsKey(mHotKey))
            {
                //未注册，首先查找全局原子表是否存在该  项目，如果存在则删除重新添加
                if (GlobalFindAtom(mHotKey.ToString()) != 0)
                {
                    GlobalDeleteAtom(GlobalFindAtom(mHotKey.ToString()));
                }

                m_HotKeyDic[mHotKey] = GlobalAddAtom(mHotKey.ToString());
            }
            else
            {
                UnregisterHotKey(hWnd, m_HotKeyDic[mHotKey]);
            }

            //如果不启用该快捷键，直接返回
            if (!modelValue.IsUsable)
            {
                return true;
            }

            // 注册热键  首先获取辅助键的选择
            if (modelValue.IsSelectCtrl && !modelValue.IsSelectShift && !modelValue.IsSelectAlt)
            {
                mModiferKeys = ModifierKeys.Control;
            }
            else if (!modelValue.IsSelectCtrl && modelValue.IsSelectShift && !modelValue.IsSelectAlt)
            {
                mModiferKeys = ModifierKeys.Shift;
            }
            else if (!modelValue.IsSelectCtrl && !modelValue.IsSelectShift && modelValue.IsSelectAlt)
            {
                mModiferKeys = ModifierKeys.Alt;
            }
            else if (modelValue.IsSelectCtrl && modelValue.IsSelectShift && !modelValue.IsSelectAlt)
            {
                mModiferKeys = ModifierKeys.Control | ModifierKeys.Shift;
            }
            else if (modelValue.IsSelectCtrl && !modelValue.IsSelectShift && modelValue.IsSelectAlt)
            {
                mModiferKeys = ModifierKeys.Control | ModifierKeys.Alt;
            }
            else if (!modelValue.IsSelectCtrl && modelValue.IsSelectShift && modelValue.IsSelectAlt)
            {
                mModiferKeys = ModifierKeys.Shift | ModifierKeys.Alt;
            }
            else if (modelValue.IsSelectCtrl && modelValue.IsSelectShift && modelValue.IsSelectAlt)
            {
                mModiferKeys = ModifierKeys.Control | ModifierKeys.Shift | ModifierKeys.Alt;
            }

            //注册
            return HotKeyManager.RegisterHotKey(hWnd, m_HotKeyDic[mHotKey], mModiferKeys, (int)modelValue.SelectKey);

        }


        /// <summary>
        /// 注册热键函数  需要调用Marshal.GetLastWin32Error()得到错误值
        /// </summary>
        /// <param name="hWnd">注册热键的窗口句柄</param>
        /// <param name="id">唯一的id</param>
        /// <param name="mk">修改键，枚举类型Ctrl Shift Alt Windows None</param>
        ///<param name="vk">需要注册的热键</param>
        ///<returns>true 成功</returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, ModifierKeys mk, int vk);

        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hWnd">注册热键的窗口句柄</param>
        /// <param name="id">唯一的id</param>
        /// <returns>true 成功</returns>
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// 向全局原子表中添加数据，返回唯一id，即需要注册的热键的唯一id
        /// </summary>
        /// <param name="lpString">需要添加的字符串，最大255</param>
        /// <returns>成功 返回原子  失败 返回 0</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern short GlobalAddAtom(string lpString);

        /// <summary>
        /// 在全局原子表中查找指定字符串的原子
        /// </summary>
        /// <param name="lpString">待查找的字符串 </param>
        /// <returns>成功 返回原子 失败 返回 0</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern short GlobalFindAtom(string lpString);

        /// <summary>
        /// 从全局原子表中删除原子，程序结束后不会自动删除，所以在调用GlobalAddAtom之前应该查找并删除
        /// </summary>
        /// <param name="nAtom">待删除的原子</param>
        /// <returns>成功 返回原子 失败 返回 0</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern short GlobalDeleteAtom(short nAtom);
    }

    /// <summary>
    /// 初始化  &  注册事件
    /// </summary>
    public class HotKeyInit
    {
        private static HotKeyInit m_Instance;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static HotKeyInit Instance
        {
            get
            {
                return m_Instance ?? (m_Instance = new HotKeyInit());
            }
        }

        /// <summary>
        /// 从配置文件中加载快捷键设置信息
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<HotKeyModel> LoadJsonFileInfo()
        {
            try
            {
                string strInfo = File.ReadAllText(".\\Component\\Configration\\HotKey.json");
                ObservableCollection<HotKeyModel> listHotKey = JsonConvert.DeserializeObject<ObservableCollection<HotKeyModel>>(strInfo);

                return listHotKey;
            }
            catch (Exception)
            {
                //      MessageBox.Show("文件错误");
                return null;
            }

        }

        /// <summary>
        /// 通知系统注册  委托
        /// </summary>
        /// <param name="listHotKeyModel"></param>
        /// <returns></returns>
        public delegate bool RegisterGlobalHotKeyHandler(ObservableCollection<HotKeyModel> listHotKeyModel);
        public event RegisterGlobalHotKeyHandler RegisterGlobalHotKeyEvent;
        public bool RegisterGlobalHotKey(ObservableCollection<HotKeyModel> listHotKeyModel)
        {
            if (RegisterGlobalHotKeyEvent != null)
            {
                return RegisterGlobalHotKeyEvent(listHotKeyModel);
            }
            return false;
        }
    }

    /// <summary>
    /// 快捷键设置枚举，一般不支持用户添加快捷键，只能设置已有的快捷键
    /// </summary>
    public enum eHotKey
    {
        UserCase1 = 0,
        UserCase2 = 1,
        UserCase3 = 2,
        UserCase4 = 3,
        UserCase5 = 4,
        UserCase6 = 5,
        UserCase7 = 6,
        UserCase8 = 7,
        UserCase9 = 8
    }

    /// <summary>
    /// 可以被设置为快捷键的键盘按键 
    /// </summary>
    public enum eKeys
    {
        Space = 32,
        Left = 37,
        Up = 38,
        Right = 39,
        Down = 40,
        A = 65,
        B = 66,
        C = 67,
        D = 68,
        E = 69,
        F = 70,
        G = 71,
        H = 72,
        I = 73,
        J = 74,
        K = 75,
        L = 76,
        M = 77,
        N = 78,
        O = 79,
        P = 80,
        Q = 81,
        R = 82,
        S = 83,
        T = 84,
        U = 85,
        V = 86,
        W = 87,
        X = 88,
        Y = 89,
        Z = 90,
        F1 = 112,
        F2 = 113,
        F3 = 114,
        F4 = 115,
        F5 = 116,
        F6 = 117,
        F7 = 118,
        F8 = 119,
        F9 = 120,
        F10 = 121,
        F11 = 122,
        F12 = 123
    }

    /// <summary>
    /// 快捷键模型  
    /// </summary>
    public class HotKeyModel
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 快捷键是否启用
        /// </summary>
        public bool IsUsable { get; set; }

        /// <summary>
        /// 是否勾选Ctrl
        /// </summary>
        public bool IsSelectCtrl { get; set; }

        /// <summary>
        /// 是否勾选Shift
        /// </summary>
        public bool IsSelectShift { get; set; }

        /// <summary>
        /// 是否勾选Alt
        /// </summary>
        public bool IsSelectAlt { get; set; }

        /// <summary>
        /// 选中的按键
        /// </summary>
        public eKeys SelectKey { get; set; }

        /// <summary>
        /// 快捷键按键集合  用于显示在ComboBox中
        /// </summary>
        public static Array Keys
        {
            get
            {
                return Enum.GetValues(typeof(eKeys));
            }
        }
    }
}
