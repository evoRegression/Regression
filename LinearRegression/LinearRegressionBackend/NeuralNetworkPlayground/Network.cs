using System.Collections.Generic;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.NeuralNetworkPlayground
{
    public class Network
    {

        public List<Layer> Layers;

        public Network(List<Layer> layers)
        {
            Layers = layers;
        }

        public Propagation Propagate(Vector<double> input)
        {
            Propagation result = new();

            result.WeightedSums.Add(null);
            result.Activations.Add(input);

            foreach (Layer layer in Layers)
            {
                (Vector<double> sum, Vector<double> activation) = layer.Propagate(input);
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
