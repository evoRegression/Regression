using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.NeuralNetworkPlayground
{
    public class Layer
    {

        public Matrix<double> Weight;
        public Vector<double> Bias;
        public IActivationFunction ActivationFunction;

        public Vector<double> WeightedSum;
        public Vector<double> Activation;

        public Layer(
            Matrix<double> weight,
            Vector<double> bias, 
            IActivationFunction activationFunction)
        {
            Weight = weight;
            Bias = bias;
            ActivationFunction = activationFunction;
        }

        public void Propagate(Vector<double> input)
        {
            WeightedSum = Weight * input + Bias;
            Activation = ActivationFunction.Activation(WeightedSum);
        }

    }
}
