using System;
using System.Windows.Input;
using Microsoft.Win32;

using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands
{
    class OpenDataFile : ICommand
    {
        private LinearRegressionViewModel _viewModel;

        public OpenDataFile(LinearRegressionViewModel viewModel)
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                _viewModel.importDataSet(openFileDialog.FileName);
            }
        }
    }
}
