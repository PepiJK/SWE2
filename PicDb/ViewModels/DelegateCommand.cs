using System;
using System.Windows.Input;

namespace PicDb.ViewModels
{
    /// <summary>
    /// Helper class for commands.
    /// source: https://intellitect.com/getting-started-model-view-viewmodel-mvvm-pattern-using-windows-presentation-framework-wpf/
    /// </summary>
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _executeAction;
        private readonly Func<object, bool> _canExecuteAction;
 
        /// <summary>
        /// Constructor that sets the execute action and can execute action params.
        /// </summary>
        /// <param name="executeAction"></param>
        /// <param name="canExecuteAction"></param>
        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecuteAction = null)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }
 
        /// <summary>
        /// Execute command.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => _executeAction(parameter);
 
        /// <summary>
        /// Evaluate if command can be executed. Defaults to true.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => _canExecuteAction?.Invoke(parameter) ?? true;
 
        /// <summary>
        /// Event handler to handle the can execute. 
        /// </summary>
        public event EventHandler CanExecuteChanged;
 
        /// <summary>
        /// Invoke the can execute event handler to update the can execute.
        /// </summary>
        public void InvokeCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}