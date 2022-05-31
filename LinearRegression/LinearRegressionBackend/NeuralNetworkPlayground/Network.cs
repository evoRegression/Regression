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
            Vector<double> delta = prop.Output() - expected;

            for (int i = Layers.Count; i > 0; i--)
            {
                Layer layer = Layers[i - 1];
                Vector<double> z = prop.WeightedSums[i];
                Vector<double> a = prop.Activations[i - 1];

                Vector<double> delz = layer.ActivationFunction.Derivative(z);
                delta = delta.MapIndexed((i, d) => d * delz[i]);

                Matrix<double> weightDelta = Matrix<double>.Build.Dense(
                    layer.Weight.RowCount,
                    layer.Weight.ColumnCount,
                    (i, j) => -learningRate * delta[i] * a[j]);
                Vector<double> biasDelta = delta.Map(d => -learningRate * d);

                layer.Weight += weightDelta;
                layer.Bias += biasDelta;

                delta = delta.MapIndexed((i, d) => layer.Weight.Row(i) * delta);
            }
        }

    }
}
