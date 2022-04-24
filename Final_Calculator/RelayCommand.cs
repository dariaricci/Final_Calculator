using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Final_Calculator
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Func<object,bool> canExecute;

        //срабатывает каждый раз, когда состояние CanExecute могло измениться
        public event EventHandler CanExecuteChanged
        {
            //очередной обработчик подписывется на событие, то есть очередной метод связывается с делегатом
            add => CommandManager.RequerySuggested += value;
            remove=> CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> Execute, Func<object, bool> CanExecute)
        {
            execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            canExecute = CanExecute;
        }

        public bool CanExecute(object parameter) => canExecute(parameter);

        public void Execute(object parameter) => execute(parameter);
        
    }
}
