using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using LinearRegressionWPF.Models;
using LinearRegressionWPF.Commands;

namespace LinearRegressionWPF.ViewModels
{
    class MainWindowViewModel
    {
        public RegressionPlot RegressionPlot { get; private set; }
        public ICommand OpenDataFileCommand { get; private set; }

        public MainWindowViewModel()
        {
            RegressionPlot = new RegressionPlot();
            OpenDataFileCommand = new OpenDataFile(RegressionPlot);
        }
    }
}
