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
        public DataProvider DataProvider { get; set; }
        public RegressionPlot RegressionPlot { get; private set; }
        public ICommand OpenDataFileCommand { get; private set; }
        public ICommand TrainCommand { get; private set; }
        public ICommand AddRandomLineCommand { get; private set; }

        private double _slope;
        private double _yIntercept;

        public MainWindowViewModel()
        {
            RegressionPlot = new RegressionPlot();
            OpenDataFileCommand = new OpenDataFile(this);
            TrainCommand = new Train(this);
            AddRandomLineCommand = new AddRandomLine(this);
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
                updateRegressionLine(_slope, _yIntercept);
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
                updateRegressionLine(_slope, _yIntercept);
                NotifyPropertyChanged(nameof(YIntercept));
            }
        }

        private void updateRegressionLine(double slope, double yIntercept)
        {
            RegressionLine line = new RegressionLine(slope, yIntercept, 0, 10);
            RegressionPlot.updateRegressionLine(line);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
