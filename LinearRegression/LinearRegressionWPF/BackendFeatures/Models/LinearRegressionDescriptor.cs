using LinearRegressionBackend.MLModel;

using LinearRegressionWPF.BackendFeatures.LossFunctions;
using LinearRegressionWPF.BackendFeatures.Optimizers;

namespace LinearRegressionWPF.BackendFeatures.Models
{
    class LinearRegressionDescriptor : IModelDescriptor
    {
        public string Name => "Linear Regression";

        public ILossFunctionDescriptor[] SupportedLossFunctions => new ILossFunctionDescriptor[] {
            new LeastAbsoluteErrorDescriptor(),
            new LeastSquareErrorDescriptor()
        };

        public IOptimizerDescriptor[] SupportedOptimizers => new IOptimizerDescriptor[] {
            new GradientDescentDescriptor(),
            new SimpleOrdinaryLeastSquareDescriptor(),
            new QuadraticOrdinaryLeastSquareDescriptor()
        };

        public IMLModel constructModel(ILossFunctionDescriptor lossFunctionDesc, IOptimizerDescriptor optimizerDesc,
            double learningRate, double slope, double yIntercept)
        {
            return new LinearRegressionModel(slope, yIntercept,
                optimizerDesc.constructOptimizer(learningRate),
                lossFunctionDesc.constructLossFunction());
        }
    }
}
