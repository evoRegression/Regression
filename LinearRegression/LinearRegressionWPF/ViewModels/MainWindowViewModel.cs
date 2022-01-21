using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Linq;

using LinearRegressionBackend.DataProvider;
using LinearRegressionBackend.MLModel;
using LinearRegressionBackend.MLCommmons;

using LinearRegressionWPF.BackendFeatures.Models;
using LinearRegressionWPF.BackendFeatures.LossFunctions;
using LinearRegressionWPF.BackendFeatures.Optimizers;

using LinearRegressionWPF.Models;
using LinearRegressionWPF.Commands;

namespace LinearRegressionWPF.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
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
            ShowCommand = new Show(this);
            PredictCommand = new Predict(this);
        }

        #region Parameters

        private const string MODEL_NAMESPACE = "LinearRegressionWPF.BackendFeatures.Models";
        private const double DEFAULT_LEARNING_RATE = 0.001;
        private const int DEFAULT_EPOCHS = 100;
        private const int DEFAULT_STEP_SIZE = 10;

        public void InitParameters()
        {
            var q = from type in Assembly.GetExecutingAssembly().GetTypes()
                    where type.IsClass
                    && type.Namespace == MODEL_NAMESPACE
                    && type.GetInterfaces().Contains(typeof(IModelDescriptor))
                    select type;
            AvailableModelsArray = q.Select(type => (IModelDescriptor)Activator.CreateInstance(type)).ToArray();
            SelectedModel = AvailableModelsArray[0];

            LearningRate = DEFAULT_LEARNING_RATE;
            Epochs = DEFAULT_EPOCHS;
            StepSize = DEFAULT_STEP_SIZE;

            StepEnabled = false;
            ShowEnabled = false;
            PredictEnabled = false;
        }

        public ICommand OpenDataFileCommand { get; private set; }
        public IModelDescriptor[] AvailableModelsArray { get; private set; }

        public IModelDescriptor SelectedModel
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

        private IModelDescriptor _selectedModel;

        public ILossFunctionDescriptor[] AvailableLossFunctionsArray { get; private set; }
        public ILossFunctionDescriptor SelectedLossFunction { get; set; }
        public IOptimizerDescriptor[] AvailableOptimizersArray { get; private set; }

        public IOptimizerDescriptor SelectedOptimizer
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
                    LearningRateEnabled = value.SupportedParameters.Contains(IOptimizerDescriptor.Parameter.LearningRate);
                    NotifyPropertyChanged(nameof(LearningRateEnabled));

                    EpochsEnabled = value.IsIterative;
                    NotifyPropertyChanged(nameof(EpochsEnabled));
                    NotifyPropertyChanged(nameof(StepEnabled));
                }
            }
        }

        private IOptimizerDescriptor _selectedOptimizer;

        public double LearningRate { get; set; }
        public bool LearningRateEnabled { get; private set; }
        public int Epochs { get; set; }
        public bool EpochsEnabled { get; private set; }

        private double[][] _data;

        public void importDataSet(string fileName)
        {
            _data = new DataProvider().Import(fileName);
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

        public void train()
        {
            IMLModel model = SelectedModel.constructModel(SelectedLossFunction, SelectedOptimizer,
                LearningRate, Slope, YIntercept);
            _history = model.Fit(_data, _data.Select(point => point[1]).ToArray(), Epochs);
            _historyIndex = 0;
            StepEnabled = _selectedOptimizer.IsIterative;
            NotifyPropertyChanged(nameof(StepEnabled));
            ShowEnabled = true;
            NotifyPropertyChanged(nameof(ShowEnabled));
            PredictEnabled = false;
            NotifyPropertyChanged(nameof(PredictEnabled));
            RegressionPlot.updateDataSet(_data);
        }

        private List<History> _history;
        private int _historyIndex;

        public int StepSize { get; set; }
        public bool StepEnabled { get; private set; }
        public bool ShowEnabled { get; private set; }

        public ICommand StepCommand { get; private set; }
        public ICommand ShowCommand { get; private set; }

        public void step()
        {
            History current = _history[_historyIndex];
            updateRegressionLine(current.Thetas[MLCommons.SLOPE_INDEX], current.Thetas[MLCommons.INTERCEPT_INDEX]);

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

        public void show()
        {
            History current = _history.Last();
            updateRegressionLine(current.Thetas[MLCommons.SLOPE_INDEX], current.Thetas[MLCommons.INTERCEPT_INDEX]);

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

        public void predict()
        {
            Prediction = Slope * PredictDataPoint + YIntercept;
            NotifyPropertyChanged(nameof(Prediction));

            RegressionPlot.addDataPoint(new double[] { PredictDataPoint, Prediction });
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

        public void updateRegressionLine(double slope, double yIntercept)
        {
            RegressionPlot.updateRegressionLine(slope, yIntercept);
            _slope = slope;
            NotifyPropertyChanged(nameof(Slope));
            _yIntercept = yIntercept;
            NotifyPropertyChanged(nameof(YIntercept));
        }

        public ICommand AddRandomLineCommand { get; private set; }

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
