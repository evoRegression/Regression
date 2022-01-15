using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Linq;

using LinearRegressionBackend.DataProvider;
using LinearRegressionBackend.MLContext;
using LinearRegressionBackend.MLModel;

using LinearRegressionWPF.BackendFeatures.Models;
using LinearRegressionWPF.BackendFeatures.LossFunctions;
using LinearRegressionWPF.BackendFeatures.Optimizers;

using LinearRegressionWPF.Models;
using LinearRegressionWPF.Commands;
using System.Diagnostics;
using System;

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
        }

        #region Parameters

        private const string MODEL_NAMESPACE = "LinearRegressionWPF.BackendFeatures.Models";
        private const double DEFAULT_LEARNING_RATE = 0.01;
        private const int DEFAULT_EPOCHS = 1;

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

                    EpochsEnabled = StepEnabled = value.IsIterative;
                    NotifyPropertyChanged(nameof(EpochsEnabled));
                    NotifyPropertyChanged(nameof(StepEnabled));
                }
            }
        }

        private IOptimizerDescriptor _selectedOptimizer;

        public double LearningRate { get; set; }
        public bool LearningRateEnabled { get; private set; }
        public ICommand TrainCommand { get; private set; }

        public bool TrainEnabled
        {
            get
            {
                return _data != null && _data.Length > 0;
            }
        }

        public int Epochs { get; set; }
        public bool EpochsEnabled { get; private set; }

        private double[][] _data;

        public void importDataSet(string fileName)
        {
            _data = new DataProvider().Import(fileName);
            RegressionPlot.updateDataSet(_data);
            NotifyPropertyChanged(nameof(TrainEnabled));
        }

        public void train()
        {
            IMLModel model = SelectedModel.constructModel(SelectedLossFunction, SelectedOptimizer,
                LearningRate, Slope, YIntercept);
            var history = model.Fit(_data, _data.Select(point => point[1]).ToArray(), Epochs);
            var result = history.Last();
            updateRegressionLine(result.Thetas[0], result.Thetas[1]);
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

        public bool StepEnabled { get; private set; }

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
