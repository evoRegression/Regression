using System;

using LinearRegressionBackend;

namespace LinearRegressionWPF.BackendFeatures
{
    internal class LossFunctionDescriptor
    {
        public string Name { get; set; }

        public Func<ILossFunction> BuildLossFunction { get; set; }
    }
}
