﻿using System;
using System.Windows.Input;
using Microsoft.Win32;

using LinearRegressionBackend.DataProvider;
using LinearRegressionWPF.Models;
using LinearRegressionWPF.ViewModels;

namespace LinearRegressionWPF.Commands
{
    class AddRandomLine : ICommand
    {
        private MainWindowViewModel _viewModel;

        public AddRandomLine(MainWindowViewModel viewModel)
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
            Random random = new Random();

            double SLOPE_MIN = 1;
            double SLOPE_MAX = 10;
            double SLOPE_RANGE = SLOPE_MAX - SLOPE_MIN;
            double slope = SLOPE_MIN + random.NextDouble() * SLOPE_RANGE;

            double INTC_MIN = -5;
            double INTC_MAX = 10;
            double INTC_RANGE = INTC_MAX - INTC_MIN;
            double yIntercept = INTC_MIN + random.NextDouble() * INTC_RANGE;

            _viewModel.Slope = slope;
            _viewModel.YIntercept = yIntercept;
        }
    }
}