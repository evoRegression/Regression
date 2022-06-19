using System;

using LinearRegressionBackend;

namespace LinearRegressionWPF.BackendDescriptors
{
    class EstimatorDescriptor
    {
        public string Name { get; set; }
        public bool IsIterative { get; set; }
        public OptimizerBuilderParams.Parameter[] SupportedParameters { get; set; }
        public Func<OptimizerBuilderParams, IEstimator> BuildOptimizer { get; set; }
    }
}
