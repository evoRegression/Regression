using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using LinearRegressionWPF.BackendDescriptors;
using LinearRegressionWPF.Commands;
using LinearRegressionWPF.Models;
using LinearRegressionBackend.DataProvider;
using LinearRegressionBackend.MLCommmons;
using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.ViewModels
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            RegressionPlot = new RegressionPlot();
            InitParameters();
            InitCommands();
        }

        public void InitCommands()
        {
            OpenDataFileCommand = new OpenDataFile(this);
            TrainCommand = new Train(this);
            AddRandomLineCommand = new AddRandomLine(this);
            StepCommand = new Step(this);
            AnimateCommand = new Animate(this);
            ShowCommand = new Show(this);
            PredictCommand = new Predict(this);
            ResetGraphViewCommand = new ResetGraphView(this);
            LineToViewCommand = new LineToView(this);
        }

        #region Parameters

        private const double DEFAULT_LEARNING_RATE = 0.001;
        private const int DEFAULT_EPOCHS = 100;
        private const int DEFAULT_STEP_SIZE = 10;

        public void InitParameters()
        {
            AvailableModelsArray = AvailableModels.AvailableModelsArray;
            SelectedModel = AvailableModelsArray[0];

            LearningRate = DEFAULT_LEARNING_RATE;
            Epochs = DEFAULT_EPOCHS;
            StepSize = DEFAULT_STEP_SIZE;

            StepEnabled = false;
            ShowEnabled = false;
            PredictEnabled = false;
        }

        public ICommand OpenDataFileCommand { get; private set; }
        public ModelDescriptor[] AvailableModelsArray { get; private set; }

        public ModelDescriptor SelectedModel
        {
            get
            {
                return _selectedModel;
            }

            set
            {
                _selectedModel = value;

                AvailableLossFunctionsArray = value.SupportedLossFunctions;
                NotifyPropertyChanged(nameof(AvailableLossFunctionsArray));
                SelectedLossFunction = AvailableLossFunctionsArray[0];
                NotifyPropertyChanged(nameof(SelectedLossFunction));

                AvailableOptimizersArray = value.SupportedOptimizers;
                NotifyPropertyChanged(nameof(AvailableOptimizersArray));
                SelectedOptimizer = AvailableOptimizersArray[0];
                NotifyPropertyChanged(nameof(SelectedOptimizer));
            }
        }

        private ModelDescriptor _selectedModel;

        public LossFunctionDescriptor[] AvailableLossFunctionsArray { get; private set; }
        public LossFunctionDescriptor SelectedLossFunction { get; set; }
        public OptimizerDescriptor[] AvailableOptimizersArray { get; private set; }

        public OptimizerDescriptor SelectedOptimizer
        {
            get
            {
                return _selectedOptimizer;
            }

            set
            {
                _selectedOptimizer = value;

                if (value != null)
                {
                    LearningRateEnabled = value.SupportedParameters.Contains(OptimizerBuilderParams.Parameter.LearningRate);
                    NotifyPropertyChanged(nameof(LearningRateEnabled));

                    EpochsEnabled = value.IsIterative;
                    NotifyPropertyChanged(nameof(EpochsEnabled));
                    NotifyPropertyChanged(nameof(StepEnabled));
                }
            }
        }

        private OptimizerDescriptor _selectedOptimizer;

        public double LearningRate { get; set; }
        public bool LearningRateEnabled { get; private set; }
        public int Epochs { get; set; }
        public bool EpochsEnabled { get; private set; }

        private double[][] _data;

        public void ImportDataSet(string fileName)
        {
            _data = Numerical.LoadText(fileName);
            RegressionPlot.updateDataSet(_data);
            NotifyPropertyChanged(nameof(TrainEnabled));
            PredictEnabled = false;
            NotifyPropertyChanged(nameof(PredictEnabled));
        }

        public ICommand TrainCommand { get; private set; }

        public bool TrainEnabled
        {
            get
            {
                return _data != null && _data.Length > 0;
            }
        }

        public void Train()
        {
            IMLModel model = SelectedModel.BuildModel(new ModelBuilderParams
            {
                LossFunctionDesc = SelectedLossFunction,
                OptimizerDesc = SelectedOptimizer,
                LearningRate = LearningRate,
                Slope = Slope,
                YIntercept = YIntercept
            });
            _history = model.Train(_data, _data.Select(point => point[1]).ToArray(), Epochs);
            _historyIndex = 0;
            StepEnabled = _selectedOptimizer.IsIterative;
            NotifyPropertyChanged(nameof(StepEnabled));
            ShowEnabled = true;
            NotifyPropertyChanged(nameof(ShowEnabled));
            PredictEnabled = false;
            NotifyPropertyChanged(nameof(PredictEnabled));
            RegressionPlot.clearPredictions();
        }

        private List<History> _history;
        private int _historyIndex;

        public int StepSize { get; set; }
        public bool StepEnabled { get; private set; }
        public bool ShowEnabled { get; private set; }

        public ICommand StepCommand { get; private set; }
        public ICommand AnimateCommand { get; private set; }
        public ICommand ShowCommand { get; private set; }

        public void Step()
        {
            History current = _history[_historyIndex];
            UpdateRegressionLine(current.Thetas[MLCommons.SLOPE_INDEX], current.Thetas[MLCommons.INTERCEPT_INDEX]);

            _historyIndex += StepSize;

            if (_historyIndex >= _history.Count)
            {
                StepEnabled = ShowEnabled = false;
                NotifyPropertyChanged(nameof(StepEnabled));
                NotifyPropertyChanged(nameof(ShowEnabled));
                PredictEnabled = true;
                NotifyPropertyChanged(nameof(PredictEnabled));
            }
        }

        public void Show()
        {
            History current = _history.Last();
            UpdateRegressionLine(current.Thetas[MLCommons.SLOPE_INDEX], current.Thetas[MLCommons.INTERCEPT_INDEX]);

            StepEnabled = ShowEnabled = false;
            NotifyPropertyChanged(nameof(StepEnabled));
            NotifyPropertyChanged(nameof(ShowEnabled));
            PredictEnabled = true;
            NotifyPropertyChanged(nameof(PredictEnabled));
        }

        #endregion

        #region Predict

        public bool PredictEnabled { get; private set; }
        public double PredictDataPoint { get; set; }
        public double Prediction { get; set; }
        public ICommand PredictCommand { get; set; }

        public void Predict()
        {
            Prediction = Slope * PredictDataPoint + YIntercept;
            NotifyPropertyChanged(nameof(Prediction));

            RegressionPlot.addPredictedPoint(new double[] { PredictDataPoint, Prediction });
            FitRegressionLineToView();
        }

        public void FitRegressionLineToView()
        {
            RegressionPlot.updateRegressionLine(Slope, YIntercept);
        }

        #endregion

        #region Graph

        public RegressionPlot RegressionPlot { get; private set; }

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

        private double _slope;

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

        private double _yIntercept;

        public void UpdateRegressionLine(double slope, double yIntercept)
        {
            RegressionPlot.updateRegressionLine(slope, yIntercept);
            _slope = slope;
            NotifyPropertyChanged(nameof(Slope));
            _yIntercept = yIntercept;
            NotifyPropertyChanged(nameof(YIntercept));
        }

        public ICommand AddRandomLineCommand { get; private set; }
        public ICommand ResetGraphViewCommand { get; private set; }
        public ICommand LineToViewCommand { get; private set; }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
