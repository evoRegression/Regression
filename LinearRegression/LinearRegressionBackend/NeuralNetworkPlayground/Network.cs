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

            Layer layer = Layers[Layers.Count - 1];
            Vector<double> z = prop.WeightedSums[Layers.Count];
            Vector<double> a = prop.Activations[Layers.Count - 1];

            Vector<double> dz = layer.ActivationFunction.Derivative(z);
            Vector<double> delta =
                (prop.Output() - expected).MapIndexed((i, d) => d * dz[i]);

            weightGradient[Layers.Count - 1] = Matrix<double>.Build.Dense(
                    layer.Weight.RowCount,
                    layer.Weight.ColumnCount,
                    (i, j) => delta[i] * a[j]);
            biasGradient[Layers.Count - 1] = delta;

            for (int i = Layers.Count - 1; i > 0; i--)
            {
                delta = delta.MapIndexed((i, d) => layer.Weight.Row(i) * delta);

                layer = Layers[i - 1];
                z = prop.WeightedSums[i];
                a = prop.Activations[i - 1];

                dz = layer.ActivationFunction.Derivative(z);
                delta = delta.MapIndexed((i, d) => d * dz[i]);

                weightGradient[i - 1] = Matrix<double>.Build.Dense(
                    layer.Weight.RowCount,
                    layer.Weight.ColumnCount,
                    (i, j) => delta[i] * a[j]);
                biasGradient[i - 1] = delta;
            }

            for (int i = 0; i < Layers.Count; i++)
            {
                layer = Layers[i];

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
