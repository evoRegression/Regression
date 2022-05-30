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

            foreach (Layer layer in Layers)
            {
                (double sum, double activation) = layer.Propagate(input);
                result.WeightedSums.Add(sum);
                result.Activations.Add(activation);
                input = activation;
            }

            return result;
        }

    }
}
