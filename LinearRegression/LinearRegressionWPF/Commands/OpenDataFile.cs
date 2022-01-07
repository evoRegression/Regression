﻿using System;
using System.Windows.Input;
using Microsoft.Win32;

using LinearRegressionBackend.DataProvider;
using LinearRegressionWPF.Models;
using LinearRegressionWPF.ViewModels;
using LinearRegressionBackend.DataProvider;

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
                _viewModel.importDataSet(openFileDialog.FileName);
            }
        }
    }
}
