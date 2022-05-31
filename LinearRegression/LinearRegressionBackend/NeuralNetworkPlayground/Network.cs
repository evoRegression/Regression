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
            Vector<double> weightGradient =
                Vector<double>.Build.Dense(Layers.Count);
            Vector<double> biasGradient =
                Vector<double>.Build.Dense(Layers.Count);
            Vector<double> gradient =
                Vector<double>.Build.Dense(2 * Layers.Count);

            double delta = prop.Output() - expected;

            for (int i = Layers.Count; i > 0; i--)
            {
                Layer layer = Layers[i - 1];
                double z = prop.WeightedSums[i];
                double a = prop.Activations[i - 1];

                delta *= layer.ActivationFunction.Derivative(z);

                weightGradient[i - 1] = delta * a;
                gradient[2 * i - 2] = delta * a;
                biasGradient[i - 1] = delta;
                gradient[2 * i - 1] = delta;

                delta *= layer.Weight;
            }

            double rateOfChange = gradient.L2Norm();

            for (int i = 0; i < Layers.Count; i++)
            {
                Layer layer = Layers[i];
                double weightDelta =
                    -learningRate * rateOfChange * weightGradient[i];
                double biasDelta =
                    -learningRate * rateOfChange * biasGradient[i];

                layer.Weight += weightDelta;
                layer.Bias += biasDelta;
            }
        }

    }
}
