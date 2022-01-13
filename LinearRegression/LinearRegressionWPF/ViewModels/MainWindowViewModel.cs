using System.Windows.Input;

using LinearRegressionBackend.DataProvider;
using LinearRegressionWPF.Models;
using LinearRegressionWPF.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace LinearRegressionWPF.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public RegressionPlot RegressionPlot { get; private set; }
        public ICommand OpenDataFileCommand { get; private set; }
        public ICommand TrainCommand { get; private set; }
        public ICommand AddRandomLineCommand { get; private set; }

        private DataProvider _dataProvider;
        private double _slope;
        private double _yIntercept;

        public double Slope
        {
            get
            {
                return _slope;
            }

            set
            {
                _slope = value;
                NotifyPropertyChanged(nameof(TextBoxSlope));
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
                NotifyPropertyChanged(nameof(TextBoxYIntercept));
            }
        }

        public double TextBoxSlope
        {
            get
            {
                return Slope;
            }

            set
            {
                Slope = value;
                RegressionPlot.updateRegressionLine(Slope, YIntercept);
                NotifyPropertyChanged(nameof(TextBoxSlope));
            }
        }

        public double TextBoxYIntercept
        {
            get
            {
                return YIntercept;
            }

            set
            {
                YIntercept = value;
                RegressionPlot.updateRegressionLine(Slope, YIntercept);
                NotifyPropertyChanged(nameof(TextBoxYIntercept));
            }
        }

        public MainWindowViewModel()
        {
            RegressionPlot = new RegressionPlot();
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
            Slope = slope;
            YIntercept = yIntercept;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
