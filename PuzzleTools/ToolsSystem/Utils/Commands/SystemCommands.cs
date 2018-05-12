using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToolsSystem
{
    public class SystemCommand : CommandBase
    {
        #region 成员

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        #endregion

        #region 构造函数

        public SystemCommand(Action execute) : this(execute, null) { }

        public SystemCommand(Action excute, Func<bool> canExecute)
        {
            _execute = excute;
            _canExecute = canExecute;
        }

        #endregion

        #region 方法

        public override bool CanExecute(object parameter)
        {
            return _execute != null && (_canExecute == null || _canExecute());
        }

        public override void Execute(object parameter)
        {
            _execute();
        }

        #endregion
    }

    public class SystemCommand<T> : CommandBase
    {
        #region 成员

        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        #endregion

        #region 构造函数

        public SystemCommand(Action<T> execute) : this(execute, null) { }

        public SystemCommand(Action<T> excute, Func<T, bool> canExecute)
        {
            _execute = excute;
            _canExecute = canExecute;
        }

        #endregion

        #region 方法

        public override bool CanExecute(object parameter)
        {
            return _execute != null && (_canExecute == null || _canExecute((T)parameter));
        }

        public override void Execute(object parameter)
        {
            _execute((T)parameter);
        }

        #endregion
    }
}
