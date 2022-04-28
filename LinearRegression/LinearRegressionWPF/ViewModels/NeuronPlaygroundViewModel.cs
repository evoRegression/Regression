using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.MLNeuralNetwork;

using LinearRegressionWPF.Commands;
using LinearRegressionWPF.BackendDescriptors;


namespace LinearRegressionWPF.ViewModels
{
    internal class NeuronPlaygroundViewModel : INotifyPropertyChanged
    {
        public NeuronPlaygroundViewModel()
        {
            InitWeights();
            InitActivation();
            InitCommands();
        }

        public void InitCommands()
        {
            // pass
        }

        #region Weights

        public void InitWeights()
        {
            // pass
        }

        #endregion

        #region Activation

        public ActivationFunctionDescriptor[] AvailableActivationFunctionsArray { get; private set; }
        public ActivationFunctionDescriptor SelectedActivationFunction {
            get { return _SelectedActivationFunction; }
            set {
                _SelectedActivationFunction = value;
                ActivationFunction = value.BuildActivationFunction();
                NotifyPropertyChanged(nameof(ActivationFunction));
                UpdateNeuron();
            }
        }
        private ActivationFunctionDescriptor _SelectedActivationFunction;

        private Vector<double> _PreviousLayerMock;

        private Vector<double> _WeightsMock;
        public double BiasMock {
            get { return _BiasMock; }
            set
            {
                _BiasMock = value;
                NotifyPropertyChanged(nameof(BiasMock));
                UpdateNeuron();
            }
        }
        private double _BiasMock;

        public IActivationFunction ActivationFunction { get; set; }
        public Neuron Neuron { get; set; }

        private void UpdateNeuron()
        {
            int DECIMAL_PLACES = 2;
            MidpointRounding MID_ROUND = MidpointRounding.ToEven;

            Neuron = new Neuron(_WeightsMock, BiasMock, ActivationFunction);
            WeightedSum = Math.Round(
                Neuron.WeightedSum(_PreviousLayerMock),
                DECIMAL_PLACES, MID_ROUND
            );
            NotifyPropertyChanged(nameof(WeightedSum));

            Activation = Math.Round(
                Neuron.Activation(_PreviousLayerMock),
                DECIMAL_PLACES, MID_ROUND
            );
            NotifyPropertyChanged(nameof(Activation));
        }

        public double WeightedSum { get; set; }
        public double Activation { get; set; }

        public void InitActivation()
        {
            InitActivationMocks();
            AvailableActivationFunctionsArray = AvailableActivationFunctions.AvailableActivationFunctionsArray;
            SelectedActivationFunction = AvailableActivationFunctionsArray[0];
        }

        public void InitActivationMocks()
        {
            _PreviousLayerMock = Vector<double>.Build.Dense(
                new double[] { 1, 0, 1, 0, 1, 0, 1, 0, 1 }
            );

            _WeightsMock = Vector<double>.Build.Dense(
                new double[] { 0.1, 0.8, 0.3, 0.6, 0.8, 0.5, 0.1, 0.7, 0.3 }
            );
            _BiasMock = 0.9;
        }

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
