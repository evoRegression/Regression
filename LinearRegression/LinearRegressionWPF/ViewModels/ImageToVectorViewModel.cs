using LinearRegressionBackend.MLNeuralNetwork;
using LinearRegressionWPF.Commands;
using MathNet.Numerics.LinearAlgebra;
using System.Windows.Ink;
using System.Windows.Input;

namespace LinearRegressionWPF.ViewModels
{
    internal class ImageToVectorViewModel
    {
        public ICommand PredictCommand { get; set; }

        public NeuralNetwork NeuralNetwork { get; set; }

        public  Vector<double> Result{ get; set;}

        public ImageToVectorViewModel()
        {
            PredictCommand = new PredictCommand(this);

            InitNeuralNetwork();

            _strokes = new StrokeCollection();
            /*(_strokes as INotifyCollectionChanged).CollectionChanged += delegate
            {
                //the strokes have changed
            };*/
        }

        private void InitNeuralNetwork()
        {
            //NeuralNetwork = NeuralNetwork.Import(File.OpenRead("placeholder")).Result;
        }

        private readonly StrokeCollection _strokes;

        public StrokeCollection Drawing
        {
            get
            {
                return _strokes;
            }
        }
    }
}