using LinearRegressionBackend.MLModel;

using LinearRegressionWPF.BackendFeatures.LossFunctions;
using LinearRegressionWPF.BackendFeatures.Optimizers;

namespace LinearRegressionWPF.BackendFeatures.Models
{
    class DummyDescriptor : IModelDescriptor
    {
        public string Name => "Dummy Model";

        public ILossFunctionDescriptor[] SupportedLossFunctions => new ILossFunctionDescriptor[] {
            new LeastAbsoluteErrorDescriptor()
        };

        public IOptimizerDescriptor[] SupportedOptimizers => new IOptimizerDescriptor[] {
            new QuadraticOrdinaryLeastSquareDescriptor()
        };

        public IMLModel constructModel(ILossFunctionDescriptor lossFunctionDesc, IOptimizerDescriptor optimizerDesc,
            double learningRate, double slope, double yIntercept)
        {
            return null;
        }
    }
}
