using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using LinearRegressionBackend.DataProvider;
using LinearRegressionBackend.MLContext;
using LinearRegressionBackend.MLModel;

using LinearRegressionWPF.Models;
using LinearRegressionWPF.Commands;
using System.Diagnostics;

namespace LinearRegressionWPF.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public RegressionPlot RegressionPlot { get; private set; }

        public string[] AvailableModelsArray { get; private set; }
        public string[] AvailableLossFunctionsArray { get; private set; }
        public string[] AvailableOptimizersArray { get; private set; }
        public double LearningRate { get; set; }

        public ICommand OpenDataFileCommand { get; private set; }
        public ICommand TrainCommand { get; private set; }
        public ICommand AddRandomLineCommand { get; private set; }

        private IDataProvider _dataProvider;
        private AvailableModel _selectedModel;
        private AvailableLossFunction _selectedLossFunction;
        private AvailableOptimizer _selectedOptimizer;

        private double _slope;
        private double _yIntercept;

        private enum AvailableModel
        {
            LinearRegressionModel
        }

        private enum AvailableLossFunction
        {
            LeastSquareError,
            LeastAbsoluteError
        }

        private enum AvailableOptimizer
        {
            GradientDescent,
            SimpleOrdinaryLeastSquare,
            QuadraticOrdinaryLeastSquare
        }

        public string SelectedModel
        {
            get
            {
                return Enum.GetName(typeof(AvailableModel), _selectedModel);
            }

            set
            {
                _selectedModel = (AvailableModel) Enum.Parse(typeof(AvailableModel), value);
            }
        }

        public string SelectedLossFunction
        {
            get
            {
                return Enum.GetName(typeof(AvailableLossFunction), _selectedLossFunction);
            }
            
            set
            {
                _selectedLossFunction = (AvailableLossFunction) Enum.Parse(typeof(AvailableLossFunction), value);
            }
        }

        public string SelectedOptimizer
        {
            get
            {
                return Enum.GetName(typeof(AvailableOptimizer), _selectedOptimizer);
            }
            
            set
            {
                _selectedOptimizer = (AvailableOptimizer) Enum.Parse(typeof(AvailableOptimizer), value);
            }
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
            const double DEFAULT_LEARNING_RATE = 0.01;

            RegressionPlot = new RegressionPlot();

            AvailableModelsArray = Enum.GetNames(typeof(AvailableModel));
            AvailableLossFunctionsArray = Enum.GetNames(typeof(AvailableLossFunction));
            AvailableOptimizersArray = Enum.GetNames(typeof(AvailableOptimizer));
            LearningRate = DEFAULT_LEARNING_RATE;

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

        public void train()
        {
            IMLContext context = new MLContext();

            ILossFunction lossFunction = _selectedLossFunction switch
            {
                AvailableLossFunction.LeastAbsoluteError => new LeastAbsoluteError(),
                AvailableLossFunction.LeastSquareError => new LeastSquareError(),
                _ => null
            };

            IOptimizer optimizer = _selectedOptimizer switch
            {
                AvailableOptimizer.GradientDescent => new GradientDescent(LearningRate),
                AvailableOptimizer.QuadraticOrdinaryLeastSquare => new QuadraticOrdinaryLeastSquare(),
                AvailableOptimizer.SimpleOrdinaryLeastSquare => new SimpleOrdinaryLeastSquare(),
                _ => null
            };

            IMLModel model = _selectedModel switch
            {
                AvailableModel.LinearRegressionModel
                    => new LinearRegressionModel(_slope, _yIntercept, optimizer, lossFunction),
                _ => null
            };

            /* TODO: implement training */
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
