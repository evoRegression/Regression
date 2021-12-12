using System;
using System.Windows.Input;
using Microsoft.Win32;

using LinearRegressionBackend.DataProvider;
using LinearRegressionWPF.Models;
using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands
{
    class OpenDataFile : ICommand
    {
        private MainWindowViewModel _viewModel;

        public OpenDataFile(MainWindowViewModel viewModel)
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
                DataProvider dp = new DataProvider();
                double[][] data = dp.Import(openFileDialog.FileName);

                _viewModel.DataProvider = dp;

                DataSet dataSet = new DataSet();

                foreach (double[] dataPoint in data)
                {
                    dataSet.addDataPoint(dataPoint[0], dataPoint[1]);
                }

                _viewModel.RegressionPlot.updateDataSet(dataSet);
            }
        }
    }
}
