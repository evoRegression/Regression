using LinearRegressionBackend.MLModel;

namespace LinearRegressionWPF.BackendFeatures.LossFunctions
{
    class LeastAbsoluteErrorDescriptor : ILossFunctionDescriptor
    {
        public string Name => "Least Absolute Error";

        public ILossFunction constructLossFunction()
        {
            return new LeastAbsoluteError();
        }
    }
}
