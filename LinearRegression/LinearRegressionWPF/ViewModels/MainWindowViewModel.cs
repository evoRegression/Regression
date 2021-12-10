using System.Windows.Input;

using LinearRegressionWPF.Models;
using LinearRegressionWPF.Commands;

namespace LinearRegressionWPF.ViewModels
{
    class MainWindowViewModel
    {
        public DataProvider.DataProvider DataProvider { get; set; }
        public RegressionPlot RegressionPlot { get; private set; }
        public ICommand OpenDataFileCommand { get; private set; }
        public ICommand TrainCommand { get; private set; }

        public MainWindowViewModel()
        {
            RegressionPlot = new RegressionPlot();
            OpenDataFileCommand = new OpenDataFile(this);
            TrainCommand = new Train(this);
        }
    }
}
