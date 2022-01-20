using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendFeatures.Optimizers
{
    class SimpleOrdinaryLeastSquareDescriptor : IOptimizerDescriptor
    {
        public string Name => "Simple Ordinary Least Square";

        public IOptimizerDescriptor.Parameter[] SupportedParameters => new IOptimizerDescriptor.Parameter[] { };

        public bool IsIterative => false;

        public IOptimizer constructOptimizer(double learningRate)
        {
            return new SimpleOrdinaryLeastSquare();
        }
    }
}
