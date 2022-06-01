using System.Collections.Generic;
using System.Diagnostics;

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
            Vector<double> expected,
            double learningRate)
        {
            Debug.Assert(prop.Output().Count == expected.Count);

            Matrix<double>[] weightGradient =
                new Matrix<double>[Layers.Count];
            Vector<double>[] biasGradient =
                new Vector<double>[Layers.Count];

            Vector<double> delta = prop.Output() - expected;

            for (int i = Layers.Count; i > 0; i--)
            {
                Layer layer = Layers[i - 1];
                Vector<double> z = prop.WeightedSums[i];
                Vector<double> a = prop.Activations[i - 1];

                Vector<double> delz = layer.ActivationFunction.Derivative(z);
                delta = delta.MapIndexed((i, d) => d * delz[i]);

                weightGradient[i - 1] = Matrix<double>.Build.Dense(
                    layer.Weight.RowCount,
                    layer.Weight.ColumnCount,
                    (i, j) => delta[i] * a[j]);
                biasGradient[i - 1] = delta;

                delta = delta.MapIndexed((i, d) => layer.Weight.Row(i) * delta);
            }

            for (int i = 0; i < Layers.Count; i++)
            {
                Layer layer = Layers[i];

                Matrix<double> weightDelta =
                    weightGradient[i].Map(g => -learningRate * g);
                Vector<double> biasDelta =
                    biasGradient[i].Map(g => -learningRate * g);

                layer.Weight += weightDelta;
                layer.Bias += biasDelta;
            }
        }

    }
}
