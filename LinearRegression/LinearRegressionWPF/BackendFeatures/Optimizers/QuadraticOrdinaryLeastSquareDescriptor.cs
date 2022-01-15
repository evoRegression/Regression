using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendFeatures.Optimizers
{
    class QuadraticOrdinaryLeastSquareDescriptor : IOptimizerDescriptor
    {
        public string Name => "Quadratic Ordinary Least Square";

        public IOptimizerDescriptor.Parameter[] SupportedParameters => new IOptimizerDescriptor.Parameter[] { };

        public bool IsIterative => false;

        public IOptimizer constructOptimizer(double learningRate)
        {
            return new QuadraticOrdinaryLeastSquare();
        }
    }
}
