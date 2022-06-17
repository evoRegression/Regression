using System;

using LinearRegressionBackend.MLNeuralNetwork;

namespace LinearRegressionWPF.BackendDescriptors
{
    class ActivationFunctionDescriptor
    {
        public string Name { get; set; }
        public Func<IActivationFunction> BuildActivationFunction { get; set; }
    }
}
