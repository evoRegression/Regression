using System.Diagnostics;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class Layer
    {
        public Matrix<double> Weights { get; set; }

        public Vector<double> Biases { get; set; }

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

        public Vector<double> WeightedSums(Vector<double> previousLayer)
        {
            Debug.Assert(previousLayer.Count == Weights.ColumnCount);
            return Weights * previousLayer + Biases;
        }

        public Vector<double> Activations(Vector<double> weightedSums)
        {
            return Vector<double>.Build.Dense(
                weightedSums.Count,
                i => ActivationFunction.Activation(weightedSums[i])
            );
        }
    }
}
