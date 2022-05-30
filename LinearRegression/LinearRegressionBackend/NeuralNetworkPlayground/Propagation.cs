using System.Collections.Generic;

namespace LinearRegressionBackend.NeuralNetworkPlayground
{
    public class Propagation
    {

        public List<double> WeightedSums;
        public List<double> Activations;

        public Propagation()
        {
            WeightedSums = new();
            Activations = new();
        }

        public double Output()
        {
            return Activations[Activations.Count - 1];
        }

    }
}
