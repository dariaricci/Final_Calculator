using Final_Calculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace Final_Calculator.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        

        private double number1;
        public double Number1
        {
            get => number1;
            set
            {
                number1 = value;
                OnPropertyChanged();
            }
        }

        private string numberSt;
        public string NumberSt
        {
            get => numberSt;
            set
            {
                numberSt = value;
                OnPropertyChanged();
            }
        }


        private string result;
        public string Result
        {
            get => result;
            set
            {
                result = value;
                OnPropertyChanged();
            }
        }


        public MainWindowViewModel()
        {
            NumberCommand = new RelayCommand(OnNumberCommandExecute, CanNumberCommandExecuted);
            OperationCommand = new RelayCommand(OnOperationCommandExecute, CanOperationCommandExecuted);
            OtherCommand = new RelayCommand(OnOtherCommandExecute, CanOtherCommandExecuted);
            EqualCommand = new RelayCommand(OnEqualCommandExecute, CanEqualCommandExecuted);
        }
        //Команда, которая считывает значение нажатых кнопок и заносит в строку
        public ICommand NumberCommand { get; }
        private void OnNumberCommandExecute(object p)
        {
            
            string buttonNumber = Convert.ToString(p);
           NumberSt+= buttonNumber;
            Result = NumberSt;
          
        }
        private bool CanNumberCommandExecuted(object p)
        {
            return true;
        }

        public ICommand OtherCommand { get; }
        string buttonOtherSymbol;
        private void OnOtherCommandExecute(object p)
        {
            buttonOtherSymbol = Convert.ToString(p);
            switch (buttonOtherSymbol)
            {
                case "CE":
                    Result = "";
                    Number1 = 0;
                    NumberSt = "";
                    break;

                case "C":
                    Result = "";
                    NumberSt = "";
                    break;

                case "←":

                    Result = Result.Remove(Result.Length - 1);
                    NumberSt = Result;

                    break;

                case "+/-":

                    if (Convert.ToDouble(Result) > 0)
                        Result = Result.Insert(0, "-");
                    else
                        Result = Result.Remove(0, 1);
                    break;
            }
        }
        private bool CanOtherCommandExecuted(object p)
        {
            return true;
        }


        public ICommand OperationCommand { get; }
        string buttonSymbol;
        private void OnOperationCommandExecute(object p)
        {
            buttonSymbol = Convert.ToString(p);
            Number1 = Convert.ToDouble(Result);
            NumberSt = "";

        }

        private bool CanOperationCommandExecuted(object p)
        {
            return true;
        }

        /*Калькулятор работает таким образом, что если, например, необходимо сложить три числа, то сначала необходимо ввести первое число,
        потом нажать знак плюс, далее ввести второе число и нажать знак равно, после чего нажать снова на знак плюс, ввести третье число и снова нажать на знак равно, чтобы увидеть результат*/

        public ICommand EqualCommand { get; }

        private void OnEqualCommandExecute(object p)
        {
            switch (buttonSymbol)
            {
                case "x":
                    {
                        if (Result.EndsWith("%") == false)
                            Result = Convert.ToString(Calculate.Multiply(Number1, Convert.ToDouble(Result)));
                        else
                            Result = Convert.ToString(Calculate.Multiply(Number1, Convert.ToDouble(Result.Remove(Result.Length - 1))) / 100);
                    }
                    break;
                case "1/x":
                    {
                        try
                        {
                            Result = Convert.ToString(Math.Round(Calculate.SelfDivide(Convert.ToDecimal(Number1)),6));
                        }
                        catch (DivideByZeroException)
                        {
                            Result = "Деление на ноль невозможно";
                        }
                        break;
                    }
                case "x²":
                    {
                        Result = Convert.ToString(Math.Pow(Convert.ToDouble(Number1), 2));
                        break;
                    }
                case "√x":
                    {
                        Result = Convert.ToString(Math.Round(Math.Sqrt(Convert.ToDouble(Number1)),6));
                        break;
                    }
                case "+":
                    {
                        Result = Convert.ToString(Calculate.Add(Number1, Convert.ToDouble(Result)));

                        break;
                    }
                case "-":
                    {
                        Result = Convert.ToString(Calculate.Subtract(Number1, Convert.ToDouble(Result)));

                        break;
                    }
                case "÷":
                    {
                        Result = Convert.ToString(Math.Round(Calculate.Divide(Number1, Convert.ToDouble(Result)),6));
                        break;
                    }
            }
        }
        private bool CanEqualCommandExecuted(object p)
        {
            return true;
        }


    }
}
