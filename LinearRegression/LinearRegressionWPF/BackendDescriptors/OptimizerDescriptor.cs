using System;

using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendFeatures
{
    class OptimizerDescriptor
    {
        public string Name { get; set; }
        public bool IsIterative { get; set; }
        public OptimizerBuilderParams.Parameter[] SupportedParameters { get; set; }
        public Func<OptimizerBuilderParams, IOptimizer> BuildOptimizer { get; set; }
    }
}
