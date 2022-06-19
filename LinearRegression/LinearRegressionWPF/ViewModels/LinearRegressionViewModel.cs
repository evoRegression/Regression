using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using LinearRegressionBackend;
using LinearRegressionBackend.MLCommmons;

using LinearRegressionWPF.BackendDescriptors;
using LinearRegressionWPF.Commands;
using LinearRegressionWPF.Models;

namespace LinearRegressionWPF.ViewModels
{
    internal class LinearRegressionViewModel : INotifyPropertyChanged
    {
        public LinearRegressionViewModel()
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

        public ICommand OpenDataFileCommand { get; set; }
        public ModelDescriptor[] AvailableModelsArray { get; set; }

        public ModelDescriptor SelectedModel
        {
            get => _selectedModel;

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

        public LossFunctionDescriptor[] AvailableLossFunctionsArray { get; set; }
        public LossFunctionDescriptor SelectedLossFunction { get; set; }
        public EstimatorDescriptor[] AvailableOptimizersArray { get; set; }

        public EstimatorDescriptor SelectedOptimizer
        {
            get => _selectedOptimizer;

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

        private EstimatorDescriptor _selectedOptimizer;

        public double LearningRate { get; set; }
        public bool LearningRateEnabled { get; set; }
        public int Epochs { get; set; }
        public bool EpochsEnabled { get; set; }
        public double[][] Data;

        public ICommand TrainCommand { get; set; }

        public bool TrainEnabled => Data != null && Data.Length > 0;

        // TODO: Move logic to the Train command
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
            _history = model.Train(Data, Data.Select(point => point[1]).ToArray(), Epochs);
            _historyIndex = 0;
            StepEnabled = _selectedOptimizer.IsIterative;
            NotifyPropertyChanged(nameof(StepEnabled));
            ShowEnabled = true;
            NotifyPropertyChanged(nameof(ShowEnabled));
            PredictEnabled = false;
            NotifyPropertyChanged(nameof(PredictEnabled));
            RegressionPlot.ClearPredictions();
        }

        private List<History> _history;
        private int _historyIndex;

        public int StepSize { get; set; }
        public bool StepEnabled { get; set; }
        public bool ShowEnabled { get; set; }

        public ICommand StepCommand { get; set; }
        public ICommand AnimateCommand { get; set; }
        public ICommand ShowCommand { get; set; }

        // TODO: Move logic to the Step command
        public void Step()
        {
            History current = _history[_historyIndex];
            UpdateRegressionLine(current.Parameters[MLCommons.SLOPE_INDEX], current.Parameters[MLCommons.INTERCEPT_INDEX]);

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

        // TODO: Move logic to the Show command
        public void Show()
        {
            History current = _history.Last();
            UpdateRegressionLine(current.Parameters[MLCommons.SLOPE_INDEX], current.Parameters[MLCommons.INTERCEPT_INDEX]);

            StepEnabled = ShowEnabled = false;
            NotifyPropertyChanged(nameof(StepEnabled));
            NotifyPropertyChanged(nameof(ShowEnabled));
            PredictEnabled = true;
            NotifyPropertyChanged(nameof(PredictEnabled));
        }

        #endregion

        #region Predict

        public bool PredictEnabled { get; set; }
        public double PredictDataPoint { get; set; }
        public double Prediction { get; set; }
        public ICommand PredictCommand { get; set; }

        // TODO: Move logic to the Predict command
        public void Predict()
        {
            Prediction = Slope * PredictDataPoint + YIntercept;
            NotifyPropertyChanged(nameof(Prediction));

            RegressionPlot.AddPredictedPoint(new double[] { PredictDataPoint, Prediction });
            FitRegressionLineToView();
        }

        // TODO: Move logic to the LineToView command
        public void FitRegressionLineToView()
        {
            RegressionPlot.UpdateRegressionLine(Slope, YIntercept);
        }

        #endregion

        #region Graph

        public RegressionPlot RegressionPlot { get; set; }

        public double Slope
        {
            get
            {
                return _slope;
            }

            set
            {
                _slope = value;
                RegressionPlot.UpdateRegressionLine(_slope, _yIntercept);
                NotifyPropertyChanged(nameof(Slope));
            }
        }

        private double _slope;

        public double YIntercept
        {
            get => _yIntercept;

            set
            {
                _yIntercept = value;
                RegressionPlot.UpdateRegressionLine(_slope, _yIntercept);
                NotifyPropertyChanged(nameof(YIntercept));
            }
        }

        private double _yIntercept;

        public void UpdateRegressionLine(double slope, double yIntercept)
        {
            RegressionPlot.UpdateRegressionLine(slope, yIntercept);
            _slope = slope;
            NotifyPropertyChanged(nameof(Slope));
            _yIntercept = yIntercept;
            NotifyPropertyChanged(nameof(YIntercept));
        }

        public ICommand AddRandomLineCommand { get; set; }
        public ICommand ResetGraphViewCommand { get; set; }
        public ICommand LineToViewCommand { get; set; }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
