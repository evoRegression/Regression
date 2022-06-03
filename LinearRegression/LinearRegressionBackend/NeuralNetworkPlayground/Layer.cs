using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.NeuralNetworkPlayground
{
    public class Layer
    {

        public Matrix<double> Weight;
        public Vector<double> Bias;
        public IActivationFunction ActivationFunction;

        public Layer(
            Matrix<double> weight,
            Vector<double> bias, 
            IActivationFunction activationFunction)
        {
            Weight = weight;
            Bias = bias;
            ActivationFunction = activationFunction;
        }

        public void Propagate(Propagation prop)
        {
            Vector<double> sum = Weight * prop.Output() + Bias;
            Vector<double> activation = ActivationFunction.Activation(sum);
            prop.WeightedSums.Add(sum);
            prop.Activations.Add(activation);
        }

    }
}
