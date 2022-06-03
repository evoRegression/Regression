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

        public (Matrix<double>[] weightGrad, Vector<double>[] biasGrad)
        Backpropagate(Propagation prop, Vector<double> expected)
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

            return (weightGradient, biasGradient);
        }

        public (Matrix<double>[] weightGrad, Vector<double>[] biasGrad)
        Backpropagate(Matrix<double> input, Matrix<double> expected)
        {
            Debug.Assert(input.RowCount == expected.RowCount);

            int exampleCount = input.RowCount;

            Matrix<double>[] weightGradient = null;
            Vector<double>[] biasGradient = null;

            Propagation prop = Propagate(input.Row(0));
            (weightGradient, biasGradient) =
                Backpropagate(prop, expected.Row(0));
            weightGradient =
                weightGradient.Select(m => m / exampleCount).ToArray();
            biasGradient =
                biasGradient.Select(m => m / exampleCount).ToArray();

            for (int i = 1; i < exampleCount; i++)
            {
                Matrix<double>[] currentWeightGradient = null;
                Vector<double>[] currentBiasGradient = null;

                prop = Propagate(input.Row(i));
                (currentWeightGradient, currentBiasGradient) =
                    Backpropagate(prop, expected.Row(i));
                weightGradient = weightGradient.Select(
                    (m, i) => m + currentWeightGradient[i] / exampleCount)
                    .ToArray();
                biasGradient = biasGradient.Select(
                    (m, i) => m + currentBiasGradient[i] / exampleCount)
                    .ToArray();
            }

            return (weightGradient, biasGradient);
        }

        public void Update(
            Matrix<double>[] weightGradient,
            Vector<double>[] biasGradient,
            double learningRate)
        {
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

        public void Train(
            Matrix<double> input,
            Matrix<double> expected,
            int epochs,
            double learningRate)
        {
            for (int i = 0; i < epochs; i++)
            {
                Matrix<double>[] weightGradient = null;
                Vector<double>[] biasGradient = null;
                (weightGradient, biasGradient) =
                    Backpropagate(input, expected);
                Update(weightGradient, biasGradient, learningRate);
            }
        }

    }
}
