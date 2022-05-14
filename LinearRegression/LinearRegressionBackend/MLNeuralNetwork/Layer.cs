using System.Diagnostics;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class Layer
    {
        public Matrix<double> Weights { get; set; }
        public Matrix<double> WeightsGradient { get; set; }

        public Vector<double> Biases { get; set; }
        public Vector<double> BiasesGradient { get; set; }

        public Vector<double> WeightedSums { get; set; }
        public Vector<double> Activations { get; set; }

        public IActivationFunction ActivationFunction { get; set; }

        public int NeuronCount
        {
            get { return Biases.Count; }
        }

        public Layer(Matrix<double> weights, Vector<double> biases, IActivationFunction activationFunction)
        {
            Debug.Assert(weights.RowCount == biases.Count);
            Weights = weights;
            Biases = biases;
            ActivationFunction = activationFunction;
        }

        public void PropagateForward(Vector<double> inputData)
        {
            WeightedSums = CalculateWeightedSums(inputData);
            Activations = CalculateActivations(WeightedSums);
        }

        private Vector<double> CalculateWeightedSums(Vector<double> inputData)
        {
            Debug.Assert(inputData.Count == Weights.ColumnCount);
            return Weights * inputData + Biases;
        }

        private Vector<double> CalculateActivations(Vector<double> weightedSums)
        {
            return Vector<double>.Build.Dense(
                weightedSums.Count,
                i => ActivationFunction.Activation(weightedSums[i])
            );
        }

        public void Update(double rateOfChange, double learningRate)
        {
            Weights += learningRate * rateOfChange * WeightsGradient;
        }
    }
}
