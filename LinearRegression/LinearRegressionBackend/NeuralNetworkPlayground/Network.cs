using System.Collections.Generic;

namespace LinearRegressionBackend.NeuralNetworkPlayground
{
    public class Network
    {

        public List<Layer> Layers;

        public Network(List<Layer> layers)
        {
            Layers = layers;
        }

        public Propagation Propagate(double input)
        {
            Propagation result = new();

            result.WeightedSums.Add(0);
            result.Activations.Add(input);

            foreach (Layer layer in Layers)
            {
                (double sum, double activation) = layer.Propagate(input);
                result.WeightedSums.Add(sum);
                result.Activations.Add(activation);
                input = activation;
            }

            return result;
        }

        public void Backpropagate(
            Propagation prop,
            double expected,
            double learningRate)
        {
            double delta = prop.Output() - expected;

            for (int i = Layers.Count; i > 0; i--)
            {
                Layer layer = Layers[i - 1];
                double z = prop.WeightedSums[i];
                double a = prop.Activations[i - 1];

                delta *= layer.ActivationFunction.Derivative(z);

                double weightDelta = -learningRate * delta * a;
                double biasDelta = -learningRate * delta;

                layer.Weight += weightDelta;
                layer.Bias += biasDelta;

                delta *= layer.Weight;
            }
        }

    }
}
