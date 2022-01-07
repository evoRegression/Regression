using System;
using System.Windows.Input;
using Microsoft.Win32;

using LinearRegressionWPF.Models;
using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands
{
    class Train : ICommand
    {
        private MainWindowViewModel _viewModel;

        public Train(MainWindowViewModel viewModel)
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
            // TODO: Update regression line
        }
    }
}
