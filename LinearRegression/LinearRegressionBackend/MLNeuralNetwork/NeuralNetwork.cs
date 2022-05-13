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
                activations = layer.Activations(activations);
            }

            return activations;
        }

        public void PropagateBackward(Matrix<double> actualValue, Matrix<double> expectedValue, double learningRate)
        {
            Debug.Assert(actualValue.RowCount == expectedValue.RowCount);
            Debug.Assert(actualValue.ColumnCount == expectedValue.ColumnCount);
            throw new NotImplementedException();
        }

        private Vector<double> CostDerivatives(Matrix<double> actualValue, Matrix<double> expectedValue)
        {
            Layer lastLayer = _Layers[_Layers.Count - 1];
            Vector<double> derivatives = Vector<double>.Build.Dense(lastLayer.NeuronCount);

            for (int i = 0; i < actualValue.RowCount; i++)
            {
                // pass
            }

            throw new NotImplementedException();
        }

        private Vector<double> ActivationDerivatives(Vector<double> actualValue, Vector<double> expectedValue)
        {
            throw new NotImplementedException();
        }

        private Vector<double> WeightSensitivities(Vector<double> actualValue, Vector<double> expectedValue)
        {
            throw new NotImplementedException();
        }

        private Vector<double> BiasSensitivities(Vector<double> actualValue, Vector<double> expectedValue)
        {
            throw new NotImplementedException();
        }

        private Vector<double> ActivationSensitivities(Vector<double> actualValue, Vector<double> expectedValue)
        {
            throw new NotImplementedException();
        }
    }
}
