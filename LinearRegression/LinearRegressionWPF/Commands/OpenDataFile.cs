using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Win32;

using LinearRegressionWPF.Models;

namespace LinearRegressionWPF.Commands
{
    class OpenDataFile : ICommand
    {
        private RegressionPlot _target;

        public OpenDataFile(RegressionPlot target)
        {
            _target = target;
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
                DataProvider.DataProvider dp = new DataProvider.DataProvider(openFileDialog.FileName);
                DataSet dataSet = new DataSet();
                dataSet.addDataPoint(1, 1);
                dataSet.addDataPoint(2, 2);
                _target.updateDataSet(dataSet);
            }
        }
    }
}
