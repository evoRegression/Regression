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

        public void Propagate(Vector<double> input)
        {
            foreach (Layer layer in Layers)
            {
                layer.Propagate(input);
                input = layer.Activation;
            }
        }

        public void Backpropagate(
            Vector<double> input,
            Vector<double> expected,
            double learningRate)
        {
            Propagate(input);
            Vector<double> output = Layers[Layers.Count - 1].Activation;

            Debug.Assert(output.Count == expected.Count);

            Matrix<double>[] weightGradient =
                new Matrix<double>[Layers.Count];
            Vector<double>[] biasGradient =
                new Vector<double>[Layers.Count];

            Vector<double> delta = output - expected;

            Layer layer = null;
            Vector<double> z = null;
            Vector<double> a = null;
            Vector<double> delz = null;

            for (int i = Layers.Count - 1; i > 0; i--)
            {
                layer = Layers[i];
                Layer previousLayer = Layers[i - 1];
                z = layer.WeightedSum;
                a = previousLayer.Activation;

                delz = layer.ActivationFunction.Derivative(z);
                delta = delta.MapIndexed((i, d) => d * delz[i]);

                weightGradient[i] = Matrix<double>.Build.Dense(
                    layer.Weight.RowCount,
                    layer.Weight.ColumnCount,
                    (i, j) => delta[i] * a[j]);
                biasGradient[i] = delta;

                delta = delta.MapIndexed((i, d) => layer.Weight.Row(i) * delta);
            }

            layer = Layers[0];
            z = layer.WeightedSum;
            a = input;

            delz = layer.ActivationFunction.Derivative(z);
            delta = delta.MapIndexed((i, d) => d * delz[i]);

            weightGradient[0] = Matrix<double>.Build.Dense(
                layer.Weight.RowCount,
                layer.Weight.ColumnCount,
                (i, j) => delta[i] * a[j]);
            biasGradient[0] = delta;

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
