using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Ink;
using System.Windows.Input;

using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.MLNeuralNetwork;
using LinearRegressionWPF.Commands;

namespace LinearRegressionWPF.ViewModels
{
    internal class ImageToVectorViewModel : INotifyPropertyChanged
    {
        public ICommand PredictCommand { get; set; }

        public NeuralNetwork NeuralNetwork { get; set; }

        public Vector<double> Result { get; set; }

        public bool IsNeuralNetworkLoaded { get; set; }

        private string myStringResult;

        public string StringResult
        {
            get { return myStringResult; }
            set
            {
                myStringResult = value;
                NotifyPropertyChanged(nameof(StringResult));
            }
        }

        public ImageToVectorViewModel()
        {
            PredictCommand = new PredictCommand(this);

            InitNeuralNetwork();

            myStringResult = "No result yet";


            _strokes = new StrokeCollection();
        }

        private async void InitNeuralNetwork()
        {
            IsNeuralNetworkLoaded = true;
            using FileStream stream = File.OpenRead(@"c:\trained_network.json");
            NeuralNetwork = await NeuralNetwork.Import(stream);
        }

        private readonly StrokeCollection _strokes;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        public StrokeCollection Drawing
        {
            get
            {
                return _strokes;
            }
        }
    }
}