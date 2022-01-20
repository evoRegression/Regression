using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendFeatures.Optimizers
{
    class GradientDescentDescriptor : IOptimizerDescriptor
    {
        public string Name => "Gradient Descent";

        public IOptimizerDescriptor.Parameter[] SupportedParameters => new IOptimizerDescriptor.Parameter[] {
            IOptimizerDescriptor.Parameter.LearningRate
        };

        public bool IsIterative => true;

        public IOptimizer constructOptimizer(double learningRate)
        {
            return new GradientDescent(learningRate);
        }
    }
}
