/*----------------------------------------------------------------
// Copyright (C) 2017 大唐移动通信设备有限公司 版权所有;
//
// 文件名：MainWindowVM.cs
// 文件功能描述：主界面控制类;
// 创建人：郭亮;
// 版本：V1.0
// 创建时间：2017-12-12
//----------------------------------------------------------------*/

using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Data;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace SCMTMainWindow
{
    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private string _title = "DTMobile Station Combine Maintain Tool";

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        private bool _btnEnabled = true;
        public bool BtnEnabled
        {
            get { return _btnEnabled; }
            set
            {
                _btnEnabled = value;
                OnPropertyChanged();
            }
        }

        private ICommand _cmdSample;
        public ICommand CmdSample => _cmdSample ?? (_cmdSample = new AsyncCommand(async () =>
        {
            Title = "Busy...";
            BtnEnabled = false;
            //do something
            await Task.Delay(2000);
            Title = "UICore.Demo";
            BtnEnabled = true;
        }));

        private ICommand _cmdSampleWithParam;
        public ICommand CmdSampleWithParam => _cmdSampleWithParam ?? (_cmdSampleWithParam = new AsyncCommand<string>(async str =>
        {
            Title = $"Hello I'm {str} currently";
            BtnEnabled = false;
            //do something
            await Task.Delay(2000);
            Title = "UICore.Demo";
            BtnEnabled = true;
        }));
    }

    #region Command

    public class AsyncCommand : ICommand
    {
        protected readonly Predicate<object> _canExecute;
        protected Func<Task> _asyncExecute;

        public AsyncCommand(Func<Task> asyncExecute, Predicate<object> canExecute = null)
        {
            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public async void Execute(object parameter)
        {
            await _asyncExecute();
        }
    }

    public class AsyncCommand<T> : ICommand
    {
        protected readonly Predicate<T> _canExecute;
        protected Func<T, Task> _asyncExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public AsyncCommand(Func<T, Task> asyncExecute, Predicate<T> canExecute = null)
        {
            _asyncExecute = asyncExecute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public async void Execute(object parameter)
        {
            await _asyncExecute((T)parameter);
        }
    }
    #endregion


    public enum OrderStatus { None, New, Processing, Shipped, Received };

    /// <summary>
    /// 初始化JsObj;
    /// </summary>
    public class CallbackObjectForJs
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private int i=0;
        
        private string option = OptionJsonString();               // 生成 option 配置项，并转成 JSON 字符串;
        public string canvas_width { get; set; }
        public string canvas_height { get; set; }

        //add by MaYi   定义全局的legend  和 series  方便进行添加和删除;
        public List<string> listForLegend = new List<string>();
        public List<series> listForSeries = new List<series>();
        public string[] listForXAxis = { };

        public static string OptionJsonString() {

            //这些只是进行初始化的信息，最后会被覆盖的;
            List<string> aaa = new List<string>();
            //aaa.Add("想怎么配");
            //aaa.Add("就怎么配");

            legend legend = new legend(aaa);                     // 数据集;

            // 生成 series 里面的 data 数据 ( 重要 );
            List<series> ser1 = new List<series>();
            
            // 生成 xAxis 中的 data 属性 ( date 的格式很重要 );
            string[] data = { "16:49:01", "16:49:02", "16:49:03", "16:49:04", "16:49:05", "16:49:06" };
            xAxis xaxis = new xAxis(data);

            Option option = new Option(legend, ser1, xaxis);

            return SCMTMainWindow.Option.ObjectToJson(option);
        }

        // 生成double随机数的一维数组
        static public double[] randomArr(int num)
        {
            Array arr = System.Array.CreateInstance(typeof(double), num);
            double[] doubleArr = { };
            Random rnd = new Random();

            // 生成随机数
            for (int i = 0; i < num; i++)
            {
                arr.SetValue(Math.Round(rnd.NextDouble()*20, 2), i);
            }

            // 数组强转
            doubleArr = (double[])arr;
            return doubleArr;
        }

        public string Option
        {
            get
            {
                return option;
            }

            set
            {
                option = value;
            }
        }

        //public void startTime() {
        //    timer.Interval = TimeSpan.FromMilliseconds(1000);
        //    timer.Tick += this.AddPoint;
        //    timer.IsEnabled = true;
        //}
        //private void AddPoint(object sender, EventArgs e)
        //{
        //    name = Convert.ToString(i);
        //    i++;            
        //}

    }
}
