using System;

using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendDescriptors
{
    class LossFunctionDescriptor
    {
        public string Name { get; set; }
        public Func<ILossFunction> BuildLossFunction { get; set; }
    }
}
