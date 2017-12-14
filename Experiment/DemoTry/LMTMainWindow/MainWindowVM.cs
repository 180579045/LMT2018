﻿/*----------------------------------------------------------------
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

namespace LMTMainWindow
{

    public class MainWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _title = "DTMobile LMT2018 Demo";

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
            Title = "Arthas.Demo";
            BtnEnabled = true;
        }));

        private ICommand _cmdSampleWithParam;
        public ICommand CmdSampleWithParam => _cmdSampleWithParam ?? (_cmdSampleWithParam = new AsyncCommand<string>(async str =>
        {
            Title = $"Hello I'm {str} currently";
            BtnEnabled = false;
            //do something
            await Task.Delay(2000);
            Title = "Arthas.Demo";
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
}
