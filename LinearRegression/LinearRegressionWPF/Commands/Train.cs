using System;
using System.Windows.Input;

using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands
{
    internal class Train : ICommand
    {
        private LinearRegressionViewModel _viewModel;

        public Train(LinearRegressionViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.Train();
        }
    }
}
