﻿using System;
using System.Windows.Input;

using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands {
    class ResetGraphView : ICommand {
        private MainWindowViewModel _viewModel;

        public ResetGraphView(MainWindowViewModel viewModel) {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            _viewModel.RegressionPlot.OxyModel.ResetAllAxes();
            _viewModel.RegressionPlot.OxyModel.InvalidatePlot(true);
        }
    }
}