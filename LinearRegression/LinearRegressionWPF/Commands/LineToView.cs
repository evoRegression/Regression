using System;
using System.Windows.Input;

using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands {
    class LineToView : ICommand {
        private MainWindowViewModel _viewModel;

        public LineToView(MainWindowViewModel viewModel) {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            _viewModel.FitRegressionLineToView();
        }
    }
}
