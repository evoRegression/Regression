namespace LinearRegressionBackend.NeuralNetworkPlayground
{
    public class Layer
    {

        public double Weight;
        public double Bias;
        public IActivationFunction ActivationFunction;

        public Layer(
            double weight, 
            double bias, 
            IActivationFunction activationFunction)
        {
            Weight = weight;
            Bias = bias;
            ActivationFunction = activationFunction;
        }

        public (double sum, double activation) Propagate(double input)
        {
            double sum = Weight * input + Bias;
            double activation = ActivationFunction.Activation(sum);
            return (sum, activation);
        }

    }
}
