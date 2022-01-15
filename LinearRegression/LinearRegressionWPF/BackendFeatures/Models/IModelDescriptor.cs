using LinearRegressionBackend.MLModel;

using LinearRegressionWPF.BackendFeatures.LossFunctions;
using LinearRegressionWPF.BackendFeatures.Optimizers;

namespace LinearRegressionWPF.BackendFeatures.Models
{
    interface IModelDescriptor
    {

        public string Name { get; }
        public ILossFunctionDescriptor[] SupportedLossFunctions { get; }
        public IOptimizerDescriptor[] SupportedOptimizers { get; }

        public IMLModel constructModel(ILossFunctionDescriptor lossFunctionDesc, IOptimizerDescriptor optimizerDesc,
            double learningRate, double slope, double yIntercept);

    }
}
