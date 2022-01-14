using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using LinearRegressionBackend.DataProvider;

using LinearRegressionWPF.Models;
using LinearRegressionWPF.Commands;

namespace LinearRegressionWPF.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public RegressionPlot RegressionPlot { get; private set; }
        public string[] AvailableModelsArray { get; private set; }
        public ICommand OpenDataFileCommand { get; private set; }
        public ICommand TrainCommand { get; private set; }
        public ICommand AddRandomLineCommand { get; private set; }

        private DataProvider _dataProvider;
        private double _slope;
        private double _yIntercept;

        private enum AvailableModels
        {
            LinearRegressionModel
        }

        public double Slope
        {
            get
            {
                return _slope;
            }

            set
            {
                _slope = value;
                RegressionPlot.updateRegressionLine(_slope, _yIntercept);
                NotifyPropertyChanged(nameof(Slope));
            }
        }

        public double YIntercept
        {
            get
            {
                return _yIntercept;
            }

            set
            {
                _yIntercept = value;
                RegressionPlot.updateRegressionLine(_slope, _yIntercept);
                NotifyPropertyChanged(nameof(YIntercept));
            }
        }

        public MainWindowViewModel()
        {
            RegressionPlot = new RegressionPlot();
            AvailableModelsArray = Enum.GetNames(typeof(AvailableModels));

            OpenDataFileCommand = new OpenDataFile(this);
            TrainCommand = new Train(this);
            AddRandomLineCommand = new AddRandomLine(this);
        }

        public void importDataSet(string fileName)
        {
            _dataProvider = new DataProvider();
            double[][] data = _dataProvider.Import(fileName);

            RegressionPlot.updateDataSet(data);
        }

        public void updateRegressionLine(double slope, double yIntercept)
        {
            RegressionPlot.updateRegressionLine(slope, yIntercept);
            _slope = slope;
            NotifyPropertyChanged(nameof(Slope));
            _yIntercept = yIntercept;
            NotifyPropertyChanged(nameof(YIntercept));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
