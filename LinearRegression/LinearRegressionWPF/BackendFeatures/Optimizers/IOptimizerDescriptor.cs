using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendFeatures.Optimizers
{
    interface IOptimizerDescriptor
    {

        public enum Parameter
        {
            LearningRate
        }

        public string Name { get; }
        public bool IsIterative { get; }
        public Parameter[] SupportedParameters { get; }

        public IOptimizer constructOptimizer(double learningRate);

    }
}
