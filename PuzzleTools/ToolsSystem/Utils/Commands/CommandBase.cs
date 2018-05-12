using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToolsSystem
{
    public abstract class CommandBase : ICommand
    {
        #region 事件

        public event EventHandler CanExecuteChanged;

        #endregion

        #region 方法

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);

        internal void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
