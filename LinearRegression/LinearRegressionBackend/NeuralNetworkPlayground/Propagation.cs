using System.Collections.Generic;

using MathNet.Numerics.LinearAlgebra;

namespace LinearRegressionBackend.NeuralNetworkPlayground
{
    public class Propagation
    {

        public List<Vector<double>> WeightedSums;
        public List<Vector<double>> Activations;

        public Propagation()
        {
            WeightedSums = new();
            Activations = new();
        }

        public Vector<double> Output()
        {
            return Activations[Activations.Count - 1];
        }

    }
}
