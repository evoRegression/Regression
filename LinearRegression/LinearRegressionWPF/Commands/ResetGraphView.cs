using System;
using System.Windows.Input;

using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands
{
    internal class ResetGraphView : ICommand
    {
        private LinearRegressionViewModel _viewModel;

        public ResetGraphView(LinearRegressionViewModel viewModel)
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
            _viewModel.RegressionPlot.OxyModel.ResetAllAxes();
            _viewModel.RegressionPlot.OxyModel.InvalidatePlot(true);
        }
    }
}
