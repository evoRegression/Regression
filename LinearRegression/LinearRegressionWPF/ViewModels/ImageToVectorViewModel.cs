using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Ink;
using System.Windows.Input;

using MathNet.Numerics.LinearAlgebra;

using LinearRegressionBackend.MLNeuralNetwork;
using LinearRegressionWPF.Commands;
using System;
using System.Text.RegularExpressions;

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

        public void FormateStringResult(string resultText)
        {
            //data comes in like this:
            //"Triangle\r\n0,999999999 1,000000000\r\n"//
            string[] shape;
            string[] values;
            shape = Regex.Split(resultText,"\r\n");
            values = Regex.Split(shape[1], " ");
            double value1 = Convert.ToDouble(values[0].Substring(0, 10));
            double value2 = Convert.ToDouble(values[1].Substring(0, 10));
            StringResult = shape[0] + "\r\n" + "Circle: " + Convert.ToString(value1)  
                                    + "\r\n" + "Triangle: " +   Convert.ToString(value2); 
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
            using FileStream stream = File.OpenRead(@"c:\trained_network_88_percent.json");
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