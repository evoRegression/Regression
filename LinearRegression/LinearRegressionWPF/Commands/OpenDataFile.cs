using System;
using System.Windows.Input;

using Microsoft.Win32;

using LinearRegressionWPF.ViewModels;
using LinearRegressionBackend.DataProvider;

namespace LinearRegressionWPF.Commands
{
    internal class OpenDataFile : ICommand
    {
        private readonly LinearRegressionViewModel _viewModel;

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
            OpenFileDialog openFileDialog = new();
            if (openFileDialog.ShowDialog() == true)
            {
                _viewModel.Data = Numerical.LoadText(openFileDialog.FileName);
                _viewModel.RegressionPlot.UpdateDataSet(_viewModel.Data);
                _viewModel.PredictEnabled = false;

                _viewModel.NotifyPropertyChanged(nameof(_viewModel.TrainEnabled));
                _viewModel.NotifyPropertyChanged(nameof(_viewModel.PredictEnabled));
            }
        }
    }
}
