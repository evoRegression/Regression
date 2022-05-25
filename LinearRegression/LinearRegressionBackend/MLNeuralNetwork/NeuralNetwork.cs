using System;
using System.Collections.Generic;
using System.Diagnostics;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.MLNeuralNetwork
{
    public class NeuralNetwork
    {
        private List<Layer> _Layers;

        public NeuralNetwork(List<Layer> layers)
        {
            _Layers = layers;
        }

        public Vector<double> PropagateForward(Vector<double> inputData)
        {
            Vector<double> activations = inputData;

            foreach (Layer layer in _Layers)
            {
                layer.PropagateForward(activations);
                activations = layer.Activations;
            }

            return activations;
        }

        public void PropagateBackward(
            List<Matrix<double>> weightedSums,
            List<Matrix<double>> activations,
            Matrix<double> actualValues,
            Matrix<double> expectedValues,
            double learningRate
        )
        {
            Layer lastLayer = _Layers[_Layers.Count - 1];

            Debug.Assert(actualValues.RowCount == lastLayer.NeuronCount);
            Debug.Assert(actualValues.RowCount == expectedValues.RowCount);
            Debug.Assert(actualValues.ColumnCount == expectedValues.ColumnCount);

            Vector<double> cumulativeDerivatives = CostDerivatives(actualValues, expectedValues);

            throw new NotImplementedException();
        }

        private Vector<double> CostDerivatives(Matrix<double> actualValues, Matrix<double> expectedValues)
        {
            Vector<double> derivatives = Vector<double>.Build.Dense(actualValues.RowCount, 1);
            derivatives = (actualValues - expectedValues) * derivatives;
            return derivatives;
        }

        private Vector<double> ActivationDerivatives(Layer layer, List<Matrix<double>> layerWeightedSums)
        {
            throw new NotImplementedException();
        }

        private Vector<double> WeightDerivatives(Vector<double> actualValue, Vector<double> expectedValue)
        {
            throw new NotImplementedException();
        }

        private Vector<double> BiasDerivatives(Vector<double> actualValue, Vector<double> expectedValue)
        {
            throw new NotImplementedException();
        }

        private Vector<double> InputDerivatives(Vector<double> actualValue, Vector<double> expectedValue)
        {
            throw new NotImplementedException();
        }
    }
}
