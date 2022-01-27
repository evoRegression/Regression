using System;

using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendFeatures
{
    class LossFunctionDescriptor
    {
        public string Name { get; set; }
        public Func<ILossFunction> BuildLossFunction { get; set; }
    }
}
