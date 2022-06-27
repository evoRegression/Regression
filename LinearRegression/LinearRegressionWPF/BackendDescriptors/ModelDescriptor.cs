using System;

using LinearRegressionBackend;

namespace LinearRegressionWPF.BackendDescriptors
{
    class ModelDescriptor
    {
        public string Name { get; set; }
        public LossFunctionDescriptor[] SupportedLossFunctions { get; set; }
        public OptimizerDescriptor[] SupportedOptimizers { get; set; }
        public Func<ModelBuilderParams, IMLModel> BuildModel { get; set; }
    }
}
