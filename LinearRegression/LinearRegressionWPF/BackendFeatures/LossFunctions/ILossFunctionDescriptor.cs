using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendFeatures.LossFunctions
{
    interface ILossFunctionDescriptor
    {

        public string Name { get; }

        public ILossFunction constructLossFunction();

    }
}
