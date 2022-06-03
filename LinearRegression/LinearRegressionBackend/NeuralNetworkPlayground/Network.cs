using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
            Propagation prop = new();

            prop.WeightedSums.Add(null);
            prop.Activations.Add(input);

            foreach (Layer layer in Layers)
            {
                layer.Propagate(prop);
            }

            return prop;
        }

        public Gradient Backpropagate(
            Propagation prop,
            Vector<double> expected)
        {
            Debug.Assert(prop.Output().Count == expected.Count);

            Gradient grad = new(Layers.Count);

            Layer layer = Layers[Layers.Count - 1];
            Vector<double> z = prop.WeightedSums[Layers.Count];
            Vector<double> a = prop.Activations[Layers.Count - 1];

            Vector<double> dz = layer.ActivationFunction.Derivative(z);
            Vector<double> delta =
                (prop.Output() - expected).MapIndexed((i, d) => d * dz[i]);

            grad.WeightGradient[Layers.Count - 1] = Matrix<double>.Build.Dense(
                    layer.Weight.RowCount,
                    layer.Weight.ColumnCount,
                    (i, j) => delta[i] * a[j]);
            grad.BiasGradient[Layers.Count - 1] = delta;

            for (int i = Layers.Count - 1; i > 0; i--)
            {
                delta = delta.MapIndexed(
                    (i, d) => layer.Weight.Row(i) * delta);

                layer = Layers[i - 1];
                z = prop.WeightedSums[i];
                a = prop.Activations[i - 1];

                dz = layer.ActivationFunction.Derivative(z);
                delta = delta.MapIndexed((i, d) => d * dz[i]);

                grad.WeightGradient[i - 1] = Matrix<double>.Build.Dense(
                    layer.Weight.RowCount,
                    layer.Weight.ColumnCount,
                    (i, j) => delta[i] * a[j]);
                grad.BiasGradient[i - 1] = delta;
            }

            return grad;
        }

        public Gradient Backpropagate(
            Matrix<double> input,
            Matrix<double> expected)
        {
            Debug.Assert(input.RowCount == expected.RowCount);

            int exampleCount = input.RowCount;

            Propagation prop = Propagate(input.Row(0));
            Gradient grad = Backpropagate(prop, expected.Row(0));
            grad.WeightGradient =
                grad.WeightGradient.Select(m => m / exampleCount).ToArray();
            grad.BiasGradient =
                grad.BiasGradient.Select(m => m / exampleCount).ToArray();

            for (int i = 1; i < exampleCount; i++)
            {
                prop = Propagate(input.Row(i));
                Gradient currentGrad = Backpropagate(prop, expected.Row(i));
                grad.WeightGradient = grad.WeightGradient.Select(
                    (m, i) => m + currentGrad.WeightGradient[i] / exampleCount)
                    .ToArray();
                grad.BiasGradient = grad.BiasGradient.Select(
                    (m, i) => m + currentGrad.BiasGradient[i] / exampleCount)
                    .ToArray();
            }

            return grad;
        }

        public void Update(Gradient gradient, double learningRate)
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                Layer layer = Layers[i];

                Matrix<double> weightDelta =
                    gradient.WeightGradient[i].Map(g => -learningRate * g);
                Vector<double> biasDelta =
                    gradient.BiasGradient[i].Map(g => -learningRate * g);

                layer.Weight += weightDelta;
                layer.Bias += biasDelta;
            }
        }

        public void Train(
            Matrix<double> input,
            Matrix<double> expected,
            int epochs,
            double learningRate)
        {
            for (int i = 0; i < epochs; i++)
            {
                Gradient gradient = Backpropagate(input, expected);
                Update(gradient, learningRate);
            }
        }

    }
}
