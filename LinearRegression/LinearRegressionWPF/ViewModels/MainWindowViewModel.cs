using System.Windows.Input;

using LinearRegressionBackend.DataProvider;
using LinearRegressionWPF.Models;
using LinearRegressionWPF.Commands;
using LinearRegressionBackend.DataProvider;

namespace LinearRegressionWPF.ViewModels
{
    class MainWindowViewModel
    {
        public DataProvider DataProvider { get; set; }
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
