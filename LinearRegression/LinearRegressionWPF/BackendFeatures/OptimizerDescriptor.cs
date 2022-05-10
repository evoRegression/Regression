using System;

using LinearRegressionBackend;

namespace LinearRegressionWPF.BackendFeatures
{
    internal class OptimizerDescriptor
    {
        public string Name { get; set; }

        public bool IsIterative { get; set; }

        public OptimizerBuilderParams.Parameter[] SupportedParameters { get; set; }

        public Func<OptimizerBuilderParams, IOptimizer> BuildOptimizer { get; set; }

        public Func<IEstimator> BuildEstimator { get; set; }
    }
}
