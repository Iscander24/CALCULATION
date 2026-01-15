using CALCULATION.Commands;
using CALCULATION.Enums;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALCULATION.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        private double? _inputStorage;

        private Operation _currentOperation;
                
        
        public double? Input
        {
            get => _input;

            set
            {
                _input = value;
                OnPropertyChanged(nameof(Input));
            }
        }
        private double? _input;


        public string InputString
        {
            get => _inputString;

            set
            {
                _inputString = value;
                OnPropertyChanged(nameof(InputString));
            }
        }

        private string _inputString;

        #region =========================== Commands =========================================

        private DelegateCommand? _commandAdd;
        public DelegateCommand CommandAdd
        {
            get
            {
                if (_commandAdd == null)
                {
                    _commandAdd = new DelegateCommand((object? o) => OperationManagement(Operation.Addition));
                }
                return _commandAdd;
            }
        }

        private DelegateCommand? _commandSubtract;
        public DelegateCommand CommandSubtract
        {
            get
            {
                if (_commandSubtract == null)
                {
                    _commandSubtract = new DelegateCommand((object? o) => OperationManagement(Operation.Subtraction));
                }
                return _commandSubtract;
            }
        }

        private DelegateCommand? _commandDivide;
        public DelegateCommand CommandDivide
        {
            get
            {
                if (_commandDivide == null)
                {
                    _commandDivide = new DelegateCommand((object? o) => OperationManagement(Operation.Division));
                }
                return _commandDivide;
            }
        }

        private DelegateCommand? _commandMultiplicate;
        public DelegateCommand CommandMultiplicate
        {
            get
            {
                if (_commandMultiplicate == null)
                {
                    _commandMultiplicate = new DelegateCommand((object? o) => OperationManagement(Operation.Multiplication));
                }
                return _commandMultiplicate;
            }
        }

        private DelegateCommand _commandResult;

        public DelegateCommand CommandResult
        {
            get
            {
                if (_commandResult == null)
                {
                    _commandResult = new DelegateCommand(Result);
                }
                return _commandResult;
            }
        }

        private DelegateCommand _commandAC;

        public DelegateCommand CommandAC
        {
            get
            {
                if (_commandAC == null)
                {
                    _commandAC = new DelegateCommand(_ => AC());            // здесь использовал лямбду, но можно поменять класс DelegateCommand на два конструктора чтобы принимал с параметрами и без или добавить object obj AC
                }
                return _commandAC;
            }
        }


        private DelegateCommand? _keyboardNumber;
        public DelegateCommand KeyboardNumber
        {
            get
            {
                if (_keyboardNumber == null)
                {
                    _keyboardNumber = new DelegateCommand(KeyBoardInput);
                }
                return _keyboardNumber;
            }
        }





        #endregion

        #region =========================== Methods =========================================
        private void OperationManagement(Operation op)
        {

            if (_currentOperation == Operation.Start)
            {
                _currentOperation = op;
                _inputStorage = Input;
                Input = null;   // убирает 
            }
            else
            {
                Result(_currentOperation,_inputStorage, _input, op);
            }
                
        }

        private void Add(double? storage, double? current, Operation newOp)
        {
            if (storage == null) return;

            if (current == null)
            {
                _currentOperation = newOp;
                return;
            }

            Input = storage + current;
            OnPropertyChanged(nameof(Input));

            _inputStorage = Input;
            
            _currentOperation = newOp;
        }
        private void Subtract(double? storage, double? current, Operation newOp)
        {
            if (storage == null) return;

            if (current == null)
            {
                _currentOperation = newOp;
                return;
            }

            Input = storage - current;
            OnPropertyChanged(nameof(Input));

            _inputStorage = Input;

            _currentOperation = newOp;
        }

        private void Divide(double? storage, double? current, Operation newOp)
        {
            if (storage == null) return;

            if (current == null)
            {
                _currentOperation = newOp;
                return;
            }

            Input = storage / current;
            OnPropertyChanged(nameof(Input));

            _inputStorage = Input;

            _currentOperation = newOp;
        }

        private void Multiply(double? storage, double? current, Operation newOp)
        {
            if (storage == null) return;

            if (current == null)
            {
                _currentOperation = newOp;
                return;
            }

            Input = storage * current;
            OnPropertyChanged(nameof(Input));

            _inputStorage = Input;

            _currentOperation = newOp;
        }

        private void AC()
        {
            _inputStorage = null;

            Input = null;

            InputString = null;

            _currentOperation = Operation.Start;
        }


        private void Result(object obj)
        {
            if (_inputStorage == null) return;

            Result(_currentOperation, _inputStorage, _input, Operation.Start);
            
        }

        private void Result(Operation? currentOp, double? storage, double? current, Operation newOp)
        {
            if (_currentOperation == null || _currentOperation == Operation.Start) return;

            switch (currentOp)
            {
                case Operation.Addition:
                    Add(storage, current, newOp);
                    break;
                case Operation.Subtraction:
                    Subtract(storage, current, newOp);
                    break;
                case Operation.Division:
                    Divide(storage, current, newOp);
                    break;
                case Operation.Multiplication:
                    Multiply(storage, current, newOp);
                    break;


            }
        }

        public void KeyBoardInput(object? obj)
        {
            string unit = (string)obj;

            if (unit.Contains('.') && InputString.Contains('.')) return;

            InputString += (string)obj;


        }

        public void RemoveLast()
        {
            if (!string.IsNullOrEmpty(InputString))
                InputString = InputString[..^1];
        }



        #endregion
    }
}
